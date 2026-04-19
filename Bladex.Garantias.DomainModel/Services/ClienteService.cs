using System;
using System.Collections.Generic;
using System.Linq;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Infrastructure.Caching;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using IClienteRepository = Bladex.Garantias.DomainModel.Repositories.IClienteRepository;

namespace Bladex.Garantias.DomainModel.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class ClienteService : ICacheableService
    {
        public const string _GARANTEE_LIMIT_EXP_DATE = "19800101";
        /// <summary>
        /// Returns all customers and garantes (active & inactive).
        /// </summary>
        /// <returns>A <see cref="Cliente"/> IList</returns>
        public IList<Cliente> GetAll()
        {
            if (CacheManager.Instance.Contains(this.GetCacheKey()))
            {
                return CacheManager.Instance.GetData(this.GetCacheKey()) as List<Cliente>;
            }
            IClienteRepository repository = RepositoryFactory.GetRepository<IClienteRepository, Cliente>();
            List<Cliente> result = repository.GetAll().OrderBy(o => o.Nombre).ToList();
            CacheManager.Instance.Add(this.GetCacheKey(), result, this.GetTimeSpan());
            return result;
        }

        /// <summary>
        /// Creates the cliente.
        /// </summary>
        /// <param name="cliente">The cliente of type <see cref="Bladex.Garantias.DomainModel.DomainBase.Cliente"/></param>
        /// <returns></returns>
        public string CreateCliente(Cliente cliente)
        {
            IClienteRepository repository = RepositoryFactory.GetRepository<IClienteRepository, Cliente>();
            if (repository.FindByName(cliente.Nombre).Count > 0)
            {
                cliente = repository.FindByName(cliente.Nombre)[0];
            }
            else
            {
                cliente.Key = null;
                cliente.IsActive = true;
                cliente.Pais = new Pais() { Key = "N/A" };
                cliente = repository.Add(cliente);
            }
            
            return cliente.GetKeyAs<string>();
        }

        /// <summary>
        /// Creates the garante.
        /// </summary>
        /// <param name="garante">The garante of type <see cref="Bladex.Garantias.DomainModel.DomainBase.Cliente"/></param>
        /// <returns></returns>
        public string CreateGarante(Cliente garante)
        {
            IClienteRepository repository = RepositoryFactory.GetRepository<IClienteRepository, Cliente>();
            if (repository.FindByName(garante.Nombre).Count > 0)
            {
                garante = repository.FindByName(garante.Nombre)[0];
            }
            else
            {
                garante.Key = null;
                garante.IsActive = true;
                garante.Pais = PaisService.GetEmpty();
                garante.LimitExpDate = _GARANTEE_LIMIT_EXP_DATE;
                garante = repository.Add(garante);
            }
            return garante.GetKeyAs<string>();
        }

        /// <summary>
        /// Returns only active garantes.
        /// </summary>
        /// <returns>A <see cref="Cliente"/> <see cref="IList{Cliente}"/></returns>
        public IList<Cliente> GetAllGarantes()
        {
            string cacheKey = "ClienteService.GetAllGarantes()";
            if (CacheManager.Instance.Contains(cacheKey))
            {
                return CacheManager.Instance.GetData(cacheKey) as List<Cliente>;
            }
            
            // Filtro por Active y Ordeno por Nombre
            List<Cliente> result = this.GetAll().Where(o => o.IsActive.HasValue && o.IsActive.Value).OrderBy(o => o.Nombre).ToList();
            CacheManager.Instance.Add(cacheKey, result, this.GetTimeSpan());
            return result;
        }

        /// <summary>
        /// Returns only active customers.
        /// </summary>
        /// <returns>A <see cref="Cliente"/> <see cref="IList{Cliente}"/></returns>
        public IList<Cliente> GetAllClientes()
        {
            string cacheKey = "ClienteService.GetAllClientes()";
            if (CacheManager.Instance.Contains(cacheKey))
            {
                return CacheManager.Instance.GetData(cacheKey) as List<Cliente>;
            }
            
            // Filtro por Active y Ordeno por Nombre
            List<Cliente> result = this.GetAll().Where(o=>o.IsActive.HasValue && o.IsActive.Value && !o.IsGarante).OrderBy(o => o.Nombre).ToList();
            CacheManager.Instance.Add(cacheKey, result, this.GetTimeSpan());
            return result;
        }

        /// <summary>
        /// Return one customer by Id
        /// </summary>
        /// <param name="customerId">Customer Id</param>
        /// <returns>A <see cref="Cliente"/> entity.</returns>
        public Cliente GetById(string customerId)
        {
            if (!string.IsNullOrEmpty(customerId))
                customerId = customerId.Trim();
            IClienteRepository repository = RepositoryFactory.GetRepository<IClienteRepository, Cliente>();
            return repository.FindBy(customerId);
        }

        /// <summary>
        /// Return one customer by Name
        /// </summary>
        /// <param name="customerName">Customer Name</param>
        /// <returns>The first Cliente that match the name or NULL if no Cliente found</returns>
        public Cliente GetByName(string customerName)
        {
            Cliente ret = null;
            IClienteRepository repository = RepositoryFactory.GetRepository<IClienteRepository, Cliente>();
            if (repository.FindByName(customerName).Count > 0)
            {
                ret = repository.FindByName(customerName)[0];
            }
            return ret;
        }

        /// <summary>
        /// Devuelve una entidad representativa vacia. 
        /// Esto se debe utilizar cuando el id es "vacio" o invalido
        /// </summary>
        /// <returns></returns>
        public static Cliente GetEmpty()
        {
            return new Cliente() { Key = "NA", Nombre = "NA", Pais = PaisService.GetEmpty() };
        }

        public string GetCacheKey()
        {
            return "ClienteService.GetAll()";
        }

        public TimeSpan GetTimeSpan()
        {
            return new TimeSpan(0, 1, 0);
        }
    }

    
}
