using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Infrastructure.Caching;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using ICategoriaSuperRepository = Bladex.Garantias.DomainModel.Repositories.ICategoriaSuperRepository;
using Bladex.Garantias.DomainModel.Repositories.Components.MakerChecker;
using Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker;
using Bladex.Garantias.DomainModel.Components.MakerChecker;
using Bladex.Garantias.DomainModel.Repositories;

namespace Bladex.Garantias.DomainModel.Services
{
    public class CategoriaSuperService : ICacheableService
    {
        /// <summary>
        /// Returns all CategoriaSuper.
        /// </summary>
        /// <returns>A <see cref="CategoriaSuper"/> IList</returns>
        public IList<CategoriaSuper> GetAll()
        {
            if (!CacheManager.Instance.Contains(this.GetCacheKey()))
            {
                ICategoriaSuperRepository repository = RepositoryFactory.GetRepository<ICategoriaSuperRepository, CategoriaSuper>();
                CacheManager.Instance.Add(this.GetCacheKey(), repository.GetAll().ToList(), this.GetTimeSpan());
            }
            return CacheManager.Instance.GetData(this.GetCacheKey()) as List<CategoriaSuper>;
        }

        /// <summary>
        /// Return one CategoriaSuper by Id
        /// </summary>
        /// <param name="categoriaSuperId">CategoriaSuper Id</param>
        /// <returns>A <see cref="CategoriaSuper"/> entity.</returns>
        public CategoriaSuper GetById(string categoriaSuperId)
        {
            string cacheKey = this.GetCacheKey() + "_" + categoriaSuperId;
            if (!CacheManager.Instance.Contains(cacheKey))
            {
                ICategoriaSuperRepository repository = RepositoryFactory.GetRepository<ICategoriaSuperRepository, CategoriaSuper>();
                CacheManager.Instance.Add(cacheKey, repository.FindBy(categoriaSuperId), this.GetTimeSpan());
            }
            return CacheManager.Instance.GetData(cacheKey) as CategoriaSuper;
        }

        public CategoriaSuper GetByGarantiaId(int garantiaId)
        {
            ICategoriaSuperRepository repository = RepositoryFactory.GetRepository<ICategoriaSuperRepository, CategoriaSuper>();
            return repository.FindByGarantiaId(garantiaId);
        }

        /// <summary>
        /// Devuelve una entidad representativa vacia. 
        /// Esto se debe utilizar cuando el id es "vacio" o invalido
        /// </summary>
        /// <returns></returns>
        public static CategoriaSuper GetEmpty()
        {
            return new CategoriaSuper() {Key="NA", Nombre="NA", IsReadOnly = true };
        }
              

        #region ICacheableService Members

        public string GetCacheKey()
        {
            return "CategoriaSuperService.GetAll()";
        }

        public TimeSpan GetTimeSpan()
        {
            return new TimeSpan(1, 0, 0);
        }

        #endregion
    }
}
