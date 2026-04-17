using System;
using System.Collections.Generic;
using System.Linq;
using Bladex.Garantias.Infrastructure.DomainBase;
using Bladex.Garantias.Infrastructure.RepositoryFramework;

namespace Bladex.Garantias.Infrastructure.Services
{
    /// <summary>
    /// Clase abstracta que determina la funcionalidad base de un servicio.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ServiceBase<T> : IService<T> where T: EntityBase, new()
    {
        protected ServiceBase(IRepository<T> repository)
        {
            Repository = repository;
        }

        /// <summary>
        /// Retrieve a cache key. 
        /// </summary>
        /// <param name="methodName">Uses the method name to generate the key.</param>
        /// <returns>Cache Key</returns>
        protected string GetCacheKey(string methodName)
        {
            return string.Concat(GetType().Name, ".", methodName).ToUpper();
        }


        protected IRepository<T> Repository
        { get; set; }

        #region IService<T> Members

        public virtual T FindBy(object key)
        {
            return Repository.FindBy(key);
        }

        public virtual T Add(T item)
        {
            return Repository.Add(item);
        }

        public virtual T this[object key]
        {
            get
            {
                return Repository.FindBy(key);
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public virtual void Remove(T item)
        {
            Repository.Remove(item);
        }

        public virtual List<T> GetAll()
        {
            return Repository.GetAll().ToList();
        }

        public virtual long Count()
        {
            return Repository.Count();
        }

        public virtual T GetNewInstance()
        {
            return new T();
        }

        #endregion
    }
}
