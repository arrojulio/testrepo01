using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Infrastructure.Caching;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using IPaisRepository = Bladex.Garantias.DomainModel.Repositories.IPaisRepository;

namespace Bladex.Garantias.DomainModel.Services
{
    public class PaisService : ICacheableService
    {
        /// <summary>
        /// Returns all Pais.
        /// </summary>
        /// <returns>A <see cref="Pais"/> IList</returns>
        public IList<Pais> GetAll()
        {
            if (CacheManager.Instance.Contains("PaisService.GetAll()"))
            {
                return CacheManager.Instance.GetData("PaisService.GetAll()") as List<Pais>;
            }
            IPaisRepository repository = RepositoryFactory.GetRepository<IPaisRepository, Pais>();
            List<Pais> result = repository.GetAll().OrderBy(o=>o.Nombre).ToList();
            CacheManager.Instance.Add(this.GetCacheKey(), result);
            return result;
            
        }

        /// <summary>
        /// Return one Pais by Id
        /// </summary>
        /// <param name="paisId">Pais Id</param>
        /// <returns>A <see cref="Pais"/> entity.</returns>
        public Pais GetById(string paisId)
        {
            string cacheKey = this.GetCacheKey() + "_" + paisId;
            if (!CacheManager.Instance.Contains(cacheKey))
            {
                IPaisRepository repository = RepositoryFactory.GetRepository<IPaisRepository, Pais>();
                CacheManager.Instance.Add(cacheKey, repository.FindBy(paisId), this.GetTimeSpan());
            }
            return CacheManager.Instance.GetData(cacheKey) as Pais;
        }

        public IList<Pais> GetByName(string name)
        {
            IPaisRepository repository = RepositoryFactory.GetRepository<IPaisRepository, Pais>();
            return repository.FindByName(name);
        }

        /// <summary>
        /// Devuelve una entidad representativa vacia. 
        /// Esto se debe utilizar cuando el id es "vacio" o invalido
        /// </summary>
        /// <returns></returns>
        public static Pais GetEmpty()
        {
            return new Pais() { Key = "N/A", Nombre = "NA", CodigoSuper = "" };
        }

        #region ICacheableService Members

        public string GetCacheKey()
        {
            return "PaisService.GetAll()";
        }

        public TimeSpan GetTimeSpan()
        {
            return new TimeSpan(1, 0, 0);
        }

        #endregion
    }
}
