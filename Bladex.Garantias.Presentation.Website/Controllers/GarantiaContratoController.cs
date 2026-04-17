using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bladex.Garantias.Application.Facades;
using Bladex.Garantias.DomainModel.Components.MakerChecker;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.DomainModel.Services;
using Bladex.Garantias.Infrastructure.Logging;
using Bladex.Garantias.Presentation.Website.Models;
using Bladex.Garantias.Presentation.Website.ViewModels;
using Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker;

namespace Bladex.Garantias.Presentation.Website.Controllers
{
    /// <summary>
    /// The garantia contrato controller class.
    /// </summary>
    public class GarantiaContratoController : Controller
    {
        //
        // GET: /GarantiaContrato/

        /// <summary>
        /// Indexes the specified garantia id.
        /// </summary>
        /// <param name="garantiaId">The garantia id of type <see cref="System.Int32"/></param>
        /// <param name="operationId">The operation id of type <see cref="System.Nullable&lt;System.Int32&gt;"/></param>
        /// <param name="categoriaSuperId">The categoria super id of type <see cref="System.String"/></param>
        /// <returns></returns>
        public ActionResult Index(int? garantiaId, int? operationId, string categoriaSuperId, bool? useRepository, bool? IsReadOnly)
        {            
            List<GarantiaContratoModel> contratos = new List<GarantiaContratoModel>();
            String customerId;
            String customerName;
            IList<GarantiaContrato> usedContracts;
            List<String> usedContractDealReferences;

            if (!useRepository.HasValue)
                useRepository = true;


            if (!useRepository.Value)
            {
                if (operationId.HasValue)
                {
                    //Ticket #1447 : verifico si el operationId es distinto a nuevo, puede darse el caso que se edite el porcentaje de un contrato, 
                    //luego se hace commit y sin salir de la pantalla se vuelve a editar. En ese caso es necesario volver a generar otro operationId.
                    //operationId = SaveGarantia(viewModel.Garantia);
                    //Se acordo que al hacer commit se redirecciona mediante javascript

                    // new guarantee setup
                    var operation = ServiceFacade.Instance.MakerCheckerService.GetOperation(operationId.Value);
                    MakerCheckerObject<GarantiaBase> mcObj = AutoMapper.Mapper.Map(operation.GetMakerCheckerObject(), operation.GetMakerCheckerObject().GetType(), typeof(MakerCheckerObject<GarantiaBase>));

                    usedContracts = ServiceFacade.Instance.GarantiaContratoService.GetContractsFromMakerChekerObject(mcObj);
                    
                    customerId = mcObj.Object.Cliente.GetKeyAs<string>();
                    customerName = mcObj.Object.Cliente.Nombre;

                    contratos = AutoMapper.Mapper.Map<IList<GarantiaContrato>, List<GarantiaContratoModel>>(usedContracts);

                    var dealReferences = ServiceFacade.Instance.GarantiaContratoService.GetDealReferencesByCustomer(customerId);

                    // Ticket #1954 - Fix listado de deals disponibles
                    usedContractDealReferences = contratos.Select(c => c.DealReference).ToList(); 
                    var dealReferencesAvailableCount = 0;
                    foreach (GarantiaContratoDealReference deal in dealReferences)
                    {
                        if (!usedContractDealReferences.Contains(deal.DealReference)) dealReferencesAvailableCount += 1;
                    }
                    // perform a filter to exclude the contracts already selected.


                    //this.ViewBag.AvailableContracts = dealReferences.Count != dealReferencesAvailableCount;
                    this.ViewBag.AvailableContracts = dealReferencesAvailableCount > 0;
                    this.ViewBag.AvailableContractsMessage = 
                            dealReferences.Count == 0 && usedContracts.Count == 0 ? 
                                string.Format("No existen contratos para el cliente {0}.", customerName) : 
                                    dealReferencesAvailableCount == 0 ? string.Format("Ya se han seleccionado todos los contratos disponibles para el cliente {0}.", customerName) 
                                        : string.Empty;

                }

            }
            else
            {
                if (garantiaId.HasValue)
                {
                    // existing guarantee setup
                    var garantia = ServiceFacade.Instance.GarantiaService.GetGarantiaById(garantiaId.Value);
                    // perform a filter to exclude the contracts already selected.
                    usedContracts = ServiceFacade.Instance.GarantiaContratoService.GetByGarantiaId(garantiaId.Value);

                    customerId = garantia.Cliente.GetKeyAs<string>();
                    customerName = garantia.Cliente.Nombre;

                    contratos = AutoMapper.Mapper.Map<List<GarantiaContrato>, List<GarantiaContratoModel>>(ServiceFacade.Instance.GarantiaContratoService.GetByGarantiaId(garantiaId.Value).ToList());
                    var dealReferences = ServiceFacade.Instance.GarantiaContratoService.GetDealReferencesByCustomer(customerId);

                    // Ticket #1954 - Fix listado de deals disponibles
                    usedContractDealReferences = contratos.Select(c => c.DealReference).ToList();
                    var dealReferencesAvailableCount = 0;
                    foreach (GarantiaContratoDealReference deal in dealReferences)
                    {
                        if (!usedContractDealReferences.Contains(deal.DealReference)) dealReferencesAvailableCount += 1;
                    }

                    //this.ViewBag.AvailableContracts = dealReferences.Count != dealReferencesAvailableCount;
                    this.ViewBag.AvailableContracts = dealReferencesAvailableCount > 0;
                    this.ViewBag.AvailableContractsMessage = dealReferences.Count == 0 && usedContracts.Count == 0 ? string.Format("No existen contratos para el cliente {0}.", customerName) : dealReferencesAvailableCount == 0 ? string.Format("Ya se han seleccionado todos los contratos disponibles para el cliente {0}.", customerName) : string.Empty;
                }
            }

            this.ViewBag.CategoriaSuperId = categoriaSuperId;
            this.ViewBag.CategoriaSuper = ServiceFacade.Instance.CategoriaSuperService.GetById(this.ViewBag.CategoriaSuperId);
            this.ViewBag.GarantiaId = garantiaId;
            this.ViewBag.OperationId = operationId;
            this.ViewBag.UseRepository = useRepository;

            if (!IsReadOnly.HasValue)
                IsReadOnly = false;

            this.ViewBag.IsReadOnly = IsReadOnly;
            return View(contratos);
        }

