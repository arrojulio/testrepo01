using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bladex.Garantias.Application.Facades;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.DomainModel.DomainBase.Summary;
using Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker;
using Bladex.Garantias.Infrastructure.Logging;
using Bladex.Garantias.Presentation.Website.Components.Attributes;
using Bladex.Garantias.Presentation.Website.Components.Authentication;
using Bladex.Garantias.Presentation.Website.Models;
using Bladex.Garantias.Presentation.Website.ViewModels;

namespace Bladex.Garantias.Presentation.Website.Controllers
{
    /// <summary>
    /// The garantia mueble controller class.
    /// </summary>
    [HandleErrorWithElmah]
    public class GarantiaMuebleController : GarantiaBaseController
    {
        protected override void PopulateModelWithLists(GarantiaBaseModel garantia)
        {            
            base.PopulateModelWithLists(garantia);
         
            if (garantia is GarantiaMuebleModel)
            {
                if (((GarantiaMuebleModel)garantia).AseguradorSuper == null)
                    ((GarantiaMuebleModel)garantia).AseguradorSuper = new AseguradorasViewModel();
                if (((GarantiaMuebleModel)garantia).AseguradorSuper.List == null)
                    ((GarantiaMuebleModel)garantia).AseguradorSuper.List = ServiceFacade.Instance.AseguradorasService.GetAll().OrderBy(o => o.Nombre).Select(entity => new SelectListItem { Text = entity.Nombre, Value = entity.Key.ToString() }).ToList();

            }
                   
        }

        /// <summary>
        /// Indexes the specified categoria super id.
        /// </summary>
        /// <param name="categoriaSuperId">The categoria super id of type <see cref="System.String"/></param>
        /// <returns></returns>
        public ActionResult Index(string categoriaSuperId)
        {
            ViewData["CategoriaSuper"] = ServiceFacade.Instance.CategoriaSuperService.GetById(categoriaSuperId);

            List<GarantiaMuebleSummary> list = ServiceFacade.Instance.GarantiaMuebleService.GetAllMuebleSQL();
            ViewData.Model = list;
            return View(list);
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            GarantiaMuebleViewModel viewModel = new GarantiaMuebleViewModel();
            GarantiaMueble garantiaMueble = new GarantiaMueble();
            garantiaMueble.FiduciariaBladex = "NA";
            garantiaMueble.FiduciariaSuper.Key = "NA";
            garantiaMueble.IdentificacionFideicomiso = "NA";
            garantiaMueble.Asegurador.Key = "NA";
            garantiaMueble.NumeroPolizaSeguro = "NA";
            viewModel.Garantia = AutoMapper.Mapper.Map<GarantiaMueble, GarantiaMuebleModel>(garantiaMueble);
            PopulateModelWithLists(viewModel.Garantia);            
            ViewData.Model = viewModel;
            
            return View(viewModel);
        }

        /// <summary>
        /// Creates the specified view model.
        /// </summary>
        /// <param name="viewModel">The view model of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.GarantiaMuebleViewModel"/></param>
        /// <returns></returns>
        [HttpPost]
        
        [ValidateInput(true)]
        public ActionResult Create(GarantiaMuebleViewModel viewModel)
        {
            Session[this.GetType().Name + "_Create"] = viewModel;
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                try
                {
                    GetDummyCustomers(viewModel.Garantia);

                    //#1242 Validacion de catalogos
                    var garantiaMueble = AutoMapper.Mapper.Map<GarantiaMuebleModel, GarantiaMueble>(viewModel.Garantia as GarantiaMuebleModel);
                    ServiceFacade.Instance.GarantiaMuebleService.ValidateCatalogs(garantiaMueble);
                    viewModel.Garantia = AutoMapper.Mapper.Map<GarantiaMueble, GarantiaMuebleModel>(garantiaMueble);

                    var operationId = SaveGarantia(viewModel.Garantia);
                    return RedirectToAction("Index", "GarantiaContrato", new { operationId = operationId, categoriaSuperId = this.ViewData["categoriaSuperId"].ToString(), useRepository = false });
                    return RedirectToAction("Index", new { categoriaSuperId = this.ViewData["categoriaSuperId"].ToString() });
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    //viewModel = Session[this.GetType().Name + "_Create"] as GarantiaMuebleViewModel;
                    this.PopulateModelWithLists(viewModel.Garantia);
                    viewModel.Garantia.BusinessError = ex.Message;
                    this.ViewData.Model = viewModel;
                    this.SetErrorModel(ex);
                    return View(this.ViewData.Model);
                }
            }
            else
            {
                //viewModel = Session[this.GetType().Name + "_Create"] as GarantiaMuebleViewModel;
                this.PopulateModelWithLists(viewModel.Garantia);
                this.ViewData.Model = viewModel;
                return View(viewModel);
            }

        }

