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
    /// The garantia deposito otro banco controller class.
    /// </summary>
    [HandleErrorWithElmah]
    public class GarantiaDepositoOtroBancoController : GarantiaBaseController
    {
        protected override void PopulateModelWithLists(GarantiaBaseModel garantia)
        {
            base.PopulateModelWithLists(garantia);
            if (garantia is GarantiaDepositoOtroBancoModel)
            {
                if (((GarantiaDepositoOtroBancoModel)garantia).BancoSuper == null) 
                    ((GarantiaDepositoOtroBancoModel)garantia).BancoSuper = new BancosViewModel();
                if(((GarantiaDepositoOtroBancoModel)garantia).BancoSuper.List == null)
                    ((GarantiaDepositoOtroBancoModel)garantia).BancoSuper.List = ServiceFacade.Instance.BancosService.GetAll().OrderBy(o => o.Nombre).Select(entity => new SelectListItem { Text = entity.Nombre, Value = entity.Key.ToString() }).ToList();
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
            List<GarantiaDepositoOtroBanco> list = ServiceFacade.Instance.GarantiaDepositoOtroBancoService.GetAll(null).ToList();
            ViewData.Model = list;
            return View(list);
        }


        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            GarantiaDepositoOtroBancoViewModel viewModel = new GarantiaDepositoOtroBancoViewModel();
            GarantiaDepositoOtroBanco garantiaDepositoOtroBanco = new GarantiaDepositoOtroBanco();
            garantiaDepositoOtroBanco.FiduciariaBladex = "NA";
            garantiaDepositoOtroBanco.FiduciariaSuper.Key = "NA";
            garantiaDepositoOtroBanco.IdentificacionFideicomiso = "NA";
            garantiaDepositoOtroBanco.Asegurador.Key = "NA";
            garantiaDepositoOtroBanco.NumeroPolizaSeguro = "NA";
            viewModel.Garantia = AutoMapper.Mapper.Map<GarantiaDepositoOtroBanco, GarantiaDepositoOtroBancoModel>(garantiaDepositoOtroBanco);
            PopulateModelWithLists(viewModel.Garantia);
            ViewData.Model = viewModel;

            return View(viewModel);
        }

        /// <summary>
        /// Creates the specified view model.
        /// </summary>
        /// <param name="viewModel">The view model of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.GarantiaDepositoOtroBancoViewModel"/></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(true)]
        public ActionResult Create(GarantiaDepositoOtroBancoViewModel viewModel)
        {
            Session[this.GetType().Name + "_Create"] = viewModel;
            if (ModelState.IsValid)
            {
                try
                {
                    GetDummyCustomers(viewModel.Garantia);

                    //#1242 Validacion de catalogos
                    var garantiaDeposito = AutoMapper.Mapper.Map<GarantiaDepositoOtroBancoModel, GarantiaDepositoOtroBanco>(viewModel.Garantia as GarantiaDepositoOtroBancoModel);
                    ServiceFacade.Instance.GarantiaDepositoOtroBancoService.ValidateCatalogs(garantiaDeposito);
                    viewModel.Garantia = AutoMapper.Mapper.Map<GarantiaDepositoOtroBanco, GarantiaDepositoOtroBancoModel>(garantiaDeposito);

                    var operationId = SaveGarantia(viewModel.Garantia);
                    return RedirectToAction("Index", "GarantiaContrato", new { operationId = operationId, categoriaSuperId = this.ViewData["categoriaSuperId"].ToString(), useRepository = false });
                    return RedirectToAction("Index", new { categoriaSuperId = this.ViewData["categoriaSuperId"].ToString() });
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    viewModel = Session[this.GetType().Name + "_Create"] as GarantiaDepositoOtroBancoViewModel;
                    this.PopulateModelWithLists(viewModel.Garantia);
                    viewModel.Garantia.BusinessError = ex.Message;
                    this.ViewData.Model = viewModel;
                    this.SetErrorModel(ex);
                    return View(this.ViewData.Model);
                }
            }
            else
            {
                viewModel = Session[this.GetType().Name + "_Create"] as GarantiaDepositoOtroBancoViewModel;
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
            try
            {
                this.ViewBag.UseRepository = useRepository;
                GarantiaDepositoOtroBancoViewModel viewModel = new GarantiaDepositoOtroBancoViewModel();
           
                var garantiaDepositoOtroBanco = ServiceFacade.Instance.GarantiaDepositoOtroBancoService.GetById(operationId, garantiaId, useRepository, this.UserId);


                if (garantiaDepositoOtroBanco.Asegurador.Key == null)
                    garantiaDepositoOtroBanco.Asegurador.Key = "NA";

                if (garantiaDepositoOtroBanco.FiduciariaSuper.Key == null)
                    garantiaDepositoOtroBanco.FiduciariaSuper.Key = "NA";

                //if (isReadOnly.HasValue)
                //    garantiaDepositoOtroBanco.CategoriaSuper.IsReadOnly = isReadOnly.Value;

                //if (operationId.HasValue)
                //    garantiaDepositoOtroBanco.selectedOperationId = operationId;

                //if (garantiaId.HasValue)
                //{
                //    if (ServiceFacade.Instance.GarantiaService.GetInternalStatus(garantiaId) == ServiceFacade.Instance.InternalStatusService.GetBlockedStatus())
                //    {
                //        garantiaDepositoOtroBanco.CategoriaSuper.IsReadOnly = true;
                //    }
                //}

                var garantiaDepositoOtroBancoModel = AutoMapper.Mapper.Map<GarantiaDepositoOtroBanco, GarantiaDepositoOtroBancoModel>(garantiaDepositoOtroBanco);

                if (isReadOnly.HasValue)
                    garantiaDepositoOtroBancoModel.CategoriaSuper.IsReadOnly = isReadOnly.Value;

                if (operationId.HasValue)
                    garantiaDepositoOtroBancoModel.selectedOperationId = operationId;

                if (garantiaId.HasValue)
                {
                    if (ServiceFacade.Instance.GarantiaService.GetInternalStatus(garantiaId) == ServiceFacade.Instance.InternalStatusService.GetBlockedStatus())
                    {
                        garantiaDepositoOtroBancoModel.CategoriaSuper.IsReadOnly = true;
                    }
                }

                viewModel.Garantia = garantiaDepositoOtroBancoModel;
                PopulateModelWithLists(viewModel.Garantia);
                this.ViewData.Model = viewModel;

                if (operationId.HasValue)
                    this.ViewBag.OperationId = operationId.Value;
                else
                    this.ViewBag.OperationId = 0;

                return View(viewModel);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                
                var res = new GarantiaDepositoOtroBancoViewModel() { Garantia = new GarantiaDepositoOtroBancoModel() };
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
        /// <param name="viewModel">The view model of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.GarantiaDepositoOtroBancoViewModel"/></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(true)]
          public ActionResult Edit(GarantiaDepositoOtroBancoViewModel viewModel, bool? useRepository)
        {
            Session[this.GetType().Name + "_Edit"] = viewModel;
            if (ModelState.IsValid)
            {
                try
                {
                    this.ViewBag.UseRepository = useRepository.Value;
                    GetDummyCustomers(viewModel.Garantia);

                    //#1242 Validacion de catalogos
                    var garantiaDeposito = AutoMapper.Mapper.Map<GarantiaDepositoOtroBancoModel, GarantiaDepositoOtroBanco>(viewModel.Garantia as GarantiaDepositoOtroBancoModel);
                    ServiceFacade.Instance.GarantiaDepositoOtroBancoService.ValidateCatalogs(garantiaDeposito);
                    viewModel.Garantia = AutoMapper.Mapper.Map<GarantiaDepositoOtroBanco, GarantiaDepositoOtroBancoModel>(garantiaDeposito);

                    // Obtiene el InternalStatus previo a deshabilitar el tipo de categoria
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
                    viewModel = Session[this.GetType().Name + "_Edit"] as GarantiaDepositoOtroBancoViewModel;
                    viewModel.Garantia.BusinessError = ex.Message;
                    PopulateModelWithLists(viewModel.Garantia);

                    this.ViewData.Model = viewModel;
                    this.SetErrorModel(ex);

                    return View(this.ViewData.Model);
                }
            }
            else
            {
                viewModel = Session[this.GetType().Name + "_Edit"] as GarantiaDepositoOtroBancoViewModel;
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
            try
            {
                var viewModel = new GarantiaDepositoOtroBancoViewModel();
                var garantiaDepositoOtroBanco = ServiceFacade.Instance.GarantiaDepositoOtroBancoService.GetById(id);
                var garantiaDepositoOtroBancoModel = AutoMapper.Mapper.Map<GarantiaDepositoOtroBanco, GarantiaDepositoOtroBancoModel>(garantiaDepositoOtroBanco);
                viewModel.Garantia = garantiaDepositoOtroBancoModel;
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
        /// Deletes the specified garantia deposito otro banco view model.
        /// </summary>
        /// <param name="garantiaDepositoOtroBancoViewModel">The garantia deposito otro banco view model of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.GarantiaDepositoOtroBancoViewModel"/></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(GarantiaDepositoOtroBancoViewModel garantiaDepositoOtroBancoViewModel)
        {
            try
            {
                var garantiaDepositoOtroBanco = AutoMapper.Mapper.Map<GarantiaDepositoOtroBancoModel, GarantiaDepositoOtroBanco>(garantiaDepositoOtroBancoViewModel.Garantia);
                ServiceFacade.Instance.GarantiaDepositoOtroBancoService.Delete(garantiaDepositoOtroBanco, UserId);
                return RedirectToAction("Index", new { categoriaSuperId = this.ViewData["categoriaSuperId"].ToString() });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                this.SetErrorModel(ex);
                PopulateModelWithLists(garantiaDepositoOtroBancoViewModel.Garantia);
                return View(garantiaDepositoOtroBancoViewModel);
            }
        }
    }
}
