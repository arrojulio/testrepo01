using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Infrastructure.DomainBase;
using Bladex.Garantias.DomainModel.Repositories.Components;


namespace Bladex.Garantias.Infrastructure.RepositoryFramework
{
    /// <summary>
    /// Interface used to define a Repository of <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T"><see cref="T"/></typeparam>
    public interface IRepository<T> where T: EntityBase
    {
        /// <summary>
        /// Finds the by.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        T FindBy(object key);
        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        T Add(T item);
        /// <summary>
        /// Gets or sets the <see cref="T"/> with the specified key.
        /// </summary>
        T this[object key] { get; set; }
        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        void Remove(T item);
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        IList<T> GetAll();
        /// <summary>
        /// Counts this instance.
        /// </summary>
        /// <returns></returns>
        /// 
        long Count();

        //IPaged<T> GetAllPaged();
    }
}
