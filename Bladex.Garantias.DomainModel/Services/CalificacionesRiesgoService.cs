using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using ICalificacionesRiesgoRepository = Bladex.Garantias.DomainModel.Repositories.ICalificacionesRiesgoRepository;

namespace Bladex.Garantias.DomainModel.Services
{
    public class CalificacionesRiesgoService
    {
        /// <summary>
        /// Returns all CalificacionesRiesgo.
        /// </summary>
        /// <returns>A <see cref="CalificacionesRiesgo"/> IList</returns>
        public IList<CalificacionesRiesgo> GetAll()
        {
            ICalificacionesRiesgoRepository repository = RepositoryFactory.GetRepository<ICalificacionesRiesgoRepository, CalificacionesRiesgo>();
            return repository.GetAll();
        }

        /// <summary>
        /// Return one CalificacionesRiesgo by Id
        /// </summary>
        /// <param name="calificacionesRiesgoId">CalificacionesRiesgo Id</param>
        /// <returns>A <see cref="CalificacionesRiesgo"/> entity.</returns>
        public CalificacionesRiesgo GetById(string calificacionesRiesgoId)
        {
            ICalificacionesRiesgoRepository repository = RepositoryFactory.GetRepository<ICalificacionesRiesgoRepository, CalificacionesRiesgo>();
            return repository.FindBy(calificacionesRiesgoId);
        }

        /// <summary>
        /// Return one CalificacionesRiesgo by Id
        /// </summary>
        /// <param name="calificacionesRiesgoId">CalificacionesRiesgo Id</param>
        /// <returns>A <see cref="CalificacionesRiesgo"/> entity.</returns>
        public CalificacionesRiesgo GetByMoodys(string Moodys)
        {
            ICalificacionesRiesgoRepository repository = RepositoryFactory.GetRepository<ICalificacionesRiesgoRepository, CalificacionesRiesgo>();
            return repository.GetAll().Where(o => o.Moodys == Moodys).FirstOrDefault();
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
    }
}
