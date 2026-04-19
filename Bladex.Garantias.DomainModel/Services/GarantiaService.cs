using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.Infrastructure.Caching;
using Bladex.Garantias.Infrastructure.Logging;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using Bladex.Garantias.DomainModel.Components.MakerChecker;
using Bladex.Garantias.DomainModel.Repositories.Components.MakerChecker;
using Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker;

namespace Bladex.Garantias.DomainModel.Services
{
    /// <summary>
    /// Garantia Service
    /// </summary>
    public class GarantiaService : ICacheableService
    {
        IGarantiaBaseRepository repository = RepositoryFactory.GetRepository<IGarantiaBaseRepository, GarantiaBase>();
        ICategoriaSuperRepository repositoryOfCategoriaSuper = RepositoryFactory.GetRepository<ICategoriaSuperRepository, CategoriaSuper>();
        IMakerCheckerOperationRepository MakerCheckerOperationRepository = RepositoryFactory.GetRepository<IMakerCheckerOperationRepository, MakerCheckerOperation>();

        /// <summary>
        /// Gets the tipos de garantias.
        /// </summary>
        /// <returns></returns>
        public IList<CategoriaSuper> GetTiposDeGarantias()
        {
            return repositoryOfCategoriaSuper.GetAll();
            
        }

        /// <summary>
        /// Gets all garantias.
        /// </summary>
        /// <returns></returns>
        public IList<GarantiaBase> GetAllGarantias()
        {
            return repository.GetAll();            
        }

        /// <summary>
        /// Gets all garantias deleted.
        /// </summary>
        /// <returns></returns>
        public IList<GarantiaBase> GetAllGarantiasDeleted()
        {
            return repository.GetAllDeleted();
        }

        /// <summary>
        /// Gets all garantias unknown.
        /// </summary>
        /// <returns></returns>
        public List<GarantiaBaseRow> GetAllGarantiasUnknown(string SearchText)
        {
            return repository.GetAllUnknown(SearchText);
        }

        /// <summary>
        /// Disable garantias.
        /// </summary>
        /// <returns></returns>
        public string DisableGuaranteeType(string UserId, string TipoGarantiaSuperId)
        {
            return repository.DisableGuaranteeType(UserId, TipoGarantiaSuperId);
        }

        /// <summary>
        /// Disable garantias.
        /// </summary>
        /// <returns></returns>
        public string GetOriginalInternalStatus(string GarantiaId)
        {
            return repository.GetOriginalInternalStatus(GarantiaId);
        }

        /*
        public bool ValidationUnknownStatus() 
        {
            return true;
        }
         */
        
        /// <summary>
        /// Gets the garantia by id.
        /// </summary>
        /// <param name="garantiaId">The garantia id.</param>
        /// <returns></returns>
        public GarantiaBase GetGarantiaById(object garantiaId)
        {
            return this.FindById(garantiaId,null);
        }

        /// <summary>
        /// Deletes the garantia.
        /// </summary>
        /// <param name="garantiaId">The garantia id.</param>
        public void DeleteGarantia(object garantiaId)
        {
            repository.Remove(repository.FindBy(garantiaId));
        }

        /// <summary>
        /// Suma de todos las coberturas que tiene relacionadas de los prestamos.
        /// </summary>
        /// <param name="garantiaId">ID de Garantia</param>
        /// <returns>Suma de ValorGarantia de cada contrato asociado a la garantia.</returns>
        public Decimal GetUdfGarantiasValorGarantia(int garantiaId)
        {
            return repository.GetUdfGarantiasValorGarantia(garantiaId);
        }

