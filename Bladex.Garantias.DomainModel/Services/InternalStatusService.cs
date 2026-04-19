using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Infrastructure.Caching;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using IInternalStatusRepository = Bladex.Garantias.DomainModel.Repositories.IInternalStatusRepository;

namespace Bladex.Garantias.DomainModel.Services
{
    public class InternalStatusService : ICacheableService
    {

        /// <summary>
        /// Returns all Status.
        /// </summary>
        /// <returns>A <see cref="InternalStatus"/> IList</returns>
        public IList<InternalStatus> GetAll()
        {
            if (CacheManager.Instance.Contains(this.GetCacheKey()))
                return CacheManager.Instance.GetData<List<InternalStatus>>(this.GetCacheKey());

            IInternalStatusRepository repository = RepositoryFactory.GetRepository<IInternalStatusRepository, InternalStatus>();
            List<InternalStatus> result = repository.GetAll().ToList();
            CacheManager.Instance.Add(this.GetCacheKey(), result, this.GetTimeSpan());
            return result;
        }

        /// <summary>
        /// Return one Status by Id
        /// </summary>
        /// <param name="statusId">Status Id</param>
        /// <returns>A <see cref="InternalStatus"/> entity.</returns>
        public InternalStatus GetById(string statusId)
        {
            string cacheKey = this.GetCacheKey() + "_" + statusId;
            if (!CacheManager.Instance.Contains(cacheKey))
            {
                IInternalStatusRepository repository = RepositoryFactory.GetRepository<IInternalStatusRepository, InternalStatus>();
                CacheManager.Instance.Add(cacheKey, repository.FindBy(statusId), this.GetTimeSpan());
            }
            return CacheManager.Instance.GetData<InternalStatus>(cacheKey);
        }

        private InternalStatus GetStatusOrCreateMissing(IInternalStatusRepository repository, String statusId)
        {
            InternalStatus status = repository.FindBy(statusId);
            if (status == null)
            {
                status = InternalStatusService.GetEmpty();
                status.Key = InternalStatus.ACTIVE_ID;
                status.Nombre = "Active";
                repository.Add(status);
            }
            return status;
        }

        public InternalStatus GetActiveStatus()
        {
            string cacheKey = this.GetCacheKey() + "_" + InternalStatus.ACTIVE_ID;
            if (!CacheManager.Instance.Contains(cacheKey))
            {
                IInternalStatusRepository repository = RepositoryFactory.GetRepository<IInternalStatusRepository, InternalStatus>();
                CacheManager.Instance.Add(cacheKey, this.GetStatusOrCreateMissing(repository, InternalStatus.ACTIVE_ID), this.GetTimeSpan());
            }
            return CacheManager.Instance.GetData<InternalStatus>(cacheKey);
        }

        public InternalStatus GetDeletedStatus()
        {
            string cacheKey = this.GetCacheKey() + "_" + InternalStatus.DELETED_ID;
            if (!CacheManager.Instance.Contains(cacheKey))
            {
                IInternalStatusRepository repository = RepositoryFactory.GetRepository<IInternalStatusRepository, InternalStatus>();
                CacheManager.Instance.Add(cacheKey, this.GetStatusOrCreateMissing(repository, InternalStatus.DELETED_ID), this.GetTimeSpan());
            }
            return CacheManager.Instance.GetData<InternalStatus>(cacheKey);
        }

        public InternalStatus GetBlockedStatus()
        {
            string cacheKey = this.GetCacheKey() + "_" + InternalStatus.BLOCKED_ID;
            if (!CacheManager.Instance.Contains(cacheKey))
            {
                IInternalStatusRepository repository = RepositoryFactory.GetRepository<IInternalStatusRepository, InternalStatus>();
                CacheManager.Instance.Add(cacheKey, this.GetStatusOrCreateMissing(repository, InternalStatus.BLOCKED_ID), this.GetTimeSpan());
            }
            return CacheManager.Instance.GetData<InternalStatus>(cacheKey);
        }

        public InternalStatus GetUnknownStatus()
        {
            string cacheKey = this.GetCacheKey() + "_" + InternalStatus.UNKNOWN_ID;
            if (!CacheManager.Instance.Contains(cacheKey))
            {
                IInternalStatusRepository repository = RepositoryFactory.GetRepository<IInternalStatusRepository, InternalStatus>();
                CacheManager.Instance.Add(cacheKey, this.GetStatusOrCreateMissing(repository, InternalStatus.UNKNOWN_ID), this.GetTimeSpan());
            }
            return CacheManager.Instance.GetData<InternalStatus>(cacheKey);
        }

        /// <summary>
        /// Devuelve una entidad representativa vacia.
        /// Esto se debe utilizar cuando el id es "vacio" o invalido
        /// </summary>
        /// <returns></returns>
        public static InternalStatus GetEmpty()
        {
            return new InternalStatus() { Key = "NA", Nombre = "NA" };
        }

        #region ICacheableService Members

        public string GetCacheKey()
        {
            return "InternalStatusService.GetAll()";
        }

        public TimeSpan GetTimeSpan()
        {
            return new TimeSpan(1, 0, 0);
        }

        #endregion
    }
}
