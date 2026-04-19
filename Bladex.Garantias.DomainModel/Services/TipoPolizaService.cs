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
    public class TipoPolizaService : ICacheableService
    {
        public IList<TipoPoliza> GetAll()
        {
            if (CacheManager.Instance.Contains(this.GetCacheKey()))
                return CacheManager.Instance.GetData<List<TipoPoliza>>(this.GetCacheKey());

            ITipoPolizaRepository repository = RepositoryFactory.GetRepository<ITipoPolizaRepository, TipoPoliza>();
            List<TipoPoliza> result = repository.GetAll().ToList();
            CacheManager.Instance.Add(this.GetCacheKey(), result, this.GetTimeSpan());
            return result;
        }

        public TipoPoliza GetById(string tipoPolizaId)
        {
            string cacheKey = this.GetCacheKey() + "_" + tipoPolizaId;
            if (!CacheManager.Instance.Contains(cacheKey))
            {
                ITipoPolizaRepository repository = RepositoryFactory.GetRepository<ITipoPolizaRepository, TipoPoliza>();
                CacheManager.Instance.Add(cacheKey, repository.FindBy(tipoPolizaId), this.GetTimeSpan());
            }
            return CacheManager.Instance.GetData<TipoPoliza>(cacheKey);
        }

        #region ICacheableService Members

        public string GetCacheKey()
        {
            return "TipoPolizaService.GetAll()";
        }

        public TimeSpan GetTimeSpan()
        {
            return new TimeSpan(1, 0, 0);
        }

        #endregion
    }
}
