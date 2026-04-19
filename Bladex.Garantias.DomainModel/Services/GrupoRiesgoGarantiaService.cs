using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using IGrupoRiesgoGarantiaRepository = Bladex.Garantias.DomainModel.Repositories.IGrupoRiesgoGarantiaRepository;

namespace Bladex.Garantias.DomainModel.Services
{
    public class GrupoRiesgoGarantiaService
    {
        /// <summary>
        /// Returns all GrupoRiesgoGarantia.
        /// </summary>
        /// <returns>A <see cref="GrupoRiesgoGarantia"/> IList</returns>
        public IList<GrupoRiesgoGarantia> GetAll()
        {
            IGrupoRiesgoGarantiaRepository repository = RepositoryFactory.GetRepository<IGrupoRiesgoGarantiaRepository, GrupoRiesgoGarantia>();
            return repository.GetAll();
        }

        /// <summary>
        /// Return one GrupoRiesgoGarantia by Id
        /// </summary>
        /// <param name="grupoRiesgoGarantiaId">GrupoRiesgoGarantia Id</param>
        /// <returns>A <see cref="GarantiaOtra"/> entity.</returns>
        public GrupoRiesgoGarantia GetById(string grupoRiesgoGarantiaId)
        {
            IGrupoRiesgoGarantiaRepository repository = RepositoryFactory.GetRepository<IGrupoRiesgoGarantiaRepository, GrupoRiesgoGarantia>();
            return repository.FindBy(grupoRiesgoGarantiaId);
        }

        /// <summary>
        /// Devuelve una entidad representativa vacia. 
        /// Esto se debe utilizar cuando el id es "vacio" o invalido
        /// </summary>
        /// <returns></returns>
        public static GrupoRiesgoGarantia GetEmpty()
        {
            return new GrupoRiesgoGarantia() { Key = "NA", Nombre = "NA" };
        }
    }
}
