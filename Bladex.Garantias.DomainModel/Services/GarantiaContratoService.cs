using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.Components.MakerChecker;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.DomainModel.Services.Components.MakerChecker;
using Bladex.Garantias.Infrastructure.Logging;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using IGarantiaContratoRepository = Bladex.Garantias.DomainModel.Repositories.IGarantiaContratoRepository;
using Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker;
using Bladex.Garantias.DomainModel.Repositories.Components.MakerChecker;

namespace Bladex.Garantias.DomainModel.Services
{
    /// <summary>
    /// The garantia contrato service class.
    /// </summary>
    public class GarantiaContratoService
    {
        /// <summary>
        ///   <see cref="System.String"/>
        /// </summary>
        public const string MakerCheckerObjectGarantiaContratoKey = "GarantiaContratoList";

        /// <summary>
        ///   <see cref="Bladex.Garantias.DomainModel.Repositories.IGarantiaContratoRepository"/>
        /// </summary>
        protected IGarantiaContratoRepository GarantiaContratoRepository = RepositoryFactory.GetRepository<IGarantiaContratoRepository, GarantiaContrato>();

        protected IGarantiaContratoDealReferenceRepository GarantiaContratoDealReferenceRepository = RepositoryFactory.GetRepository<IGarantiaContratoDealReferenceRepository, GarantiaContratoDealReference>();
        protected IMakerCheckerOperationRepository MakerCheckerOperationRepository = RepositoryFactory.GetRepository<IMakerCheckerOperationRepository, MakerCheckerOperation>();
        /// <summary>
        /// Returns all avales.
        /// </summary>
        /// <returns>A <see cref="GarantiaContrato"/> IList</returns>
        public IList<GarantiaContrato> GetAll()
        {
            
            return GarantiaContratoRepository.GetAll();
        }

        /// <summary>
        /// Return one GarantiaContrato by Id
        /// </summary>
        /// <param name="garantiaContratoId">The garantia contrato id of type <see cref="System.String"/></param>
        /// <returns>
        /// A <see cref="GarantiaContrato"/> entity.
        /// </returns>
        public GarantiaContrato GetById(string garantiaContratoId,string dealReference,int? operationId,bool? useRepository)
        {
            MakerCheckerOperation operation = new MakerCheckerOperation();

            if (useRepository.HasValue && !useRepository.Value == true)
            {
                if (operationId.HasValue && !string.IsNullOrEmpty(dealReference))
                {
                    //hacer el get operation
                    //operation =   MakerCheckerOperationRepository.GetAll().Where(o => o.OperationId == Convert.ToInt32(operationId.Value)).FirstOrDefault();
                    //Ticket #1447 : Se quita el getAll por temas de performance
                    operation = MakerCheckerOperationRepository.GetSQLOperationsByFilter(operationId.Value.ToString(), "OperationId").FirstOrDefault();

                    if (operation == null)
                    {
                        throw new Exception(string.Format("La operacion con id '{0}' fue eliminada", operationId.Value));
                    }
                    else
                    {
                        MakerCheckerObject<GarantiaBase> mcObjOriginal = AutoMapper.Mapper.Map(operation.GetMakerCheckerObject(), operation.GetMakerCheckerObject().GetType(), typeof(MakerCheckerObject<GarantiaBase>));
                        List<GarantiaContrato> contratos = GetContractsFromMakerChekerObject(mcObjOriginal);
                        var contratoSeleccionado = contratos.Find(o => o.DealReference == dealReference);

                        if (contratoSeleccionado != null)
                            return contratoSeleccionado;
                        else
                            throw new Exception(string.Format("El contrato '{0}' no ha sido encontrado", dealReference));
                    }
                        
                }
                else
                {
                    throw new Exception("Operation id null al leer los contratos"); 
                }
            }
            else
            {
                return GarantiaContratoRepository.FindBy(garantiaContratoId);
            }
            //return GarantiaContratoRepository.FindBy(garantiaContratoId);
        }

        
        /// <summary>
        /// Gets the deal references to use as selection into the create/edit garantia contrato view. It uses the customer id the retrieve the entity and pass as parameters to the repository the Customer Id and the Global Line Description.
        /// </summary>
        /// <param name="customerId">The customer id of type <see cref="System.String"/></param>
        /// <returns></returns>
        public List<GarantiaContratoDealReference> GetDealReferencesByCustomer(string customerId)
        {
            var customerService = new ClienteService();
            var customer = customerService.GetById(customerId);
            if (customer == null) return new List<GarantiaContratoDealReference>();
            // returns deals that doesn't have net balance equals to zero.
            return GarantiaContratoDealReferenceRepository.GetGarantiaContratoDealReferenceByCustomerOrEconomicGroup(customer.GetKeyAs<string>(), customer.GlobalLineDescription).Where(o => o.NetBalance != decimal.Zero).ToList();
        }

