using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Bladex.Garantias.Application.Facades;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.DomainModel.Repositories.Components.MakerChecker;
using Bladex.Garantias.DomainModel.Services;
using Bladex.Garantias.Infrastructure.Logging;
using Bladex.Garantias.Presentation.Website.Components.Attributes;
using Bladex.Garantias.Presentation.Website.Models;
using Bladex.Garantias.Presentation.Website.ViewModels;
using Bladex.Garantias.DomainModel.DomainBase.Components.Security;

namespace Bladex.Garantias.Presentation.Website.Controllers
{
    /// <summary>
    /// The garantia base controller class.
    /// </summary>
    [HandleErrorWithElmah]
    public class GarantiaBaseController : BaseController
    {
        /// <summary>
        /// 01	Garantía Hipotecaria Mueble
        /// 02	Garantía Hipotecaria Inmueble
        /// 03	Depósitos Pignorados en el Banco
        /// 04	Depósitos Pignorados en Otros Bancos
        /// 05	Garantía Prendaria
        /// 06	Otras Garantías
        /// </summary>
        /// <param name="garantiaModel">The garantia model of type <see cref="Bladex.Garantias.Presentation.Website.Models.GarantiaBaseModel"/></param>
        /// <returns></returns>
        protected int CommitGarantia(GarantiaBaseModel garantiaModel)
        {
            if (garantiaModel == null || garantiaModel.CategoriaSuper == null || garantiaModel.CategoriaSuper.Key == null)
                throw new ApplicationException("Error intentando salvar la garantia");
            string categoriaSuperId = garantiaModel.CategoriaSuper.Key;
            int result = default(int);
            if (garantiaModel.Key.HasValue && garantiaModel.Key.Value != 0)
            {
                // Update
                result = garantiaModel.Key.Value;
                switch (categoriaSuperId)
                {
                    case "01":
                        var garantiaMueble = AutoMapper.Mapper.Map<GarantiaMuebleModel, GarantiaMueble>(garantiaModel as GarantiaMuebleModel);
                        ServiceFacade.Instance.GarantiaMuebleService.Commit(garantiaMueble, User.Identity.Name);
                        break;
                    case "02":
                        var garantiaInmueble = AutoMapper.Mapper.Map<GarantiaInmuebleModel, GarantiaInmueble>(garantiaModel as GarantiaInmuebleModel);
                        ServiceFacade.Instance.GarantiaInmuebleService.Commit(garantiaInmueble, User.Identity.Name);
                        break;

                    case "03":
                        var garantiaDeposito = AutoMapper.Mapper.Map<GarantiaDepositoModel, GarantiaDeposito>(garantiaModel as GarantiaDepositoModel);
                        ServiceFacade.Instance.GarantiaDepositoService.Commit(garantiaDeposito, User.Identity.Name);
                        break;
                    case "04":
                        var garantiaDepositoOtroBanco = AutoMapper.Mapper.Map<GarantiaDepositoOtroBancoModel, GarantiaDepositoOtroBanco>(garantiaModel as GarantiaDepositoOtroBancoModel);
                        ServiceFacade.Instance.GarantiaDepositoOtroBancoService.Commit(garantiaDepositoOtroBanco, User.Identity.Name);
                        break;
                    case "05":
                        var garantiaPrenda = AutoMapper.Mapper.Map<GarantiaPrendaModel, GarantiaPrenda>(garantiaModel as GarantiaPrendaModel);
                        ServiceFacade.Instance.GarantiaPrendaService.Commit(garantiaPrenda, User.Identity.Name);
                        break;
                    case "06":
                        var garantiaOtra = AutoMapper.Mapper.Map<GarantiaOtraModel, GarantiaOtra>(garantiaModel as GarantiaOtraModel);
                        ServiceFacade.Instance.GarantiaOtraService.Commit(garantiaOtra, User.Identity.Name);
                        break;
                }
            }
            else
            {
                // Insert
                // Aqui guardo el resultado de la operacion.
                GarantiaBase objGenerated = null;
                garantiaModel.Key = null;
                switch (categoriaSuperId)
                {
                    case "01":
                        var garantiaMueble = AutoMapper.Mapper.Map<GarantiaMuebleModel, GarantiaMueble>(garantiaModel as GarantiaMuebleModel);
                        objGenerated = ServiceFacade.Instance.GarantiaMuebleService.Commit(garantiaMueble, User.Identity.Name);

                        break;
                    case "02":
                        var garantiaInmueble = AutoMapper.Mapper.Map<GarantiaInmuebleModel, GarantiaInmueble>(garantiaModel as GarantiaInmuebleModel);
                        objGenerated = ServiceFacade.Instance.GarantiaInmuebleService.Commit(garantiaInmueble, User.Identity.Name);
                        break;

                    case "03":
                        var garantiaDeposito = AutoMapper.Mapper.Map<GarantiaDepositoModel, GarantiaDeposito>(garantiaModel as GarantiaDepositoModel);
                        objGenerated = ServiceFacade.Instance.GarantiaDepositoService.Commit(garantiaDeposito, User.Identity.Name);
                        break;
                    case "04":
                        var garantiaDepositoOtroBanco = AutoMapper.Mapper.Map<GarantiaDepositoOtroBancoModel, GarantiaDepositoOtroBanco>(garantiaModel as GarantiaDepositoOtroBancoModel);
                        objGenerated = ServiceFacade.Instance.GarantiaDepositoOtroBancoService.Commit(garantiaDepositoOtroBanco, User.Identity.Name);
                        break;
                    case "05":
                        var garantiaPrenda = AutoMapper.Mapper.Map<GarantiaPrendaModel, GarantiaPrenda>(garantiaModel as GarantiaPrendaModel);
                        objGenerated = ServiceFacade.Instance.GarantiaPrendaService.Commit(garantiaPrenda, User.Identity.Name);
                        break;
                    case "06":
                        var garantiaOtra = AutoMapper.Mapper.Map<GarantiaOtraModel, GarantiaOtra>(garantiaModel as GarantiaOtraModel);
                        objGenerated = ServiceFacade.Instance.GarantiaOtraService.Commit(garantiaOtra, User.Identity.Name);
                        break;

                }
                if (objGenerated != null)
                    result = (int)objGenerated.Key;
            }

            return result;
        }

