using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bladex.Garantias.Application.Facades;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.DomainModel.Services;
using Bladex.Garantias.Presentation.Website.Components.Attributes;
using Bladex.Garantias.Presentation.Website.Models;
using Bladex.Garantias.Presentation.Website.ViewModels;

namespace Bladex.Garantias.Presentation.Website.Controllers
{
    /// <summary>
    /// The garantia deposito controller class.
    /// </summary>
    [HandleErrorWithElmah]
    public class GarantiaDepositoController : GarantiaBaseController
    {
        /// <summary>
        /// Populates the model with lists.
        /// </summary>
        /// <param name="garantia">The garantia of type <see cref="Bladex.Garantias.Presentation.Website.Models.GarantiaBaseModel"/></param>
        protected override void PopulateModelWithLists(GarantiaBaseModel garantia)
        {
            base.PopulateModelWithLists(garantia);
            if (garantia is GarantiaDepositoModel)
            {
                if (((GarantiaDepositoModel)garantia).BancoLocalSuper == null)
                    ((GarantiaDepositoModel)garantia).BancoLocalSuper = new BancosViewModel();
                if(((GarantiaDepositoModel)garantia).BancoLocalSuper.List == null)
                    ((GarantiaDepositoModel)garantia).BancoLocalSuper.List = ServiceFacade.Instance.BancosService.GetAll().OrderBy(o => o.Nombre).Select(entity => new SelectListItem { Text = entity.Nombre, Value = entity.Key.ToString() }).ToList();
                
            }
        }


        /// <summary>
        /// Indexes the specified categoria super id.
        /// </summary>
        /// <param name="categoriaSuperId">The categoria super id of type <see cref="System.String"/></param>
        /// <returns></returns>
        public ActionResult Index(string categoriaSuperId)
        {
            List<GarantiaDeposito> list = ServiceFacade.Instance.GarantiaDepositoService.GetAll(null).ToList();
            ViewData["CategoriaSuper"] = ServiceFacade.Instance.CategoriaSuperService.GetById(categoriaSuperId);
            ViewData.Model = list;
            return View(list);
        }


        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Authorization()]
        public ActionResult Create()
        {
            GarantiaDepositoViewModel viewModel = new GarantiaDepositoViewModel();
            GarantiaDeposito garantiaDepositio = new GarantiaDeposito();
            garantiaDepositio.FiduciariaBladex = "NA";
            garantiaDepositio.FiduciariaSuper.Key = "NA";
            garantiaDepositio.IdentificacionFideicomiso = "NA";
            garantiaDepositio.ValorPolizaSeguro = 0;
            garantiaDepositio.NumeroPolizaSeguro = "NA";
            garantiaDepositio.Asegurador.Key = "NA";
            garantiaDepositio.Asegurador.Nombre = "NA";
            viewModel.Garantia = AutoMapper.Mapper.Map<GarantiaDeposito, GarantiaDepositoModel>(garantiaDepositio);
            PopulateModelWithLists(viewModel.Garantia);
            ViewData.Model = viewModel;
            return View(viewModel);
        }