        /// <summary>
        /// Gets the deal references.
        /// </summary>
        /// <returns></returns>
        public List<GarantiaContratoDealReference> GetDealReferences()
        {
            return GarantiaContratoDealReferenceRepository.GetAll().ToList();
        }

        /// <summary>
        /// Gets the deal reference.
        /// </summary>
        /// <param name="dealReferenceId">The deal reference id of type <see cref="System.String"/></param>
        /// <returns></returns>
        public GarantiaContratoDealReference GetDealReference(string dealReferenceId)
        {
            return GarantiaContratoDealReferenceRepository.FindBy(dealReferenceId);
        }

        /// <summary>
        /// Gets the by garantia id deal reference.
        /// </summary>
        /// <param name="garantiaId">The garantia id of type <see cref="System.Int32"/></param>
        /// <param name="dealReference">The deal reference of type <see cref="System.String"/></param>
        /// <returns></returns>
        public GarantiaContrato GetByGarantiaIdDealReference(int garantiaId, string dealReference)
        {
            return GarantiaContratoRepository.GetByGarantiaIdDealReference(garantiaId, dealReference);
        }

        /// <summary>
        /// Saves the specified garantia contrato.
        /// </summary>
        /// <param name="garantiaContrato">The garantia contrato of type <see cref="Bladex.Garantias.DomainModel.DomainBase.GarantiaContrato"/></param>
        /// <returns></returns>
        public GarantiaContrato Save(GarantiaContrato garantiaContrato)
        {
            return Save(garantiaContrato, "novaris", false,null);
        }

        public GarantiaContrato Save(GarantiaContrato garantiaContrato, string auditUserId)
        {
            return Save(garantiaContrato, auditUserId, true,null);
        }

        public GarantiaContrato Save(GarantiaContrato garantiaContrato, string auditUserId,int? operationId)
        {
            return Save(garantiaContrato, auditUserId, true, operationId);
        }

        public GarantiaContrato Commit(GarantiaContrato garantiaContrato, string auditUserId)
        {
            ApplicationLogger.Instance.Info(string.Format("Accediendo al sistema de maker & checker bajo el usuario {0}", auditUserId));
            ApplicationLogger.Instance.Info(string.Format("Efectuando operacion de commit solicitada por el usuario {0}.", auditUserId));
            return this.Save(garantiaContrato, auditUserId, false,null);
        }