        /// <summary>
        /// 01	Garantía Hipotecaria Mueble
        /// 02	Garantía Hipotecaria Inmueble
        /// 03	Depósitos Pignorados en el Banco
        /// 04	Depósitos Pignorados en Otros Bancos
        /// 05	Garantía Prendaria
        /// 06	Otras Garantías
        /// </summary>
        /// <param name="garantiaModel">The garantia model of type <see cref="Bladex.Garantias.Presentation.Website.Models.GarantiaBaseModel"/></param>
        /// <returns></returns>
        protected int SaveGarantia(GarantiaBaseModel garantiaModel)
        {            
            if (garantiaModel == null || garantiaModel.CategoriaSuper == null || garantiaModel.CategoriaSuper.Key == null)
                throw new ApplicationException("Error intentando salvar la garantia");
            // Si no hubo seleccion coloco valor por defecto.
            if (garantiaModel.PaisGarantia != null && garantiaModel.PaisGarantia.Key == "-1")
            {
                garantiaModel.PaisGarantia = new PaisViewModel();
            }
            string categoriaSuperId = garantiaModel.CategoriaSuper.Key;
            int result = default(int);
            if (garantiaModel.Key.HasValue && garantiaModel.Key.Value != 0)
            {
                // Update
                // Aqui guardo el resultado de la operacion.
                GarantiaBase objGenerated = null;
                switch (categoriaSuperId)
                {
                    case CategoriaSuper.MUEBLE_ID:
                        var garantiaMueble = AutoMapper.Mapper.Map<GarantiaMuebleModel, GarantiaMueble>(garantiaModel as GarantiaMuebleModel);
                        objGenerated = ServiceFacade.Instance.GarantiaMuebleService.Save(garantiaMueble, User.Identity.Name, garantiaModel.selectedOperationId);
                        break;
                    case CategoriaSuper.INMUEBLE_ID:
                        var garantiaInmueble = AutoMapper.Mapper.Map<GarantiaInmuebleModel, GarantiaInmueble>(garantiaModel as GarantiaInmuebleModel);
                        objGenerated = ServiceFacade.Instance.GarantiaInmuebleService.Save(garantiaInmueble, User.Identity.Name, garantiaModel.selectedOperationId);
                        break;

                    case CategoriaSuper.DEPOSITO_ID:
                        var garantiaDeposito = AutoMapper.Mapper.Map<GarantiaDepositoModel, GarantiaDeposito>(garantiaModel as GarantiaDepositoModel);
                        objGenerated = ServiceFacade.Instance.GarantiaDepositoService.Save(garantiaDeposito, User.Identity.Name, garantiaModel.selectedOperationId);
                        break;
                    case CategoriaSuper.DEPOSITO_OTROS_ID:
                        var garantiaDepositoOtroBanco = AutoMapper.Mapper.Map<GarantiaDepositoOtroBancoModel, GarantiaDepositoOtroBanco>(garantiaModel as GarantiaDepositoOtroBancoModel);
                        objGenerated = ServiceFacade.Instance.GarantiaDepositoOtroBancoService.Save(garantiaDepositoOtroBanco, User.Identity.Name, garantiaModel.selectedOperationId);
                        break;
                    case CategoriaSuper.PRENDARIA_ID:
                        var garantiaPrenda = AutoMapper.Mapper.Map<GarantiaPrendaModel, GarantiaPrenda>(garantiaModel as GarantiaPrendaModel);
                        objGenerated = ServiceFacade.Instance.GarantiaPrendaService.Save(garantiaPrenda, User.Identity.Name, garantiaModel.selectedOperationId);
                        break;
                    case CategoriaSuper.OTRAS_ID:
                        var garantiaOtra = AutoMapper.Mapper.Map<GarantiaOtraModel, GarantiaOtra>(garantiaModel as GarantiaOtraModel);
                        objGenerated = ServiceFacade.Instance.GarantiaOtraService.Save(garantiaOtra, User.Identity.Name, garantiaModel.selectedOperationId);
                        break;
                }
                // If we use M&C objGenerated.Key is OperationId, if not, is the Key of the guarantee.
                if (objGenerated != null)
                    result = (int)objGenerated.Key;
            }
            else
            {
                // Insert
                // Aqui guardo el resultado de la operacion.
                GarantiaBase objGenerated = null;
                switch (categoriaSuperId)
                {
                    case CategoriaSuper.MUEBLE_ID:
                        var garantiaMueble = AutoMapper.Mapper.Map<GarantiaMuebleModel, GarantiaMueble>(garantiaModel as GarantiaMuebleModel);
                        objGenerated = ServiceFacade.Instance.GarantiaMuebleService.Save(garantiaMueble, User.Identity.Name,garantiaModel.selectedOperationId);

                        break;
                    case CategoriaSuper.INMUEBLE_ID:
                        var garantiaInmueble = AutoMapper.Mapper.Map<GarantiaInmuebleModel, GarantiaInmueble>(garantiaModel as GarantiaInmuebleModel);
                        objGenerated = ServiceFacade.Instance.GarantiaInmuebleService.Save(garantiaInmueble, User.Identity.Name, garantiaModel.selectedOperationId);
                        break;

                    case CategoriaSuper.DEPOSITO_ID:
                        var garantiaDeposito = AutoMapper.Mapper.Map<GarantiaDepositoModel, GarantiaDeposito>(garantiaModel as GarantiaDepositoModel);
                        objGenerated = ServiceFacade.Instance.GarantiaDepositoService.Save(garantiaDeposito, User.Identity.Name, garantiaModel.selectedOperationId);
                        break;
                    case CategoriaSuper.DEPOSITO_OTROS_ID:
                        var garantiaDepositoOtroBanco = AutoMapper.Mapper.Map<GarantiaDepositoOtroBancoModel, GarantiaDepositoOtroBanco>(garantiaModel as GarantiaDepositoOtroBancoModel);
                        objGenerated = ServiceFacade.Instance.GarantiaDepositoOtroBancoService.Save(garantiaDepositoOtroBanco, User.Identity.Name, garantiaModel.selectedOperationId);
                        break;
                    case CategoriaSuper.PRENDARIA_ID:
                        var garantiaPrenda = AutoMapper.Mapper.Map<GarantiaPrendaModel, GarantiaPrenda>(garantiaModel as GarantiaPrendaModel);
                        objGenerated = ServiceFacade.Instance.GarantiaPrendaService.Save(garantiaPrenda, User.Identity.Name, garantiaModel.selectedOperationId);
                        break;
                    case CategoriaSuper.OTRAS_ID:
                        var garantiaOtra = AutoMapper.Mapper.Map<GarantiaOtraModel, GarantiaOtra>(garantiaModel as GarantiaOtraModel);
                        objGenerated = ServiceFacade.Instance.GarantiaOtraService.Save(garantiaOtra, User.Identity.Name, garantiaModel.selectedOperationId);
                        break;

                }
                // If we use M&C objGenerated.Key is OperationId, if not, is the Key of the guarantee.
                if (objGenerated != null)
                    result = (int)objGenerated.Key;
            }

            return result;
        }

