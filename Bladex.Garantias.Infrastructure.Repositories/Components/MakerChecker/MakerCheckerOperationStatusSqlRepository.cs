using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker;
using Bladex.Garantias.DomainModel.Repositories.Components.MakerChecker;
using Bladex.Garantias.Infrastructure.Logging;

namespace Bladex.Garantias.Infrastructure.Repositories.Components.MakerChecker
{
    /// <summary>
    /// The maker checker user SQL repository class.
    /// </summary>
    public class MakerCheckerOperationStatusSqlRepository:  SqlRepositoryBase<MakerCheckerOperationStatus>, IMakerCheckerOperationStatusRepository
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MakerCheckerRoleSqlRepository"/> class.
        /// </summary>
        public MakerCheckerOperationStatusSqlRepository()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MakerCheckerRoleSqlRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work of type <see cref="Bladex.Garantias.Infrastructure.IUnitOfWork"/></param>
        public MakerCheckerOperationStatusSqlRepository(IUnitOfWork unitOfWork) 
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
            return "APP_MAKERCHECKER_OperationStatus";
        }

        /// <summary>
        /// Returns the Key of the entity
        /// </summary>
        /// <returns>Primary Key of the entity.</returns>
        protected override string GetKeyFieldName()
        {
            return MakerCheckerOperationStatusFactory.FieldNames.OperationStatusId;
        }

        protected override void BuildChildCallbacks()
        {
        }

        protected override MakerCheckerOperationStatus PersistNewItem(MakerCheckerOperationStatus item)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendFormat("INSERT INTO {0} ({1}) VALUES (@{1}) SELECT @@IDENTITY", this.GetEntityName(), MakerCheckerOperationStatusFactory.FieldNames.OperationStatusDescription);
            using (DbCommand cmd = this.Database.GetSqlStringCommand(strBuilder.ToString()))
            {
                this.Database.AddInParameter(cmd, MakerCheckerOperationStatusFactory.FieldNames.OperationStatusDescription, System.Data.DbType.String, item.OperationStatusDescription);
                var affectedRows = this.Database.ExecuteScalar(cmd);
                item.Key = Convert.ToInt32(affectedRows);
            }
            return item;
        }

        protected override MakerCheckerOperationStatus PersistUpdatedItem(MakerCheckerOperationStatus item)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendFormat("UPDATE {0} SET {1} = @{1} WHERE {2} = @{2}", this.GetEntityName(), MakerCheckerOperationStatusFactory.FieldNames.OperationStatusDescription, MakerCheckerOperationStatusFactory.FieldNames.OperationStatusId);
            using (DbCommand cmd = this.Database.GetSqlStringCommand(strBuilder.ToString()))
            {
                this.Database.AddInParameter(cmd, MakerCheckerOperationStatusFactory.FieldNames.OperationStatusId, System.Data.DbType.Int32, DataHelper.GetSqlValue(item.OperationStatusId));
                this.Database.AddInParameter(cmd, MakerCheckerOperationStatusFactory.FieldNames.OperationStatusDescription, System.Data.DbType.String, item.OperationStatusDescription);
                var affectedRows = this.Database.ExecuteNonQuery(cmd);
                if (affectedRows == 0) this._logger.Log(ApplicationLogger.LogType.Warning, "Update operation with affected rows equal to zero.");
            }
            return item;
        }
    }
}