        private GarantiaContrato Save(GarantiaContrato garantiaContrato, string auditUserId, bool useMakerAndChecker,int? operationId)
        {
            if (useMakerAndChecker)
            {
                ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Debug, string.Format("Utilizando el sistema de maker & checker para operaciones de guardado sobre el contrato ID: {0} - Identificador Garantia/Operacion: {1}.", garantiaContrato.Key, garantiaContrato.GarantiaId), new Dictionary<string, object>() { { "auditUserId", auditUserId }, { "useMakerAndChecker", useMakerAndChecker } });

                GarantiaCommonService<IGarantiaBaseRepository<GarantiaBase>, GarantiaBase> gService = new GarantiaCommonService<IGarantiaBaseRepository<GarantiaBase>, GarantiaBase>();
                MakerCheckerObject<GarantiaBase> mcObj;
                List<GarantiaContrato> tempContratos = new List<GarantiaContrato>();
                if(operationId.HasValue)
                    mcObj = gService.GetMakerCheckerObject(operationId.Value);
                else
                    mcObj = gService.GetMakerCheckerObject(garantiaContrato.GarantiaId.Value);

                //mcObj.Deserialize();
                if (mcObj.AdditionalAttributes != null)
                {
                    // si no cree la coleccion, la genero
                    if (!mcObj.AdditionalAttributes.ContainsKey(MakerCheckerObjectGarantiaContratoKey))
                    {
                        mcObj.AdditionalAttributes.Add(MakerCheckerObjectGarantiaContratoKey, new List<GarantiaContrato>());
                    }
                    // agrego el contrato a la coleccion
                    dynamic objColl = mcObj.AdditionalAttributes[MakerCheckerObjectGarantiaContratoKey];
                    List<GarantiaContrato> gContratoList = null;
                    if(objColl is List<GarantiaContrato>)
                        gContratoList = objColl as List<GarantiaContrato>;
                    else 
                        gContratoList = objColl.ToObject<List<GarantiaContrato>>();

                    if (gContratoList.Count(o => o.DealReference == garantiaContrato.DealReference) > 0)
                    {
                        
                        foreach (GarantiaContrato item in gContratoList)
                        {
                            if (item.DealReference == garantiaContrato.DealReference)
                            {                                
                                tempContratos.Add(garantiaContrato);
                            }
                            else
                            {
                                tempContratos.Add(item);
                            }
                        }

                        gContratoList = new List<GarantiaContrato>();
                        gContratoList = tempContratos;
                    }
                    else
                    {
                        gContratoList.Add(garantiaContrato);
                    }
                                        

                    mcObj.AdditionalAttributes[MakerCheckerObjectGarantiaContratoKey] = gContratoList;
                    
                    // actualizo la operacion.                    
                    if (operationId.HasValue)
                        gService.SetMakerCheckerObject(operationId.Value, mcObj);
                    else
                        gService.SetMakerCheckerObject(garantiaContrato.GarantiaId.Value, mcObj);
                }
                return garantiaContrato;
            }
            else
            {
                ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Debug, string.Format("No se utilizará el sistema de maker & checker para operaciones de guardado sobre el contrato ID: {0} - Identificador Garantia/Operacion: {1}.", garantiaContrato.Key, garantiaContrato.GarantiaId), new Dictionary<string, object>() { { "auditUserId", auditUserId }, { "useMakerAndChecker", useMakerAndChecker } });
                
                bool IsValid = GarantiaContratoRepository.GetByGarantiaId(garantiaContrato.GarantiaId.Value).Where(c=>c.DealReference==garantiaContrato.DealReference).Count() > 0 ? false : true;                    
                
                GarantiaContrato gContrato=new GarantiaContrato();

                if (IsValid)
                    gContrato = GarantiaContratoRepository.Add(garantiaContrato);
                else
                {
                    //#Ticket #1447: Se agrega update para garantia contrato luego del bug reporatdo por nitza
                    GarantiaContratoRepository.FindBy(garantiaContrato.ID);
                    gContrato = GarantiaContratoRepository.Add(garantiaContrato);
                    //ApplicationLogger.Instance.Info(string.Format("El contrato {0} ya se encuentra asociado a la garantia {2}. UserId {2}", garantiaContrato.DealReference, garantiaContrato.GarantiaId, auditUserId));
                }