        /*
           /// <summary>
        /// 01	Garantía Hipotecaria Mueble
        /// 02	Garantía Hipotecaria Inmueble
        /// 03	Depósitos Pignorados en el Banco
        /// 04	Depósitos Pignorados en Otros Bancos
        /// 05	Garantía Prendaria
        /// 06	Otras Garantías
        /// </summary>
        /// <param name="garantiaModel">The garantia model of type <see cref="Bladex.Garantias.Presentation.Website.Models.GarantiaBaseModel"/></param>
        /// <returns></returns>
        public void ValidateCatalogs(GarantiaBaseModel garantiaModel)
        {           

            if (garantiaModel == null || garantiaModel.CategoriaSuper == null || garantiaModel.CategoriaSuper.Key == null)
                throw new ApplicationException("Error al intentar validar el catalogo");

            // Si no hubo seleccion coloco valor por defecto.
            if (garantiaModel.PaisGarantia != null && garantiaModel.PaisGarantia.Key == "-1")
            {
                garantiaModel.PaisGarantia = new PaisViewModel();
            }
            string categoriaSuperId = garantiaModel.CategoriaSuper.Key;
            
                //GarantiaBase objGenerated = null;
                switch (categoriaSuperId)
                {
                    case CategoriaSuper.MUEBLE_ID:
                        var garantiaMueble = AutoMapper.Mapper.Map<GarantiaMuebleModel, GarantiaMueble>(garantiaModel as GarantiaMuebleModel);
                        ServiceFacade.Instance.GarantiaMuebleService.ValidateCatalogs(garantiaMueble);
                        break;
                    case CategoriaSuper.INMUEBLE_ID:
                        var garantiaInmueble = AutoMapper.Mapper.Map<GarantiaInmuebleModel, GarantiaInmueble>(garantiaModel as GarantiaInmuebleModel);
                        ServiceFacade.Instance.GarantiaInmuebleService.ValidateCatalogs(garantiaInmueble);
                        break;

                    case CategoriaSuper.DEPOSITO_ID:
                        var garantiaDeposito = AutoMapper.Mapper.Map<GarantiaDepositoModel, GarantiaDeposito>(garantiaModel as GarantiaDepositoModel);
                        ServiceFacade.Instance.GarantiaDepositoService.ValidateCatalogs(garantiaDeposito);
                        break;
                    case CategoriaSuper.DEPOSITO_OTROS_ID:
                        var garantiaDepositoOtroBanco = AutoMapper.Mapper.Map<GarantiaDepositoOtroBancoModel, GarantiaDepositoOtroBanco>(garantiaModel as GarantiaDepositoOtroBancoModel);
                        ServiceFacade.Instance.GarantiaDepositoOtroBancoService.ValidateCatalogs(garantiaDepositoOtroBanco);
                        break;
                    case CategoriaSuper.PRENDARIA_ID:
                        var garantiaPrenda = AutoMapper.Mapper.Map<GarantiaPrendaModel, GarantiaPrenda>(garantiaModel as GarantiaPrendaModel);
                        ServiceFacade.Instance.GarantiaPrendaService.ValidateCatalogs(garantiaPrenda);
                        break;
                    case CategoriaSuper.OTRAS_ID:
                        var garantiaOtra = AutoMapper.Mapper.Map<GarantiaOtraModel, GarantiaOtra>(garantiaModel as GarantiaOtraModel);
                         ServiceFacade.Instance.GarantiaOtraService.ValidateCatalogs(garantiaOtra);
                        break;
                }            

            return result;
        }
        */
        
        /// <summary>
        /// Gets the dummy customers.
        /// </summary>
        /// <param name="model">The model of type <see cref="Bladex.Garantias.Presentation.Website.Models.GarantiaBaseModel"/></param>
        public void GetDummyCustomers(GarantiaBaseModel model)
        {
            string[] keys = this.ControllerContext.HttpContext.Request.Form.AllKeys.Where(o => o.Contains("hidden_Garantia.")).ToArray();
            if (keys != null && keys.Length > 0)
            {
                var dummyCustomerName = this.ControllerContext.HttpContext.Request.Form["hidden_Garantia.Cliente"];

                if (!string.IsNullOrEmpty(dummyCustomerName))
                {
                    model.Cliente.IsInternal = true;
                    model.Cliente.Nombre = dummyCustomerName;
                }
                else
                    model.Cliente.IsInternal = false;
                
                var dummyGaranteName = this.ControllerContext.HttpContext.Request.Form["hidden_Garantia.Garante"];
                if (!string.IsNullOrEmpty(dummyGaranteName))
                {
                    model.Garante.IsInternal = true;
                    model.Garante.Nombre = dummyGaranteName;
                }
                else
                    model.Garante.IsInternal = false;
            }
            
        }

