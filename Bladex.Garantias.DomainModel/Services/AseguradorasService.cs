using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Infrastructure.Caching;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using IAseguradorasRepository = Bladex.Garantias.DomainModel.Repositories.IAseguradorasRepository;

namespace Bladex.Garantias.DomainModel.Services
{
    public class AseguradorasService : ICacheableService
    {
        /// <summary>
        /// Returns all Aseguradoras.
        /// </summary>
        /// <returns>A <see cref="Aseguradoras"/> IList</returns>
        public IList<Aseguradoras> GetAll()
        {
            if (CacheManager.Instance.Contains(this.GetCacheKey()))
                return CacheManager.Instance.GetData<List<Aseguradoras>>(this.GetCacheKey());

            IAseguradorasRepository repository = RepositoryFactory.GetRepository<IAseguradorasRepository, Aseguradoras>();
            List<Aseguradoras> result = repository.GetAll().OrderBy(o => o.Nombre).ToList();
            CacheManager.Instance.Add(this.GetCacheKey(), result, this.GetTimeSpan());
            return result;
        }

        /// <summary>
        /// Return one Aseguradora by Id
        /// </summary>
        /// <param name="aseguradoraId">Aseguradora Id</param>
        /// <returns>A <see cref="Aseguradoras"/> entity.</returns>
        public Aseguradoras GetById(string aseguradoraId)
        {
            string cacheKey = this.GetCacheKey() + "_" + aseguradoraId;
            if (!CacheManager.Instance.Contains(cacheKey))
            {
                IAseguradorasRepository repository = RepositoryFactory.GetRepository<IAseguradorasRepository, Aseguradoras>();
                CacheManager.Instance.Add(cacheKey, repository.FindBy(aseguradoraId), this.GetTimeSpan());
            }
            return CacheManager.Instance.GetData<Aseguradoras>(cacheKey);
        }

        /// <summary>
        /// Devuelve una entidad representativa vacia.
        /// Esto se debe utilizar cuando el id es "vacio" o invalido
        /// </summary>
        /// <returns></returns>
        public static Aseguradoras GetEmpty()
        {
            return new Aseguradoras() { Key = "NA", Nombre = "NA" };
        }

        #region ICacheableService Members

        public string GetCacheKey()
        {
            return "AseguradorasService.GetAll()";
        }

        public TimeSpan GetTimeSpan()
        {
            return new TimeSpan(1, 0, 0);
        }

        #endregion
    }
}
