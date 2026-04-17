using System;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;

namespace Bladex.Garantias.Infrastructure.Caching
{
    /// <summary>
    /// Clase Singleton encargada de controlar los accesos al cache implementando <see cref="ICacheManager"/>
    /// </summary>
    public sealed class CacheManager : ICacheManager
    {
        private static volatile CacheManager instance;
        private static object syncRoot = new object();

        /// <summary>
        /// Variable de acceso a Singleton
        /// </summary>
        public static CacheManager Instance
        {
            get
            {
                // Use 'Lazy initialization'
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (null == instance)
                            instance = new CacheManager();
                    }

                }
                return instance;
            }
        }

        /// <summary>
        /// Añade un elemento al cache con un tiempo de vida de 10 minutos.
        /// </summary>
        /// <param name="key">Key del elemento del tipo <see cref="String"/></param>
        /// <param name="value">Elemento del tipo <see cref="Object"/></param>
        public void Add(string key, object value)
        {
            this.Add(key, value, new TimeSpan(0, 10, 0));
        }

        /// <summary>
        /// Añade un elemento al cache con un tiempo de vida especificado en <paramref name="slidingTime"/>
        /// </summary>
        /// <param name="key">Key del elemento del tipo <see cref="String"/></param>
        /// <param name="value">Elemento del tipo <see cref="Object"/></param>
        /// <param name="slidingTime">Tiempo de vida del elemento del tipo <see cref="TimeSpan"/></param>
        public void Add(string key, object value, TimeSpan slidingTime)
        {
            Microsoft.Practices.EnterpriseLibrary.Caching.CacheManager cache = CacheFactory.GetCacheManager();
            if (cache != null)
            {
                cache.Add(key, value, CacheItemPriority.Normal, null, new SlidingTime(slidingTime));
            }
        }

        /// <summary>
        /// Determina si un elemento existe en el cache o no dado su <paramref name="key"/>
        /// </summary>
        /// <param name="key">Key del elemento del tipo <see cref="String"/></param>
        /// <returns><example>True si el elemento existe. False si el elemento no existe.</example></returns>
        public bool Contains(string key)
        {
            Microsoft.Practices.EnterpriseLibrary.Caching.CacheManager cache = CacheFactory.GetCacheManager();
            if (cache == null) return false;
            return cache.Contains(key) && cache.GetData(key) != null;
        }

        /// <summary>
        /// Removes the specified key from the cache.
        /// </summary>
        /// <param name="key">The key.</param>
        public void Remove(string key)
        {
            Microsoft.Practices.EnterpriseLibrary.Caching.CacheManager cache = CacheFactory.GetCacheManager();
            if (cache == null) return;
            cache.Remove(key);
        }

        /// <summary>
        /// Metodo genérico que devuelve el elemento del cache convirtiendolo a <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> que especifica el tipo a retornar.</typeparam>
        /// <param name="key">Key del elemento del tipo <see cref="String"/></param>
        /// <returns>Elemento del tipo <typeparamref name="T"/> almacenado en el cache.</returns>
        public T GetData<T>(string key) where T : class
        {
            Microsoft.Practices.EnterpriseLibrary.Caching.CacheManager cache = CacheFactory.GetCacheManager();
            if (cache == null) return default(T);
            return (T)cache.GetData(key);
        }

        /// <summary>
        /// Metodo que devuelve el elemento del cache.
        /// </summary>
        /// <param name="key">Key del elemento del tipo <see cref="String"/></param>
        /// <returns><see cref="Object"/> almacenado en el cache.</returns>
        public object GetData(string key)
        {
            Microsoft.Practices.EnterpriseLibrary.Caching.CacheManager cache = CacheFactory.GetCacheManager();
            if (cache == null) return null;
            return cache.GetData(key);
        }


    }
}
