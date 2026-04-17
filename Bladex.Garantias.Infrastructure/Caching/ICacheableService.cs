using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bladex.Garantias.Infrastructure.Caching
{
    /// <summary>
    /// Interfaz que determina la funcionalidad de un servicio que implementa caching.
    /// </summary>
    public interface ICacheableService
    {
        /// <summary>
        /// Returns the key used to store an item into the cache.
        /// </summary>
        /// <returns></returns>
        string GetCacheKey();
        /// <summary>
        /// Returns the timespan representing the time that the object will be stored into the cache.
        /// </summary>
        /// <returns></returns>
        TimeSpan GetTimeSpan();

    }
}
