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
    /// The user service class.
    /// </summary>
    public class UserService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        public UserService():this(RepositoryFactory.GetRepository<IUserRepository, User>())
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository of type <see cref="Bladex.Garantias.DomainModel.Repositories.IUserRepository"/></param>
        public UserService(IUserRepository userRepository)
        {
            this.UserRepository = userRepository;
        }

        /// <summary>
        ///   <see cref="Bladex.Garantias.DomainModel.Repositories.IUserRepository"/>
        /// </summary>
        protected readonly IUserRepository UserRepository;

        /// <summary>
        /// Returns all Users.
        /// </summary>
        /// <returns>A <see cref="User"/> IList</returns>
        public IList<User> GetAll()
        {
            return this.UserRepository.GetAll();
        }

        /// <summary>
        /// Return one User by Id
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>A <see cref="User"/> entity.</returns>
        public User GetById(string userId)
        {
            return this.UserRepository.FindBy(userId);
        }

        /// <summary>
        /// Devuelve una entidad representativa vacia. 
        /// Esto se debe utilizar cuando el id es "vacio" o invalido
        /// </summary>
        /// <returns></returns>
        public static User GetEmpty()
        {
            return new User() { Key = "guest", Role = RoleService.GetEmpty(), UserName = "Guest Users" };
        }

    }
}