        /// <summary>
        /// Changes the type garantia.
        /// </summary>
        /// <param name="id">The id of type <see cref="System.String"/></param>
        /// <param name="currentType">Type of the current.</param>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult ChangeTypeGarantia(string id, string currentType)
        {
            int garantiaId = int.Parse(id);
            GarantiaBase gBase = null;
            switch (currentType)
            {
                case "01":
                    gBase = ServiceFacade.Instance.GarantiaMuebleService.GetById(garantiaId);
                    break;
                case "02":
                    gBase = ServiceFacade.Instance.GarantiaInmuebleService.GetById(garantiaId);
                    break;

                case "03":
                    gBase = ServiceFacade.Instance.GarantiaDepositoService.GetById(garantiaId);
                    break;
                case "04":
                    gBase = ServiceFacade.Instance.GarantiaDepositoOtroBancoService.GetById(garantiaId);
                    break;
                case "05":
                    gBase = ServiceFacade.Instance.GarantiaPrendaService.GetById(garantiaId);
                    break;
                case "06":
                    gBase = ServiceFacade.Instance.GarantiaOtraService.GetById(garantiaId);
                    break;
            }
            
            CategoriaSuperChangeViewModel viewModel = new CategoriaSuperChangeViewModel();
            viewModel.Garantia = gBase;
            List<CategoriaSuper> categorias = ServiceFacade.Instance.CategoriaSuperService.GetAll().ToList();
            categorias.Remove(categorias.FirstOrDefault(o => o.Key.ToString() == currentType));
            viewModel.CategoriaSuperList = new SelectList(categorias.Where(o=>!o.IsReadOnly).ToList() , "Key", "Nombre");
            viewModel.CurrentCategoriaSuperId = currentType;
            return PartialView("CategoriaSuperChange", viewModel);
        }

        /// <summary>
        /// Changes the type garantia.
        /// </summary>
        /// <param name="id">The id of type <see cref="System.String"/></param>
        /// <param name="currentType">Type of the current.</param>
        /// <param name="newType">The new type of type <see cref="System.String"/></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ChangeTypeGarantia(string id, string currentType, string newType)
        {
            bool result = false;
            if (ModelState.IsValid)
            {
                GarantiaBase garantia = ServiceFacade.Instance.GarantiaService.FindById(int.Parse(id),null);
                
                switch (garantia.CategoriaSuper.Key.ToString())
                {
                    case "01":

                        result = ServiceFacade.Instance.GarantiaMuebleService.ChangeType(garantia.GetKeyAs<int>(), garantia.CategoriaSuper.Key.ToString(), newType, UserId);
                        break;
                    case "02":
                        result = ServiceFacade.Instance.GarantiaInmuebleService.ChangeType(garantia.GetKeyAs<int>(), garantia.CategoriaSuper.Key.ToString(), newType, UserId);
                        break;

                    case "03":
                        result = ServiceFacade.Instance.GarantiaDepositoService.ChangeType(garantia.GetKeyAs<int>(), garantia.CategoriaSuper.Key.ToString(), newType, UserId);
                        break;
                    case "04":
                        result = ServiceFacade.Instance.GarantiaDepositoOtroBancoService.ChangeType(garantia.GetKeyAs<int>(), garantia.CategoriaSuper.Key.ToString(), newType, UserId);
                        break;
                    case "05":
                        result = ServiceFacade.Instance.GarantiaPrendaService.ChangeType(garantia.GetKeyAs<int>(), garantia.CategoriaSuper.Key.ToString(), newType, UserId);
                        break;
                    case "06":
                        result = ServiceFacade.Instance.GarantiaOtraService.ChangeType(garantia.GetKeyAs<int>(), garantia.CategoriaSuper.Key.ToString(), newType, UserId);
                        break;
                }
            }
            
            return Json(result, JsonRequestBehavior.AllowGet);
            
        }

