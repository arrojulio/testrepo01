using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Infrastructure.Caching;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using ITipoGarantiaBladexRepository = Bladex.Garantias.DomainModel.Repositories.ITipoGarantiaBladexRepository;

namespace Bladex.Garantias.DomainModel.Services
{
    public class TipoGarantiaBladexService: ICacheableService
    {
        /// <summary>
        /// Returns all TipoGarantiaBladex.
        /// </summary>
        /// <returns>A <see cref="TipoGarantiaBladex"/> IList</returns>
        public IList<TipoGarantiaBladex> GetAll()
        {
            if (!CacheManager.Instance.Contains(this.GetCacheKey()))
            {
                ITipoGarantiaBladexRepository repository = RepositoryFactory.GetRepository<ITipoGarantiaBladexRepository, TipoGarantiaBladex>();
                CacheManager.Instance.Add(this.GetCacheKey(), repository.GetAll().ToList(), this.GetTimeSpan());
            }
            return CacheManager.Instance.GetData(this.GetCacheKey()) as List<TipoGarantiaBladex>;
        }

        /// <summary>
        /// Return one TipoGarantiaBladex by Id
        /// </summary>
        /// <param name="tipoGarantiaBladexId">TipoGarantiaBladex Id</param>
        /// <returns>A <see cref="TipoGarantiaBladex"/> entity.</returns>
        public TipoGarantiaBladex GetById(string tipoGarantiaBladexId)
        {
            string cacheKey = this.GetCacheKey() + "_" + tipoGarantiaBladexId;
            if (!CacheManager.Instance.Contains(cacheKey))
            {
                ITipoGarantiaBladexRepository repository = RepositoryFactory.GetRepository<ITipoGarantiaBladexRepository, TipoGarantiaBladex>();
                CacheManager.Instance.Add(cacheKey, repository.FindBy(tipoGarantiaBladexId), this.GetTimeSpan());
            }
            return CacheManager.Instance.GetData<TipoGarantiaBladex>(cacheKey);
        }

        public TipoGarantiaBladex GetByName(string tipoGarantiaBladexName)
        {
            return GetAll().FirstOrDefault(o => o.Nombre == tipoGarantiaBladexName);
        }
        /// <summary>
        /// Devuelve una entidad representativa vacia. 
        /// Esto se debe utilizar cuando el id es "vacio" o invalido
        /// </summary>
        /// <returns></returns>
        public static TipoGarantiaBladex GetEmpty()
        {
            return new TipoGarantiaBladex() { Key = "NA", Nombre = "NA" };
        }

        #region Implementation of ICacheableService

        /// <summary>
        /// Returns the key used to store an item into the cache.
        /// </summary>
        /// <returns></returns>
        public string GetCacheKey()
        {
            return "TipoGarantiaBladexService.GetAll()";
        }

        /// <summary>
        /// Returns the timespan representing the time that the object will be stored into the cache.
        /// </summary>
        /// <returns></returns>
        public TimeSpan GetTimeSpan()
        {
            return new TimeSpan(1, 0, 0);
        }

        #endregion
    }
}
