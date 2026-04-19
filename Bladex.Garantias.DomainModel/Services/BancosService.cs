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
            IBancoRepository repository = RepositoryFactory.GetRepository<IBancoRepository, Bancos>();
            return repository.GetAll();
        }

        /// <summary>
        /// Return one bank by Id
        /// </summary>
        /// <param name="customerId">Bank Id</param>
        /// <returns>A <see cref="Bancos"/> entity.</returns>
        public Bancos GetById(string bancoId)
        {
            IBancoRepository repository = RepositoryFactory.GetRepository<IBancoRepository, Bancos>();            
            return repository.FindBy(bancoId);
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
            throw new NotImplementedException();
        }

        public TimeSpan GetTimeSpan()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