        /// <summary>
        /// Edits the specified id.
        /// </summary>
        /// <param name="id">The id of type <see cref="System.Int32"/></param>
        /// <param name="categoriaSuperId">The categoria super id of type <see cref="System.String"/></param>
        /// <returns></returns>
        public ActionResult Edit(int? operationId, int? garantiaId, string categoriaSuperId, bool useRepository,bool? isReadOnly)
        {
            //retriveCategoriaSuper(categoriaSuperId);
            try
            {
                Logger.Debug("Inicio Edit garantia Mueble");
                
                GarantiaMuebleViewModel viewModel = new GarantiaMuebleViewModel();                
                var garantiaMueble = ServiceFacade.Instance.GarantiaMuebleService.GetById(operationId,garantiaId, useRepository, this.UserId);
                
                //Ticket #1550 : workaround  fecha vencimiento riesgo
                if(!garantiaMueble.FechaVencimientoRiesgo.HasValue && garantiaMueble.FechaVencimientoGarantia.HasValue){
                 DateTime fixFechaVencimiento =garantiaMueble.FechaVencimientoGarantia.Value;
                 garantiaMueble.FechaVencimientoRiesgo = fixFechaVencimiento.AddDays(15);
                }

                if (garantiaMueble.Asegurador.Key == null)
                    garantiaMueble.Asegurador.Key = "NA";

                if (garantiaMueble.FiduciariaSuper.Key == null)
                    garantiaMueble.FiduciariaSuper.Key = "NA";

                var garantiaMuebleModel = AutoMapper.Mapper.Map<GarantiaMueble, GarantiaMuebleModel>(garantiaMueble);
                
                if(isReadOnly.HasValue)
                    garantiaMuebleModel.CategoriaSuper.IsReadOnly = isReadOnly.Value;
                
                if(operationId.HasValue)                
                    garantiaMuebleModel.selectedOperationId = operationId;

                if (garantiaId.HasValue)
                {
                    if (ServiceFacade.Instance.GarantiaService.GetInternalStatus(garantiaId) == ServiceFacade.Instance.InternalStatusService.GetBlockedStatus())
                    {
                        garantiaMuebleModel.CategoriaSuper.IsReadOnly = true;
                    }
                }

                viewModel.Garantia = garantiaMuebleModel;
                Logger.Debug("Inicio Populate List");
                PopulateModelWithLists(viewModel.Garantia);
                Logger.Debug("Fin Populate List");
                this.ViewData.Model = viewModel;
                this.ViewBag.UseRepository = useRepository;

                if (operationId.HasValue)
                    this.ViewBag.OperationId = operationId.Value;
                else
                    this.ViewBag.OperationId = 0;

                return View(viewModel);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                var res = new GarantiaMuebleViewModel() { Garantia = new GarantiaMuebleModel() };
                PopulateModelWithLists(res.Garantia);
                this.ViewData.Model = res;
                this.SetErrorModel(ex);
                return View(this.ViewData.Model);
            }
        }