        /// <summary>
        /// Changes the status garantia.
        /// </summary>
        /// <param name="id">The id of type <see cref="System.String"/></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ChangeStatusGarantia(string id)
        {
            bool result = false;
            int garantiaId;
            if (ModelState.IsValid && int.TryParse(id, out garantiaId))
            {
                ServiceFacade.Instance.GarantiaService.SetInternalStatus(garantiaId, ServiceFacade.Instance.InternalStatusService.GetActiveStatus());
                result = true;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        

        /// <summary>
        /// Errors this instance.
        /// </summary>
        /// <returns></returns>
        [Components.Attributes.Authorization(false)]
        public virtual ActionResult Error()
        {
            return View("Error", this.ViewData["Error"] ?? null);
        }

        /// <summary>
        /// Retrives the categoria super.
        /// </summary>
        /// <param name="categoriaSuperSelectedId">The categoria super selected id of type <see cref="System.String"/></param>
        protected virtual void retriveCategoriaSuper(string categoriaSuperSelectedId)
        {
            if (string.IsNullOrEmpty(categoriaSuperSelectedId))
            {
                throw new ApplicationException("The ID of CategoriaSuper sent is invalid.");
            }
            var categoriaSuper = ServiceFacade.Instance.CategoriaSuperService.GetById(categoriaSuperSelectedId);
            if (categoriaSuper != null)
            {
                this._CategoriaSuper = categoriaSuper;
                ViewData["CategoriaSuperTitle"] = string.IsNullOrEmpty(categoriaSuper.Nombre) ? "Garantía" : categoriaSuper.Nombre;
                ViewData["CategoriaSuperId"] = categoriaSuper.Key.ToString();
            }
            else
            {
                throw new ApplicationException("The ID of CategoriaSuper sent is invalid.");
            }
        }

        /// <summary>
        /// Called before the action method is invoked.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
            if (filterContext.RequestContext.HttpContext.Request.QueryString["categoriaSuperId"] != null)
            {
                retriveCategoriaSuper(filterContext.RequestContext.HttpContext.Request.QueryString["categoriaSuperId"]);
            }
            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        ///   <see cref="Bladex.Garantias.DomainModel.DomainBase.CategoriaSuper"/>
        /// </summary>
        private CategoriaSuper _CategoriaSuper;

        /// <summary>
        /// Populates the model with lists.
        /// </summary>
        /// <param name="garantia">The garantia of type <see cref="Bladex.Garantias.Presentation.Website.Models.GarantiaBaseModel"/></param>
        protected virtual void PopulateModelWithLists(GarantiaBaseModel garantia)
        {
            var paises = ServiceFacade.Instance.PaisService.GetAll().OrderBy(o => o.Nombre).ToList();

            if (garantia.Cliente == null) garantia.Cliente = new ClienteViewModel();
            if (garantia.Cliente.List == null)
            {                
                garantia.Cliente.List = ServiceFacade.Instance.ClienteService.GetAllClientes().OrderBy(o => o.Nombre).Select(entity => new SelectListItem { Text = entity.Nombre, Value = entity.Key.ToString() }).ToList();             
            }

            if (garantia.Garante == null) garantia.Garante = new ClienteViewModel();
            if (garantia.Garante.List == null)
            {                
                List<SelectListItem> garantes = ServiceFacade.Instance.ClienteService.GetAllGarantes().OrderBy(o => o.Nombre).Select(entity => new SelectListItem { Text = entity.Nombre, Value = entity.Key.ToString() }).ToList();                
                if (!string.IsNullOrEmpty(garantia.Garante.Nombre) && garantia.Garante.Key != null  && garantia.Garante.Key.ToString() == "-1")
                {
                    garantes.Add(new SelectListItem { Selected = true, Text = garantia.Garante.Nombre, Value = garantia.Garante.Key });
                    var item=garantes.Find(o=> o.Value==garantia.Garante.Key);
                }

                garantia.Garante.List = garantes;
               
            }
            
            if (garantia.PaisGarantia == null) garantia.PaisGarantia = new PaisViewModel();
                        
            if (garantia.PaisGarantia.List == null)
            {                
                garantia.PaisGarantia.List = paises.Select(entity => new SelectListItem { Text = entity.Nombre, Value = entity.Key.ToString() }).ToList();             
            }

            // Ticket #1619 - 1
            if (garantia.Region == null) garantia.Region = new RegionViewModel();

            if (garantia.Region.List == null)
            {
                garantia.Region.List = ServiceFacade.Instance.RegionService.GetAll().OrderBy(o => o.Nombre).Select(entity => new SelectListItem { Text = entity.Nombre, Value = entity.Key.ToString() }).ToList();
            }

            // Ticket #1619 - 2
            if (garantia.TipoPoliza == null) garantia.TipoPoliza = new TipoPolizaViewModel();

            if (garantia.TipoPoliza.List == null)
            {
                garantia.TipoPoliza.List = ServiceFacade.Instance.TipoPolizaService.GetAll().OrderBy(o => o.Nombre).Select(entity => new SelectListItem { Text = entity.Nombre, Value = entity.Key.ToString() }).ToList();
            }
            
            var listOfActors = ServiceFacade.Instance.ActorService.GetAll().Select(entity => new SelectListItem { Text = entity != null ? entity.Nombre : "(empty)", Value = (entity != null && entity.Key != null) ? entity.Key as string : string.Empty }).OrderBy(o => o.Text).ToList();
                                    
            if (garantia.Administrador == null) garantia.Administrador = new ActorViewModel();
            
                garantia.Administrador.List = listOfActors;
            if (garantia.Administrador.Pais == null) garantia.Administrador.Pais = new PaisViewModel();
            if (garantia.Administrador.Pais.List == null)
            {            
                garantia.Administrador.Pais.List = paises.Select(entity => new SelectListItem { Text = entity.Nombre, Value = entity.Key.ToString() }).ToList();
             
            }
            
            if (garantia.Asegurador == null) garantia.Asegurador = new ActorViewModel();            
                garantia.Asegurador.List = listOfActors;
            if (garantia.Asegurador.Pais == null) garantia.Asegurador.Pais = new PaisViewModel();
            if (garantia.Asegurador.Pais.List == null)
            {                
                garantia.Asegurador.Pais.List = paises.Select(entity => new SelectListItem { Text = entity.Nombre, Value = entity.Key.ToString() }).ToList();             
            }
            
            if (garantia.Depositante == null) garantia.Depositante = new ActorViewModel();            
                garantia.Depositante.List = listOfActors;
            if (garantia.Depositante.Pais == null) garantia.Depositante.Pais = new PaisViewModel();
            if (garantia.Depositante.Pais.List == null)
            {                
                garantia.Depositante.Pais.List = paises.Select(entity => new SelectListItem { Text = entity.Nombre, Value = entity.Key.ToString() }).ToList();             
            }
            
            if (garantia.Evaluador == null) garantia.Evaluador = new ActorViewModel();            
                garantia.Evaluador.List = listOfActors;
            
            if (garantia.Evaluador.Pais == null) garantia.Evaluador.Pais = new PaisViewModel();
            if (garantia.Evaluador.Pais.List == null)
            {                
                garantia.Evaluador.Pais.List = paises.Select(entity => new SelectListItem { Text = entity.Nombre, Value = entity.Key.ToString() }).ToList();             
            }
            
            if (garantia.Revisor == null) garantia.Revisor = new ActorViewModel();            
                garantia.Revisor.List = listOfActors;
            
            if (garantia.Revisor.Pais == null) garantia.Revisor.Pais = new PaisViewModel();
            if (garantia.Revisor.Pais.List == null)
            {                
                garantia.Revisor.Pais.List = paises.Select(entity => new SelectListItem { Text = entity.Nombre, Value = entity.Key.ToString() }).ToList();             
            }
            
            
            
            if (garantia.FiduciariaSuper == null) garantia.FiduciariaSuper = new FiduciariasViewModel();
            if (garantia.FiduciariaSuper.List == null)
            {                
                garantia.FiduciariaSuper.List = ServiceFacade.Instance.FiduciariasService.GetAll().OrderBy(o => o.Nombre).Select(entity => new SelectListItem { Text = entity.Nombre, Value = entity.Key.ToString() }).ToList();             
            }

            if (this._CategoriaSuper != null && this._CategoriaSuper.Key != null)
            {
                var objListOfTipoGarantiaSuper = ServiceFacade.Instance.TipoGarantiaSuperService.GetAll(this._CategoriaSuper.Key.ToString()).Where(c=>c.IsActive == true).ToList();

                if (garantia.TipoGarantiaSuper == null) garantia.TipoGarantiaSuper = new TipoGarantiaSuperViewModel();
                
                var listOfTipoGarantiaSuper = objListOfTipoGarantiaSuper.OrderBy(o => o.Nombre).Select(entity => new SelectListItem { Text = entity.Nombre, Value = entity.Key.ToString() }).ToList();
                
                if (garantia.InternalStatus!=null && garantia.InternalStatus.Key == InternalStatus.UNKNOWN_ID)
                {
                    listOfTipoGarantiaSuper.Add(new SelectListItem() { Text = "Desvinculado", Value = "-1", Selected = true });
                    garantia.TipoGarantiaSuper.Key = "-1";
                }

                garantia.TipoGarantiaSuper.List = listOfTipoGarantiaSuper;
                
                if (objListOfTipoGarantiaSuper.Count > 0)
                {
                    garantia.TipoGarantiaSuper.Categoria = new CategoriaSuperViewModel() { Key = objListOfTipoGarantiaSuper[0].Categoria.Key.ToString(), Nombre = objListOfTipoGarantiaSuper[0].Categoria.Nombre };

                }
            }
                                    
            if (garantia.TipoGarantiaBladex == null) garantia.TipoGarantiaBladex = new TipoGarantiaBladexViewModel();
            if (garantia.TipoGarantiaBladex.List == null)
            {
                garantia.TipoGarantiaBladex.List = ServiceFacade.Instance.TipoGarantiaBladexService.GetAll().OrderBy(o => o.Nombre).Select(entity => new SelectListItem { Text = entity.Nombre, Value = entity.Key.ToString() }).ToList();             
            }
            
            
            if (garantia.FrecuenciaRevision == null) garantia.FrecuenciaRevision = new FrecuenciasViewModel();
            if (garantia.FrecuenciaRevision.List == null)
            {            
                garantia.FrecuenciaRevision.List = ServiceFacade.Instance.FrecuenciasService.GetAll().OrderBy(o => o.Nombre).Select(entity => new SelectListItem { Text = entity.Nombre, Value = entity.Key.ToString() }).ToList();             
            }
                        
            if (garantia.CategoriaRiesgoGarantia == null) garantia.CategoriaRiesgoGarantia = new CategoriaRiesgoGarantiaViewModel();
            if (garantia.CategoriaRiesgoGarantia.List == null)
            {                
                garantia.CategoriaRiesgoGarantia.List = ServiceFacade.Instance.CategoriaRiesgoGarantiaService.GetAll().OrderBy(o => o.Nombre).Select(entity => new SelectListItem { Text = entity.Nombre, Value = entity.Key.ToString() }).ToList();             
            }
            
            if (garantia.Moneda == null) garantia.Moneda = new MonedasViewModel();
            if (garantia.Moneda.List == null)
            {            
                garantia.Moneda.List = ServiceFacade.Instance.MonedasService.GetAll().OrderBy(o => o.Nombre).Select(entity => new SelectListItem { Text = entity.Nombre, Value = entity.Key.ToString() }).ToList();             
            }
                        
            var listOfCalificacionesRiesgo = ServiceFacade.Instance.CalificacionesRiesgoService.GetAll().OrderBy(o => o.Orden).Select(entity => new SelectListItem { Text = entity.ToString(), Value = entity.Key.ToString() }).ToList();
                        
            if (garantia.RatingGarante == null) garantia.RatingGarante = new CalificacionesRiesgoViewModel();
            if(garantia.RatingGarante.List == null)
                garantia.RatingGarante.List = listOfCalificacionesRiesgo;
                        
            var listOfCategoriaSuper = ServiceFacade.Instance.CategoriaSuperService.GetAll().OrderBy(o => o.Nombre).Select(entity => new SelectListItem { Text = entity.Nombre, Value = entity.Key.ToString() }).ToList();
                        
            if (garantia.CategoriaSuper == null) garantia.CategoriaSuper = new CategoriaSuperViewModel();
            if(garantia.CategoriaSuper.List == null) 
                garantia.CategoriaSuper.List = listOfCategoriaSuper;
                        
            var listOfStatus = ServiceFacade.Instance.StatusService.GetAll().OrderBy(o => o.Nombre).Select(entity => new SelectListItem { Text = entity.Nombre, Value = entity.Key.ToString() }).ToList();
                        
            var listOfInternalStatus = ServiceFacade.Instance.InternalStatusService.GetAll().OrderBy(o => o.Nombre).Select(entity => new SelectListItem { Text = entity.Nombre, Value = entity.Key.ToString() }).ToList();
                        
            if (garantia.InternalStatus == null) garantia.InternalStatus = new InternalStatusViewModel();
            if(garantia.InternalStatus.List == null)
                garantia.InternalStatus.List = listOfInternalStatus;

            if (garantia.Status == null) garantia.Status = new StatusViewModel();
            if (garantia.Status.List == null)
                garantia.Status.List = listOfStatus;                 
            
            
        }

        /// <summary>
        /// Checks the over utilization of the specified guarantee.
        /// </summary>
        /// <param name="garantiaId">The garantia id of type <see cref="System.Int32"/></param>
        /// <returns>True if the guarantee is over utilized, else false.</returns>
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post | HttpVerbs.Head | HttpVerbs.Delete | HttpVerbs.Put)]
        public PartialViewResult CheckOverUtilization(int garantiaId)
        {
            var result = ServiceFacade.Instance.GarantiaService.GarantiaSobreUtilizada(garantiaId);
            return PartialView("OverUtilizationAlert", result);
        }


