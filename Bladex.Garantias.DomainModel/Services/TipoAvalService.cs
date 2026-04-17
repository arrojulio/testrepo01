using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.Infrastructure.RepositoryFramework;

namespace Bladex.Garantias.DomainModel.Services
{
    public class TipoAvalService
    {
        /// <summary>
        /// Returns all tipo aval.
        /// </summary>
        /// <returns>A <see cref="TipoAval"/> IList</returns>
        public IList<TipoAval> GetAll()
        {
            ITipoAvalRepository repository = RepositoryFactory.GetRepository<ITipoAvalRepository, TipoAval>();
            return repository.GetAll();
        }

        /// <summary>
        /// Return one tipo aval by Id
        /// </summary>
        /// <param name="tipoAvalId">Tipo Aval Id</param>
        /// <returns>A <see cref="TipoAval"/> entity.</returns>
        public TipoAval GetById(string tipoAvalId)
        {
            ITipoAvalRepository repository = RepositoryFactory.GetRepository<ITipoAvalRepository, TipoAval>();
            return repository.FindBy(tipoAvalId);
        }

        /// <summary>
        /// Devuelve una entidad representativa vacia. 
        /// Esto se debe utilizar cuando el id es "vacio" o invalido
        /// </summary>
        /// <returns></returns>
        public static TipoAval GetEmpty()
        {
            return new TipoAval() { Key = "NA", Nombre = "NA" };
        }

    }
}
