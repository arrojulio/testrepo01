using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using IAseguradorasRepository = Bladex.Garantias.DomainModel.Repositories.IAseguradorasRepository;

namespace Bladex.Garantias.DomainModel.Services
{
    public class AseguradorasService
    {
        /// <summary>
        /// Returns all Aseguradoras.
        /// </summary>
        /// <returns>A <see cref="Aseguradoras"/> IList</returns>
        public IList<Aseguradoras> GetAll()
        {
            IAseguradorasRepository repository = RepositoryFactory.GetRepository<IAseguradorasRepository, Aseguradoras>();
            return repository.GetAll();
        }

        /// <summary>
        /// Return one Aseguradora by Id
        /// </summary>
        /// <param name="aseguradoraId">Aseguradora Id</param>
        /// <returns>A <see cref="Aseguradoras"/> entity.</returns>
        public Aseguradoras GetById(string aseguradoraId)
        {
            IAseguradorasRepository repository = RepositoryFactory.GetRepository<IAseguradorasRepository, Aseguradoras>();
            return repository.FindBy(aseguradoraId);
        }

        /// <summary>
        /// Devuelve una entidad representativa vacia. 
        /// Esto se debe utilizar cuando el id es "vacio" o invalido
        /// </summary>
        /// <returns></returns>
        public static Aseguradoras GetEmpty()
        {
            return new Aseguradoras() { Key = "NA", Nombre = "NA" };
        }
    }
}
