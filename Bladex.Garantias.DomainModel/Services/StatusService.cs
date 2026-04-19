using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.Infrastructure.Caching;
using Bladex.Garantias.Infrastructure.RepositoryFramework;

namespace Bladex.Garantias.DomainModel.Services
{
    public class StatusService : ICacheableService
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public IList<Status> GetAll()
        {
            if (CacheManager.Instance.Contains(this.GetCacheKey()))
                return CacheManager.Instance.GetData<List<Status>>(this.GetCacheKey());

            IStatusRepository repository = RepositoryFactory.GetRepository<IStatusRepository, Status>();
            List<Status> result = repository.GetAll().ToList();
            CacheManager.Instance.Add(this.GetCacheKey(), result, this.GetTimeSpan());
            return result;
        }

        /// <summary>
        /// Return one Status by Id
        /// </summary>
        /// <param name="statusId">Status Id</param>
        /// <returns>A <see cref="Status"/> entity.</returns>
        public Status GetById(string statusId)
        {
            string cacheKey = this.GetCacheKey() + "_" + statusId;
            if (!CacheManager.Instance.Contains(cacheKey))
            {
                IStatusRepository repository = RepositoryFactory.GetRepository<IStatusRepository, Status>();
                CacheManager.Instance.Add(cacheKey, repository.FindBy(statusId), this.GetTimeSpan());
            }
            return CacheManager.Instance.GetData<Status>(cacheKey);
        }

        public Status GetNormalStatus()
        {
            string cacheKey = this.GetCacheKey() + "_" + Status.NORMAL;
            if (!CacheManager.Instance.Contains(cacheKey))
            {
                IStatusRepository repository = RepositoryFactory.GetRepository<IStatusRepository, Status>();
                CacheManager.Instance.Add(cacheKey, repository.FindBy(Status.NORMAL), this.GetTimeSpan());
            }
            return CacheManager.Instance.GetData<Status>(cacheKey);
        }

        public Status GetEnEjecucionStatus()
        {
            string cacheKey = this.GetCacheKey() + "_" + Status.EN_EJECUCION;
            if (!CacheManager.Instance.Contains(cacheKey))
            {
                IStatusRepository repository = RepositoryFactory.GetRepository<IStatusRepository, Status>();
                CacheManager.Instance.Add(cacheKey, repository.FindBy(Status.EN_EJECUCION), this.GetTimeSpan());
            }
            return CacheManager.Instance.GetData<Status>(cacheKey);
        }

        public Status GetEjecutadaStatus()
        {
            string cacheKey = this.GetCacheKey() + "_" + Status.EJECUTADA;
            if (!CacheManager.Instance.Contains(cacheKey))
            {
                IStatusRepository repository = RepositoryFactory.GetRepository<IStatusRepository, Status>();
                CacheManager.Instance.Add(cacheKey, repository.FindBy(Status.EJECUTADA), this.GetTimeSpan());
            }
            return CacheManager.Instance.GetData<Status>(cacheKey);
        }

        /// <summary>
        /// Devuelve una entidad representativa vacia.
        /// Esto se debe utilizar cuando el id es "vacio" o invalido
        /// </summary>
        /// <returns></returns>
        public static Status GetEmpty()
        {
            return new Status() { Key = "NA", Nombre = "NA" };
        }

        #region ICacheableService Members

        public string GetCacheKey()
        {
            return "StatusService.GetAll()";
        }

        public TimeSpan GetTimeSpan()
        {
            return new TimeSpan(1, 0, 0);
        }

        #endregion
    }
}
