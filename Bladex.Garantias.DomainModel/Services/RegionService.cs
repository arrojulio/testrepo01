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
        /// <summary>
        /// Returns all Pais.
        /// </summary>
        /// <returns>A <see cref="Pais"/> IList</returns>
        public IList<Region> GetAll()
        {
            //if (CacheManager.Instance.Contains("RegionService.GetAll()"))
            //{
            //    return CacheManager.Instance.GetData("RegionService.GetAll()") as List<Region>;
            //}
            IRegionRepository repository = RepositoryFactory.GetRepository<IRegionRepository, Region>();
            List<Region> result = repository.GetAll().OrderBy(o => o.Nombre).ToList();
            //CacheManager.Instance.Add(this.GetCacheKey(), result);
            return result;

        }

        /// <summary>
        /// Devuelve una entidad representativa vacia. 
        /// Esto se debe utilizar cuando el id es "vacio" o invalido
        /// </summary>
        /// <returns></returns>
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