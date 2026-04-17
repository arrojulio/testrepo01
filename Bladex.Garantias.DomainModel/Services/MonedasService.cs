using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using IMonedasRepository = Bladex.Garantias.DomainModel.Repositories.IMonedasRepository;

namespace Bladex.Garantias.DomainModel.Services
{
    public class MonedasService
    {
        /// <summary>
        /// Returns all Monedas.
        /// </summary>
        /// <returns>A <see cref="Monedas"/> IList</returns>
        public IList<Monedas> GetAll()
        {
            IMonedasRepository repository = RepositoryFactory.GetRepository<IMonedasRepository, Monedas>();
            return repository.GetAll();
        }

        /// <summary>
        /// Return one Monedas by Id
        /// </summary>
        /// <param name="monedasId">Monedas Id</param>
        /// <returns>A <see cref="Monedas"/> entity.</returns>
        public Monedas GetById(string monedasId)
        {
            IMonedasRepository repository = RepositoryFactory.GetRepository<IMonedasRepository, Monedas>();
            return repository.FindBy(monedasId);
        }

        /// <summary>
        /// Devuelve una entidad representativa vacia. 
        /// Esto se debe utilizar cuando el id es "vacio" o invalido
        /// </summary>
        /// <returns></returns>
        public static Monedas GetEmpty()
        {
            return new Monedas() { Key = "NA", Nombre = "NA" };
        }
    }
}
