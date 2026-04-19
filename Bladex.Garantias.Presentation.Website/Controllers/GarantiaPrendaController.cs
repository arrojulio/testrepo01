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
    /// The garantia prenda controller class.
    /// </summary>
    [HandleErrorWithElmah]
    public class GarantiaPrendaController : GarantiaBaseController
    {
        protected override void PopulateModelWithLists(GarantiaBaseModel garantia)
        {
            base.PopulateModelWithLists(garantia);
            if (garantia is GarantiaPrendaModel)
            {
                // mapear las propiedades que son exclusivas de este tipo de garantias.
                
                if (((GarantiaPrendaModel)garantia).Emisor == null) ((GarantiaPrendaModel)garantia).Emisor = new ActorViewModel();
                //if(((GarantiaPrendaModel)garantia).Emisor.List == null)
                    ((GarantiaPrendaModel)garantia).Emisor.List = ServiceFacade.Instance.ActorService.GetAll().Select(entity => new SelectListItem { Text = entity.Nombre, Value = entity.Key.ToString() }).ToList();
                
                if (((GarantiaPrendaModel)garantia).Emisor.Pais == null) ((GarantiaPrendaModel)garantia).Emisor.Pais = new PaisViewModel();
                ((GarantiaPrendaModel)garantia).Emisor.Pais.List = ServiceFacade.Instance.PaisService.GetAll().Select(entity => new SelectListItem { Text = entity.Nombre, Value = entity.Key.ToString() }).ToList();

                
                if (((GarantiaPrendaModel)garantia).TipoInstrumentoFinanciero == null) ((GarantiaPrendaModel)garantia).TipoInstrumentoFinanciero = new InstrumentoFinancieroViewModel();
                if(((GarantiaPrendaModel)garantia).TipoInstrumentoFinanciero.List == null)
                    ((GarantiaPrendaModel)garantia).TipoInstrumentoFinanciero.List = ServiceFacade.Instance.InstrumentoFinancieroService.GetAll().OrderBy(o => o.Nombre).Select(entity => new SelectListItem { Text = entity.Nombre, Value = entity.Key.ToString() }).ToList();
                
                if (((GarantiaPrendaModel)garantia).CalificacionesRiesgoEmision == null) ((GarantiaPrendaModel)garantia).CalificacionesRiesgoEmision = new CalificacionesRiesgoViewModel();
                if(((GarantiaPrendaModel)garantia).CalificacionesRiesgoEmision.List == null)
                    ((GarantiaPrendaModel)garantia).CalificacionesRiesgoEmision.List = ServiceFacade.Instance.CalificacionesRiesgoService.GetAll().OrderBy(o => o.ToString()).Select(entity => new SelectListItem { Text = entity.ToString(), Value = entity.Key.ToString() }).ToList();


                if (((GarantiaPrendaModel)garantia).CalificacionesRiesgoEmisor == null) ((GarantiaPrendaModel)garantia).CalificacionesRiesgoEmisor = new CalificacionesRiesgoViewModel();
                if(((GarantiaPrendaModel)garantia).CalificacionesRiesgoEmisor.List == null)
                    ((GarantiaPrendaModel)garantia).CalificacionesRiesgoEmisor.List = ServiceFacade.Instance.CalificacionesRiesgoService.GetAll().OrderBy(o => o.ToString()).Select(entity => new SelectListItem { Text = entity.ToString(), Value = entity.Key.ToString() }).ToList();

                if (((GarantiaPrendaModel)garantia).PaisEmision == null) ((GarantiaPrendaModel)garantia).PaisEmision = new PaisViewModel();
                if(((GarantiaPrendaModel)garantia).PaisEmision.List == null)
                    ((GarantiaPrendaModel)garantia).PaisEmision.List = ServiceFacade.Instance.PaisService.GetAll().Select(entity => new SelectListItem { Text = entity.Nombre, Value = entity.Key.ToString() }).ToList();
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
            List<GarantiaPrenda> list = ServiceFacade.Instance.GarantiaPrendaService.GetAll(null).ToList();
            ViewData.Model = list;
            return View(list);
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            GarantiaPrendaViewModel viewModel = new GarantiaPrendaViewModel();
            GarantiaPrenda garantiaPrenda = new GarantiaPrenda();
            garantiaPrenda.FiduciariaBladex = "NA";
            garantiaPrenda.FiduciariaSuper.Key = "NA";
            garantiaPrenda.IdentificacionFideicomiso = "NA";
            garantiaPrenda.TipoInstrumentoFinanciero.Key = "NA";
            garantiaPrenda.CalificacionEmisor.Key = "NA";
            garantiaPrenda.CalificacionEmision.Key = "NA";
            garantiaPrenda.PaisEmision.Key = "N/A";
            garantiaPrenda.Asegurador.Key = "NA";

            viewModel.Garantia = AutoMapper.Mapper.Map<GarantiaPrenda, GarantiaPrendaModel>(garantiaPrenda);
            PopulateModelWithLists(viewModel.Garantia);
            ViewData.Model = viewModel;
            return View(viewModel);
        }

        /// <summary>
        /// Creates the specified view model.
        /// </summary>
        /// <param name="viewModel">The view model of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.GarantiaPrendaViewModel"/></param>
        /// <returns></returns>
        [HttpPost]
        
        [ValidateInput(true)]
        public ActionResult Create(GarantiaPrendaViewModel viewModel)
        {
            Session[this.GetType().Name + "_Create"] = viewModel;
            if (ModelState.IsValid)
            {
                try
                {                                       
                    GetDummyCustomers(viewModel.Garantia);

                    //#1242 Validacion de catalogos
                    var garantiaPrenda = AutoMapper.Mapper.Map<GarantiaPrendaModel, GarantiaPrenda>(viewModel.Garantia as GarantiaPrendaModel);
                    ServiceFacade.Instance.GarantiaPrendaService.ValidateCatalogs(garantiaPrenda);                                        
                    viewModel.Garantia = AutoMapper.Mapper.Map<GarantiaPrenda, GarantiaPrendaModel>(garantiaPrenda);

                    var operationId = SaveGarantia(viewModel.Garantia);
                    return RedirectToAction("Index", "GarantiaContrato", new { operationId = operationId, categoriaSuperId = this.ViewData["categoriaSuperId"].ToString(), useRepository = false });
                    return RedirectToAction("Index", new { categoriaSuperId = this.ViewData["categoriaSuperId"].ToString() });
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    viewModel = Session[this.GetType().Name + "_Create"] as GarantiaPrendaViewModel;
                    this.PopulateModelWithLists(viewModel.Garantia);
                    viewModel.Garantia.BusinessError = ex.Message;
                    this.ViewData.Model = viewModel;
                    this.SetErrorModel(ex);
                    return View(this.ViewData.Model);
                }
            }
            else
            {
                viewModel = Session[this.GetType().Name + "_Create"] as GarantiaPrendaViewModel;
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
                GarantiaPrendaViewModel viewModel = new GarantiaPrendaViewModel();
                var garantiaPrenda = ServiceFacade.Instance.GarantiaPrendaService.GetById(operationId, garantiaId, useRepository, this.UserId);


                //Ticket #1550 : workaround  fecha vencimiento riesgo
                if (!garantiaPrenda.FechaVencimientoRiesgo.HasValue && garantiaPrenda.FechaVencimientoGarantia.HasValue)
                {
                    DateTime fixFechaVencimiento = garantiaPrenda.FechaVencimientoGarantia.Value;
                    garantiaPrenda.FechaVencimientoRiesgo = fixFechaVencimiento.AddDays(15);
                }

                if (garantiaPrenda.Asegurador.Key == null)
                    garantiaPrenda.Asegurador.Key = "NA";

                if (garantiaPrenda.FiduciariaSuper.Key == null)
                    garantiaPrenda.FiduciariaSuper.Key = "NA";

                if (garantiaPrenda.TipoInstrumentoFinanciero.Key == null)
                    garantiaPrenda.TipoInstrumentoFinanciero.Key = "NA";

                if (garantiaPrenda.CalificacionEmision.Key == null)
                    garantiaPrenda.CalificacionEmision.Key = "NA";

                if (garantiaPrenda.CalificacionEmisor.Key == null)
                    garantiaPrenda.CalificacionEmisor.Key = "NA";

                if (garantiaPrenda.PaisEmision.Key == null)
                    garantiaPrenda.PaisEmision.Key = "NA";

                var garantiaPrendaModel = AutoMapper.Mapper.Map<GarantiaPrenda, GarantiaPrendaModel>(garantiaPrenda);
                
                if (isReadOnly.HasValue)
                    garantiaPrendaModel.CategoriaSuper.IsReadOnly = isReadOnly.Value;

                if(operationId.HasValue)                    
                    garantiaPrendaModel.selectedOperationId= operationId.Value;

                if(garantiaId.HasValue)                   
                {
                    if (ServiceFacade.Instance.GarantiaService.GetInternalStatus(garantiaId) == ServiceFacade.Instance.InternalStatusService.GetBlockedStatus()) 
                    {
                        garantiaPrendaModel.CategoriaSuper.IsReadOnly = true;
                    }
                }

                viewModel.Garantia = garantiaPrendaModel;
                PopulateModelWithLists(viewModel.Garantia);
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
                
                var res = new GarantiaPrendaViewModel() { Garantia = new GarantiaPrendaModel() };
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
        /// <param name="viewModel">The view model of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.GarantiaPrendaViewModel"/></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(true)]
        public ActionResult Edit(GarantiaPrendaViewModel viewModel, bool? useRepository)
        {
            this.ViewBag.UseRepository = useRepository.Value;
            Session[this.GetType().Name + "_Edit"] = viewModel;
            if (ModelState.IsValid)
            {
                try
                {
                    GetDummyCustomers(viewModel.Garantia);

                    //#1242 Validacion de catalogos
                    var garantiaPrenda = AutoMapper.Mapper.Map<GarantiaPrendaModel, GarantiaPrenda>(viewModel.Garantia as GarantiaPrendaModel);
                    ServiceFacade.Instance.GarantiaPrendaService.ValidateCatalogs(garantiaPrenda);
                    viewModel.Garantia = AutoMapper.Mapper.Map<GarantiaPrenda, GarantiaPrendaModel>(garantiaPrenda);

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
                    viewModel = Session[this.GetType().Name + "_Edit"] as GarantiaPrendaViewModel;
                    viewModel.Garantia.BusinessError = ex.Message;
                    PopulateModelWithLists(viewModel.Garantia);

                    this.ViewData.Model = viewModel;
                    this.SetErrorModel(ex);

                    return View(this.ViewData.Model);
                }
            }
            else
            {
                viewModel = Session[this.GetType().Name + "_Edit"] as GarantiaPrendaViewModel;
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
                var viewModel = new GarantiaPrendaViewModel();
                var garantiaPrenda = ServiceFacade.Instance.GarantiaPrendaService.GetById(id);
                var garantiaPrendaModel = AutoMapper.Mapper.Map<GarantiaPrenda, GarantiaPrendaModel>(garantiaPrenda);
                viewModel.Garantia = garantiaPrendaModel;
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
        /// Deletes the specified garantia prenda view model.
        /// </summary>
        /// <param name="garantiaPrendaViewModel">The garantia prenda view model of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.GarantiaPrendaViewModel"/></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(true)]
        public ActionResult Delete(GarantiaPrendaViewModel garantiaPrendaViewModel)
        {
            try
            {
                var garantiaPrenda = AutoMapper.Mapper.Map<GarantiaPrendaModel, GarantiaPrenda>(garantiaPrendaViewModel.Garantia);
                ServiceFacade.Instance.GarantiaPrendaService.Delete(garantiaPrenda, UserId);
                return RedirectToAction("Index", new { categoriaSuperId = this.ViewData["categoriaSuperId"].ToString() });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                this.SetErrorModel(ex);
                PopulateModelWithLists(garantiaPrendaViewModel.Garantia);
                return View(garantiaPrendaViewModel);
            }
        }
    }
}