        /// <summary>
        /// Creates the specified garantia id.
        /// </summary>
        /// <param name="garantiaId">The garantia id of type <see cref="System.Int32"/></param>
        /// <param name="operationId">The operation id of type <see cref="System.Nullable&lt;System.Int32&gt;"/></param>
        /// <param name="categoriaSuperId">The categoria super id of type <see cref="System.String"/></param>
        /// <returns></returns>
        ///         //Ticket #1847 - mejora garantias contratos intradiarios

        public ActionResult Create(int? garantiaId, int? operationId, string categoriaSuperId,bool? useRepository, bool? callRefreshContratos)
        {
            ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Debug, "Accediendo al formulario de asociacion de contratos en modo Create.", new Dictionary<string, object>() { { "garantiaId", garantiaId }, { "operationId", operationId }, { "categoriaSuperId", categoriaSuperId }, { "UserId", User.Identity.Name } }, null);


            //Ticket #1847 - Garantias - contratos intradiarios
            if (callRefreshContratos != null && callRefreshContratos.HasValue && callRefreshContratos == true)
                ServiceFacade.Instance.GarantiaContratoService.RefreshContratosIntradiarios();



            this.ViewData["CREATE_SUCCESSFULL"] = false;
            this.ViewBag.CategoriaSuperId = categoriaSuperId;
            this.ViewBag.CategoriaSuper = ServiceFacade.Instance.CategoriaSuperService.GetById(this.ViewBag.CategoriaSuperId);
            this.ViewBag.GarantiaId = garantiaId;
            this.ViewBag.OperationId = operationId;
            this.ViewBag.UseRepository = useRepository;
            return View(this.getCreateViewModel(garantiaId, operationId));
        }



        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.GarantiaContratoViewModel"/></param>
        /// <param name="categoriaSuperId">The categoria super id of type <see cref="System.String"/></param>
        /// <param name="garantiaId">The garantia id of type <see cref="int"/></param>
        /// <param name="operationId">The operation id of type <see cref="int"/></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(GarantiaContratoViewModel model, string categoriaSuperId, int? garantiaId, int? operationId,bool? useRepository)
        {
            ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Debug, "Accediendo al formulario de asociacion de contratos en modo Create (Post).", new Dictionary<string, object>() { { "garantiaId", garantiaId }, { "operationId", operationId }, { "model.ID", model.GarantiaContratoModel.ID }, { "categoriaSuperId", categoriaSuperId }, { "UserId", User.Identity.Name } }, null);
            this.ViewData["CREATE_SUCCESSFULL"] = false;
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = new GarantiaContrato { ID = model.GarantiaContratoModel.ID, PorcUtilization = model.GarantiaContratoModel.PorcUtilization, DealReference = model.GarantiaContratoModel.DealReference, FechaRegistroInicial = model.GarantiaContratoModel.FechaRegistroInicial, FechaVencimientoGarantia = model.GarantiaContratoModel.FechaVencimientoGarantia, FechaVencimientoRiesgo = model.GarantiaContratoModel.FechaVencimientoRiesgo, GarantiaId = model.GarantiaContratoModel.GarantiaId, NetBalancePrincipal = model.GarantiaContratoModel.NetBalancePrincipal };
                    if (operationId.HasValue)
                        entity.GarantiaId = operationId;
                    else if (garantiaId.HasValue)
                        entity.GarantiaId = garantiaId;
                    var result = ServiceFacade.Instance.GarantiaContratoService.Save(entity, User.Identity.Name);
                    this.ViewData["CREATE_SUCCESSFULL"] = true;
                    model.GarantiaContratoModel = AutoMapper.Mapper.Map<GarantiaContrato, GarantiaContratoModel>(result);
                    ViewBag.GarantiaId = garantiaId;
                    ViewBag.OperationId = operationId;
                    ViewBag.UseRepository = useRepository;
                    ViewBag.CategoriaSuperId = categoriaSuperId;
                    this.ViewBag.CategoriaSuper = ServiceFacade.Instance.CategoriaSuperService.GetById(ViewBag.CategoriaSuperId);
                    return View(model);
                }
                else
                {
                    return View(this.getCreateViewModel(garantiaId, operationId));
                }
            }
            catch (Exception ex)
            {
                ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Error, string.Format("Ha ocurrido un error generando la asociacion a un contrato para la garantia {0}", garantiaId), new Dictionary<string, object>() { { "garantiaId", garantiaId}, { "operationId", operationId }, { "categoriaSuperId", categoriaSuperId }, { "garantiaContratoId", model.GarantiaContratoModel.ID } }, ex);
                ModelState.AddModelError(ex.ToString(), ex.Message);
                return View(this.getCreateViewModel(garantiaId, operationId));
            }
        }

        /// <summary>
        /// Gets the create view model.
        /// </summary>
        /// <param name="garantiaId">The garantia id of type <see cref="int"/></param>
        /// <param name="operationId">The operation id of type <see cref="int"/></param>
        /// <returns></returns>
        private GarantiaContratoViewModel getCreateViewModel(int? garantiaId, int? operationId)
        {
            if (operationId.HasValue && garantiaId.HasValue)
            {
                var operation = ServiceFacade.Instance.MakerCheckerService.GetOperation(operationId.Value);
                MakerCheckerObject<GarantiaBase> mcObj = AutoMapper.Mapper.Map(operation.GetMakerCheckerObject(), operation.GetMakerCheckerObject().GetType(), typeof(MakerCheckerObject<GarantiaBase>));
                var dealReferences = ServiceFacade.Instance.GarantiaContratoService.GetDealReferencesByCustomer(mcObj.Object.Cliente.GetKeyAs<string>());
                GarantiaContratoViewModel vm = new GarantiaContratoViewModel();
                vm.CustomerId = mcObj.Object.Cliente.Key.ToString();
                vm.CustomerName = mcObj.Object.Cliente.Nombre;
                vm.GarantiaContratoModel = new GarantiaContratoModel() { GarantiaId = operationId };
                vm.DealReferenceList = AutoMapper.Mapper.Map<List<GarantiaContratoDealReference>, List<DealReferenceSelectionViewModel>>(dealReferences);
                // perform a filter to exclude the contracts already selected.
                var usedContracts = ServiceFacade.Instance.GarantiaContratoService.GetByGarantiaId(garantiaId.Value).ToList();
                if (mcObj.AdditionalAttributes.ContainsKey(GarantiaContratoService.MakerCheckerObjectGarantiaContratoKey))
                {
                    dynamic serializedColl = mcObj.AdditionalAttributes[GarantiaContratoService.MakerCheckerObjectGarantiaContratoKey];
                    var mcUsedContracts = serializedColl.ToObject<List<GarantiaContrato>>();
                    if (mcUsedContracts != null)
                        usedContracts.AddRange(mcUsedContracts);
                }

                vm.DealReferenceList = new List<DealReferenceSelectionViewModel>(vm.DealReferenceList.Where(o => usedContracts.FirstOrDefault(used => used.DealReference == o.DealReference) == null));
                return vm;

            }
            else if (operationId.HasValue)
            {
                var operation = ServiceFacade.Instance.MakerCheckerService.GetOperation(operationId.Value);
                MakerCheckerObject<GarantiaBase> mcObj = AutoMapper.Mapper.Map(operation.GetMakerCheckerObject(), operation.GetMakerCheckerObject().GetType(), typeof(MakerCheckerObject<GarantiaBase>));
                var dealReferences = ServiceFacade.Instance.GarantiaContratoService.GetDealReferencesByCustomer(mcObj.Object.Cliente.GetKeyAs<string>());
                GarantiaContratoViewModel vm = new GarantiaContratoViewModel();
                vm.CustomerId = mcObj.Object.Cliente.Key.ToString();
                vm.CustomerName = mcObj.Object.Cliente.Nombre;
                vm.GarantiaContratoModel = new GarantiaContratoModel() { GarantiaId = garantiaId.HasValue ? garantiaId.Value : operationId.Value };
                vm.DealReferenceList = AutoMapper.Mapper.Map<List<GarantiaContratoDealReference>, List<DealReferenceSelectionViewModel>>(dealReferences);
                // perform a filter to exclude the contracts already selected.
                var usedContracts = new List<GarantiaContrato>();
                if (mcObj.AdditionalAttributes.ContainsKey(GarantiaContratoService.MakerCheckerObjectGarantiaContratoKey))
                {
                    dynamic serializedColl = mcObj.AdditionalAttributes[GarantiaContratoService.MakerCheckerObjectGarantiaContratoKey];
                    usedContracts = serializedColl.ToObject<List<GarantiaContrato>>();
                }
                
                vm.DealReferenceList = new List<DealReferenceSelectionViewModel>(vm.DealReferenceList.Where(o => usedContracts.FirstOrDefault(used => used.DealReference == o.DealReference) == null));
                return vm;
            }
            else if (garantiaId.HasValue)
            {
                var garantia = ServiceFacade.Instance.GarantiaService.FindById(garantiaId.Value,null);
                var dealReferences = ServiceFacade.Instance.GarantiaContratoService.GetDealReferencesByCustomer(garantia.Cliente.GetKeyAs<string>());
                GarantiaContratoViewModel vm = new GarantiaContratoViewModel();
                vm.CustomerId = garantia.Cliente.Key.ToString();
                vm.CustomerName = garantia.Cliente.Nombre;
                vm.GarantiaContratoModel = new GarantiaContratoModel() { GarantiaId = garantiaId.Value };
                vm.DealReferenceList = AutoMapper.Mapper.Map<List<GarantiaContratoDealReference>, List<DealReferenceSelectionViewModel>>(dealReferences);
                // perform a filter to exclude the contracts already selected.
                var usedContracts = ServiceFacade.Instance.GarantiaContratoService.GetByGarantiaId(garantiaId.Value);
                vm.DealReferenceList = new List<DealReferenceSelectionViewModel>(vm.DealReferenceList.Where(o => usedContracts.FirstOrDefault(used => used.DealReference == o.DealReference) == null));
                return vm;
            }
            else
            {
                return new GarantiaContratoViewModel() { GarantiaContratoModel = new GarantiaContratoModel() {  }  }; 
            }
        }

        /// <summary>
        /// Edits the specified id.
        /// </summary>
        /// <param name="id">The id of type <see cref="System.Int32"/></param>
        /// <param name="garantiaId">The garantia id of type <see cref="int"/></param>
        /// <param name="operationId">The operation id of type <see cref="int"/></param>
        /// <returns></returns>
        public ActionResult Edit(int id, int? garantiaId, int? operationId, bool? useRepository, string dealReference,string categoriaSuperId)
        {
            
            try
            {
                ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Debug, "Accediendo al formulario de asociacion de contratos en modo Edit.", new Dictionary<string, object>() { { "garantiaId", garantiaId }, { "operationId", operationId }, { "id", id }, { "UserId", User.Identity.Name } }, null);
                this.ViewData["EDIT_SUCCESSFULL"] = false;
                GarantiaContratoViewModel vm = this.getEditViewModel(id, garantiaId, operationId, useRepository, dealReference);
                ViewBag.UseRepository = useRepository.Value;

                // Ticket #78343321 - Garantias - % de cobertura
                int garantiaIdValue = 0;
                if (garantiaId != null)
                {
                    garantiaIdValue = garantiaId.Value;
                    vm.IsReadOnly = ServiceFacade.Instance.GarantiaContratoService.IsPorcUtilizationImported(garantiaIdValue, dealReference);
                } else
                {
                    vm.IsReadOnly = false;
                }
                ViewBag.IsReadOnly = vm.IsReadOnly;

                if (garantiaId.HasValue)
                    ViewBag.LabelGarantiaContrato = string.Format("({0},{1})", garantiaId.Value.ToString(), vm.GarantiaContratoModel.DealReference);
                else
                    ViewBag.LabelGarantiaContrato = string.Format("({0})", "Nueva");
                //return View(this.getEditViewModel(id, garantiaId, operationId));
                return View(vm);
            }
            catch (Exception ex)
            {               
                ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Error, "Ha ocurrido un error al editar un contrato asociado a una garantia.", new Dictionary<string, object>() { { "id", id } }, ex);
                ModelState.AddModelError("", ex);             
                return RedirectToAction("Index", "Garantia",null);
            }
        }

        /// <summary>
        /// Edits the specified contrato.
        /// </summary>
        /// <param name="model">The model of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.GarantiaContratoViewModel"/></param>
        /// <param name="categoriaSuperId">The categoria super id of type <see cref="System.String"/></param>
        /// <param name="garantiaId">The garantia id of type <see cref="int"/></param>
        /// <param name="operationId">The operation id of type <see cref="int"/></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(GarantiaContratoViewModel model, string categoriaSuperId, int? garantiaId, int? operationId, bool? useRepository)
        {
            this.ViewBag.GarantiaId = garantiaId;
            this.ViewBag.OperationId = operationId;
            this.ViewBag.UseRepository = useRepository.Value;

            ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Debug, "Accediendo al formulario de asociacion de contratos en modo Edit (Post).", new Dictionary<string, object>() { { "garantiaId", garantiaId }, { "operationId", operationId }, { "model.ID", model.GarantiaContratoModel.ID }, { "UserId", User.Identity.Name } }, null);
            this.ViewData["EDIT_SUCCESSFULL"] = false;
            try
            {
                if (ModelState.IsValid)
                {                    
                    var contratoOriginal = ServiceFacade.Instance.GarantiaContratoService.GetById(model.GarantiaContratoModel.ID.ToString(),model.GarantiaContratoModel.DealReference,operationId,useRepository);
                    contratoOriginal.PorcUtilization = model.GarantiaContratoModel.PorcUtilization;
                    var result = ServiceFacade.Instance.GarantiaContratoService.Save(contratoOriginal, User.Identity.Name,operationId);
                    this.ViewData["EDIT_SUCCESSFULL"] = true;
                    this.ViewBag.CategoriaSuperId = ServiceFacade.Instance.CategoriaSuperService.GetByGarantiaId(contratoOriginal.GarantiaId.Value).Key.ToString();
                    this.ViewBag.UseRepository = useRepository;
                    var vm = this.getEditViewModel(model.GarantiaContratoModel.ID, garantiaId, operationId, useRepository,model.GarantiaContratoModel.DealReference);
                    ViewBag.CategoriaSuperId = categoriaSuperId;
                    ViewBag.CategoriaSuper = ServiceFacade.Instance.CategoriaSuperService.GetById(ViewBag.CategoriaSuperId);
                    vm.GarantiaContratoModel = AutoMapper.Mapper.Map<GarantiaContrato, GarantiaContratoModel>(result);
                    return View(vm);
                }
                else
                    return View(this.getEditViewModel(model.GarantiaContratoModel.ID, garantiaId, operationId, useRepository, model.GarantiaContratoModel.DealReference));
            }
            catch (Exception ex)
            {
                ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Error, string.Format("Ha ocurrido un error editando la asociacion a un contrato para la garantia {0}", garantiaId), new Dictionary<string, object>() { { "garantiaId", garantiaId }, { "operationId", operationId }, { "categoriaSuperId", categoriaSuperId }, { "garantiaContratoId", model.GarantiaContratoModel.ID } }, ex);
                ModelState.AddModelError("", ex);
                return View(this.getEditViewModel(model.GarantiaContratoModel.ID, garantiaId, operationId, useRepository, model.GarantiaContratoModel.DealReference));
            }
        }

        private GarantiaContratoViewModel getEditViewModel(int id, int? garantiaId, int? operationId, bool? useRepository, string dealReference)
        {
            try
            {
                GarantiaContrato contrato = ServiceFacade.Instance.GarantiaContratoService.GetById(id.ToString(), dealReference, operationId, useRepository);

                int key;

                if (useRepository.HasValue && useRepository.Value == false)
                    key = operationId.Value;
                else
                    key = contrato.GarantiaId.Value;


                var garantia = ServiceFacade.Instance.GarantiaService.FindById(key, useRepository);


                ViewBag.GarantiaId = garantiaId;
                ViewBag.OperationId = operationId;
                ViewBag.CategoriaSuperId = garantia.CategoriaSuper.GetKeyAs<string>();
                ViewBag.CategoriaSuper = ServiceFacade.Instance.CategoriaSuperService.GetById(this.ViewBag.CategoriaSuperId);

                var contratoDealReference = ServiceFacade.Instance.GarantiaContratoService.GetDealReference(contrato.DealReference);
                GarantiaContratoViewModel vm = new GarantiaContratoViewModel { CustomerId = garantia.Cliente.Key.ToString(), CustomerName = garantia.Cliente.Nombre, GarantiaContratoModel = AutoMapper.Mapper.Map<GarantiaContrato, GarantiaContratoModel>(contrato), DealReferenceList = new List<DealReferenceSelectionViewModel>() { AutoMapper.Mapper.Map<GarantiaContratoDealReference, DealReferenceSelectionViewModel>(contratoDealReference) } };
                var usedContracts = ServiceFacade.Instance.GarantiaContratoService.GetByGarantiaId(garantia.GetKeyAs<int>());
                vm.DealReferenceList = new List<DealReferenceSelectionViewModel>(vm.DealReferenceList.Where(o => usedContracts.FirstOrDefault(used => used.DealReference == o.DealReference) == null));

                // Ticket #78343321 - Garantias - % de cobertura
                if (garantiaId.HasValue)
                    vm.IsReadOnly = ServiceFacade.Instance.GarantiaContratoService.IsPorcUtilizationImported(garantiaId.Value, dealReference);
                else
                    vm.IsReadOnly = false;

                ViewBag.IsReadOnly = vm.IsReadOnly;
                return vm;
            }
            catch(Exception ex)
            {
                ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Error, string.Format("Ha ocurrido un error editando la asociacion a un contrato para la garantia {0}", garantiaId), new Dictionary<string, object>() { { "garantiaId", garantiaId }, { "operationId", operationId }}, ex);
                ModelState.AddModelError("", ex);
                return null;
            }
            
        }

        /// <summary>
        /// Deletes the specified id.
        /// </summary>
        /// <param name="id">The id of type <see cref="System.Int32"/></param>
        /// <param name="garantiaId">The garantia id of type <see cref="System.Int32"/></param>
        /// <param name="operationId">The operation id of type <see cref="int"/></param>
        /// <param name="categoriaSuperId">The categoria super id of type <see cref="System.String"/></param>
        /// <returns></returns>
        public ActionResult Delete(int id, int? garantiaId, int? operationId, string categoriaSuperId,bool? useRepository,string dealReference)
        {
            ViewBag.GarantiaId = garantiaId;
            ViewBag.OperationId = operationId;
            ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Debug, "Accediendo al formulario de asociacion de contratos en modo Delete.", new Dictionary<string, object>() { { "garantiaId", garantiaId }, { "operationId", operationId }, { "categoriaSuperId", categoriaSuperId }, { "id", id }, { "UserId", User.Identity.Name } }, null);
            try
            {
                /*
                if (garantiaId.HasValue)
                {
                    var contrato = ServiceFacade.Instance.GarantiaContratoService.GetById(id.ToString(), dealReference, operationId, useRepository);
                    ServiceFacade.Instance.GarantiaContratoService.Delete(contrato,useRepository,operationId);
                    return RedirectToAction("Index", "GarantiaContrato", new { garantiaId, operationId, categoriaSuperId, useRepository });
                }
                else if (operationId.HasValue)
                {
                    return RedirectToAction("Index", "GarantiaContrato", new { garantiaId, operationId, categoriaSuperId, useRepository });
                }
                else
                {
                    return RedirectToAction("Index", "GarantiaContrato", new { garantiaId, operationId, categoriaSuperId, useRepository });
                }*/

                var contrato = ServiceFacade.Instance.GarantiaContratoService.GetById(id.ToString(), dealReference, operationId, useRepository);
                ServiceFacade.Instance.GarantiaContratoService.Delete(contrato, useRepository, operationId);
                return RedirectToAction("Index", "GarantiaContrato", new { garantiaId, operationId, categoriaSuperId, useRepository });
            }
            catch (Exception ex)
            {
                ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Error, "Ha ocurrido un error al eliminar un contrato asociado a una garantia.", new Dictionary<string, object>() { { "id", id } }, ex);
                return RedirectToAction("Index", "GarantiaContrato", new { garantiaId, operationId, categoriaSuperId, useRepository });
            }
        }



        

 
    }
}
