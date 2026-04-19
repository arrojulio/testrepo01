using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using IAvaluadorasRepository = Bladex.Garantias.DomainModel.Repositories.IAvaluadorasRepository;

namespace Bladex.Garantias.DomainModel.Services
{
    public class AvaluadoraService
    {
        /// <summary>
        /// Returns all Avaluadoras.
        /// </summary>
        /// <returns>A <see cref="Bancos"/> IList</returns>
        public IList<Avaluadoras> GetAll()
        {
            IAvaluadorasRepository repository = RepositoryFactory.GetRepository<IAvaluadorasRepository, Avaluadoras>();
            return repository.GetAll();
        }

        /// <summary>
        /// Return one Avaluadoras by Id
        /// </summary>
        /// <param name="avaluadoraId">Avaluadora Id</param>
        /// <returns>A <see cref="Avaluadoras"/> entity.</returns>
        public Avaluadoras GetById(string avaluadoraId)
        {
            IAvaluadorasRepository repository = RepositoryFactory.GetRepository<IAvaluadorasRepository, Avaluadoras>();
            return repository.FindBy(avaluadoraId);
        }

        /// <summary>
        /// Devuelve una entidad representativa vacia. 
        /// Esto se debe utilizar cuando el id es "vacio" o invalido
        /// </summary>
        /// <returns></returns>
        public static Avaluadoras GetEmpty()
        {
            return new Avaluadoras() { Key = "NA", Nombre = "NA" };
        }
    }
}
