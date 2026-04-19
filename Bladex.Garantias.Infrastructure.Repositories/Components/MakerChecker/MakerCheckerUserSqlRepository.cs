using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker;
using Bladex.Garantias.DomainModel.Repositories.Components.MakerChecker;
using Bladex.Garantias.Infrastructure.Logging;
using Bladex.Garantias.Infrastructure.RepositoryFramework;

namespace Bladex.Garantias.Infrastructure.Repositories.Components.MakerChecker
{
    /// <summary>
    /// The maker checker user SQL repository class.
    /// </summary>
    public class MakerCheckerUserSqlRepository:  SqlRepositoryBase<MakerCheckerUser>, IMakerCheckerUserRepository
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MakerCheckerUserSqlRepository"/> class.
        /// </summary>
        public MakerCheckerUserSqlRepository()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MakerCheckerUserSqlRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work of type <see cref="Bladex.Garantias.Infrastructure.IUnitOfWork"/></param>
        public MakerCheckerUserSqlRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {

        }

        #endregion

        /// <summary>
        /// Get the base query to retrieve entities.
        /// </summary>
        /// <returns>SQL Select Query to retrieve entities</returns>
        protected override string GetBaseQuery()
        {
            return string.Format("SELECT * FROM {0} ", this.GetEntityName());
        }

        /// <summary>
        /// Get the where clause used to retrieve one entity.
        /// </summary>
        /// <returns>SQL Where Clases to retrieve one entity.</returns>
        protected override string GetBaseWhereClause()
        {
            return string.Concat(string.Format(" WHERE {0} ", this.GetKeyFieldName()), " = {0};");
        }

        /// <summary>
        /// Returns the entity table name.
        /// </summary>
        /// <returns>Entity Table Nombre</returns>
        protected override string GetEntityName()
        {
            return "APP_MAKERCHECKER_User";
        }

        /// <summary>
        /// Returns the Key of the entity
        /// </summary>
        /// <returns>Primary Key of the entity.</returns>
        protected override string GetKeyFieldName()
        {
            return MakerCheckerUserFactory.FieldNames.UserId;
        }

        /// <summary>
        /// Persists the new item.
        /// </summary>
        /// <param name="item">The item of type <see cref="Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker.MakerCheckerUser"/></param>
        /// <returns></returns>
        protected override MakerCheckerUser PersistNewItem(MakerCheckerUser item)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendFormat("INSERT INTO {0} ({1},{2},{3}) VALUES (@{1},@{2},@{3})", this.GetEntityName(), MakerCheckerUserFactory.FieldNames.UserId, MakerCheckerUserFactory.FieldNames.Email, MakerCheckerUserFactory.FieldNames.RoleId);
            using(DbCommand cmd = this.Database.GetSqlStringCommand(strBuilder.ToString()))
            {
                this.Database.AddInParameter(cmd, MakerCheckerUserFactory.FieldNames.UserId, System.Data.DbType.String, item.UserId);
                this.Database.AddInParameter(cmd, MakerCheckerUserFactory.FieldNames.Email, System.Data.DbType.String, item.Email);
                this.Database.AddInParameter(cmd, MakerCheckerUserFactory.FieldNames.RoleId, System.Data.DbType.Int32, DataHelper.GetSqlValue(item.Role));
                var affectedRows = this.Database.ExecuteNonQuery(cmd);
                if (affectedRows == 0) this._logger.Log(ApplicationLogger.LogType.Warning, "Insert operation with affected rows equal to zero.");
            }
            return item;
        }

        /// <summary>
        /// Persists the updated item.
        /// </summary>
        /// <param name="item">The item of type <see cref="Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker.MakerCheckerUser"/></param>
        /// <returns></returns>
        protected override MakerCheckerUser PersistUpdatedItem(MakerCheckerUser item)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendFormat("UPDATE {0} SET {1} = @{1}, {2} = @{2} WHERE {3} = @{3}", this.GetEntityName(), MakerCheckerUserFactory.FieldNames.Email, MakerCheckerUserFactory.FieldNames.RoleId, MakerCheckerUserFactory.FieldNames.UserId);
            using (DbCommand cmd = this.Database.GetSqlStringCommand(strBuilder.ToString()))
            {
                this.Database.AddInParameter(cmd, MakerCheckerUserFactory.FieldNames.UserId, System.Data.DbType.String, item.UserId);
                this.Database.AddInParameter(cmd, MakerCheckerUserFactory.FieldNames.Email, System.Data.DbType.String, item.Email);
                this.Database.AddInParameter(cmd, MakerCheckerUserFactory.FieldNames.RoleId, System.Data.DbType.Int32, DataHelper.GetSqlValue(item.Role));
                var affectedRows = this.Database.ExecuteNonQuery(cmd);
                if (affectedRows == 0) this._logger.Log(ApplicationLogger.LogType.Warning, "Update operation with affected rows equal to zero.");
            }
            return item;
        }

        /// <summary>
        /// Builds the child callbacks.
        /// </summary>
        protected override void BuildChildCallbacks()
        {
            this.ChildCallbacks.Add(MakerCheckerUserFactory.FieldNames.RoleId, this.AppendMakerCheckerRole);
        }

        /// <summary>
        /// Appends the maker checker role.
        /// </summary>
        /// <param name="entity">The entity of type <see cref="Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker.MakerCheckerUser"/></param>
        /// <param name="foreignKey">The foreign key of type <see cref="System.Object"/></param>
        private void AppendMakerCheckerRole(DomainModel.DomainBase.Components.MakerChecker.MakerCheckerUser entity, object foreignKey)
        {
            IMakerCheckerRoleRepository repository = RepositoryFactory.GetRepository<IMakerCheckerRoleRepository, MakerCheckerRole>(this.UnitOfWork);
            entity.Role = repository.FindBy(foreignKey);
        }
    }
}
