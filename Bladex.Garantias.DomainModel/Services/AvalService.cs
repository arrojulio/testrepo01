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
using IAvalRepository = Bladex.Garantias.DomainModel.Repositories.IAvalRepository;
using Bladex.Garantias.DomainModel.Repositories.Components.MakerChecker;
using Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker;

namespace Bladex.Garantias.DomainModel.Services
{
    public class AvalService
    {
        /// <summary>
        /// Returns all avales.
        /// </summary>
        /// <returns>A <see cref="Bancos"/> IList</returns>
        public IList<Aval> GetAll()
        {
            IAvalRepository repository = RepositoryFactory.GetRepository<IAvalRepository, Aval>();
            return repository.GetAll();
        }

        /// <summary>
        /// Return one Aval by Id
        /// </summary>
        /// <param name="avalId">Aval Id</param>
        /// <returns>A <see cref="Aval"/> entity.</returns>
        public Aval GetById(int avalId)
        {
            IAvalRepository repository = RepositoryFactory.GetRepository<IAvalRepository, Aval>();
            return repository.FindBy(avalId);
        }

        public IList<Aval> GetByGarantiaId(int garantiaId)
        {
            IAvalRepository repository = RepositoryFactory.GetRepository<IAvalRepository, Aval>();

            return repository.GetByGarantiaId(garantiaId);
        }

        public Aval Save(Aval aval, string auditUserId)
        {
            IMakerCheckerOperationRepository mcRepository = RepositoryFactory.GetRepository<IMakerCheckerOperationRepository, MakerCheckerOperation>();
            IGarantiaBaseRepository garantiaBaseRepo = RepositoryFactory.GetRepository<IGarantiaBaseRepository, GarantiaBase>();

            GarantiaBase garantia = garantiaBaseRepo.FindBy(aval.GarantiaId);
            bool isNew = garantia == null ? true : false;
                        
            if (aval != null && !isNew)            
                return this.Save(aval, auditUserId, false);
            else
                return this.Save(aval, auditUserId, true);
        }

        public Aval Commit(Aval aval, string auditUserId)
        {
            return this.Save(aval, auditUserId, false);
        }

        private Aval Save(Aval aval, string auditUserId, bool useMakerAndChecker)
        {
            IMakerCheckerOperationRepository mcRepository = RepositoryFactory.GetRepository<IMakerCheckerOperationRepository, MakerCheckerOperation>();
            if (useMakerAndChecker)
            {
                ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Debug, string.Format("Utilizando el sistema de maker & checker para operaciones de guardado sobre el aval ID: {0} - Identificador Garantia/Operacion: {1}.", aval.Key, aval.GarantiaId), new Dictionary<string, object>() { { "auditUserId", auditUserId }, { "useMakerAndChecker", useMakerAndChecker } });
                GarantiaCommonService<IGarantiaOtraRepository, GarantiaOtra> gService = new GarantiaCommonService<IGarantiaOtraRepository, GarantiaOtra>();

                MakerCheckerObject<GarantiaOtra> mcObj = gService.GetMakerCheckerObject(aval.GarantiaId);                             
                if (mcObj.Object != null && mcObj.Object.Avales != null)
                {
                    mcObj.Object.Avales.Add(aval);                    
                    gService.SetMakerCheckerObject(aval.GarantiaId, mcObj);                    
                }
                return aval;
            }
            else
            {
                ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Debug, string.Format("No se utilizará el sistema de maker & checker para operaciones de guardado sobre el aval ID: {0} - Identificador Garantia/Operacion: {1}.", aval.Key, aval.GarantiaId), new Dictionary<string, object>() { { "auditUserId", auditUserId }, { "useMakerAndChecker", useMakerAndChecker } });
                IAvalRepository repository = RepositoryFactory.GetRepository<IAvalRepository, Aval>();
                if ((int)aval.Key == 0 || (int)aval.Key == -1)
                    aval.Key = null;

