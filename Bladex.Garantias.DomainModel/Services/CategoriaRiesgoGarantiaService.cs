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
    public class CategoriaRiesgoGarantiaService : ICacheableService
    {
        public IList<CategoriaRiesgoGarantia> GetAll()
        {
            if (CacheManager.Instance.Contains(this.GetCacheKey()))
                return CacheManager.Instance.GetData<List<CategoriaRiesgoGarantia>>(this.GetCacheKey());

            ICategoriaRiesgoGarantiaRepository repository = RepositoryFactory.GetRepository<ICategoriaRiesgoGarantiaRepository, CategoriaRiesgoGarantia>();
            List<CategoriaRiesgoGarantia> result = repository.GetAll().ToList();
            CacheManager.Instance.Add(this.GetCacheKey(), result, this.GetTimeSpan());
            return result;
        }

        public CategoriaRiesgoGarantia GetById(string categoriaRiesgoId)
        {
            string cacheKey = this.GetCacheKey() + "_" + categoriaRiesgoId;
            if (!CacheManager.Instance.Contains(cacheKey))
            {
                ICategoriaRiesgoGarantiaRepository repository = RepositoryFactory.GetRepository<ICategoriaRiesgoGarantiaRepository, CategoriaRiesgoGarantia>();
                CacheManager.Instance.Add(cacheKey, repository.FindBy(categoriaRiesgoId), this.GetTimeSpan());
            }
            return CacheManager.Instance.GetData<CategoriaRiesgoGarantia>(cacheKey);
        }

        public CategoriaRiesgoGarantia GetByName(string categoriaSuperName)
        {
            return GetAll().FirstOrDefault(o => o.Nombre == categoriaSuperName);
        }

        public static CategoriaRiesgoGarantia GetEmpty()
        {
            return new CategoriaRiesgoGarantia() { Key = "NA", Nombre = "NA" };
        }

        #region ICacheableService Members

        public string GetCacheKey()
        {
            return "CategoriaRiesgoGarantiaService.GetAll()";
        }

        public TimeSpan GetTimeSpan()
        {
            return new TimeSpan(1, 0, 0);
        }

        #endregion
    }
}
