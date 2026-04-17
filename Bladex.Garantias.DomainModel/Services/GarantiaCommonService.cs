using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Transactions;
using Bladex.Garantias.DomainModel.Components.MakerChecker;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.DomainModel.Repositories.Components.MakerChecker;
using Bladex.Garantias.Infrastructure.Logging;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
//using Bladex.Garantias.DomainModel.Components.GarantiaBase;
using Bladex.Garantias.DomainModel.Components.CommonEnum;
using System.Linq.Expressions;
using Bladex.Garantias.DomainModel.Repositories.Components;

namespace Bladex.Garantias.DomainModel.Services
{
    /// <summary>
    /// Common Service for all <see cref="GarantiaBase"/> implementations.
    /// </summary>
    /// <typeparam name="TRepository">The type of the repository.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public class GarantiaCommonService<TRepository, TEntity>
        where TRepository : class, IGarantiaBaseRepository<TEntity>
        where TEntity : GarantiaBase 
    {
        private TRepository _repository = RepositoryFactory.GetRepository<TRepository, TEntity>();
        public const string MakerCheckerObjectGarantiaContratoKey = "GarantiaContratoList";
        private const string _INTERNAL_USER_ID = "novaris";

        protected readonly MakerCheckerCore<TEntity> MakerCheckerCore = new MakerCheckerCore<TEntity>(RepositoryFactory.GetRepository<IMakerCheckerUserRepository, MakerCheckerUser>(), RepositoryFactory.GetRepository<IMakerCheckerRoleRepository, MakerCheckerRole>(), RepositoryFactory.GetRepository<IMakerCheckerOperationStatusRepository, MakerCheckerOperationStatus>(), RepositoryFactory.GetRepository<IMakerCheckerChangesetRepository, MakerCheckerChangeset>(), RepositoryFactory.GetRepository<IMakerCheckerOperationRepository, MakerCheckerOperation>());
        protected readonly IMakerCheckerOperationRepository MakerCheckerOperationRepository = RepositoryFactory.GetRepository<IMakerCheckerOperationRepository, MakerCheckerOperation>();
        protected readonly IMakerCheckerChangesetRepository MakerCheckerChengesetRepository = RepositoryFactory.GetRepository<IMakerCheckerChangesetRepository, MakerCheckerChangeset>();
        protected readonly IGarantiaOtraRepository GarantiaOtraRepository = RepositoryFactory.GetRepository<IGarantiaOtraRepository, GarantiaOtra>();
        
        private ILogger _logger = Bladex.Garantias.Infrastructure.Logging.ApplicationLogger.Instance;

        /// <summary>
        /// Returns all <see cref="TEntity"/>.
        /// </summary>
        /// <returns>A <see cref="TEntity"/> IList</returns>
        public virtual IList<TEntity> GetAll(FechaVencimientoFilterEnum? optionInternalStatus)
        {
            var garantiasList = _repository.GetAll() as IList<TEntity>;

            if (optionInternalStatus == null || !optionInternalStatus.HasValue || optionInternalStatus.Value == FechaVencimientoFilterEnum.All)
            {
                IList<TEntity> lst = garantiasList.Where(o => o.InternalStatus != null && o.InternalStatus.GetKeyAs<string>() != InternalStatus.DELETED_ID).ToList();
                return lst;
            }
            else
            {
                if (optionInternalStatus.Value == FechaVencimientoFilterEnum.Expired)
                {
                    return garantiasList.Where(o => o.InternalStatus != null && o.InternalStatus.GetKeyAs<string>() == InternalStatus.EXPIRED_ID).ToList();
                }
                else
                {
                    if (optionInternalStatus.Value == FechaVencimientoFilterEnum.Active)
                    {
                        return garantiasList.Where(o => o.InternalStatus != null && o.InternalStatus.GetKeyAs<string>() == InternalStatus.ACTIVE_ID).ToList();
                    }
                    else
                    {
                        return garantiasList.Where(o => o.InternalStatus != null && o.InternalStatus.GetKeyAs<string>() != InternalStatus.DELETED_ID).ToList();
                    }
                }
            }
            //return garantiasList.Where(g => g.Status.Key != null ? g.Status.Key.ToString() != Status.DELETED_ID : true).ToList();
        }

        /*
        public virtual IList<TEntity> GetAllOtrasGarantiasNotExpired()
        {            
            return GarantiaOtraRepository.GetAllNotExpiredSQL() as IList<TEntity>;
            //return _repository.GetAll().Where(o => o.InternalStatus != null && o.InternalStatus.GetKeyAs<string>() != InternalStatus.DELETED_ID && o.InternalStatus.GetKeyAs<string>() != InternalStatus.EXPIRED_ID).ToList();
        }
        */
     

        /// <summary>
        /// Indica si la garantia posee alguna operacion pendiente.
        /// </summary>
        /// <param name="garantiaId">The garantia id of type <see cref="System.Int32"/></param>
        /// <returns></returns>
        public bool GetGarantiaBloqueada(int garantiaId)
        {
            //return this.MakerCheckerCore.CheckItemExistence(garantiaId);
            return this._repository.GetAll().ToList().Where(o => (int)o.Key == garantiaId && o.InternalStatus.Key == InternalStatus.BLOCKED_ID).Count() > 0 ? true : false;            
        }

        /// <summary>
        /// Gets the maker checker object.
        /// </summary>
        /// <param name="operationId">The operation id of type <see cref="System.Int32"/></param>
        /// <returns></returns>
        public virtual MakerCheckerObject<TEntity> GetMakerCheckerObject(int operationId)
        {
            return this.MakerCheckerCore.GetMakerCheckerObject<TEntity>(operationId);
        }

        public virtual void SetMakerCheckerObject(int operationId, MakerCheckerObject<TEntity> makerCheckerObject)
        {
            this.MakerCheckerCore.Update(operationId, makerCheckerObject);
        }

        /// <summary>
        /// Return one <see cref="TEntity"/> by Id
        /// </summary>
        /// <param name="garantiaId">Garantia Id</param>
        /// <returns>A <see cref="TEntity"/> entity.</returns>
         public virtual TEntity GetById(int garantiaId)
         {           
                return _repository.FindBy(garantiaId);   
         }

         public virtual TEntity GetById(int? operationId, int? garantiaId, bool useRepository, string userId)
         {
                          
            MakerCheckerOperation Operation;
            

            if (garantiaId != null && garantiaId > 0)
            {                
                return this._repository.FindBy(garantiaId);
            }
            else
            {
                if (operationId != null && operationId > 0)
                {
                    //#Ticket1320 : Reconexion sql
                    Operation = MakerCheckerOperationRepository.GetSqlOperationNotApprovedById(operationId.Value);
                   // Operation = MakerCheckerOperationRepository.GetAll().Where(o => o.OperationId == operationId && (o.OperationStatusId == (int)MakerCheckerOperationStatus.OperationStatus.New || o.OperationStatusId == (int)MakerCheckerOperationStatus.OperationStatus.Rejected)).FirstOrDefault();                    
                }
                else
                {
                    throw new Exception("Operation not found");
                }
            }

            if (Operation != null)
            {
                if (useRepository)
                {                        
                    return _repository.FindBy(Operation.ItemId);            
                }
                else
                {
                    //#Ticket1320 : Reconexion sql
                    //int? makerFlag = MakerCheckerChengesetRepository.GetAll().Where(o => o.MakerUserId == userId && o.ChangesetId == Operation.ChangesetId).Count();
                    int? makerFlag = MakerCheckerChengesetRepository.GetCountChangesetByUserAndId(userId, Operation.ChangesetId);

                    if (makerFlag > 0)
                    {
                        MakerCheckerObject<TEntity> makerCheckerObject = this.MakerCheckerCore.GetMakerCheckerObject<TEntity>(Operation.OperationId);
                        return makerCheckerObject.Object;
                    }
                    else
                    {
                        throw new Exception(string.Format("There are no operations related to the user '{0}'.", userId));
                    }
                }
            }
            else
            {
                throw new Exception(string.Format("Operation with Id {0} not found.", Operation.OperationId));
            }                
            
        }

        /// <summary>
        /// Metodo utilizado para cambiar <see cref="CategoriaSuper"/> de una <see cref="GarantiaBase"/>.
        /// </summary>
        /// <param name="garantiaId">ID de <see cref="GarantiaBase"/></param>
        /// <param name="oldType">ID original de <see cref="CategoriaSuper"/></param>
        /// <param name="newType">ID nuevo de <see cref="CategoriaSuper"/></param>
        /// <param name="auditUserId">Usuario solicitante del cambio.</param>
        /// <returns>Retorna un valor <see cref="Boolean"/> indicando si la operacion fue satisfactoria o no. <example>If True, the operation succeed. If not, the operation fail.</example></returns>
        public virtual bool ChangeType(int garantiaId, string oldType, string newType, string auditUserId)
        {
            this.checkPrivileges(auditUserId);
            bool result = false;
            using (TransactionScope scope = new TransactionScope())
            {
                var garantia = this.GetById(garantiaId);
                if (garantia == null)
                {
                    this._logger.Warn(string.Format("El usuario {0} ha solicitado un cambio de tipo de garantia a la garantia con identificador {1}. Tipo Original: {2}, Tipo Nuevo: {3}, pero la garantia no existe.", auditUserId, garantiaId, oldType, newType));
                }
                else
                {
                    this._logger.Info(string.Format("El usuario {0} ha solicitado un cambio de tipo de garantia a la garantia con identificador {1}. Tipo Original: {2}, Tipo Nuevo: {3}", auditUserId, garantiaId, oldType, newType));
                    // Verificacion de concurrencia.
                    if (garantia.CategoriaSuper.GetKeyAs<string>() == oldType && !string.IsNullOrEmpty(newType))
                    {
                        CategoriaSuperService svc = new CategoriaSuperService();
                        // Obtengo el nuevo tipo de garantia
                        var categoriaSuperNew = svc.GetById(newType);
                        if (categoriaSuperNew != null)
                        {
                            this._logger.Info(string.Format("Cambiando garantia con identificador {0} del tipo {1} al tipo {2}", garantia.GetKeyAs<int>(), garantia.CategoriaSuper.Nombre, categoriaSuperNew.Nombre));

                            // Cambio el tipo de la garantia base.
                            result = _repository.ChangeType(garantia, categoriaSuperNew);
                            if (result)
                            {
                                // Obtengo la nueva instancia de la garantia con su nuevo tipo
                                dynamic newGarantia = RepositoryFactory.GetRepository<IGarantiaBaseRepository, GarantiaBase>().FindBy(garantia.GetKeyAs<int>());
                                // Inserto un nuevo registro en la tabla de garantia del nuevo tipo.
                                this.WriteNewRecord(newGarantia);
                                this._logger.Info(string.Format("Cambio de garantia con identificador {0} al tipo {1} efectuado correctamente.", garantia.GetKeyAs<int>(), categoriaSuperNew.Nombre));

                            }
                            else
                                this._logger.Error(string.Format("El cambio de garantia con identificador {0} al tipo {1} no ha podido concretarse.", garantia.GetKeyAs<int>(), categoriaSuperNew.Nombre));
                        }
                    }
                }
                scope.Complete();
            }
            return result;
        }

        private void WriteNewRecord(GarantiaPrenda garantia)
        {
            RepositoryFactory.GetRepository<IGarantiaPrendaRepository, GarantiaPrenda>().WriteRecord(garantia);
            
        }

        private void WriteNewRecord(GarantiaOtra garantia)
        {
            RepositoryFactory.GetRepository<IGarantiaOtraRepository, GarantiaOtra>().WriteRecord(garantia);
        }

        private void WriteNewRecord(GarantiaInmueble garantia)
        {
            RepositoryFactory.GetRepository<IGarantiaInmuebleRepository, GarantiaInmueble>().WriteRecord(garantia);
        }

        private void WriteNewRecord(GarantiaMueble garantia)
        {
            RepositoryFactory.GetRepository<IGarantiaMuebleRepository, GarantiaMueble>().WriteRecord(garantia);
        }

        private void WriteNewRecord(GarantiaDeposito garantia)
        {
            RepositoryFactory.GetRepository<IGarantiaDepositoRepository, GarantiaDeposito>().WriteRecord(garantia);
        }

        private void WriteNewRecord(GarantiaDepositoOtroBanco garantia)
        {
            RepositoryFactory.GetRepository<IGarantiaDepositoOtroBancoRepository, GarantiaDepositoOtroBanco>().WriteRecord(garantia);
        }

        public virtual TEntity Commit(TEntity garantia, string auditUserId)
        {
            this._logger.Info(string.Format("Accediendo al sistema de maker & checker bajo el usuario {0}", auditUserId));
            this._logger.Info(string.Format("Efectuando operacion de commit solicitada por el usuario {0}.", auditUserId));
            return this.Save(garantia, auditUserId, false,null);
        }

        public virtual TEntity Save(TEntity garantia)
        {
            this._logger.Debug(string.Format("No se utilizará el sistema de maker & checker para operaciones efectuadas a la garantia ID: {0} - Identificador: {1}.", garantia.GetKeyAs<int>(), garantia.GetIdentificacionDocumentoGarantia()));
            return this.Save(garantia, _INTERNAL_USER_ID, false,null);
        }
               

        /// <summary>
        /// Aqui se guardan las garantias. Metodo indicado para la implementacion de logica de negocio adicional.
        /// </summary>
        /// <param name="garantia">Garantia a modificar.</param>
        /// <param name="auditUserId">Usuario solicitante de la operacion.</param>
        /// <returns></returns>
        public virtual TEntity Save(TEntity garantia, string auditUserId,int? operationId)
        {
            this._logger.Info(string.Format("Accediendo al sistema de maker & checker bajo el usuario {0}", auditUserId));
            return this.Save(garantia, auditUserId, true,operationId);
        }

        /// <summary>
        /// Checks the privileges.
        /// </summary>
        /// <param name="userId">The user id of type <see cref="System.String"/></param>
        private void checkPrivileges(string userId)
        {
            ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Debug, string.Format("Verificando privilegios sobre el usuario {0}.", userId), null, null);
            // Privileges verification
            User user = new UserService().GetById(userId);
            if (user == null || !user.IsPowerUser)
            {
                var ex = new System.Security.Authentication.InvalidCredentialException(string.Format("The user {0} is not valid or does not have write privileges.", userId));
                ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Error, ex.Message, null, ex);
                throw ex;
            }
        }

        private TEntity Save(TEntity garantia, string auditUserId, bool useMakerAndChecker,int? operationId)
        {
            GarantiaContratoService gcService = new GarantiaContratoService();

            this.checkPrivileges(auditUserId);
            TEntity result;
            var opId=0;
            MakerCheckerOperation operation=new MakerCheckerOperation();
            MakerCheckerObject<TEntity> mcObj=new MakerCheckerObject<TEntity>();
            GarantiaService garantiaSVC = new GarantiaService();
            List<GarantiaContrato> contratos=new List<GarantiaContrato>();                                                            
                                    
            if (useMakerAndChecker)
            {
                // Bloque de codigo transaccional
                using (TransactionScope scope = new TransactionScope())
                {

                    // Si la garantia es nueva, coloco el codigo de banco y el status.
                    if (garantia.Key == null)
                    {

                        //Si el operationId tiene valor es una edicion desde el changeset viewer se hace update
                        if (operationId.HasValue)
                        {
                            //#Ticket1320 : Reconexion sql
                            //if (this.MakerCheckerOperationRepository.GetAll().Where(o => o.OperationId == Convert.ToInt32(operationId.Value)).Count() > 0)
                            if (this.MakerCheckerOperationRepository.GetOperationsByItemId(operationId.Value).Count()>0)
                            {
                                //#Ticket1320 : Reconexion sql
                                //operation = this.MakerCheckerOperationRepository.GetAll().Where(o => o.OperationId == Convert.ToInt32(operationId.Value)).FirstOrDefault();
                                operation = this.MakerCheckerOperationRepository.FindBy(operationId.Value);
                                MakerCheckerObject<GarantiaBase> mcObjOriginal = AutoMapper.Mapper.Map(operation.GetMakerCheckerObject(), operation.GetMakerCheckerObject().GetType(), typeof(MakerCheckerObject<GarantiaBase>));
                                contratos = gcService.GetContractsFromMakerChekerObject(mcObjOriginal);
                                mcObj.AdditionalAttributes[MakerCheckerObjectGarantiaContratoKey] = contratos;
                            }

                            mcObj.Object = garantia;

                            string makerUserId = MakerCheckerChengesetRepository.FindBy(operation.ChangesetId).MakerUserId;

                            //Verifico si no esta bloqueada
                            if (garantia.InternalStatus != new InternalStatusService().GetBlockedStatus() || auditUserId == makerUserId)
                            {
                                // Si es rechazada creo un nuevo operation, sino hago update
                                if (operation.OperationStatusId == (int)MakerCheckerOperationStatus.OperationStatus.Rejected)
                                {
                                    if (contratos != null && contratos.Count > 0)
                                    {
                                        Dictionary<string, object> contratosDictionary = new Dictionary<string, object>();
                                        contratosDictionary.Add("GarantiaContratoList", contratos);
                                        operationId = MakerCheckerCore.Save(auditUserId, (int)MakerCheckerOperationStatus.OperationStatus.New, garantia, MakerAndCheckerActionEnum.Add, contratosDictionary);
                                    }
                                    else
                                    {
                                        operationId = MakerCheckerCore.Save(auditUserId, (int)MakerCheckerOperationStatus.OperationStatus.New, garantia, MakerAndCheckerActionEnum.Add);
                                    }

                                    if (garantia.Key != null)
                                        garantiaSVC.SetInternalStatus((int)garantia.Key, new InternalStatusService().GetBlockedStatus());
                                }
                                else
                                {

                                    MakerCheckerCore.Update(operationId.Value, mcObj);
                                }
                            }
                            else
                            {
                                throw new Exception("La garantia se encuentra bloqueada, no es posible realizar cambios");
                            }
                        }
                        else
                        {
                            //La primera vez que se genera la operacion pasa por aca
                            if (garantia.InternalStatus != new InternalStatusService().GetBlockedStatus())
                            {
                                garantia.InternalStatus = new InternalStatusService().GetActiveStatus();
                                garantia.Status = new StatusService().GetNormalStatus();
                                if (string.IsNullOrEmpty(garantia.CodigoBanco))
                                    garantia.CodigoBanco = "027";

                                if (garantia.Key == null)
                                {
                                    operationId = MakerCheckerCore.Save(auditUserId, (int)MakerCheckerOperationStatus.OperationStatus.New, garantia, MakerAndCheckerActionEnum.Add);
                                }
                                else if (garantia.Key != null)
                                {
                                    operationId = MakerCheckerCore.Save(auditUserId, (int)MakerCheckerOperationStatus.OperationStatus.New, garantia, MakerAndCheckerActionEnum.Edit);
                                    garantiaSVC.SetInternalStatus((int)garantia.Key, new InternalStatusService().GetBlockedStatus());
                                }
                            }
                            else
                            {
                                throw new Exception("La garantia se encuentra bloqueada, no es posible realizar cambios");
                            }
                        }
                    }
                    else
                    {
                        //Edicion de una garantia existente                    
                        if (operationId.HasValue)
                        {
                            //#Ticket1320 : Reconexion sql
                            //operation = this.MakerCheckerOperationRepository.GetAll().Where(o => o.OperationId == Convert.ToInt32(operationId.Value)).FirstOrDefault();
                            operation = this.MakerCheckerOperationRepository.FindBy(operationId.Value);
                            string makerUserId = MakerCheckerChengesetRepository.FindBy(operation.ChangesetId).MakerUserId;

                            MakerCheckerObject<GarantiaBase> mcObjOriginal = AutoMapper.Mapper.Map(operation.GetMakerCheckerObject(), operation.GetMakerCheckerObject().GetType(), typeof(MakerCheckerObject<GarantiaBase>));
                            contratos = gcService.GetContractsFromMakerChekerObject(mcObjOriginal);
                            mcObj.AdditionalAttributes[MakerCheckerObjectGarantiaContratoKey] = contratos;

                            if (garantia.InternalStatus != new InternalStatusService().GetBlockedStatus() || makerUserId == auditUserId)
                            {
                                //Si es rechazada creo un nuevo operation
                                if (operation.OperationStatusId == (int)MakerCheckerOperationStatus.OperationStatus.Rejected)
                                {
                                    if (garantia.Key == null)
                                    {
                                        operationId = MakerCheckerCore.Save(auditUserId, (int)MakerCheckerOperationStatus.OperationStatus.New, garantia, MakerAndCheckerActionEnum.Add);
                                    }
                                    else if (garantia.Key != null)
                                    {
                                        operationId = MakerCheckerCore.Save(auditUserId, (int)MakerCheckerOperationStatus.OperationStatus.New, garantia, MakerAndCheckerActionEnum.Edit);
                                        garantiaSVC.SetInternalStatus((int)garantia.Key, new InternalStatusService().GetBlockedStatus());
                                    }
                                }
                                else
                                {
                                    mcObj.Object = garantia;
                                    MakerCheckerCore.Update(operation.OperationId, mcObj);
                                }
                            }
                            else
                            {
                                throw new Exception("La garantia se encuentra bloqueada, no es posible realizar cambios");
                            }
                        }
                        else
                        {
                            // Si no encuentro el operation creo uno nuevo

                            if (garantia.Key != null)
                            {
                                contratos = gcService.GetByGarantiaId(Convert.ToInt32(garantia.Key)).ToList();
                                garantiaSVC.SetInternalStatus((int)garantia.Key, new InternalStatusService().GetBlockedStatus());

                                if (contratos != null && contratos.Count > 0)
                                {
                                    Dictionary<string, object> contratosDictionary = new Dictionary<string, object>();
                                    contratosDictionary.Add("GarantiaContratoList", contratos);

                                    operationId = MakerCheckerCore.Save(auditUserId, (int)MakerCheckerOperationStatus.OperationStatus.New, garantia, MakerAndCheckerActionEnum.Edit, contratosDictionary);
                                }
                                else
                                {
                                    operationId = MakerCheckerCore.Save(auditUserId, (int)MakerCheckerOperationStatus.OperationStatus.New, garantia, MakerAndCheckerActionEnum.Edit);
                                }
                            }
                            else
                            {
                                operationId = MakerCheckerCore.Save(auditUserId, (int)MakerCheckerOperationStatus.OperationStatus.New, garantia, MakerAndCheckerActionEnum.Add);
                            }
                        }


                    }

                    // Commit de la transaccion
                    scope.Complete();
                }
                

                //var operationId = MakerCheckerCore.Save(auditUserId, (int)MakerCheckerOperationStatus.OperationStatus.New, garantia, MakerAndCheckerActionEnum.Add);
                // We use the operation id of the changeset as key of the entity.
                garantia.Key = operationId;
                return garantia;
            }
            else
            {
                this._logger.Debug(string.Format("Guardando/Actualizando garantia con identificador ID: {0} - Identificador: {1}", garantia.GetKeyAs<int>(), garantia.GetIdentificacionDocumentoGarantia()));
                // Bloque de codigo transaccional
                using (TransactionScope scope = new TransactionScope())
                {
                    ActorService svc = new ActorService();
                    PaisService paisSvc = new PaisService();
                    ClienteService clienteSvc = new ClienteService();
                    List<Actor> actores = svc.GetAll().ToList();
                    Cliente garante;

                    if (garantia.Cliente.IsInternal)
                    {
                        clienteSvc.CreateCliente(garantia.Cliente);
                    }
                                
                    
                    if (garantia.Garante.IsInternal || garantia.Garante != null)
                    {
                        if (!string.IsNullOrEmpty(garantia.Garante.Nombre) && garantia.Garante.Key.ToString() == "-1")
                        {
                            garantia.Garante=createGarante(garantia.Garante);
                        }
                        else
                        {
                            garante = clienteSvc.GetById(garantia.Garante.Key.ToString());

                            if (garante == null)
                                garante = createGarante(garantia.Garante);
                            
                                garantia.Garante = garante;
                        }
                    }
                    else 
                    {
                        //si no es interno pregunto si no es nulo y si existe en el catalogo
                        
                        if (garantia.Garante != null)
                        {
                            garante=clienteSvc.GetById(garantia.Garante.Key.ToString());

                            //si no lo encuentro lo creo
                            if (garante == null)
                                garante=createGarante(garantia.Garante);

                            garantia.Garante = garante;
                        }
                    }

                    // Logica necesaria para dar de alta los nuevos actores.
                    if (garantia.Administrador.Key != null && actores.FirstOrDefault(o => o.Key.ToString() == garantia.Administrador.Key.ToString() || o.Nombre == garantia.Administrador.Key.ToString()) == null)
                    {
                        var newActor = svc.Save(new Actor() {Nombre = garantia.Administrador.Key.ToString(), Pais = paisSvc.GetById(garantia.Administrador.Pais.Key.ToString())});
                        garantia.Administrador = newActor;
                    }
                    else
                    {
                        //Si existe
                        if (garantia.Administrador!=null && garantia.Administrador.Key != null)
                        {
                            string AdministradorId = garantia.Administrador.Key.ToString();
                            garantia.Administrador = null;
                            garantia.Administrador = getActor(AdministradorId);
                        }
                    }

                    if (garantia.Asegurador.Key != null && actores.FirstOrDefault(o => o.Key.ToString() == garantia.Asegurador.Key.ToString() || o.Nombre == garantia.Asegurador.Key.ToString()) == null)
                    {
                        var newActor = svc.Save(new Actor() {Nombre = garantia.Asegurador.Key.ToString(), Pais = paisSvc.GetById(garantia.Asegurador.Pais.Key.ToString())});
                        garantia.Asegurador = newActor;
                    }
                    else
                    {
                        //Si existe
                        if (garantia.Asegurador!=null && garantia.Asegurador.Key != null)
                        {
                            string AseguradorId = garantia.Asegurador.Key.ToString();
                            garantia.Asegurador = null;
                            garantia.Asegurador = getActor(AseguradorId);
                        }
                    }

                    if (garantia.Depositante.Key != null && actores.FirstOrDefault(o => o.Key.ToString() == garantia.Depositante.Key.ToString() || o.Nombre == garantia.Depositante.Key.ToString()) == null)
                    {
                        var newActor = svc.Save(new Actor() {Nombre = garantia.Depositante.Key.ToString(), Pais = paisSvc.GetById(garantia.Depositante.Pais.Key.ToString())});
                        garantia.Depositante = newActor;
                    }
                    else
                    {
                        //Si existe
                        if (garantia.Depositante!= null && garantia.Depositante.Key != null)
                        {
                            string DepositanteId = garantia.Depositante.Key.ToString();
                            garantia.Depositante = null;
                            garantia.Depositante = getActor(DepositanteId);
                        }
                    }

                    if (garantia.Evaluador.Key != null && actores.FirstOrDefault(o => o.Key.ToString() == garantia.Evaluador.Key.ToString() || o.Nombre == garantia.Evaluador.Key.ToString()) == null)
                    {
                        var newActor = svc.Save(new Actor() { Nombre = garantia.Evaluador.Key.ToString(), Pais = paisSvc.GetById(garantia.Evaluador.Pais.Key.ToString()) });
                        garantia.Evaluador = newActor;
                    }
                    else
                    {
                        //Si existe
                        if (garantia.Evaluador !=null && garantia.Evaluador.Key != null)
                        {
                            string EvaluadorId = garantia.Evaluador.Key.ToString();
                            garantia.Evaluador = null;
                            garantia.Evaluador = getActor(EvaluadorId);
                        }          
                    }

                    //Si no existe
                    if (garantia.Revisor.Key != null && actores.FirstOrDefault(o => o.Key.ToString() == garantia.Revisor.Key.ToString() || o.Nombre == garantia.Revisor.Key.ToString()) == null)
                    {
                        var newActor = svc.Save(new Actor() { Nombre = garantia.Revisor.Key.ToString(), Pais = paisSvc.GetById(garantia.Revisor.Pais.Key.ToString()) });
                        garantia.Revisor = newActor;
                    }
                    else
                    { 
                        //Si existe
                        if (garantia.Revisor!=null && garantia.Revisor.Key != null)
                        {
                            string RevisorId = garantia.Revisor.Key.ToString();
                            garantia.Revisor = null;
                            garantia.Revisor = getActor(RevisorId);                                                      
                        }                        
                    }

                    if (garantia is GarantiaOtra)
                    {
                        // Si recibo un Emisor..
                        if ((garantia as GarantiaOtra).Emisor != null && (garantia as GarantiaOtra).Emisor.Key != null)
                        {
                            // Busco por Key o por Nombre.
                            // Si Existe
                            string emisorKey = (garantia as GarantiaOtra).Emisor.Key.ToString();
                            var actor = actores.FirstOrDefault(o => o.Key.ToString() == emisorKey || o.Nombre == emisorKey);
                            if (actor == null)
                            {
                                // Si no existe lo doy de alta.
                                actor = svc.Save(new Actor() { Nombre = (garantia as GarantiaOtra).Emisor.Key.ToString(), Pais = paisSvc.GetById((garantia as GarantiaOtra).Emisor.Pais.Key.ToString()) });
                            }
                            // Realizo la asignacion.
                            (garantia as GarantiaOtra).Emisor = actor;
                        }
                        else
                        {
                            (garantia as GarantiaOtra).Emisor = ActorService.GetEmpty();
                        }
                    }
                    if (garantia is GarantiaPrenda)
                    {

                        // Si recibo un Emisor..
                        if (( garantia as GarantiaPrenda ).Emisor != null && ( garantia as GarantiaPrenda ).Emisor.Key != null)
                        {
                            // Busco por Key o por Nombre.
                            // Si Existe
                            string emisorKey = ( garantia as GarantiaPrenda ).Emisor.Key.ToString();
                            var actor = actores.FirstOrDefault(o => o.Key.ToString() == emisorKey || o.Nombre == emisorKey);
                            if (actor == null)
                            {
                                // Si no existe lo doy de alta.
                                actor = svc.Save(new Actor() {Nombre = ( garantia as GarantiaPrenda ).Emisor.Key.ToString(), Pais = paisSvc.GetById(( garantia as GarantiaPrenda ).Emisor.Pais.Key.ToString())});
                            }
                            // Realizo la asignacion.
                            ( garantia as GarantiaPrenda ).Emisor = actor;
                        }
                    }

                    // Si la garantia es nueva, coloco el codigo de banco y el status.
                    if (garantia.Key == null)
                    {
                        garantia.InternalStatus = new InternalStatusService().GetActiveStatus();
                        garantia.Status = new StatusService().GetNormalStatus();
                        if (string.IsNullOrEmpty(garantia.CodigoBanco))
                            garantia.CodigoBanco = "027";
                    }
                    else
                    {
                        if (garantia.InternalStatus == null || garantia.InternalStatus.Key == null || string.IsNullOrEmpty((string) garantia.InternalStatus.Key))
                        {
                            garantia.InternalStatus = new InternalStatusService().GetActiveStatus();
                        }
                        if (garantia.Status == null || garantia.Status.Key == null || string.IsNullOrEmpty((string)garantia.Status.Key))
                        {
                            garantia.Status = new StatusService().GetNormalStatus();
                        }
                        if (string.IsNullOrEmpty(garantia.CodigoBanco))
                        {
                            garantia.CodigoBanco = "027";
                        }
                    }

                    // Si no viene del sincronizador de teinsa, la marco como interna.
                    if (!garantia.Source.HasValue)
                        garantia.Source = GarantiaSourceEnum.Interna;

                    // Salvo la garantia
                    result = _repository.Add(garantia);
                                        
                    AvalService avalSvc = new AvalService();


                        // Salvo los avales de la garantia
                        if (garantia is GarantiaOtra)
                        {
                            List<Aval> avales = (garantia as GarantiaOtra).Avales;
                            
                            if (avales == null)
                            {
                                avales = new List<Aval>();
                                (garantia as GarantiaOtra).Avales = avales;

                            }

                            List<Aval> oldAvales = avalSvc.GetByGarantiaId(result.GetKeyAs<int>()).ToList();

                            //Eliminar avales que no estan en la lista
                            avalSvc.removeAvalNotInList(result.GetKeyAs<int>(), avales);

                            foreach (var newAval in avales)
                            {
                                newAval.GarantiaId = result.GetKeyAs<int>();
                                avalSvc.Save(newAval);
                            }
                            
                        }
                    
                    // Commit de la transaccion
                    scope.Complete();
                    this._logger.Debug(string.Format("Guardado/Actualizacion de garantia con ID: {0} - Identificador: {1} efectuado correctamente.", result.GetKeyAs<int>(), garantia.GetIdentificacionDocumentoGarantia()));
                    // Limpio el cache de actores para que se vean reflejados los nuevos actores dados de alta.
                    svc.ClearCache();
                    return result;
                }
            }
        }

        /// <summary>
        /// Metodo utilizado para marcar como eliminada una garantia colocando <see cref="Status"/> como deleted.
        /// </summary>
        /// <param name="garantia"><see cref="GarantiaBase"/> a marcar como eliminada.</param>
        /// <param name="auditUserId">Nombre del usuario solicitante.</param>
        /// <param name="useMakerAndChecker"><see cref="Boolean"/> que indica si debe usarse el modulo de Maker & Checker o no.</param>
        public virtual void Delete(TEntity garantia, string auditUserId, bool useMakerAndChecker)
        {
            GarantiaService garantiaSVC = new GarantiaService();
            this.checkPrivileges(auditUserId);
            if (!useMakerAndChecker)
            {
                this._logger.Debug(string.Format("Eliminando garantia con identificador {0}", garantia.GetKeyAs<int>()));
                using (TransactionScope scope = new TransactionScope())
                {
                    var garantiaToUpdate = this.GetById(garantia.GetKeyAs<int>());

                    garantiaToUpdate.InternalStatus = new InternalStatusService().GetDeletedStatus();

                    _repository.Add(garantiaToUpdate);

                    scope.Complete();
                    
                    ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Information, string.Format("La Garantia con identificador {0} se ha marcado como eliminada.", garantiaToUpdate.GetKeyAs<int>()), new Dictionary<string, object>() { { "auditUserId", auditUserId }, { "useMakerAndChecker", useMakerAndChecker } }, null);
                }
            }
            else
            {
                this._logger.Debug(string.Format("Marcando como eliminada la garantia con identificador {0}", garantia.GetKeyAs<int>()));
                var gEntity = this.GetById(garantia.GetKeyAs<int>());
                if (gEntity != null)
                {
                    //Bloque la garantia en garantiaBase
                    if(gEntity.Key!=null)
                        garantiaSVC.SetInternalStatus((int)garantia.Key, new InternalStatusService().GetBlockedStatus());

                    //Se asigna status deleted para la persistencia en JSON
                    gEntity.InternalStatus = new InternalStatusService().GetDeletedStatus();
                    var operationId = MakerCheckerCore.Save(auditUserId, (int)MakerCheckerOperationStatus.OperationStatus.New, gEntity, MakerAndCheckerActionEnum.Delete);
                    ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Information, string.Format("Se genero una operacion de delete para la Garantia con identificador {0}. Id Operacion: {1}", gEntity.GetKeyAs<int>(), operationId), new Dictionary<string, object>() { { "auditUserId", auditUserId }, { "useMakerAndChecker", useMakerAndChecker } }, null);
                }
                else
                {
                    ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Error, string.Format("La garantía con identificador {0} no existe en el sistema.", garantia.GetKeyAs<int>()), new Dictionary<string, object>() {{ "auditUserId", auditUserId}, {"useMakerAndChecker", useMakerAndChecker }} , null);
                    throw new Exception(string.Format("La garantía con identificador {0} no existe en el sistema.", garantia.GetKeyAs<int>()));
                }
            }
        }

        /// <summary>
        /// Metodo utilizado para marcar como eliminada una garantia colocando <see cref="InternalStatus"/> como deleted.
        /// </summary>
        /// <param name="garantia"><see cref="GarantiaBase"/> a marcar como eliminada.</param>
        /// <param name="auditUserId">Nombre del usuario solicitante.</param>
        public virtual void Delete(TEntity garantia, string auditUserId)
        {
            this._logger.Info(string.Format("Accediendo al sistema de maker & checker bajo el usuario {0}", auditUserId));
            this.Delete(garantia, auditUserId, true);
        }

        /// <summary>
        /// Metodo utilizado para marcar como eliminada una garantia colocando <see cref="InternalStatus"/> como deleted.
        /// </summary>
        /// <param name="garantia"><see cref="GarantiaBase"/> a marcar como eliminada.</param>
        public virtual void Delete(TEntity garantia)
        {
            this._logger.Debug(string.Format("No se utilizará el sistema de maker & checker para operaciones efectuadas a la garantia {0}.", garantia.GetKeyAs<int>()));
            this.Delete(garantia, string.Empty, false);
            
        }

        /// <summary>
        /// Returns all <see cref="TEntity"/>.
        /// </summary>
        /// <returns>A <see cref="TEntity"/> IList</returns>
        public virtual IList<TEntity> GetAllAndDeleted()
        {
            return _repository.GetAll() as IList<TEntity>;
        }


        private Cliente createGarante(Cliente garante) 
        {
            try
            {                
                    ClienteService clienteSvc = new ClienteService();
                    string id = clienteSvc.CreateGarante(garante);
                    Cliente result = clienteSvc.GetById(id);

                    if (garante != null)
                        return result;
                    else
                        return null;           
            }
            catch (ApplicationException ex) 
            {
                ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Error, string.Format("Error al intentar crear un nuevo garante {0}.", garante.Nombre), null, ex);
                return null;
            }
        }

        private Actor getActor(string searchActor) 
        {
            ActorService svc = new ActorService();
            Actor objActor;
            try
            {
                //Busco por Id
                objActor = svc.GetById(searchActor);

                if (objActor == null)
                {
                    //Si no lo encuentro por Id lo busco por Name
                    objActor = svc.GetByName(searchActor);

                    if (objActor != null)
                        return objActor;
                    else
                        return null;
                }
                else
                {
                    return objActor;
                }
            }
            catch(ApplicationException ex)
            {
                ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Error, string.Format("Error al intentar obtener el actor con identificador {0}.", searchActor), null, ex);
                return null;
            }

        }


        public virtual void ValidateCatalogs(TEntity garantia)
        {
            bool res;
            FiduciariasService fiduciariaSvc = new FiduciariasService();
            TipoGarantiaSuperService tipoGarantiaSuperSvc = new TipoGarantiaSuperService();
            TipoGarantiaBladexService tipoGarantiaBladexSvc = new TipoGarantiaBladexService();
            CategoriaRiesgoGarantiaService categoriaRiesgoGarantiaSvc = new CategoriaRiesgoGarantiaService();
            FrecuenciasService frecuenciaSvc = new FrecuenciasService();
            CalificacionesRiesgoService calificacionRiesgoSvc = new CalificacionesRiesgoService();
            InstrumentoFinancieroService instrumentoFinancieroSvc = new InstrumentoFinancieroService();
            

            Fiduciarias fiduciaria=new Fiduciarias();
            TipoGarantiaSuper tipoGarantiaSuper = new TipoGarantiaSuper();
            TipoGarantiaBladex tipoGarantiaBladex = new TipoGarantiaBladex();
            CategoriaRiesgoGarantia categoriaRiesgoGarantia = new CategoriaRiesgoGarantia();
            Frecuencias frecuenciaRevision = new Frecuencias();
            CalificacionesRiesgo calificacionRiesgo=new CalificacionesRiesgo();
            InstrumentoFinanciero instrumentoFinanciero = new InstrumentoFinanciero();

            #region Validacion Fiduciaria Super
            if (garantia.FiduciariaSuper!=null && (garantia.FiduciariaSuper.Key!=null || garantia.FiduciariaSuper.Nombre!=null))
            {
                if (garantia.FiduciariaSuper.Key != null)
                {
                    fiduciaria = fiduciariaSvc.GetById(garantia.FiduciariaSuper.Key.ToString());

                    if (fiduciaria != null)
                    {
                        garantia.FiduciariaSuper = fiduciaria;
                        res = true;
                    }
                    else
                    {
                        fiduciaria = fiduciariaSvc.GetByName(garantia.FiduciariaSuper.Nombre);

                        if (fiduciaria != null)
                        {
                            garantia.FiduciariaSuper = fiduciaria;
                            res = true;
                        }
                        else
                        {
                            res = false;
                            throw new Exception("Error al validar el catalogo de fiduciaria Super");
                        }
                    }
                }
                else
                {
                    fiduciaria = fiduciariaSvc.GetByName(garantia.FiduciariaSuper.Nombre);

                    if (fiduciaria != null)
                    {
                        garantia.FiduciariaSuper = fiduciaria;
                        res = true;
                    }
                    else
                    {
                        res = false;
                        throw new Exception("Error al validar el catalogo de fiduciaria Super");
                    }
                }
            }

            #endregion

            #region Validacion Tipo Garantia Super
            //Validacion Tipo Garantia Super
            if (garantia.TipoGarantiaSuper != null && (garantia.TipoGarantiaSuper.Key != null || garantia.TipoGarantiaSuper.Nombre != null))
            {
                if (garantia.TipoGarantiaSuper.Key != null)
                {
                    tipoGarantiaSuper = tipoGarantiaSuperSvc.GetById(garantia.TipoGarantiaSuper.Key.ToString());

                    if (tipoGarantiaSuper != null)
                    {
                        res = true;
                        garantia.TipoGarantiaSuper = tipoGarantiaSuper;
                    }
                    else
                    {
                        if (garantia.TipoGarantiaSuper.Nombre != null)
                        {
                            tipoGarantiaSuper = tipoGarantiaSuperSvc.GetByName(garantia.TipoGarantiaSuper.Nombre);

                            if (tipoGarantiaSuper != null)
                            {
                                res = true;
                                garantia.TipoGarantiaSuper = tipoGarantiaSuper;
                            }
                            else
                            {
                                res = false;
                                throw new Exception("Error al validar el catalogo de tipo garantia Super");
                            }
                        }
                    }
                }
                else
                {
                    if (garantia.TipoGarantiaSuper.Nombre != null)
                    {
                        tipoGarantiaSuper = tipoGarantiaSuperSvc.GetByName(garantia.TipoGarantiaSuper.Nombre);

                        if (tipoGarantiaSuper != null)
                        {
                            res = true;
                            garantia.TipoGarantiaSuper = tipoGarantiaSuper;
                        }
                        else
                        {
                            res = false;
                            throw new Exception("Error al validar el catalogo de tipo garantia Super");
                        }
                    }
                }
            }

            #endregion
            
            #region Tipo Garantia Bladex

            if (garantia.TipoGarantiaBladex != null && (garantia.TipoGarantiaBladex.Key != null || garantia.TipoGarantiaBladex.Nombre != null))
            {
                if (tipoGarantiaBladex.Key != null)
                {
                    tipoGarantiaBladex = tipoGarantiaBladexSvc.GetById(garantia.TipoGarantiaBladex.Key.ToString());

                    if (tipoGarantiaBladex != null)
                    {
                        res = true;
                        garantia.TipoGarantiaBladex = tipoGarantiaBladex;
                    }
                    else
                    {
                        if (garantia.TipoGarantiaBladex.Nombre != null)
                        {
                            tipoGarantiaBladex = tipoGarantiaBladexSvc.GetByName(garantia.TipoGarantiaBladex.Nombre);

                            if (tipoGarantiaBladex != null)
                            {
                                res = true;
                                garantia.TipoGarantiaBladex = tipoGarantiaBladex;
                            }
                            else
                            {
                                res = false;
                                throw new Exception("Error al validar el catalogo tipo garantia bladex");
                            }
                        }
                    }
                }
                else
                {
                    if (garantia.TipoGarantiaBladex.Nombre != null)
                    {
                        tipoGarantiaBladex = tipoGarantiaBladexSvc.GetByName(garantia.TipoGarantiaBladex.Nombre);

                        if (tipoGarantiaBladex != null)
                        {
                            res = true;
                            garantia.TipoGarantiaBladex = tipoGarantiaBladex;
                        }
                        else
                        {
                            res = false;
                            throw new Exception("Error al validar el catalogo tipo garantia bladex");
                        }
                    }
                }
            }

            #endregion

            #region Categoria Riesgo Garantia
            
            if (garantia.CategoriaRiesgoGarantia != null && (garantia.CategoriaRiesgoGarantia.Key != null || garantia.CategoriaRiesgoGarantia.Nombre != null))
            {
                if (garantia.CategoriaRiesgoGarantia.Key != null)
                {
                    categoriaRiesgoGarantia = categoriaRiesgoGarantiaSvc.GetById(garantia.CategoriaRiesgoGarantia.Key.ToString());

                    if (categoriaRiesgoGarantia != null)
                    {
                        res = true;
                        garantia.CategoriaRiesgoGarantia = categoriaRiesgoGarantia;
                    }
                    else
                    {
                        if (garantia.CategoriaRiesgoGarantia.Nombre != null)
                        {
                            categoriaRiesgoGarantia = categoriaRiesgoGarantiaSvc.GetByName(garantia.CategoriaRiesgoGarantia.Nombre);

                            if (categoriaRiesgoGarantia != null)
                            {
                                res = true;
                                garantia.CategoriaRiesgoGarantia = categoriaRiesgoGarantia;
                            }
                            else
                            {
                                res = false;
                                throw new Exception("Error al validar el catalogo Categoria Riesgo Garantia");
                            }
                        }
                    }
                }
                else
                {
                    if (garantia.CategoriaRiesgoGarantia.Nombre != null)
                    {
                        categoriaRiesgoGarantia = categoriaRiesgoGarantiaSvc.GetByName(garantia.CategoriaRiesgoGarantia.Nombre);

                        if (categoriaRiesgoGarantia != null)
                        {
                            res = true;
                            garantia.CategoriaRiesgoGarantia = categoriaRiesgoGarantia;
                        }
                        else
                        {
                            res = false;
                            throw new Exception("Error al validar el catalogo Categoria Riesgo Garantia");
                        }
                    }
                }
            }

            #endregion

            #region Frecuencia Revision

            if (garantia.FrecuenciaRevision != null && (garantia.FrecuenciaRevision.Key != null && garantia.FrecuenciaRevision.Nombre != null))
            {
                if (garantia.FrecuenciaRevision.Key != null)
                {
                    frecuenciaRevision = frecuenciaSvc.GetById(garantia.FrecuenciaRevision.Key.ToString());

                    if (frecuenciaRevision != null)
                    {
                        res = true;
                        garantia.FrecuenciaRevision = frecuenciaRevision;
                    }
                    else
                    {
                        if (garantia.FrecuenciaRevision.Nombre != null)
                        {
                            frecuenciaRevision = frecuenciaSvc.GetByName(garantia.FrecuenciaRevision.Nombre);

                            if (frecuenciaSvc != null)
                            {
                                res = true;
                                garantia.FrecuenciaRevision = frecuenciaRevision;
                            }
                            else
                            {
                                res = false;
                                throw new Exception("Error al validar el catalogo de frecuencia Revision");
                            }
                        }
                    }

                }
                else
                {
                    if (garantia.FrecuenciaRevision.Nombre != null)
                    {
                        frecuenciaRevision = frecuenciaSvc.GetByName(garantia.FrecuenciaRevision.Nombre);

                        if (frecuenciaSvc != null)
                        {
                            res = true;
                            garantia.FrecuenciaRevision = frecuenciaRevision;
                        }
                        else
                        {
                            res = false;
                            throw new Exception("Error al validar el catalogo de frecuencia Revision");
                        }
                    }
                }
            }

            #endregion

            #region Rating Garante

            if (garantia.RatingGarante != null && (garantia.RatingGarante.Key != null || garantia.RatingGarante.Moodys != null))
            {
                if (garantia.RatingGarante.Key != null)
                {
                    calificacionRiesgo = calificacionRiesgoSvc.GetById(garantia.RatingGarante.Key.ToString());

                    if (calificacionRiesgo != null)
                    {
                        res = true;
                        garantia.RatingGarante = calificacionRiesgo;
                    }
                    else
                    {
                        if (garantia.RatingGarante.Moodys != null)
                        {
                            calificacionRiesgo = calificacionRiesgoSvc.GetByMoodys(garantia.RatingGarante.Moodys);

                            if (calificacionRiesgo != null)
                            {
                                res = true;
                                garantia.RatingGarante = calificacionRiesgo;
                            }
                            else
                            {
                                res = false;
                                throw new Exception("Error al intentar validar el catalogo de rating garante");
                            }
                        }
                    }
                }
                else
                {
                    if (garantia.RatingGarante.Moodys != null)
                    {
                        calificacionRiesgo = calificacionRiesgoSvc.GetByMoodys(garantia.RatingGarante.Moodys);

                        if (calificacionRiesgo != null)
                        {
                            res = true;
                            garantia.RatingGarante = calificacionRiesgo;
                        }
                        else
                        {
                            res = false;
                            throw new Exception("Error al intentar validar el catalogo de rating garante");
                        }
                    }
                }
            }

            #endregion

            if (garantia.CategoriaSuper.Key.ToString() == CategoriaSuper.PRENDARIA_ID)
            {
                //Si es prendaria se deben validar los siguientes campos

                #region Tipo Instrumento Financiero

                if ((garantia as GarantiaPrenda).TipoInstrumentoFinanciero != null && (garantia as GarantiaPrenda).TipoInstrumentoFinanciero.Key != null || (garantia as GarantiaPrenda).TipoInstrumentoFinanciero.Nombre != null)
                {
                    if ((garantia as GarantiaPrenda).TipoInstrumentoFinanciero.Key != null)
                    {
                        instrumentoFinanciero = instrumentoFinancieroSvc.GetById((garantia as GarantiaPrenda).TipoInstrumentoFinanciero.Key.ToString());

                        if (instrumentoFinanciero != null)
                        {
                            res = true;
                            (garantia as GarantiaPrenda).TipoInstrumentoFinanciero = instrumentoFinanciero;
                        }
                        else
                        {
                            if ((garantia as GarantiaPrenda).TipoInstrumentoFinanciero.Nombre != null)
                            {
                                instrumentoFinanciero = instrumentoFinancieroSvc.GetByNombre((garantia as GarantiaPrenda).TipoInstrumentoFinanciero.Nombre);

                                if (instrumentoFinanciero != null)
                                {
                                    res = true;
                                    (garantia as GarantiaPrenda).TipoInstrumentoFinanciero = instrumentoFinanciero;
                                }
                                else
                                {
                                    throw new Exception("Error al validar el catalogo instrumento financiero");
                                }
                            }
                        }
                    }
                }
                else
                {
                    if ((garantia as GarantiaPrenda).TipoInstrumentoFinanciero.Nombre != null)
                    {
                        instrumentoFinanciero = instrumentoFinancieroSvc.GetByNombre((garantia as GarantiaPrenda).TipoInstrumentoFinanciero.Nombre);

                        if (instrumentoFinanciero != null)
                        {
                            res = true;
                            (garantia as GarantiaPrenda).TipoInstrumentoFinanciero = instrumentoFinanciero;
                        }
                        else
                        {
                            throw new Exception("Error al validar el catalogo instrumento financiero");
                        }
                    }
                }

                #endregion

                #region Calificacion Riesgo Emision

                if ((garantia as GarantiaPrenda).CalificacionEmision != null && ((garantia as GarantiaPrenda).CalificacionEmision.Key != null || (garantia as GarantiaPrenda).CalificacionEmision.Moodys!=null))
                {
                    if ((garantia as GarantiaPrenda).CalificacionEmision.Key != null)
                    {
                        calificacionRiesgo = null;
                        calificacionRiesgo = calificacionRiesgoSvc.GetById((garantia as GarantiaPrenda).CalificacionEmision.Key.ToString());

                        if (calificacionRiesgo != null)
                        {
                            res = true;
                            (garantia as GarantiaPrenda).CalificacionEmision = calificacionRiesgo;
                        }
                        else
                        {
                            calificacionRiesgo = calificacionRiesgoSvc.GetByMoodys((garantia as GarantiaPrenda).CalificacionEmision.Moodys);

                            if (calificacionRiesgo != null)
                            {
                                res = true;
                                (garantia as GarantiaPrenda).CalificacionEmision = calificacionRiesgo;
                            }
                            else
                            {
                                res = false;
                                throw new Exception("Error al validar el catalogo calificacion riesgo emision");
                            }
                        }
                    }
                    else
                    {
                        if ((garantia as GarantiaPrenda).CalificacionEmision.Moodys != null)
                        {
                            calificacionRiesgo = calificacionRiesgoSvc.GetByMoodys((garantia as GarantiaPrenda).CalificacionEmision.Moodys);

                            if (calificacionRiesgo != null)
                            {
                                res = true;
                                (garantia as GarantiaPrenda).CalificacionEmision = calificacionRiesgo;
                            }
                            else
                            {
                                res = false;
                                throw new Exception("Error al validar el catalogo calificacion riesgo emision");
                            }
                        }
                    }
                }

                #endregion

                #region Calificacion Riesgo Emisor

                if ((garantia as GarantiaPrenda).CalificacionEmisor != null && ((garantia as GarantiaPrenda).CalificacionEmisor.Key != null || (garantia as GarantiaPrenda).CalificacionEmisor.Moodys != null))
                {
                    if ((garantia as GarantiaPrenda).CalificacionEmisor.Key != null)
                    {
                        calificacionRiesgo = null;
                        calificacionRiesgo = calificacionRiesgoSvc.GetById((garantia as GarantiaPrenda).CalificacionEmisor.Key.ToString());

                        if (calificacionRiesgo != null)
                        {
                            res = true;
                            (garantia as GarantiaPrenda).CalificacionEmisor = calificacionRiesgo;
                        }
                        else
                        {
                            calificacionRiesgo = calificacionRiesgoSvc.GetByMoodys((garantia as GarantiaPrenda).CalificacionEmisor.Moodys);

                            if (calificacionRiesgo != null)
                            {
                                res = true;
                                (garantia as GarantiaPrenda).CalificacionEmisor = calificacionRiesgo;
                            }
                            else
                            {
                                res = false;
                                throw new Exception("Error al validar el catalogo calificacion riesgo emisor");
                            }
                        }
                    }
                    else
                    {
                        if ((garantia as GarantiaPrenda).CalificacionEmisor.Moodys != null)
                        {
                            calificacionRiesgo = calificacionRiesgoSvc.GetByMoodys((garantia as GarantiaPrenda).CalificacionEmisor.Moodys);

                            if (calificacionRiesgo != null)
                            {
                                res = true;
                                (garantia as GarantiaPrenda).CalificacionEmisor = calificacionRiesgo;
                            }
                            else
                            {
                                res = false;
                                throw new Exception("Error al validar el catalogo calificacion riesgo emisor");
                            }
                        }
                    }
                }
                #endregion
            }                

            //return garantia;
        } 
      
    }
}