                return gContrato;
            }
        }

        /// <summary>
        /// Gets the by garantia id.
        /// </summary>
        /// <param name="GarantiaId">The garantia id of type <see cref="System.Int32"/></param>
        /// <returns></returns>
        public IList<GarantiaContrato> GetByGarantiaId(int GarantiaId)
        {
            return GarantiaContratoRepository.GetByGarantiaId(GarantiaId);
        }

        /// <summary>
        /// Deletes the specified garantia contrato.
        /// </summary>
        /// <param name="garantiaContrato">The garantia contrato of type <see cref="Bladex.Garantias.DomainModel.DomainBase.GarantiaContrato"/></param>
        public void Delete(GarantiaContrato garantiaContrato, bool? useRepository, int? operationId)
        {
            if (useRepository.HasValue && useRepository.Value == true)
            {
                GarantiaContratoRepository.Remove(garantiaContrato);
            }
            else
            {
                if (operationId.HasValue)
                {
                    GarantiaCommonService<IGarantiaBaseRepository<GarantiaBase>, GarantiaBase> gService = new GarantiaCommonService<IGarantiaBaseRepository<GarantiaBase>, GarantiaBase>();
                    MakerCheckerObject<GarantiaBase> mcObj;
                    List<GarantiaContrato> tempContratos = new List<GarantiaContrato>();
                    
                    mcObj = gService.GetMakerCheckerObject(operationId.Value);
                    dynamic objColl = mcObj.AdditionalAttributes[MakerCheckerObjectGarantiaContratoKey];
                    List<GarantiaContrato> gContratoList = null;
                    
                    if (objColl is List<GarantiaContrato>)
                        gContratoList = objColl as List<GarantiaContrato>;
                    else
                        gContratoList = objColl.ToObject<List<GarantiaContrato>>();

                    int res = gContratoList.RemoveAll(o => o.DealReference == garantiaContrato.DealReference);
                    mcObj.AdditionalAttributes[MakerCheckerObjectGarantiaContratoKey] = gContratoList;
                    gService.SetMakerCheckerObject(operationId.Value, mcObj);
                }
                else
                {
                    throw new Exception("Opeartion ID not found");
                }
            
            }

        }

        public void DeleteMappingsWhereNotInJson(int garantiaId,IList<GarantiaContrato> JsonList) 
        {
            if (garantiaId > 0)
            {
                IList<GarantiaContrato> gContratoList = GetByGarantiaId(garantiaId);                
                
                if (gContratoList != null && gContratoList.Count > 0)
                {
                    foreach(GarantiaContrato item in gContratoList)
                    {
                        if (JsonList == null || JsonList.Count == 0)
                        {
                            GarantiaContratoRepository.Remove(item);
                        }
                        else
                        {
                            if (!JsonList.ToList().Exists(o => o.DealReference == item.DealReference))
                            {
                                GarantiaContratoRepository.Remove(item);
                            }
                        }
                    }
                }
            }
        }
                
        public List<GarantiaContrato> GetContractsFromMakerChekerObject(MakerCheckerObject<GarantiaBase> mcObj)
        {
            List<GarantiaContrato> contracts = new List<GarantiaContrato>();
            if (mcObj.AdditionalAttributes.ContainsKey(GarantiaContratoService.MakerCheckerObjectGarantiaContratoKey))
            {
                dynamic serializedColl = mcObj.AdditionalAttributes[GarantiaContratoService.MakerCheckerObjectGarantiaContratoKey];
                contracts = serializedColl.ToObject<List<GarantiaContrato>>();                
            }

            return contracts;
        }

        //Ticket #1847 - Garantias - Contratos intradiarios
        public void RefreshContratosIntradiarios()
        {
            GarantiaContratoRepository.RefreshContratosIntradiarios();
            
        }


        // Ticket #78343321 - Garantias - % de cobertura

        /// <summary>
        /// Determina si la asignacion Garantia - Contrato provisto posee el valor de cobertura establecido externamente
        /// </summary>
        /// <param name="GarantiaId"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public bool IsPorcUtilizationImported(int GarantiaId, string DealReference)
        {
            try
            {
                return GarantiaContratoRepository.IsPorcUtilizationImported(GarantiaId, DealReference);
            }
            catch (Exception ex)
            {
                ApplicationLogger.Instance.Error("Error obteniendo cobertura de seguro.", ex);
                return false;
            }
        }
    }
}