                var newAval = repository.Add(aval);
                return newAval;
            }
        }

        public Aval Save(Aval aval)
        {
            return this.Save(aval, "novaris", false);
        }

        private void Remove(Aval aval, string auditUserId, bool useMakerAndChecker)
        {
            if (useMakerAndChecker)
            {
                ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Debug, string.Format("Utilizando el sistema de maker & checker para operaciones de eliminacion sobre el aval ID: {0} - Identificador Garantia/Operacion: {1}.", aval.Key, aval.GarantiaId), new Dictionary<string, object>() { { "auditUserId", auditUserId }, { "useMakerAndChecker", useMakerAndChecker } });
                GarantiaCommonService<IGarantiaOtraRepository, GarantiaOtra> gService = new GarantiaCommonService<IGarantiaOtraRepository, GarantiaOtra>();
                MakerCheckerObject<GarantiaOtra> mcObj = gService.GetMakerCheckerObject(aval.GarantiaId);
                mcObj.Deserialize();
                if (mcObj.Object != null && mcObj.Object.Avales != null)
                {
                    // checking the existence of the aval.
                    var objAval = mcObj.Object.Avales.FirstOrDefault(o => o.GetKeyAs<int>() == aval.GetKeyAs<int>());
                    if (objAval != null)
                    {
                        mcObj.Object.Avales.Remove(objAval);
                        gService.SetMakerCheckerObject(aval.GarantiaId, mcObj);
                    }
                }
            }
            else
            {
                ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Debug, string.Format("No se utilizará el sistema de maker & checker para operaciones de eliminacion sobre el aval ID: {0} - Identificador Garantia/Operacion: {1}.", aval.Key, aval.GarantiaId), new Dictionary<string, object>() { { "auditUserId", auditUserId }, { "useMakerAndChecker", useMakerAndChecker } });
                IAvalRepository repository = RepositoryFactory.GetRepository<IAvalRepository, Aval>();
                repository.Remove(aval);
            }
        }

        public void Remove(Aval aval, string auditUserId)
        {
            // TODO: Set to true to enable maker and checker, after implementation of delete of m&c document.
            this.Remove(aval, auditUserId, false);
        }

        public void Remove(Aval aval)
        {
            this.Remove(aval, "novaris", false);
        }

        /// <summary>
        /// Devuelve una entidad representativa vacia. 
        /// Esto se debe utilizar cuando el id es "vacio" o invalido
        /// </summary>
        /// <returns></returns>
        public static Aval GetEmpty()
        {
            return new Aval();
        }

        /// <summary>
        /// Elimina del repositorio todos los avales que no se encuentran en la lista provista
        /// </summary>
        /// <param name="garantiaId">id de garantia relacionada</param>
        /// <param name="targetList">Lista de avales target</param>
        public void removeAvalNotInList(int garantiaId, IList<Aval> targetList)
        {
            if (targetList == null)
                throw new ArgumentException("Target list is null");

            IList<Aval> avalesRepositorio = this.GetByGarantiaId(garantiaId);
            if (avalesRepositorio == null || avalesRepositorio.Count == 0)
                return;

            foreach (Aval avalRepo in avalesRepositorio)
            {
                if (!this.isAvalInList(avalRepo, targetList))
                {
                    //Debo eliminar aval del repo
                    this.Remove(avalRepo);
                }
            }

            
        }

        /// <summary>
        /// Verifica si el aval se encuentra en la lista
        /// NOTA: Solo busca por Key cuando la misma es de repositorio > 0
        ///         Caso contrario devuelve false
        /// </summary>
        /// <param name="aval"></param>
        /// <param name="avales"></param>
        /// <returns></returns>
        private bool isAvalInList(Aval aval, IList<Aval> avales)
        {
            //si no tiene clave o es temporal salgo
            if (aval.Key == null || aval.GetKeyAs<int>() <=0) return false;

            //Para la lista
            foreach (Aval a in avales)
            {
                if (a.Key != null && a.GetKeyAs<int>() >= 0)    //Si es un elemento valido en repositorio
                {
                    if (a.GetKeyAs<int>() == aval.GetKeyAs<int>())
                        return true;
                }
            }
            return false;
        }
    }
}
