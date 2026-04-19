using System.Collections.Generic;
using System.Text;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.Infrastructure.DomainBase;

namespace Bladex.Garantias.Infrastructure.Repositories.Role
{
    /// <summary>
    /// The role SQL repository class.
    /// </summary>
    public class RoleSqlRepository : SqlRepositoryBase<DomainModel.DomainBase.Role>, IRoleRepository
    {
        #region Private Members

        #endregion

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleSqlRepository"/> class.
        /// </summary>
        public RoleSqlRepository()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleSqlRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work of type <see cref="Bladex.Garantias.Infrastructure.IUnitOfWork"/></param>
        public RoleSqlRepository(IUnitOfWork unitOfWork) 
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
            var entity = item as DomainModel.DomainBase.Role;
            return entity != null ? this.PersistNewItem(entity) : item;
        }

        /// <summary>
        /// Persists the updated item.
        /// </summary>
        /// <param name="item">The item of type <see cref="Bladex.Garantias.Infrastructure.DomainBase.EntityBase"/></param>
        /// <returns></returns>
        public override EntityBase PersistUpdatedItem(EntityBase item)
        {
            var entity = item as DomainModel.DomainBase.Role;
            return entity != null ? this.PersistUpdatedItem(entity) : item;
        }

        /// <summary>
        /// Persists the deleted item.
        /// </summary>
        /// <param name="item">The item of type <see cref="Bladex.Garantias.Infrastructure.DomainBase.EntityBase"/></param>
        public override void PersistDeletedItem(EntityBase item)
        {
            var entity = item as DomainModel.DomainBase.Role;
            if (entity != null)
            {
                this.PersistDeletedItem(entity);
            }
        }

        /// <summary>
        /// Persists the new item.
        /// </summary>
        /// <param name="item">The item of type <see cref="Bladex.Garantias.DomainModel.DomainBase.Role"/></param>
        /// <returns></returns>
        protected override DomainModel.DomainBase.Role PersistNewItem(DomainModel.DomainBase.Role item)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Format("INSERT INTO {0} ({1},{2}) ",
                this.GetEntityName(),
                RoleFactory.FieldNames.RoleId,
                RoleFactory.FieldNames.RoleName
                ));
            builder.Append(string.Format("VALUES ({0},{1})",
                DataHelper.GetSqlValue(item.Key),
                DataHelper.GetSqlValue(item.RoleName)
                ));
            this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(builder.ToString()));
            return item;
        }

        /// <summary>
        /// Persists the updated item.
        /// </summary>
        /// <param name="item">The item of type <see cref="Bladex.Garantias.DomainModel.DomainBase.Role"/></param>
        /// <returns></returns>
        protected override DomainModel.DomainBase.Role PersistUpdatedItem(DomainModel.DomainBase.Role item)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("UPDATE {0} SET ", this.GetEntityName());

            builder.Append(string.Format("{0} = {1}",
                RoleFactory.FieldNames.RoleName,
                DataHelper.GetSqlValue(item.RoleName)));
            builder.Append(" ");
            builder.Append(this.BuildBaseWhereClause(item.Key));

            this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(builder.ToString()));
            return item;
        }

        /// <summary>
        /// Persists the deleted item.
        /// </summary>
        /// <param name="item">The item of type <see cref="Bladex.Garantias.DomainModel.DomainBase.Role"/></param>
        protected override void PersistDeletedItem(DomainModel.DomainBase.Role item)
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
            return " WHERE P.[" + RoleFactory.FieldNames.RoleId + "] = {0};";
        }

        /// <summary>
        /// Returns the entity table name.
        /// </summary>
        /// <returns>Entity Table Nombre</returns>
        protected override string GetEntityName()
        {
            return RoleFactory.TableName;
        }

        /// <summary>
        /// Returns the Key of the entity
        /// </summary>
        /// <returns>Primary Key of the entity.</returns>
        protected override string GetKeyFieldName()
        {
            return RoleFactory.FieldNames.RoleId;
        }
    }
}