        /// <summary>
        /// Edits the specified view model.
        /// </summary>
        /// <param name="viewModel">The view model of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.GarantiaMuebleViewModel"/></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(true)]
        public ActionResult Edit(GarantiaMuebleViewModel viewModel, bool? useRepository)
        {
            this.ViewBag.UseRepository = useRepository.Value;
            Session[this.GetType().Name + "_Edit"] = viewModel;

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                try
                {
                    
                    GetDummyCustomers(viewModel.Garantia);
                    
                    //#1242 Validacion de catalogos
                    var garantiaMueble = AutoMapper.Mapper.Map<GarantiaMuebleModel, GarantiaMueble>(viewModel.Garantia as GarantiaMuebleModel);
                    ServiceFacade.Instance.GarantiaMuebleService.ValidateCatalogs(garantiaMueble);
                    viewModel.Garantia = AutoMapper.Mapper.Map<GarantiaMueble, GarantiaMuebleModel>(garantiaMueble);

                    // Obtiene el InternalStatus original, si el internal status actual es Unknown.
                    if (viewModel.Garantia.InternalStatus.Key == InternalStatus.UNKNOWN_ID)
                    {
                        viewModel.Garantia.InternalStatus.Key = ServiceFacade.Instance.GarantiaService.GetOriginalInternalStatus(viewModel.Garantia.Key.ToString());
                    }

                    var operationId = SaveGarantia(viewModel.Garantia);
                    //Ticket 1174: luego de generar el operation se cambia el useRepository a false para que trabaje con el json generado
                    return RedirectToAction("Index", "GarantiaContrato", new { garantiaId = viewModel.Garantia.Key, operationId = operationId, categoriaSuperId = this.ViewData["categoriaSuperId"].ToString(), useRepository = false });
                    return RedirectToAction("Index", new { categoriaSuperId = this.ViewData["categoriaSuperId"].ToString() });
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    viewModel = Session[this.GetType().Name + "_Edit"] as GarantiaMuebleViewModel;
                    viewModel.Garantia.BusinessError = ex.Message;
                    PopulateModelWithLists(viewModel.Garantia);

                    this.ViewData.Model = viewModel;
                    this.SetErrorModel(ex);

                    return View(this.ViewData.Model);
                }
            }
            else
            {
                viewModel = Session[this.GetType().Name + "_Edit"] as GarantiaMuebleViewModel;
                PopulateModelWithLists(viewModel.Garantia);
                this.ViewData.Model = viewModel;
                return View(viewModel);
            }

        }

        /// <summary>
        /// Deletes the specified id.
        /// </summary>
        /// <param name="id">The id of type <see cref="System.Int32"/></param>
        /// <param name="categoriaSuperId">The categoria super id of type <see cref="System.String"/></param>
        /// <returns></returns>
        public ActionResult Delete(int id, string categoriaSuperId)
        {
            //retriveCategoriaSuper(categoriaSuperId);
            try
            {
                var viewModel = new GarantiaMuebleViewModel();
                var garantiaMueble = ServiceFacade.Instance.GarantiaMuebleService.GetById(id);
                var garantiaMuebleModel = AutoMapper.Mapper.Map<GarantiaMueble, GarantiaMuebleModel>(garantiaMueble);
                viewModel.Garantia = garantiaMuebleModel;
                PopulateModelWithLists(viewModel.Garantia);
                this.ViewData.Model = viewModel;
                this.ViewData["Key"] = id;
                return View(viewModel);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                this.SetErrorModel(ex);
                return View();
            }
        }

        /// <summary>
        /// Deletes the specified garantia mueble view model.
        /// </summary>
        /// <param name="garantiaMuebleViewModel">The garantia mueble view model of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.GarantiaMuebleViewModel"/></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(true)]
        public ActionResult Delete(GarantiaMuebleViewModel garantiaMuebleViewModel)
        {
            try
            {
                var garantiaMueble = AutoMapper.Mapper.Map<GarantiaMuebleModel, GarantiaMueble>(garantiaMuebleViewModel.Garantia);
                ServiceFacade.Instance.GarantiaMuebleService.Delete(garantiaMueble, UserId);
                return RedirectToAction("Index", new { categoriaSuperId = this.ViewData["categoriaSuperId"].ToString() });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                
                PopulateModelWithLists(garantiaMuebleViewModel.Garantia);
                this.ViewData.Model = garantiaMuebleViewModel;
                this.SetErrorModel(ex);
                return View(garantiaMuebleViewModel);
            }
        }
    }
}