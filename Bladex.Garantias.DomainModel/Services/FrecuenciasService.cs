using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Infrastructure.Caching;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using IFrecuenciasRepository = Bladex.Garantias.DomainModel.Repositories.IFrecuenciasRepository;

namespace Bladex.Garantias.DomainModel.Services
{
    public class FrecuenciasService : ICacheableService
    {
        /// <summary>
        /// Returns all Frecuencias.
        /// </summary>
        /// <returns>A <see cref="Frecuencias"/> IList</returns>
        public IList<Frecuencias> GetAll()
        {
            if (!CacheManager.Instance.Contains(this.GetCacheKey()))
            {
                IFrecuenciasRepository repository = RepositoryFactory.GetRepository<IFrecuenciasRepository, Frecuencias>();
                CacheManager.Instance.Add(this.GetCacheKey(), repository.GetAll().ToList(), this.GetTimeSpan());
            }
            return CacheManager.Instance.GetData(this.GetCacheKey()) as List<Frecuencias>;
        }

        /// <summary>
        /// Return one Frecuencias by Id
        /// </summary>
        /// <param name="frecuenciasId">Frecuencias Id</param>
        /// <returns>A <see cref="Frecuencias"/> entity.</returns>
        public Frecuencias GetById(string frecuenciasId)
        {
            string cacheKey = this.GetCacheKey() + "_" + frecuenciasId;
            if (!CacheManager.Instance.Contains(cacheKey))
            {
                IFrecuenciasRepository repository = RepositoryFactory.GetRepository<IFrecuenciasRepository, Frecuencias>();
                CacheManager.Instance.Add(cacheKey, repository.FindBy(frecuenciasId), this.GetTimeSpan());
            }
            return CacheManager.Instance.GetData<Frecuencias>(cacheKey);
        }

        /// <summary>
        /// Return one Frecuencias by Name
        /// </summary>
        /// <param name="frecuenciasId">Frecuencias Id</param>
        /// <returns>A <see cref="Frecuencias"/> entity.</returns>
        public Frecuencias GetByName(string frecuenciasName)
        {
            IFrecuenciasRepository repository = RepositoryFactory.GetRepository<IFrecuenciasRepository, Frecuencias>();
            return repository.GetAll().Where(o => o.Nombre == frecuenciasName).FirstOrDefault();
        }

        /// <summary>
        /// Devuelve una entidad representativa vacia. 
        /// Esto se debe utilizar cuando el id es "vacio" o invalido
        /// </summary>
        /// <returns></returns>
        public static Frecuencias GetEmpty()
        {
            return new Frecuencias() { Key = "NA", Nombre = "NA" };
        }

        #region Implementation of ICacheableService

        /// <summary>
        /// Returns the key used to store an item into the cache.
        /// </summary>
        /// <returns></returns>
        public string GetCacheKey()
        {
            return "FrecuenciasService.GetAll()";
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
