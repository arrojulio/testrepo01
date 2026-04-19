using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Infrastructure.Caching;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using IRegionRepository = Bladex.Garantias.DomainModel.Repositories.IRegionRepository;

namespace Bladex.Garantias.DomainModel.Services
{
    public class RegionService : ICacheableService
    {
        public IList<Region> GetAll()
        {
            if (CacheManager.Instance.Contains(this.GetCacheKey()))
                return CacheManager.Instance.GetData<List<Region>>(this.GetCacheKey());

            IRegionRepository repository = RepositoryFactory.GetRepository<IRegionRepository, Region>();
            List<Region> result = repository.GetAll().OrderBy(o => o.Nombre).ToList();
            CacheManager.Instance.Add(this.GetCacheKey(), result, this.GetTimeSpan());
            return result;
        }

        public Region GetById(string regionId)
        {
            string cacheKey = this.GetCacheKey() + "_" + regionId;
            if (!CacheManager.Instance.Contains(cacheKey))
            {
                IRegionRepository repository = RepositoryFactory.GetRepository<IRegionRepository, Region>();
                CacheManager.Instance.Add(cacheKey, repository.FindBy(regionId), this.GetTimeSpan());
            }
            return CacheManager.Instance.GetData<Region>(cacheKey);
        }

        public static Region GetEmpty()
        {
            return new Region() { Key = "N/A", Nombre = "NA" };
        }

        #region ICacheableService Members

        public string GetCacheKey()
        {
            return "RegionService.GetAll()";
        }

        public TimeSpan GetTimeSpan()
        {
            return new TimeSpan(1, 0, 0);
        }

        #endregion
    }
}
