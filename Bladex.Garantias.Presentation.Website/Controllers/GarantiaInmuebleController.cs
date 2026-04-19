using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bladex.Garantias.Application.Facades;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Presentation.Website.Components.Attributes;
using Bladex.Garantias.Presentation.Website.Models;
using Bladex.Garantias.Presentation.Website.ViewModels;

namespace Bladex.Garantias.Presentation.Website.Controllers
{
    /// <summary>
    /// The garantia inmueble controller class.
    /// </summary>
    [HandleErrorWithElmah]
    public class GarantiaInmuebleController : GarantiaBaseController
    {
        /// <summary>
        /// Populates the model with lists.
        /// </summary>
        /// <param name="garantia">The garantia of type <see cref="Bladex.Garantias.Presentation.Website.Models.GarantiaBaseModel"/></param>
        protected override void PopulateModelWithLists(GarantiaBaseModel garantia)
        {
            base.PopulateModelWithLists(garantia);
            if (garantia is GarantiaInmuebleModel)
            {
                if ((garantia as GarantiaInmuebleModel).AseguradorSuper == null)
                    (garantia as GarantiaInmuebleModel).AseguradorSuper = new AvaluadorasViewModel();
                if ((garantia as GarantiaInmuebleModel).AseguradorSuper.List == null)
                    (garantia as GarantiaInmuebleModel).AseguradorSuper.List = ServiceFacade.Instance.AvaluadoraService.GetAll().OrderBy(o => o.Nombre).Select(entity => new SelectListItem { Text = entity.Nombre, Value = entity.Key.ToString() }).ToList();
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
            List<GarantiaInmueble> list = ServiceFacade.Instance.GarantiaInmuebleService.GetAll(null).ToList();
            ViewData.Model = list;
            return View(list);
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            GarantiaInmuebleViewModel viewModel = new GarantiaInmuebleViewModel();
            GarantiaInmueble garantiaInmueble = new GarantiaInmueble();
            garantiaInmueble.FiduciariaBladex = "NA";
            garantiaInmueble.FiduciariaSuper.Key = "NA";
            garantiaInmueble.IdentificacionFideicomiso = "NA";
            garantiaInmueble.ValorPolizaSeguro = 0;
            garantiaInmueble.NumeroPolizaSeguro = "NA";
            garantiaInmueble.Asegurador.Key = "NA";
            garantiaInmueble.Asegurador.Nombre = "NA";
            viewModel.Garantia = AutoMapper.Mapper.Map<GarantiaInmueble, GarantiaInmuebleModel>(garantiaInmueble);
            PopulateModelWithLists(viewModel.Garantia);
            ViewData.Model = viewModel;
            return View(viewModel);
        }

        /// <summary>
        /// Creates the specified view model.
        /// </summary>
        /// <param name="viewModel">The view model of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.GarantiaInmuebleViewModel"/></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(true)]
        public ActionResult Create(GarantiaInmuebleViewModel viewModel)
        {
            Session[this.GetType().Name + "_Create"] = viewModel;
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                try
                {
                    GetDummyCustomers(viewModel.Garantia);
                    
                    //#1242 Validacion de catalogos
                    var garantiaInmueble = AutoMapper.Mapper.Map<GarantiaInmuebleModel, GarantiaInmueble>(viewModel.Garantia as GarantiaInmuebleModel);
                    ServiceFacade.Instance.GarantiaInmuebleService.ValidateCatalogs(garantiaInmueble);
                    viewModel.Garantia = AutoMapper.Mapper.Map<GarantiaInmueble, GarantiaInmuebleModel>(garantiaInmueble);

                    var operationId = SaveGarantia(viewModel.Garantia);
                    return RedirectToAction("Index", "GarantiaContrato", new { operationId = operationId, categoriaSuperId = this.ViewData["categoriaSuperId"].ToString(), useRepository=false });
                    return RedirectToAction("Index", new { categoriaSuperId = this.ViewData["categoriaSuperId"].ToString() });
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    viewModel = Session[this.GetType().Name + "_Create"] as GarantiaInmuebleViewModel;
                    this.PopulateModelWithLists(viewModel.Garantia);
                    viewModel.Garantia.BusinessError = ex.Message;
                    this.ViewData.Model = viewModel;
                    this.SetErrorModel(ex);
                    return View(this.ViewData.Model);
                }
            }
            else
            {
                viewModel = Session[this.GetType().Name + "_Create"] as GarantiaInmuebleViewModel;
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
                GarantiaInmuebleViewModel viewModel = new GarantiaInmuebleViewModel();
                var garantiaInmueble = ServiceFacade.Instance.GarantiaInmuebleService.GetById(operationId,garantiaId, useRepository, this.UserId);

                //Ticket #1550 : workaround  fecha vencimiento riesgo
                if (!garantiaInmueble.FechaVencimientoRiesgo.HasValue && garantiaInmueble.FechaVencimientoGarantia.HasValue)
                {
                    DateTime fixFechaVencimiento = garantiaInmueble.FechaVencimientoGarantia.Value;
                    garantiaInmueble.FechaVencimientoRiesgo = fixFechaVencimiento.AddDays(15);
                }

                if (garantiaInmueble.Asegurador.Key == null)
                    garantiaInmueble.Asegurador.Key = "NA";

                if (garantiaInmueble.FiduciariaSuper.Key == null)
                    garantiaInmueble.FiduciariaSuper.Key = "NA";

                var garantiaInmuebleModel = AutoMapper.Mapper.Map<GarantiaInmueble, GarantiaInmuebleModel>(garantiaInmueble);

                if (isReadOnly.HasValue)
                    garantiaInmuebleModel.CategoriaSuper.IsReadOnly = isReadOnly.Value;

                if(operationId.HasValue)
                    garantiaInmuebleModel.selectedOperationId = operationId.Value;

                if (garantiaId.HasValue)
                {
                    if (ServiceFacade.Instance.GarantiaService.GetInternalStatus(garantiaId) == ServiceFacade.Instance.InternalStatusService.GetBlockedStatus())
                    {
                        garantiaInmuebleModel.CategoriaSuper.IsReadOnly = true;
                    }
                }

                viewModel.Garantia = garantiaInmuebleModel;
                PopulateModelWithLists(viewModel.Garantia);
                this.ViewData.Model = viewModel;                 
                this.ViewBag.UseRepository = useRepository;
                if(operationId.HasValue)
                    this.ViewBag.OperationId = operationId.Value;
                else
                    this.ViewBag.OperationId = 0;

                return View(viewModel);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                var res = new GarantiaInmuebleViewModel() { Garantia = new GarantiaInmuebleModel() };
                PopulateModelWithLists(res.Garantia);
                res.Garantia.BusinessError = ex.Message;
                this.ViewData.Model = res;
                this.SetErrorModel(ex);
                return View(this.ViewData.Model);
            }
        }

