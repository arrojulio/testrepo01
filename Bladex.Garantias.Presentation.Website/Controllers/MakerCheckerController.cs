using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Bladex.Garantias.Application.Facades;
using Bladex.Garantias.DomainModel.Components.MakerChecker;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker;
using Bladex.Garantias.DomainModel.Repositories.Components.MakerChecker;
using Bladex.Garantias.DomainModel.Services;
using Bladex.Garantias.Infrastructure.Logging;
using Bladex.Garantias.Infrastructure.Extensions;
using Bladex.Garantias.Infrastructure.Serialization;
using Bladex.Garantias.Presentation.Website.Components.Authentication;
using Bladex.Garantias.Presentation.Website.Models;
using Bladex.Garantias.Presentation.Website.ViewModels;
using Telerik.Web.Mvc;
using Bladex.Garantias.DomainModel.Repositories;

namespace Bladex.Garantias.Presentation.Website.Controllers
{
    /// <summary>
    /// The maker checker controller class.
    /// </summary>
    [HandleError]
    public class MakerCheckerController : GarantiaBaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            MakerCheckerUser user = getCurrentUser();
            if (user != null)
            {

                List<MakerCheckerOperationSummary> summaries = ServiceFacade.Instance.MakerCheckerService.GetPendingSummaryOperations(user);
                return View(summaries);

            }
            return View();
        }




        /// <summary>
        /// Detailses the specified changeset id.
        /// </summary>
        /// <param name="ChangesetId">The changeset id of type <see cref="System.Guid"/></param>
        /// <param name="Page">The page of type <see cref="System.Int32"/></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Details(Guid ChangesetId, int Page)
        {
            MakerCheckerUser user = getCurrentUser();

            // Ticket #1654: deshabilitar los botones de Reject y Aprove cuando el APP User no es PowerUser
            var appUser = ServiceFacade.Instance.UserService.GetById(this.User.Identity.Name);

            MakerCheckerChangeset changeset = ServiceFacade.Instance.MakerCheckerService.GetChangeset(ChangesetId);
            List<MakerCheckerOperation> operations = ServiceFacade.Instance.MakerCheckerService.GetOperations(ChangesetId);
            MakerCheckerOperation operation = Page > operations.Count ? operations[0] : operations[Page - 1];

            dynamic makerCheckerObject = operation.GetMakerCheckerObject();

            //READYONLY si el usuario NO esta autorizado para aprobar O la operacion ya fue revisada (NOT operationStatus = NEW)
            bool readOnly = !ServiceFacade.Instance.MakerCheckerService.IsCheckUserAllowedToApproveOperation(operation, appUser) 
                || operation.OperationStatusId != (int)MakerCheckerOperationStatus.OperationStatus.New;

            MakerCheckerDetailViewModel vm = new MakerCheckerDetailViewModel
            {
                Changeset = changeset,
                Operation = operation,
                TotalPages = operations.Count,
                Page = Page,
                ReadOnly = readOnly
            };
            

            GarantiaBaseModel model = ((GarantiaBase)makerCheckerObject.Object).CreateModel();
            vm.Proposed = model.GetProperties();
            
            if (vm.Proposed == null) vm.Proposed = new Dictionary<string, object>();
            if (vm.Original == null) vm.Original = new Dictionary<string, object>();
            
            // If we have a previous guarantee, we retrieve it to compare with the last one.
            if (model.Key.HasValue)
            {
                GarantiaBase old = ServiceFacade.Instance.GarantiaService.FindById(model.Key.Value,null);
                // Ticket #1624: workaround
                if (old != null)
                {
                    string tempIdent = string.Empty;
                    tempIdent = ServiceFacade.Instance.GarantiaOtraService.GetIdentificacionDocumentoGarantia(model.Key.Value);
                    old.SetIdentificacionDocumentoGarantia(tempIdent);
                }

                if (old != null)
                {
                    vm.Original = old.CreateModel().GetProperties();
                    // Obtengo los contratos asociados a la garantia ya existentes
                    var contratos = ServiceFacade.Instance.GarantiaContratoService.GetByGarantiaId(model.Key.Value);
                    StringBuilder strContractBuilder = new StringBuilder();
                    strContractBuilder.Append("<ul class='garantia-contrato-list'>");
                    foreach (var gc in contratos)
                    {
                        strContractBuilder.AppendFormat("<li>{0}</li>", gc.ToString());
                    }
                    strContractBuilder.Append("</ul>");
                    vm.Original.Add("Contratos asociados", strContractBuilder.ToString());

                    if (model.CategoriaSuper.Key.ToString() == "06")
                    {
                        //Avales
                        var avales = ServiceFacade.Instance.AvalService.GetByGarantiaId(model.Key.Value);
                        StringBuilder strAvalBuilder = new StringBuilder();
                        strAvalBuilder.Append("<ul class='garantia-Aval-list'>");
                        foreach (var aval in avales)
                        {
                            strAvalBuilder.AppendFormat("<li>{0} : {1}%</li>", aval.Nombre, aval.PorcentajeCobertura);
                        }
                        strAvalBuilder.Append("</ul>");
                        vm.Original.Add("Inclusion de Avales Adicionales", strAvalBuilder.ToString());
                    }
                }
            }
            
            if (makerCheckerObject.AdditionalAttributes != null)
            {
                SerializableDictionary<string, object> dict = makerCheckerObject.AdditionalAttributes as SerializableDictionary<string, object>;
                if (dict != null)
                {
                    if (dict.ContainsKey(GarantiaContratoService.MakerCheckerObjectGarantiaContratoKey))
                    {
                        dynamic objColl = dict[GarantiaContratoService.MakerCheckerObjectGarantiaContratoKey];
                        List<GarantiaContrato> gContratoList = objColl.ToObject<List<GarantiaContrato>>();
                        StringBuilder strContractBuilder = new StringBuilder();
                        strContractBuilder.Append("<ul class='garantia-contrato-list'>");
                        // agrego los contratos ya existentes
                        /*
                        if (model.Key.HasValue)
                        {
                            
                            var contratosExistentes = ServiceFacade.Instance.GarantiaContratoService.GetByGarantiaId(model.Key.Value).ToList();
                            foreach (var cOld in contratosExistentes)
                            {
                                strContractBuilder.AppendFormat("<li>{0}</li>", cOld.ToString());
                            }
                        }*/

                        // agrego los nuevos contratos
                        foreach (var gc in gContratoList)
                        {
                            strContractBuilder.AppendFormat("<li>{0}</li>", gc.ToString());
                        }
                        strContractBuilder.Append("</ul>");
                        vm.Proposed.Add("Contratos asociados", strContractBuilder.ToString());
                    }
                }
            }
                     
            //TODO : Cambiar esto por una constante, estoy quemado.
            if (model.CategoriaSuper.Key.ToString()== "06" && makerCheckerObject.Object != null && makerCheckerObject.Object.Avales != null)
            {
                List<Aval> avalesNuevos = makerCheckerObject.Object.Avales;                
                StringBuilder strAvalOrigBuilder = new StringBuilder();
                strAvalOrigBuilder.Append("<ul class='garantia-Aval-list'>");                
                // agrego los avales nuevos
                foreach (var item in avalesNuevos)
                {
                    strAvalOrigBuilder.AppendFormat("<li>{0} : {1}%</li>", item.Nombre, item.PorcentajeCobertura);
                }
                strAvalOrigBuilder.Append("</ul>");
                vm.Proposed.Add("Inclusion de Avales Adicionales", strAvalOrigBuilder.ToString());
            }
            

            return View(vm);
        }

        /// <summary>
        /// Gets the current user.
        /// </summary>
        /// <returns></returns>
        private MakerCheckerUser getCurrentUser()
        {
            var user = ServiceFacade.Instance.UserService.GetById(this.User.Identity.Name);
            if (user == null)
            {
                InvalidUserException ex = new InvalidUserException(string.Format("The User with id {0} not found into the system.", UserId));
                ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Warning, "Acceso al sistema sin envio de id de usuario o el usuario no existe.", new Dictionary<string, object>() { { "UserId", UserId } }, ex);
                throw ex;
            }
            MakerCheckerUser makerCheckerUser = ServiceFacade.Instance.MakerCheckerService.GetUser(this.User.Identity.Name);
            if (makerCheckerUser == null)
            {
                InvalidRoleException ex = new InvalidRoleException(string.Format("The User with id {0} not found or doesn't have Maker privileges.", UserId));
                ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Warning, "El usuario no posee privilegios de Maker.", new Dictionary<string, object>() { { "UserId", UserId } }, ex);
                throw ex;
            }
            return makerCheckerUser;
        }

        /// <summary>
        /// Approves the specified operation id.
        /// </summary>
        /// <param name="operationId">The operation id of type <see cref="System.Int32"/></param>
        /// <param name="comment">The comment of type <see cref="System.String"/></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Approve(int operationId, string comment)
        {
            var operation = ServiceFacade.Instance.MakerCheckerService.GetOperation(operationId);



            operation.OperationStatusId = (int)MakerCheckerOperationStatus.OperationStatus.Approved;
            // restore and commit the guarantee.
            dynamic makerCheckerObject = operation.GetMakerCheckerObject();
            GarantiaBaseModel model = ((GarantiaBase)makerCheckerObject.Object).CreateModel();
            int garantiaIdGenerated = this.CommitGarantia(model);
            // salvo los contratos asociados a la garantia
            if (makerCheckerObject.AdditionalAttributes != null)
            {
                SerializableDictionary<string, object> addAttributes = makerCheckerObject.AdditionalAttributes as SerializableDictionary<string, object>;
                if (addAttributes != null && addAttributes.ContainsKey(GarantiaContratoService.MakerCheckerObjectGarantiaContratoKey))
                {
                    
                    dynamic objList = addAttributes[GarantiaContratoService.MakerCheckerObjectGarantiaContratoKey];
                    List<GarantiaContrato> gContratoList = objList.ToObject<List<GarantiaContrato>>();
                    
                    if (garantiaIdGenerated != null && garantiaIdGenerated > 0)
                        ServiceFacade.Instance.GarantiaContratoService.DeleteMappingsWhereNotInJson(garantiaIdGenerated,gContratoList);

                    foreach (var gContrato in gContratoList)
                    {
                        gContrato.GarantiaId = garantiaIdGenerated;
                        var r = ServiceFacade.Instance.GarantiaContratoService.Commit(gContrato, User.Identity.Name);
                        ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Debug,string.Format("GarantiaContrato creado. ID: {0}", r.ID),null, null);
                    }
                }
            }

            // update the operation
            operation.ItemId = garantiaIdGenerated;
            operation.CheckerUserId = this.UserId;
            operation.CheckerDate = DateTime.Now;
            operation.Comment = comment;
            try
            {
                ServiceFacade.Instance.MakerCheckerService.SaveOperation(operation, GetUser());
            }
            catch (DBConcurrencyException dbEx)
            {
                return Json(dbEx.Message);
            }
            return Json("success");

            
        }

        /// <summary>
        /// Rejects the specified operation id.
        /// </summary>
        /// <param name="operationId">The operation id of type <see cref="System.Int32"/></param>
        /// <param name="comment">The comment of type <see cref="System.String"/></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Reject(int operationId, string comment)
        {
            ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Error, string.Format("Llamada ajax reject Operation ID: {0}", operationId), null, null);
            var operation = ServiceFacade.Instance.MakerCheckerService.GetOperation(operationId);
            operation.OperationStatusId = (int)MakerCheckerOperationStatus.OperationStatus.Rejected;
            operation.CheckerUserId = this.UserId;
            operation.CheckerDate = DateTime.Now;
            operation.Comment = comment;
            try
            {
                ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Error, string.Format("Dentro del reject intento salvar Operation ID: {0}", operationId), null, null);
                ServiceFacade.Instance.MakerCheckerService.SaveOperation(operation, GetUser());
                ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Error, string.Format("Dentro del reject savaldo success Operation ID: {0}", operationId), null, null);
            }
            catch (DBConcurrencyException dbEx)
            {
                ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Error, string.Format("Error reject savaldo success Operation ID: {0} - {1}", operationId, dbEx.Message), null, null);
                return Json(dbEx.Message);
            }
            return Json("success");
        }

        /// <summary>
        /// Gets the current changeset for the user.
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post | HttpVerbs.Head | HttpVerbs.Delete | HttpVerbs.Put)]
        public ActionResult Current()
        {
            try
            {                
                // get the user
                MakerCheckerUser user = getCurrentUser();
                // get the changeset
                MakerCheckerChangeset changeset = ServiceFacade.Instance.MakerCheckerService.GetCurrentChangeset(user.UserId);
                // get the operations
                List<MakerCheckerOperation> operations = ServiceFacade.Instance.MakerCheckerService.GetOperations(changeset.ChangesetId);
                // prepare the view model
                MakerCheckerChangesetViewerViewModel vm = new MakerCheckerChangesetViewerViewModel() { Changeset = changeset };
                // add the operations view models
                vm.Operations.AddRange(operations.Select(o => new MakerCheckerOperationViewerViewModel(o)));
                // return the partial view
                return PartialView("ChangesetViewer", vm);
            }
            catch (InvalidUserException iUsr)
            {
                throw iUsr;
            }
            catch (InvalidRoleException iRole)
            {
                MakerCheckerChangesetViewerViewModel vm = new MakerCheckerChangesetViewerViewModel();
                // dummy changeset
                vm.Changeset = new MakerCheckerChangeset() { ChangesetId = Guid.NewGuid() };
                // dummy operations
                vm.Operations = new List<MakerCheckerOperationViewerViewModel>();
                return PartialView("ChangesetViewer", vm);
            }

        }

        /// <summary>
        /// Commits the specified changeset id.
        /// </summary>
        /// <param name="changesetId">The changeset id of type <see cref="System.Guid"/></param>
        /// <param name="changesetComment">The changeset comment of type <see cref="System.String"/></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Commit(Guid changesetId, string changesetComment)
        {
            ServiceFacade.Instance.MakerCheckerService.CommitChangeset(changesetId, changesetComment);

            return Json(new { redirectToUrl = Url.Action("Index", "Garantia") });             
        }

        /// <summary>
        /// Cancels the operation.
        /// </summary>
        /// <param name="operationId">The operation id of type <see cref="System.Int32"/></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CancelOperation(int operationId, int? currentOperation)
        {
            try
            {
                var operation = ServiceFacade.Instance.MakerCheckerService.GetOperation(operationId);
                ServiceFacade.Instance.MakerCheckerService.DeleteOperation(operation);

                if (currentOperation.HasValue && operationId == currentOperation)                    
                    return Content("reload");
                else
                    return Content("success");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult Review(int operationId)
        {
            GarantiaCommonService<IGarantiaBaseRepository<GarantiaBase>, GarantiaBase> commonSvc = new GarantiaCommonService<IGarantiaBaseRepository<GarantiaBase>, GarantiaBase>();
            string source=string.Empty;

            try
            {                
                var garantia= commonSvc.GetMakerCheckerObject(operationId);
                string categoriaSuper =garantia.Object.CategoriaSuper.Key.ToString();

                  switch (categoriaSuper) 
                {
                    case "01":
                        source = "GarantiaMueble";
                        break;

                    case "02":
                        source = "GarantiaInmueble";
                        break;

                    case "04":
                        source = "GarantiaDepositoOtroBanco";
                        break;

                    case "05":
                        source = "GarantiaPrenda";
                        break;
                        
                    case "06":
                        source = "GarantiaOtra";
                        break;                        
                }

                  return this.RedirectToAction("Edit", source, new { operationId = operationId, garantiaId = 0,categoriaSuperId = categoriaSuper, useRepository = false });
             
            }
            catch (Exception ex)
            {
                ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Error, string.Format("Error revisando Operation ID: {0}", operationId), null, null);
                return Content(ex.Message);
            }
        }
      
    } 
}