        public JsonResult CalcProximaFechaRevisionEvaluacion(string frecuenciaRevisionId, DateTime fechaUltimaRevisionEvaluacion)
        {
            CalcProximaFechaRevisionEvaluacion_Result result = new CalcProximaFechaRevisionEvaluacion_Result();
            var date = GarantiaBase.FechaProximaRevisionEvaluacionCalculator(frecuenciaRevisionId, fechaUltimaRevisionEvaluacion);
            if (date.HasValue)
                result.ProximaFechaRevisionEvaluacion = date.Value.ToShortDateString();
            return Json(result);
        }

        /// <summary>
        /// Calcs the valor garantia super intendencia.
        /// </summary>
        /// <param name="ValorInicial">The valor inicial of type <see cref="System.String"/></param>
        /// <param name="ValorNecesarioDeGarantia">The valor necesario de garantia of type <see cref="System.String"/></param>
        /// <param name="ValorPolizaSeguro">The valor poliza seguro of type <see cref="System.String"/></param>
        /// <param name="ValorMercado">The valor mercado of type <see cref="System.String"/></param>
        /// <param name="ValorEvaluacionVentaRapida">The valor evaluacion venta rapida of type <see cref="System.String"/></param>
        /// <param name="ValorGarantiaSuperIntendencia">The valor garantia super intendencia of type <see cref="System.String"/></param>
        /// <returns></returns>
        public JsonResult CalcValorGarantiaSuperIntendencia(string ValorInicial, string ValorNecesarioDeGarantia, string ValorPolizaSeguro, string ValorMercado, string ValorEvaluacionVentaRapida, string ValorGarantiaSuperIntendencia, string ValorAvaluo)
        {
            CalcValorGarantiaSuperIntendencia_Result param = new CalcValorGarantiaSuperIntendencia_Result();
            param.ValorInicial = string.IsNullOrEmpty(ValorInicial) ? 0 : decimal.Parse(ValorInicial);
            param.ValorNecesarioDeGarantia = string.IsNullOrEmpty(ValorNecesarioDeGarantia) ? 0 : decimal.Parse(ValorNecesarioDeGarantia);
            param.ValorPolizaSeguro = string.IsNullOrEmpty(ValorPolizaSeguro) ? 0 : decimal.Parse(ValorPolizaSeguro);
            param.ValorMercado = string.IsNullOrEmpty(ValorMercado) ? 0 : decimal.Parse(ValorMercado);
            param.ValorEvaluacionVentaRapida = string.IsNullOrEmpty(ValorEvaluacionVentaRapida) ? default(decimal?) : decimal.Parse(ValorEvaluacionVentaRapida);
            //param.ValorInicial = ValorInicial;
            //param.ValorNecesarioDeGarantia = ValorNecesarioDeGarantia;
            //param.ValorPolizaSeguro = ValorPolizaSeguro;
            //param.ValorMercado = ValorMercado;
            param.ValorGarantiaSuperIntendencia = ValorGarantiaSuperIntendencia;
            param.ValorAvaluo = string.IsNullOrEmpty(ValorAvaluo) ? default(decimal?) : decimal.Parse(ValorAvaluo);
            GarantiaBase garantia = null;
            if (this is GarantiaPrendaController)
            {
                AutoMapper.Mapper.CreateMap<CalcValorGarantiaSuperIntendencia_Result, GarantiaPrenda>();
                garantia = AutoMapper.Mapper.Map<CalcValorGarantiaSuperIntendencia_Result, GarantiaPrenda>(param);
            }
            else if (this is GarantiaOtraController)
            {
                AutoMapper.Mapper.CreateMap<CalcValorGarantiaSuperIntendencia_Result, GarantiaOtra>();
                garantia = AutoMapper.Mapper.Map<CalcValorGarantiaSuperIntendencia_Result, GarantiaOtra>(param);
            }
            else if (this is GarantiaMuebleController)
            {
                AutoMapper.Mapper.CreateMap<CalcValorGarantiaSuperIntendencia_Result, GarantiaMueble>();
                garantia = AutoMapper.Mapper.Map<CalcValorGarantiaSuperIntendencia_Result, GarantiaMueble>(param);
            }
            else if (this is GarantiaInmuebleController)
            {
                AutoMapper.Mapper.CreateMap<CalcValorGarantiaSuperIntendencia_Result, GarantiaInmueble>();
                garantia = AutoMapper.Mapper.Map<CalcValorGarantiaSuperIntendencia_Result, GarantiaInmueble>(param);
            }
            else if (this is GarantiaDepositoController)
            {
                AutoMapper.Mapper.CreateMap<CalcValorGarantiaSuperIntendencia_Result, GarantiaDeposito>();
                garantia = AutoMapper.Mapper.Map<CalcValorGarantiaSuperIntendencia_Result, GarantiaDeposito>(param);
            }
            else if (this is GarantiaDepositoOtroBancoController)
            {
                AutoMapper.Mapper.CreateMap<CalcValorGarantiaSuperIntendencia_Result, GarantiaDepositoOtroBanco>();
                garantia = AutoMapper.Mapper.Map<CalcValorGarantiaSuperIntendencia_Result, GarantiaDepositoOtroBanco>(param);
            }
            else
                return Json(param);

            param.ValorGarantiaSuperIntendencia = garantia.GetValorGarantiaSuperIntendencia().ToString("c");

            return Json(param);
        }

