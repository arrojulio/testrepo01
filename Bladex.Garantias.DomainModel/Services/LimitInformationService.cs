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
    public class LimitInformationService : ICacheableService
    {
        /// <summary>
        /// Returns all LimitInformation.
        /// </summary>
        /// <returns>A <see cref="LimitInformation"/> IList</returns>
        public IList<LimitInformation> GetAll()
        {
            if (!CacheManager.Instance.Contains(this.GetCacheKey()))
            {
                ILimitInformationRepository repository = RepositoryFactory.GetRepository<ILimitInformationRepository, LimitInformation>();
                CacheManager.Instance.Add(this.GetCacheKey(), repository.GetAll().ToList(), this.GetTimeSpan());
            }
            return CacheManager.Instance.GetData(this.GetCacheKey()) as List<LimitInformation>;
        }

        public LimitInformation FindBy(string customerId, int definitionId = 578)
        {
            string key = string.Concat(customerId, "_", definitionId);
            if (!CacheManager.Instance.Contains(key))
            {
                ILimitInformationRepository repository = RepositoryFactory.GetRepository<ILimitInformationRepository, LimitInformation>();
                CacheManager.Instance.Add(key, repository.FindBy(definitionId, customerId), this.GetTimeSpan());
            }
            return CacheManager.Instance.GetData(key) as LimitInformation;
        }

        #region Implementation of ICacheableService

        /// <summary>
        /// Returns the key used to store an item into the cache.
        /// </summary>
        /// <returns></returns>
        public string GetCacheKey()
        {
            return "LimitInformationService.GetAll()";
        }

        /// <summary>
        /// Returns the timespan representing the time that the object will be stored into the cache.
        /// </summary>
        /// <returns></returns>
        public TimeSpan GetTimeSpan()
        {
            return new TimeSpan(0, 10, 0);
        }

        #endregion
    }
}