        /// <summary>
        /// Indica si es valido desbloquear una garantia
        /// </summary>
        /// <param name="garantiaId">ID de Garantia</param>
        /// <returns>Devuelve true o false para indicar si es valido desbloquear una garantia</returns>
        public Boolean GetUdfValidationUnblockGuarantee(int garantiaId)
        {
            return repository.GetUdfValidationUnblockGuarantee(garantiaId);
        }
        /// <summary>
        /// Decide si la garantia se encuentra sobre utilizada o no.
        /// </summary>
        /// <param name="garantiaId">The garantia id of type <see cref="System.Int32"/></param>
        /// <returns><example>true</example> si la garantia se encuentra sobre utilizada. <example>false</example> si no.</returns>
        public bool GarantiaSobreUtilizada(int garantiaId)
        {
            try
            {
                // obtengo la garantia
                var garantia = this.FindById(garantiaId,null);
                // si no existe arrojo una excepcion
                if (garantia == null)
                {
                    ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Warning, string.Format("No se puede consultar la sobre utilizacion de la garantia con identificador {0} ya que no existe.", garantiaId), new Dictionary<string, object>() { { "garantiaId", garantiaId } }, null);
                    throw new Exception(string.Format("No se puede consultar la sobre utilizacion de la garantia con identificador {0} ya que no existe.", garantiaId));

                }
                // obtengo el valor necesario
                var valorNecesario = this.GetUdfGarantiasValorGarantia(garantiaId);
                // obtengo el valor de la super intendencia
                var valorSuperIntendencia = garantia.GetValorGarantiaSuperIntendencia();
                // si el valor de la super es menor que el valor necesario la garantia se encuentra sobre utilizada.
                return valorSuperIntendencia < valorNecesario;
            }
            catch (Exception ex)
            {
                ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Error, string.Format("Ha ocurrido un error consultando la sobre utilizacion de la garantia con identificador {0}.", garantiaId), new Dictionary<string, object>() { { "garantiaId", garantiaId } }, ex);
                return false;
            }
        }

        public GarantiaBase GetByFccRef(string FccRef)
        {
            return repository.GetByFccRef(FccRef);
        }

        /// <summary>
        ///Trae los TeinsaIds(FCCReference) de Garantia Base 
        /// </summary>
        /// <returns><see cref="Dictionary{String,Int}"/> with GarantiaBase.FCCReference as key and GarantiaBase.ID as value.</returns>
        public IDictionary<String, int> GetFccReferences()
        {
            return repository.GetFccReferences();
        }

        /// <summary>
        /// Finds the by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public GarantiaBase FindById(object id,bool? useRepository)
        {
            if (useRepository.HasValue && useRepository.Value == false)
            {
                //var operation = MakerCheckerOperationRepository.GetAll().Where(o => o.OperationId == Convert.ToInt32(id)).FirstOrDefault();
                
                //Ticket #1447 : Se quita el getAll para mejorar la performance
                var operation = MakerCheckerOperationRepository.GetSQLOperationsByFilter(id.ToString(), "OperationId").FirstOrDefault();
                MakerCheckerObject<GarantiaBase> obj = AutoMapper.Mapper.Map(operation.GetMakerCheckerObject(), operation.GetMakerCheckerObject().GetType(), typeof(MakerCheckerObject<GarantiaBase>));

                return obj.Object;
            }
            else
            {
                // We perform the lookup of the Categoria Super to know the specialization
                // If we use only repository.FindBy(id); we receive the correct specialization but its wrongly populated.
                CategoriaSuper catSuper = repositoryOfCategoriaSuper.FindByGarantiaId((int)id);
                Type garantiaType = CategoriaSuperResolver.Resolve(catSuper);

                if (garantiaType == typeof(GarantiaInmueble))
                {
                    return RepositoryFactory.GetRepository<IGarantiaInmuebleRepository, GarantiaInmueble>().FindBy(id);
                }
                else if (garantiaType == typeof(GarantiaMueble))
                {
                    return RepositoryFactory.GetRepository<IGarantiaMuebleRepository, GarantiaMueble>().FindBy(id);
                }
                else if (garantiaType == typeof(GarantiaDeposito))
                {
                    return RepositoryFactory.GetRepository<IGarantiaDepositoRepository, GarantiaDeposito>().FindBy(id);
                }
                else if (garantiaType == typeof(GarantiaDepositoOtroBanco))
                {
                    return RepositoryFactory.GetRepository<IGarantiaDepositoOtroBancoRepository, GarantiaDepositoOtroBanco>().FindBy(id);
                }
                else if (garantiaType == typeof(GarantiaOtra))
                {
                    return RepositoryFactory.GetRepository<IGarantiaOtraRepository, GarantiaOtra>().FindBy(id);
                }
                else if (garantiaType == typeof(GarantiaPrenda))
                {
                    return RepositoryFactory.GetRepository<IGarantiaPrendaRepository, GarantiaPrenda>().FindBy(id);
                }
                return repository.FindBy(id);
            }

        }

        /// <summary>
        /// Sets the status.
        /// </summary>
        /// <param name="garantiaId">The garantia id.</param>
        /// <param name="status">The status.</param>
        public void SetInternalStatus(object garantiaId, InternalStatus status)
        {            
            try
            {
                if (status == null) throw new ArgumentNullException("status", "The status can not be null.");
                GarantiaBase garantia = repository.FindBy(garantiaId);
                // Ticket #1624: workaround para que nose pierda el valor del campo, ya que se calcula en base a otro de otra tabla.
                GarantiaOtraService serv = new GarantiaOtraService();
                string strIdDocGarantia = serv.GetIdentificacionDocumentoGarantia((int)garantiaId);
                
                if (garantia != null)
                {
                    //Si el status que se quiere setear es activo y el status de la garantia es bloqueado, llamo a la udf de validacion
                    if (status == new InternalStatusService().GetActiveStatus() && garantia.InternalStatus == new InternalStatusService().GetBlockedStatus())
                    {
                        ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Information, string.Format("Seteando internal status de la garantia {0}, valor status={1} original satus={2}. valor udf={3}.", garantiaId, status.Nombre, garantia.InternalStatus.Nombre, this.GetUdfValidationUnblockGuarantee((int)garantiaId)), new Dictionary<string, object>() { { "garantiaId", garantiaId } }, null);
                        if(this.GetUdfValidationUnblockGuarantee((int)garantiaId))
                            garantia.InternalStatus= new InternalStatusService().GetActiveStatus();
                        else
                            garantia.InternalStatus= new InternalStatusService().GetBlockedStatus();
                    }
                    else
                    {
                        garantia.InternalStatus = status;
                    }

                    repository.Add(garantia);
                    // Ticket #1624: workaround para que nose pierda el valor del campo, ya que se calcula en base a otro de otra tabla.
                    serv.UpdateIdentificacionDocumentoGarantia((int)garantiaId, strIdDocGarantia);
                }
            }
            catch(ApplicationException ex)
            {
                ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Error, string.Format("Error al intentar modificar el internal status de la garantia {0}. Status {1}.", garantiaId,status.Nombre), new Dictionary<string, object>() { { "garantiaId", garantiaId } },ex);
            }
        }

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <param name="garantiaId">The garantia id.</param>        
        public InternalStatus GetInternalStatus(object garantiaId)
        {
            InternalStatus status=new InternalStatus();
            GarantiaBase garantia = repository.FindBy(garantiaId);

            if (garantia != null)
            {
                status= garantia.InternalStatus;                
            }

            return status;
        }

        public List<GarantiaBaseRow> SearchGarantias(string SearchText)
        {
            return repository.SearchGarantias(SearchText);
        }


        #region ICacheableService Members

        public string GetCacheKey()
        {
            throw new NotImplementedException();
        }

        public TimeSpan GetTimeSpan()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