        /// <summary>
        /// Creates the specified view model.
        /// </summary>
        /// <param name="viewModel">The view model of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.GarantiaDepositoViewModel"/></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(true)]
        public ActionResult Create(GarantiaDepositoViewModel viewModel)
        {
            Session[this.GetType().Name + "_Create"] = viewModel;
            if (ModelState.IsValid)
            {
                try
                {
                    GetDummyCustomers(viewModel.Garantia);

                    //#1242 Validacion de catalogo
                    var garantiaDeposito = AutoMapper.Mapper.Map<GarantiaDepositoModel, GarantiaDeposito>(viewModel.Garantia as GarantiaDepositoModel);
                    ServiceFacade.Instance.GarantiaDepositoService.ValidateCatalogs(garantiaDeposito);
                    viewModel.Garantia = AutoMapper.Mapper.Map<GarantiaDeposito, GarantiaDepositoModel>(garantiaDeposito);

                    var operationId = SaveGarantia(viewModel.Garantia);
                    return RedirectToAction("Index", "GarantiaContrato", new { operationId = operationId, categoriaSuperId = this.ViewData["categoriaSuperId"].ToString(), useRepository = false });
                    return RedirectToAction("Index", new { categoriaSuperId = this.ViewData["categoriaSuperId"].ToString() });
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    viewModel = Session[this.GetType().Name + "_Create"] as GarantiaDepositoViewModel;
                    this.PopulateModelWithLists(viewModel.Garantia);
                    viewModel.Garantia.BusinessError = ex.Message;
                    this.ViewData.Model = viewModel;
                    this.SetErrorModel(ex);
                    return View(this.ViewData.Model);
                }
            }
            else
            {
                viewModel = Session[this.GetType().Name + "_Create"] as GarantiaDepositoViewModel;
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
        public ActionResult Edit(int id, string categoriaSuperId)
        {
            try
            {
                GarantiaDepositoViewModel viewModel = new GarantiaDepositoViewModel();
                var garantiaDeposito = ServiceFacade.Instance.GarantiaDepositoService.GetById(id);

                var garantiaDepositoModel = AutoMapper.Mapper.Map<GarantiaDeposito, GarantiaDepositoModel>(garantiaDeposito);
                garantiaDeposito.NombreOrganismo = garantiaDeposito.BancoLocalSuper.Nombre;
                viewModel.Garantia = garantiaDepositoModel;
                PopulateModelWithLists(viewModel.Garantia);
                this.ViewData.Model = viewModel;
                
                return View(viewModel);
            }
            catch (Exception ex)
            {
                var res = new GarantiaDepositoViewModel() { Garantia = new GarantiaDepositoModel() };
                PopulateModelWithLists(res.Garantia);
                this.HttpContext.Application.Logger().Error("Error at Edit method.", ex);
                this.ViewData.Model = res;
                this.SetErrorModel(ex);
                return View(this.ViewData.Model);
            }
        }

        /// <summary>
        /// Edits the specified view model.
        /// </summary>
        /// <param name="viewModel">The view model of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.GarantiaDepositoViewModel"/></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(true)]
        public ActionResult Edit(GarantiaDepositoViewModel viewModel)
        {
            Session[this.GetType().Name + "_Edit"] = viewModel;
            if (ModelState.IsValid)
            {
                try
                {
                    GetDummyCustomers(viewModel.Garantia);

                    //#1242 Validacion de catalogo
                    var garantiaDeposito = AutoMapper.Mapper.Map<GarantiaDepositoModel, GarantiaDeposito>(viewModel.Garantia as GarantiaDepositoModel);
                    ServiceFacade.Instance.GarantiaDepositoService.ValidateCatalogs(garantiaDeposito);
                    viewModel.Garantia = AutoMapper.Mapper.Map<GarantiaDeposito, GarantiaDepositoModel>(garantiaDeposito);
                    
                    // Obtiene el InternalStatus original, si el internal status actual es Unknown.
                    if (viewModel.Garantia.InternalStatus.Key == InternalStatus.UNKNOWN_ID)
                    {
                        viewModel.Garantia.InternalStatus.Key = ServiceFacade.Instance.GarantiaService.GetOriginalInternalStatus(viewModel.Garantia.Key.ToString());
                    }

                    //var operationId = SaveGarantia(viewModel.Garantia);
                    int? operationId=null;
                    
                    return RedirectToAction("Index", "GarantiaContrato", new { garantiaId = viewModel.Garantia.Key, operationId = operationId.Value, categoriaSuperId = this.ViewData["categoriaSuperId"].ToString() });
                    return RedirectToAction("Index", new { categoriaSuperId = this.ViewData["categoriaSuperId"].ToString() });
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    viewModel = Session[this.GetType().Name + "_Edit"] as GarantiaDepositoViewModel;
                    viewModel.Garantia.BusinessError = ex.Message;
                    PopulateModelWithLists(viewModel.Garantia);

                    this.ViewData.Model = viewModel;
                    this.SetErrorModel(ex);

                    return View(this.ViewData.Model);
                }
            }
            else
            {
                viewModel = Session[this.GetType().Name + "_Edit"] as GarantiaDepositoViewModel;
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
                var viewModel = new GarantiaDepositoViewModel();
                var garantiaDeposito = ServiceFacade.Instance.GarantiaDepositoService.GetById(id);
                var garantiaDepositoModel = AutoMapper.Mapper.Map<GarantiaDeposito, GarantiaDepositoModel>(garantiaDeposito);
                viewModel.Garantia = garantiaDepositoModel;
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
        /// Deletes the specified garantia deposito view model.
        /// </summary>
        /// <param name="garantiaDepositoViewModel">The garantia deposito view model of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.GarantiaDepositoViewModel"/></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(true)]
        public ActionResult Delete(GarantiaDepositoViewModel garantiaDepositoViewModel)
        {
            try
            {
                var garantiaDeposito = AutoMapper.Mapper.Map<GarantiaDepositoModel, GarantiaDeposito>(garantiaDepositoViewModel.Garantia);
                ServiceFacade.Instance.GarantiaDepositoService.Delete(garantiaDeposito, UserId);
                return RedirectToAction("Index", new { categoriaSuperId = this.ViewData["categoriaSuperId"].ToString() });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                this.SetErrorModel(ex);
                PopulateModelWithLists(garantiaDepositoViewModel.Garantia);
                return View(garantiaDepositoViewModel);
            }
        }
    }
}
