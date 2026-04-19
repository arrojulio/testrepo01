using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Infrastructure.Caching;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using ICalificacionesRiesgoRepository = Bladex.Garantias.DomainModel.Repositories.ICalificacionesRiesgoRepository;

namespace Bladex.Garantias.DomainModel.Services
{
    public class CalificacionesRiesgoService : ICacheableService
    {
        /// <summary>
        /// Returns all CalificacionesRiesgo.
        /// </summary>
        /// <returns>A <see cref="CalificacionesRiesgo"/> IList</returns>
        public IList<CalificacionesRiesgo> GetAll()
        {
            if (CacheManager.Instance.Contains(this.GetCacheKey()))
                return CacheManager.Instance.GetData<List<CalificacionesRiesgo>>(this.GetCacheKey());

            ICalificacionesRiesgoRepository repository = RepositoryFactory.GetRepository<ICalificacionesRiesgoRepository, CalificacionesRiesgo>();
            List<CalificacionesRiesgo> result = repository.GetAll().ToList();
            CacheManager.Instance.Add(this.GetCacheKey(), result, this.GetTimeSpan());
            return result;
        }

        /// <summary>
        /// Return one CalificacionesRiesgo by Id
        /// </summary>
        /// <param name="calificacionesRiesgoId">CalificacionesRiesgo Id</param>
        /// <returns>A <see cref="CalificacionesRiesgo"/> entity.</returns>
        public CalificacionesRiesgo GetById(string calificacionesRiesgoId)
        {
            string cacheKey = this.GetCacheKey() + "_" + calificacionesRiesgoId;
            if (!CacheManager.Instance.Contains(cacheKey))
            {
                ICalificacionesRiesgoRepository repository = RepositoryFactory.GetRepository<ICalificacionesRiesgoRepository, CalificacionesRiesgo>();
                CacheManager.Instance.Add(cacheKey, repository.FindBy(calificacionesRiesgoId), this.GetTimeSpan());
            }
            return CacheManager.Instance.GetData<CalificacionesRiesgo>(cacheKey);
        }

        public CalificacionesRiesgo GetByMoodys(string Moodys)
        {
            return GetAll().FirstOrDefault(o => o.Moodys == Moodys);
        }

        /// <summary>
        /// Devuelve una entidad representativa vacia.
        /// Esto se debe utilizar cuando el id es "vacio" o invalido
        /// </summary>
        /// <returns></returns>
        public static CalificacionesRiesgo GetEmpty()
        {
            return new CalificacionesRiesgo() { Key = "NA", Fitch = "NA", Moodys = "NA", Orden = 0, SnP = "NA" };
        }

        #region ICacheableService Members

        public string GetCacheKey()
        {
            return "CalificacionesRiesgoService.GetAll()";
        }

        public TimeSpan GetTimeSpan()
        {
            return new TimeSpan(1, 0, 0);
        }

        #endregion
    }
}
