using System.Text;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.DomainModel.Services;
using Bladex.Garantias.Infrastructure.DomainBase;
using Bladex.Garantias.Infrastructure.Repositories.Role;
using Bladex.Garantias.Infrastructure.RepositoryFramework;

namespace Bladex.Garantias.Infrastructure.Repositories.User
{
    /// <summary>
    /// The user SQL repository class.
    /// </summary>
    public class UserSqlRepository : SqlRepositoryBase<DomainModel.DomainBase.User>, IUserRepository
    {
        #region Private Members

        #endregion

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UserSqlRepository"/> class.
        /// </summary>
        public UserSqlRepository()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserSqlRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work of type <see cref="Bladex.Garantias.Infrastructure.IUnitOfWork"/></param>
        public UserSqlRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        #endregion

        #region IRepository Members

        #endregion

        #region BuildChildCallbacks

        /// <summary>
        /// Builds the child callbacks.
        /// </summary>
        protected override void BuildChildCallbacks()
        {
            this.ChildCallbacks.Add(UserFactory.FieldNames.RoleId, this.AppendRole);
        }

        /// <summary>
        /// Appends the role.
        /// </summary>
        /// <param name="entity">The entity of type <see cref="Bladex.Garantias.DomainModel.DomainBase.User"/></param>
        /// <param name="roleId">The role id of type <see cref="System.Object"/></param>
        private void AppendRole(DomainModel.DomainBase.User entity, object roleId)
        {
            IRoleRepository repository = RepositoryFactory.GetRepository<IRoleRepository, DomainModel.DomainBase.Role>(this.UnitOfWork);
            entity.Role = repository.FindBy(roleId) ?? RoleService.GetEmpty();
        }

        #endregion

        #region Unit of Work Implementation

        /// <summary>
        /// Persists the new item.
        /// </summary>
        /// <param name="item">The item of type <see cref="Bladex.Garantias.Infrastructure.DomainBase.EntityBase"/></param>
        /// <returns></returns>
        public override EntityBase PersistNewItem(EntityBase item)
        {
            var entity = item as DomainModel.DomainBase.User;
            return entity != null ? this.PersistNewItem(entity) : item;
        }

        /// <summary>
        /// Persists the updated item.
        /// </summary>
        /// <param name="item">The item of type <see cref="Bladex.Garantias.Infrastructure.DomainBase.EntityBase"/></param>
        /// <returns></returns>
        public override EntityBase PersistUpdatedItem(EntityBase item)
        {
            var entity = item as DomainModel.DomainBase.User;
            return entity != null ? this.PersistUpdatedItem(entity) : item;
        }

        /// <summary>
        /// Persists the deleted item.
        /// </summary>
        /// <param name="item">The item of type <see cref="Bladex.Garantias.Infrastructure.DomainBase.EntityBase"/></param>
        public override void PersistDeletedItem(EntityBase item)
        {
            var entity = item as DomainModel.DomainBase.User;
            if (entity != null)
            {
                this.PersistDeletedItem(entity);
            }
        }

        /// <summary>
        /// Persists the new item.
        /// </summary>
        /// <param name="item">The item of type <see cref="Bladex.Garantias.DomainModel.DomainBase.User"/></param>
        /// <returns></returns>
        protected override DomainModel.DomainBase.User PersistNewItem(DomainModel.DomainBase.User item)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Format("INSERT INTO {0} ({1},{2},{3}) ",
                this.GetEntityName(),
                UserFactory.FieldNames.UserId,
                UserFactory.FieldNames.UserName,
                UserFactory.FieldNames.RoleId
                ));
            builder.Append(string.Format("VALUES ({0},{1},{2})",
                DataHelper.GetSqlValue(item.Key),
                DataHelper.GetSqlValue(item.UserName),
                DataHelper.GetSqlValue(item.RoleId)
                ));
            this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(builder.ToString()));
            return item;
        }

        /// <summary>
        /// Persists the updated item.
        /// </summary>
        /// <param name="item">The item of type <see cref="Bladex.Garantias.DomainModel.DomainBase.User"/></param>
        /// <returns></returns>
        protected override DomainModel.DomainBase.User PersistUpdatedItem(DomainModel.DomainBase.User item)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("UPDATE {0} SET ", this.GetEntityName());

            builder.Append(string.Format("{0} = {1}",
                UserFactory.FieldNames.UserName,
                DataHelper.GetSqlValue(item.UserName)));
            builder.Append(string.Format(",{0} = {1}",
                UserFactory.FieldNames.RoleId,
                DataHelper.GetSqlValue(item.RoleId)));
            builder.Append(" ");
            builder.Append(this.BuildBaseWhereClause(item.Key));

            this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(builder.ToString()));
            return item;
        }

        /// <summary>
        /// Persists the deleted item.
        /// </summary>
        /// <param name="item">The item of type <see cref="Bladex.Garantias.DomainModel.DomainBase.User"/></param>
        protected override void PersistDeletedItem(DomainModel.DomainBase.User item)
        {
            // We could delete related objects here, and then, call the base method to delete the entity.
            base.PersistDeletedItem(item);
        }

        #endregion

        #region Private Callback and Helper Methods
       
        #endregion

        /// <summary>
        /// Get the base query to retrieve entities.
        /// </summary>
        /// <returns>SQL Select Query to retrieve entities</returns>
        protected override string GetBaseQuery()
        {
            return string.Format("SELECT * FROM [{0}] P ", this.GetEntityName());
        }

        /// <summary>
        /// Get the where clause used to retrieve one entity.
        /// </summary>
        /// <returns>SQL Where Clases to retrieve one entity.</returns>
        protected override string GetBaseWhereClause()
        {
            return " WHERE P.["+ UserFactory.FieldNames.UserId +"] = {0};";
        }

        /// <summary>
        /// Returns the entity table name.
        /// </summary>
        /// <returns>Entity Table Nombre</returns>
        protected override string GetEntityName()
        {
            return UserFactory.TableName;
        }

        /// <summary>
        /// Returns the Key of the entity
        /// </summary>
        /// <returns>Primary Key of the entity.</returns>
        protected override string GetKeyFieldName()
        {
            return UserFactory.FieldNames.UserId;
        }
    }
}
