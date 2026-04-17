using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Infrastructure.Caching;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using IFiduciariasRepository = Bladex.Garantias.DomainModel.Repositories.IFiduciariasRepository;


namespace Bladex.Garantias.DomainModel.Services
{
    public class FiduciariasService : ICacheableService
    {
        /// <summary>
        /// Returns all Fiduciarias.
        /// </summary>
        /// <returns>A <see cref="Fiduciarias"/> IList</returns>
        public IList<Fiduciarias> GetAll()
        {
            if (!CacheManager.Instance.Contains(this.GetCacheKey()))
            {
                IFiduciariasRepository repository = RepositoryFactory.GetRepository<IFiduciariasRepository, Fiduciarias>();
                CacheManager.Instance.Add(this.GetCacheKey(), repository.GetAll().ToList(), this.GetTimeSpan());
            }
            return CacheManager.Instance.GetData(this.GetCacheKey()) as List<Fiduciarias>;
        }

        /// <summary>
        /// Return one Fiduciarias by Id
        /// </summary>
        /// <param name="fiduciariasId">Fiduciarias Id</param>
        /// <returns>A <see cref="Fiduciarias"/> entity.</returns>
        public Fiduciarias GetById(string fiduciariasId)
        {
            IFiduciariasRepository repository = RepositoryFactory.GetRepository<IFiduciariasRepository, Fiduciarias>();
            return repository.FindBy(fiduciariasId);
        }

        /// <summary>
        /// Return one Fiduciarias by Name
        /// </summary>
        /// <param name="fiduciariasId">Fiduciarias Id</param>
        /// <returns>A <see cref="Fiduciarias"/> entity.</returns>
        public Fiduciarias GetByName(string fiduciariasName)
        {
            IFiduciariasRepository repository = RepositoryFactory.GetRepository<IFiduciariasRepository, Fiduciarias>();
            return repository.GetAll().Where(o=>o.Nombre==fiduciariasName).FirstOrDefault();
        }
        /// <summary>
        /// Devuelve una entidad representativa vacia. 
        /// Esto se debe utilizar cuando el id es "vacio" o invalido
        /// </summary>
        /// <returns></returns>
        public static Fiduciarias GetEmpty()
        {
            return new Fiduciarias() {Key="NA", Nombre="NA" };
        }

        #region ICacheableService Members

        public string GetCacheKey()
        {
            return "FiduciariasService.GetAll()";
        }

        public TimeSpan GetTimeSpan()
        {
            return new TimeSpan(1, 0, 0);
        }

        #endregion
    }
}
