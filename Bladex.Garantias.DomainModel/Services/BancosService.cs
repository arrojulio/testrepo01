using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Infrastructure.Caching;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using IBancoRepository = Bladex.Garantias.DomainModel.Repositories.IBancosRepository;

namespace Bladex.Garantias.DomainModel.Services
{
    public class BancosService : ICacheableService
    {
        /// <summary>
        /// Returns all banks.
        /// </summary>
        /// <returns>A <see cref="Bancos"/> IList</returns>
        public IList<Bancos> GetAll()
        {
            if (CacheManager.Instance.Contains(this.GetCacheKey()))
                return CacheManager.Instance.GetData<List<Bancos>>(this.GetCacheKey());

            IBancoRepository repository = RepositoryFactory.GetRepository<IBancoRepository, Bancos>();
            List<Bancos> result = repository.GetAll().OrderBy(o => o.Nombre).ToList();
            CacheManager.Instance.Add(this.GetCacheKey(), result, this.GetTimeSpan());
            return result;
        }

        /// <summary>
        /// Return one bank by Id
        /// </summary>
        /// <param name="bancoId">Bank Id</param>
        /// <returns>A <see cref="Bancos"/> entity.</returns>
        public Bancos GetById(string bancoId)
        {
            string cacheKey = this.GetCacheKey() + "_" + bancoId;
            if (!CacheManager.Instance.Contains(cacheKey))
            {
                IBancoRepository repository = RepositoryFactory.GetRepository<IBancoRepository, Bancos>();
                CacheManager.Instance.Add(cacheKey, repository.FindBy(bancoId), this.GetTimeSpan());
            }
            return CacheManager.Instance.GetData<Bancos>(cacheKey);
        }

        public Bancos GetByName(string name)
        {
            IBancoRepository repository = RepositoryFactory.GetRepository<IBancoRepository, Bancos>();
            return repository.FindByName(name);
        }

        /// <summary>
        /// Devuelve una entidad representativa vacia.
        /// Esto se debe utilizar cuando el id es "vacio" o invalido
        /// </summary>
        /// <returns></returns>
        public static Bancos GetEmpty()
        {
            return new Bancos() { Key = "027", Nombre = "NA" };
        }

        #region ICacheableService Members

        public string GetCacheKey()
        {
            return "BancosService.GetAll()";
        }

        public TimeSpan GetTimeSpan()
        {
            return new TimeSpan(1, 0, 0);
        }

        #endregion
    }
}
