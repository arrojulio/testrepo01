using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.Infrastructure.RepositoryFramework;

namespace Bladex.Garantias.DomainModel.Services
{
    public class CategoriaRiesgoGarantiaService
    {
        private ICategoriaRiesgoGarantiaRepository repository = RepositoryFactory.GetRepository<ICategoriaRiesgoGarantiaRepository, CategoriaRiesgoGarantia>();
        /// <summary>
        /// Returns all CategoriaSuper.
        /// </summary>
        /// <returns>A <see cref="CategoriaRiesgoGarantia"/> IList</returns>
        public IList<CategoriaRiesgoGarantia> GetAll()
        {
            return repository.GetAll();
        }

        /// <summary>
        /// Return one CategoriaRiesgoGarantia by Id
        /// </summary>
        /// <param name="categoriaSuperId">CategoriaRiesgoGarantia Id</param>
        /// <returns>A <see cref="CategoriaRiesgoGarantia"/> entity.</returns>
        public CategoriaRiesgoGarantia GetById(string categoriaSuperId)
        {
            return repository.FindBy(categoriaSuperId);
        }

        /// <summary>
        /// Return one CategoriaRiesgoGarantia by Name
        /// </summary>
        /// <param name="categoriaSuperId">CategoriaRiesgoGarantia Name</param>
        /// <returns>A <see cref="CategoriaRiesgoGarantia"/> entity.</returns>
        public CategoriaRiesgoGarantia GetByName(string categoriaSuperName)
        {
            return repository.GetAll().Where(o => o.Nombre == categoriaSuperName).FirstOrDefault();
        }
        /// <summary>
        /// Devuelve una entidad representativa vacia. 
        /// Esto se debe utilizar cuando el id es "vacio" o invalido
        /// </summary>
        /// <returns></returns>
        public static CategoriaRiesgoGarantia GetEmpty()
        {
            return new CategoriaRiesgoGarantia() { Key = "NA", Nombre = "NA" };
        }
    }
}