        public JsonResult ClientLimitInformation(string CustomerId)
        {
            
            LimitInformation limitInfo = ServiceFacade.Instance.LimitInformationService.FindBy(CustomerId);
            LimitInformationModel result = AutoMapper.Mapper.Map<LimitInformation, LimitInformationModel>(limitInfo);
            return Json(result);
        }

        /// <summary>
        /// Gets the autocomplete.
        /// </summary>
        /// <param name="htmlControlId">The HTML control id of type <see cref="System.String"/></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetAutocomplete(string htmlControlId)
        {
            var result = ServiceFacade.Instance.AutocompleteValueService.GetByHtmlControlId(htmlControlId);
            return new JsonResult { Data = result.OrderBy(o=>o.Value).Select(s=>s.Value).ToList() };
        }

        /// <summary>
        /// Saves the autocomplete.
        /// </summary>
        /// <param name="htmlControlId">The HTML control id of type <see cref="System.String"/></param>
        /// <param name="value">The value of type <see cref="System.String"/></param>
        /// <returns></returns>
        public JsonResult SaveAutocomplete(string htmlControlId, string value)
        {
            if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(htmlControlId) )
            {
                value = value.Length > 50 ? value.Substring(0, 50):value;
                ServiceFacade.Instance.AutocompleteValueService.Save(new DomainModel.DomainBase.Components.Autocomplete.AutocompleteValue() {HtmlControlId = htmlControlId, Value = value.Trim()});
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTooltip(string id)
        {
            Tooltip tooltip = null;
            if (!string.IsNullOrEmpty(id))
                tooltip = ServiceFacade.Instance.TooltipService.GetById(id);

            if (tooltip == null)
                tooltip = TooltipService.GetEmpty(id);

            return Content(string.Format("<b><u>{0}</u></b><br/><p>{1}</p>", tooltip.TooltipName, tooltip.TooltipHtmlText), MediaTypeNames.Text.Html);

        }

        /// <summary>
        /// Gets the contratos.
        /// </summary>
        /// <param name="garantiaId">The garantia id of type <see cref="System.Int32"/></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetContratos(int garantiaId)
        {
            List<GarantiaContrato> contratos = ServiceFacade.Instance.GarantiaContratoService.GetByGarantiaId(garantiaId).ToList();
            return View("ContratosList", contratos);
        }

        /// <summary>
        /// Saves the contrato.
        /// </summary>
        /// <param name="contrato">The contrato of type <see cref="Bladex.Garantias.DomainModel.DomainBase.GarantiaContrato"/></param>
        /// <returns></returns>
        [HttpPost] 
        public ActionResult SaveContrato(GarantiaContrato contrato)
        {
            if(ModelState.IsValid)
            {
                var result = ServiceFacade.Instance.GarantiaContratoService.Save(contrato);
                return View("GarantiaContratoTable", result);                
            }
            return View("GarantiaContratoTable", contrato);
        }

        /// <summary>
        /// Gets the actor.
        /// </summary>
        /// <param name="ActorId">The actor id of type <see cref="System.String"/></param>
        /// <returns></returns>
        public JsonResult GetActor(string ActorId)
        {
            if (string.IsNullOrEmpty(ActorId)) return Json(new Actor());
            Actor actor = ServiceFacade.Instance.ActorService.GetById(ActorId);
            if (actor == null) actor = new Actor();
            return Json(actor);
        }

        /// <summary>
        /// Gets the customer.
        /// </summary>
        /// <param name="CustomerId">The customer id of type <see cref="System.String"/></param>
        /// <returns></returns>
        public JsonResult GetCustomer(string CustomerId)
        {
            if (string.IsNullOrEmpty(CustomerId)) return Json(new Cliente());
            Cliente cliente = ServiceFacade.Instance.ClienteService.GetById(CustomerId);
            if (cliente == null) cliente = new Cliente();
            return Json(cliente);
        }

        /// <summary>
        /// Gets the customers.
        /// </summary>
        /// <param name="text">The text of type <see cref="System.String"/></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetCustomers(string text)
        {
            return Json(ServiceFacade.Instance.ClienteService.GetAllClientes().ToList().Select(c => new { Value = c.GetKeyAs<string>(), Text = c.Nombre, NationalId = c.NationalId, IsInternal = c.IsInternal }).ToList());
        }

        public PartialViewResult VerifyGuaranteeBlock(int garantiaId)
        {
            // da igual el tipo de servicio ya que se expone para todos.
            var result = ServiceFacade.Instance.GarantiaDepositoOtroBancoService.GetGarantiaBloqueada(garantiaId);
            var message = result ? string.Format("La garantia se encuentra pendiente de revision, no podrá modificarse.") : string.Format("La garantia no posee operaciones pendientes de revision.");
            var model = new VerifyGuaranteeBlock_Result() { Result = result, Message = message, GarantiaId = garantiaId};
            return PartialView("GuaranteeBlockedAlert", model);
        }

        /// <summary>
        /// Gets the garantes.
        /// </summary>
        /// <param name="text">The text of type <see cref="System.String"/></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetGarantes(string text)
        {
            return Json(ServiceFacade.Instance.ClienteService.GetAllGarantes().ToList().Select(c => new { Value = c.GetKeyAs<string>(), Text = c.Nombre, NationalId = c.NationalId, IsInternal = c.IsInternal }).ToList());
        }

        ///<summary>
        ///</summary>
        public struct CalcValorGarantiaSuperIntendencia_Result
        {
            public decimal ValorInicial;
            public decimal ValorNecesarioDeGarantia;
            public decimal ValorPolizaSeguro;
            public decimal ValorMercado;
            public decimal? ValorEvaluacionVentaRapida;
            public string ValorGarantiaSuperIntendencia;
            public decimal? ValorAvaluo;
        }

        public struct CalcProximaFechaRevisionEvaluacion_Result
        {
            public string ProximaFechaRevisionEvaluacion;
        }

        public struct VerifyGuaranteeBlock_Result
        {
            public int GarantiaId { get; set; }
            public string Message { get; set; }
            public bool Result { get; set; }
        }
    }

}
