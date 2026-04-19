using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Infrastructure.Caching;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using IMonedasRepository = Bladex.Garantias.DomainModel.Repositories.IMonedasRepository;

namespace Bladex.Garantias.DomainModel.Services
{
    public class MonedasService : ICacheableService
    {
        /// <summary>
        /// Returns all Monedas.
        /// </summary>
        /// <returns>A <see cref="Monedas"/> IList</returns>
        public IList<Monedas> GetAll()
        {
            if (CacheManager.Instance.Contains(this.GetCacheKey()))
                return CacheManager.Instance.GetData<List<Monedas>>(this.GetCacheKey());

            IMonedasRepository repository = RepositoryFactory.GetRepository<IMonedasRepository, Monedas>();
            List<Monedas> result = repository.GetAll().OrderBy(o => o.Nombre).ToList();
            CacheManager.Instance.Add(this.GetCacheKey(), result, this.GetTimeSpan());
            return result;
        }

        /// <summary>
        /// Return one Monedas by Id
        /// </summary>
        /// <param name="monedasId">Monedas Id</param>
        /// <returns>A <see cref="Monedas"/> entity.</returns>
        public Monedas GetById(string monedasId)
        {
            string cacheKey = this.GetCacheKey() + "_" + monedasId;
            if (!CacheManager.Instance.Contains(cacheKey))
            {
                IMonedasRepository repository = RepositoryFactory.GetRepository<IMonedasRepository, Monedas>();
                CacheManager.Instance.Add(cacheKey, repository.FindBy(monedasId), this.GetTimeSpan());
            }
            return CacheManager.Instance.GetData<Monedas>(cacheKey);
        }

        /// <summary>
        /// Devuelve una entidad representativa vacia.
        /// Esto se debe utilizar cuando el id es "vacio" o invalido
        /// </summary>
        /// <returns></returns>
        public static Monedas GetEmpty()
        {
            return new Monedas() { Key = "NA", Nombre = "NA" };
        }

        #region ICacheableService Members

        public string GetCacheKey()
        {
            return "MonedasService.GetAll()";
        }

        public TimeSpan GetTimeSpan()
        {
            return new TimeSpan(1, 0, 0);
        }

        #endregion
    }
}
