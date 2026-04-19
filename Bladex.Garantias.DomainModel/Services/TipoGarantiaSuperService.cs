using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using ITipoGarantiaSuperRepository = Bladex.Garantias.DomainModel.Repositories.ITipoGarantiaSuperRepository;

namespace Bladex.Garantias.DomainModel.Services
{
    public class TipoGarantiaSuperService
    {
        /// <summary>
        /// Returns all TipoGarantiaSuper.
        /// </summary>
        /// <returns>A <see cref="TipoGarantiaSuper"/> IList</returns>
        public IList<TipoGarantiaSuper> GetAll(string CategoriaSuperId)
        {
            ITipoGarantiaSuperRepository repository = RepositoryFactory.GetRepository<ITipoGarantiaSuperRepository, TipoGarantiaSuper>();
            var list = repository.GetAll();
            return list.Where(o => o.Categoria.Key.ToString() == CategoriaSuperId).ToList();
        }

        public TipoGarantiaSuper GetFianzasYAvalesNoBancarios()
        {
            // Id del tipo de garantia super "Fianzas y Avales No Bancarios".
            string tipoGarantiaSuperId = "0605";
            ITipoGarantiaSuperRepository repository = RepositoryFactory.GetRepository<ITipoGarantiaSuperRepository, TipoGarantiaSuper>();
            return repository.FindBy(tipoGarantiaSuperId);
        }

        /// <summary>
        /// Return one TipoGarantiaSuper by Id
        /// </summary>
        /// <param name="tipoGarantiaSuperId">TipoGarantiaSuper Id</param>
        /// <returns>A <see cref="TipoGarantiaSuper"/> entity.</returns>
        public TipoGarantiaSuper GetById(string tipoGarantiaSuperId)
        {
            ITipoGarantiaSuperRepository repository = RepositoryFactory.GetRepository<ITipoGarantiaSuperRepository, TipoGarantiaSuper>();
            return repository.FindBy(tipoGarantiaSuperId);
        }

        /// <summary>
        /// Return one TipoGarantiaSuper by Name
        /// </summary>
        /// <param name="tipoGarantiaSuperId">TipoGarantiaSuper Id</param>
        /// <returns>A <see cref="TipoGarantiaSuper"/> entity.</returns>
        public TipoGarantiaSuper GetByName(string tipoGarantiaSuperName)
        {
            ITipoGarantiaSuperRepository repository = RepositoryFactory.GetRepository<ITipoGarantiaSuperRepository, TipoGarantiaSuper>();
            return repository.GetAll().Where(o=>o.Nombre== tipoGarantiaSuperName).FirstOrDefault();
        }

        /// <summary>
        /// Devuelve una entidad representativa vacia. 
        /// Esto se debe utilizar cuando el id es "vacio" o invalido
        /// </summary>
        /// <returns></returns>
        public static TipoGarantiaSuper GetEmpty(CategoriaSuper categoriaSuper)
        {
            return new TipoGarantiaSuper() { Key = categoriaSuper.GetKeyAs<string>() +  "NA", Nombre = "N/A" };
        }

    }
}
