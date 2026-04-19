using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using Bladex.Garantias.DomainModel.Repositories;

namespace Bladex.Garantias.DomainModel.Services
{
    public class TipoPolizaService
    {
        public TipoPolizaService():this(RepositoryFactory.GetRepository<ITipoPolizaRepository, TipoPoliza>())
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleService"/> class.
        /// </summary>
        /// <param name="roleRepository">The role repository of type <see cref="Bladex.Garantias.DomainModel.Repositories.IRoleRepository"/></param>
        public TipoPolizaService(ITipoPolizaRepository tipoPolizaRepository)
        {
            this.TipoPolizaRepository = tipoPolizaRepository;
        }

        /// <summary>
        ///   <see cref="Bladex.Garantias.DomainModel.Repositories.IRoleRepository"/>
        /// </summary>
        protected readonly ITipoPolizaRepository TipoPolizaRepository;

        /// <summary>
        /// Returns all Roles.
        /// </summary>
        /// <returns>A <see cref="Role"/> IList</returns>
        public IList<TipoPoliza> GetAll()
        {
            return this.TipoPolizaRepository.GetAll();
        }

        /// <summary>
        /// Return one Role by Id
        /// </summary>
        /// <param name="roleId">Role Id</param>
        /// <returns>A <see cref="Role"/> entity.</returns>
        public TipoPoliza GetById(string tipoPolizaId)
        {
            return this.TipoPolizaRepository.FindBy(tipoPolizaId);
        }
    }
}
