using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using ITipoGarantiaSuperRepository = Bladex.Garantias.DomainModel.Repositories.ITipoGarantiaSuperRepository;

namespace Bladex.Garantias.DomainModel.Services
{
    /// <summary>
    /// The role service class.
    /// </summary>
    public class RoleService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleService"/> class.
        /// </summary>
        public RoleService():this(RepositoryFactory.GetRepository<IRoleRepository, Role>())
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleService"/> class.
        /// </summary>
        /// <param name="roleRepository">The role repository of type <see cref="Bladex.Garantias.DomainModel.Repositories.IRoleRepository"/></param>
        public RoleService(IRoleRepository roleRepository)
        {
            this.RoleRepository = roleRepository;
        }

        /// <summary>
        ///   <see cref="Bladex.Garantias.DomainModel.Repositories.IRoleRepository"/>
        /// </summary>
        protected readonly IRoleRepository RoleRepository;

        /// <summary>
        /// Returns all Roles.
        /// </summary>
        /// <returns>A <see cref="Role"/> IList</returns>
        public IList<Role> GetAll()
        {
            return this.RoleRepository.GetAll();
        }

        /// <summary>
        /// Return one Role by Id
        /// </summary>
        /// <param name="roleId">Role Id</param>
        /// <returns>A <see cref="Role"/> entity.</returns>
        public Role GetById(string roleId)
        {
            return this.RoleRepository.FindBy(roleId);
        }

        /// <summary>
        /// Devuelve una entidad representativa vacia. 
        /// Esto se debe utilizar cuando el id es "vacio" o invalido
        /// </summary>
        /// <returns></returns>
        public static Role GetEmpty()
        {
            return new Role() { Key = (int)Role.AvailableRoles.ReadOnly, RoleName = "Read-Only"  };
        }

    }
}
