using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Infrastructure.Caching;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using ITipoGarantiaSuperRepository = Bladex.Garantias.DomainModel.Repositories.ITipoGarantiaSuperRepository;

namespace Bladex.Garantias.DomainModel.Services
{
    public class TipoGarantiaSuperService : ICacheableService
    {
        /// <summary>
        /// Returns all TipoGarantiaSuper filtered by CategoriaSuperId.
        /// Uses the cached full list to avoid hitting the DB per category.
        /// </summary>
        public IList<TipoGarantiaSuper> GetAll(string CategoriaSuperId)
        {
            return GetAllCached()
                .Where(o => o.Categoria.Key.ToString() == CategoriaSuperId)
                .ToList();
        }

        public TipoGarantiaSuper GetFianzasYAvalesNoBancarios()
        {
            // Id del tipo de garantia super "Fianzas y Avales No Bancarios".
            return GetById("0605");
        }

        /// <summary>
        /// Return one TipoGarantiaSuper by Id
        /// </summary>
        /// <param name="tipoGarantiaSuperId">TipoGarantiaSuper Id</param>
        /// <returns>A <see cref="TipoGarantiaSuper"/> entity.</returns>
        public TipoGarantiaSuper GetById(string tipoGarantiaSuperId)
        {
            string cacheKey = this.GetCacheKey() + "_" + tipoGarantiaSuperId;
            if (!CacheManager.Instance.Contains(cacheKey))
            {
                ITipoGarantiaSuperRepository repository = RepositoryFactory.GetRepository<ITipoGarantiaSuperRepository, TipoGarantiaSuper>();
                CacheManager.Instance.Add(cacheKey, repository.FindBy(tipoGarantiaSuperId), this.GetTimeSpan());
            }
            return CacheManager.Instance.GetData<TipoGarantiaSuper>(cacheKey);
        }

        /// <summary>
        /// Return one TipoGarantiaSuper by Name
        /// </summary>
        public TipoGarantiaSuper GetByName(string tipoGarantiaSuperName)
        {
            return GetAllCached().FirstOrDefault(o => o.Nombre == tipoGarantiaSuperName);
        }

        /// <summary>
        /// Devuelve una entidad representativa vacia.
        /// Esto se debe utilizar cuando el id es "vacio" o invalido
        /// </summary>
        /// <returns></returns>
        public static TipoGarantiaSuper GetEmpty(CategoriaSuper categoriaSuper)
        {
            return new TipoGarantiaSuper() { Key = categoriaSuper.GetKeyAs<string>() + "NA", Nombre = "N/A" };
        }

        private List<TipoGarantiaSuper> GetAllCached()
        {
            if (!CacheManager.Instance.Contains(this.GetCacheKey()))
            {
                ITipoGarantiaSuperRepository repository = RepositoryFactory.GetRepository<ITipoGarantiaSuperRepository, TipoGarantiaSuper>();
                CacheManager.Instance.Add(this.GetCacheKey(), repository.GetAll().ToList(), this.GetTimeSpan());
            }
            return CacheManager.Instance.GetData<List<TipoGarantiaSuper>>(this.GetCacheKey());
        }

        #region ICacheableService Members

        public string GetCacheKey()
        {
            return "TipoGarantiaSuperService.GetAll()";
        }

        public TimeSpan GetTimeSpan()
        {
            return new TimeSpan(1, 0, 0);
        }

        #endregion
    }
}