        /// <summary>
        /// Edits the specified view model.
        /// </summary>
        /// <param name="viewModel">The view model of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.GarantiaInmuebleViewModel"/></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(true)]
        public ActionResult Edit(GarantiaInmuebleViewModel viewModel, bool? useRepository)
        {
            
            this.ViewBag.UseRepository = useRepository.Value;
            Session[this.GetType().Name + "_Edit"] = viewModel;
            if (ModelState.IsValid)
            {
                try
                {                    
                    GetDummyCustomers(viewModel.Garantia);

                    //#1242 Validacion de catalogos
                    var garantiaInmueble = AutoMapper.Mapper.Map<GarantiaInmuebleModel, GarantiaInmueble>(viewModel.Garantia as GarantiaInmuebleModel);
                    ServiceFacade.Instance.GarantiaInmuebleService.ValidateCatalogs(garantiaInmueble);
                    viewModel.Garantia = AutoMapper.Mapper.Map<GarantiaInmueble, GarantiaInmuebleModel>(garantiaInmueble);

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
                    viewModel = Session[this.GetType().Name + "_Edit"] as GarantiaInmuebleViewModel;
                    viewModel.Garantia.BusinessError = ex.Message;
                    PopulateModelWithLists(viewModel.Garantia);

                    this.ViewData.Model = viewModel;
                    this.SetErrorModel(ex);

                    return View(this.ViewData.Model);
                }
            }
            else
            {
                viewModel = Session[this.GetType().Name + "_Edit"] as GarantiaInmuebleViewModel;


                var errors = ModelState.Select(x => x.Value.Errors).Where(y=>y.Count>0).ToList();


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
                var viewModel = new GarantiaInmuebleViewModel();
                var garantiaInmueble = ServiceFacade.Instance.GarantiaInmuebleService.GetById(id);
                var garantiaInmuebleModel = AutoMapper.Mapper.Map<GarantiaInmueble, GarantiaInmuebleModel>(garantiaInmueble);
                viewModel.Garantia = garantiaInmuebleModel;
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
        /// Deletes the specified garantia inmueble view model.
        /// </summary>
        /// <param name="garantiaInmuebleViewModel">The garantia inmueble view model of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.GarantiaInmuebleViewModel"/></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(true)]
        public ActionResult Delete(GarantiaInmuebleViewModel garantiaInmuebleViewModel)
        {
            try
            {
                var garantiaInmueble = AutoMapper.Mapper.Map<GarantiaInmuebleModel, GarantiaInmueble>(garantiaInmuebleViewModel.Garantia);
                ServiceFacade.Instance.GarantiaInmuebleService.Delete(garantiaInmueble, UserId);
                return RedirectToAction("Index", new { categoriaSuperId = this.ViewData["categoriaSuperId"].ToString() });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                this.SetErrorModel(ex);
                PopulateModelWithLists(garantiaInmuebleViewModel.Garantia);
                return View(garantiaInmuebleViewModel);
            }
        }
    }
}