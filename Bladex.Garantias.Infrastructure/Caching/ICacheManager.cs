using System;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;

namespace Bladex.Garantias.Infrastructure.Caching
{
    /// <summary>
    /// Interfaz que determina la funcionalidad a implementar por un cache manager.
    /// </summary>
    interface ICacheManager
    {

        void Add(string key, object value);
        void Add(string key, object value, TimeSpan slidingTime);
        void Remove(string key);
        bool Contains(string key);
        object GetData(string key);
        /// <summary>
        /// Generic method to retrieve objects from the cache.
        /// </summary>
        /// <typeparam name="T">Type to retrive</typeparam>
        /// <param name="key">Key of the item.</param>
        /// <returns><see cref="T"/> item</returns>
        T GetData<T>(string key) where T: class;
    }
}
