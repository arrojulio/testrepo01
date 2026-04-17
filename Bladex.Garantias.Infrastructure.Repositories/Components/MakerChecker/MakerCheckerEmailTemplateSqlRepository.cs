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
    public class MakerCheckerEmailTemplateSqlRepository:  SqlRepositoryBase<MakerCheckerEmailTemplate>, IMakerCheckerEmailTemplateRepository
    {

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MakerCheckerEmailTemplateSqlRepository"/> class.
        /// </summary>
        public MakerCheckerEmailTemplateSqlRepository()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MakerCheckerEmailTemplateSqlRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work of type <see cref="Bladex.Garantias.Infrastructure.IUnitOfWork"/></param>
        public MakerCheckerEmailTemplateSqlRepository(IUnitOfWork unitOfWork) 
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
        /// Get the where clause used to retrieve entities by role.
        /// </summary>
        /// <returns>SQL Where Clases to retrieve entities by role.</returns>
        protected string GetRoleWhereClause()
        {
            return string.Concat(string.Format(" WHERE R.{0} ", MakerCheckerRoleFactory.FieldNames.RoleId), " = {0};");
        }

        /// <summary>
        /// Returns the entity table name.
        /// </summary>
        /// <returns>Entity Table Nombre</returns>
        protected override string GetEntityName()
        {
            return MakerCheckerEmailTemplateFactory.TableNames.EmailTemplateTable;
        }

        /// <summary>
        /// Returns the Key of the entity
        /// </summary>
        /// <returns>Primary Key of the entity.</returns>
        protected override string GetKeyFieldName()
        {
            return MakerCheckerEmailTemplateFactory.FieldNames.EmailTemplateId;
        }

        protected override void BuildChildCallbacks()
        {
        }

        protected override MakerCheckerEmailTemplate PersistNewItem(MakerCheckerEmailTemplate item)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendFormat("INSERT INTO {0} ({1},{2},{3},{4},{5},{6},{7},{8}) VALUES (@{1},@{2},@{3},@{4},@{5},@{6},@{7},@{8}) SELECT @@IDENTITY", this.GetEntityName(), 
                MakerCheckerEmailTemplateFactory.FieldNames.TemplateName,
                MakerCheckerEmailTemplateFactory.FieldNames.Subject,
                MakerCheckerEmailTemplateFactory.FieldNames.Body,
                MakerCheckerEmailTemplateFactory.FieldNames.MakerIdentifier,
                MakerCheckerEmailTemplateFactory.FieldNames.CheckerIdentifier,
                MakerCheckerEmailTemplateFactory.FieldNames.DataIdentifier,
                MakerCheckerEmailTemplateFactory.FieldNames.Cc,
                MakerCheckerEmailTemplateFactory.FieldNames.Bcc);
            using (DbCommand cmd = this.Database.GetSqlStringCommand(strBuilder.ToString()))
            {
                this.Database.AddInParameter(cmd, MakerCheckerEmailTemplateFactory.FieldNames.TemplateName, System.Data.DbType.String, item.TemplateName);
                this.Database.AddInParameter(cmd, MakerCheckerEmailTemplateFactory.FieldNames.Subject, System.Data.DbType.String, item.Subject);
                this.Database.AddInParameter(cmd, MakerCheckerEmailTemplateFactory.FieldNames.Body, System.Data.DbType.String, item.Body);
                this.Database.AddInParameter(cmd, MakerCheckerEmailTemplateFactory.FieldNames.MakerIdentifier, System.Data.DbType.String, item.MakerIdentifier);
                this.Database.AddInParameter(cmd, MakerCheckerEmailTemplateFactory.FieldNames.CheckerIdentifier, System.Data.DbType.String, item.CheckerIdentifier);
                this.Database.AddInParameter(cmd, MakerCheckerEmailTemplateFactory.FieldNames.DataIdentifier, System.Data.DbType.String, item.DataIdentifier);
                this.Database.AddInParameter(cmd, MakerCheckerEmailTemplateFactory.FieldNames.Cc, System.Data.DbType.String, item.Cc);
                this.Database.AddInParameter(cmd, MakerCheckerEmailTemplateFactory.FieldNames.Bcc, System.Data.DbType.String, item.Bcc);
                var affectedRows = this.Database.ExecuteScalar(cmd);
                item.Key = Convert.ToInt32(affectedRows);
            }
            return item;
        }

        protected override MakerCheckerEmailTemplate PersistUpdatedItem(MakerCheckerEmailTemplate item)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendFormat("UPDATE {0} SET {1} = @{1}, {2} = @{2}, {3} = @{3}, {4} = @{4}, {5} = @{5}, {6} = @{6}, {7} = @{7}, {8} = @{8} WHERE {9} = @{9}", this.GetEntityName(),
                MakerCheckerEmailTemplateFactory.FieldNames.TemplateName,
                MakerCheckerEmailTemplateFactory.FieldNames.Subject,
                MakerCheckerEmailTemplateFactory.FieldNames.Body,
                MakerCheckerEmailTemplateFactory.FieldNames.MakerIdentifier,
                MakerCheckerEmailTemplateFactory.FieldNames.CheckerIdentifier,
                MakerCheckerEmailTemplateFactory.FieldNames.DataIdentifier,
                MakerCheckerEmailTemplateFactory.FieldNames.Cc,
                MakerCheckerEmailTemplateFactory.FieldNames.Bcc,
                MakerCheckerEmailTemplateFactory.FieldNames.EmailTemplateId);
            using (DbCommand cmd = this.Database.GetSqlStringCommand(strBuilder.ToString()))
            {
                this.Database.AddInParameter(cmd, MakerCheckerEmailTemplateFactory.FieldNames.TemplateName, System.Data.DbType.String, item.TemplateName);
                this.Database.AddInParameter(cmd, MakerCheckerEmailTemplateFactory.FieldNames.Subject, System.Data.DbType.String, item.Subject);
                this.Database.AddInParameter(cmd, MakerCheckerEmailTemplateFactory.FieldNames.Body, System.Data.DbType.String, item.Body);
                this.Database.AddInParameter(cmd, MakerCheckerEmailTemplateFactory.FieldNames.MakerIdentifier, System.Data.DbType.String, item.MakerIdentifier);
                this.Database.AddInParameter(cmd, MakerCheckerEmailTemplateFactory.FieldNames.CheckerIdentifier, System.Data.DbType.String, item.CheckerIdentifier);
                this.Database.AddInParameter(cmd, MakerCheckerEmailTemplateFactory.FieldNames.DataIdentifier, System.Data.DbType.String, item.DataIdentifier);
                this.Database.AddInParameter(cmd, MakerCheckerEmailTemplateFactory.FieldNames.Cc, System.Data.DbType.String, item.Cc);
                this.Database.AddInParameter(cmd, MakerCheckerEmailTemplateFactory.FieldNames.Bcc, System.Data.DbType.String, item.Bcc);
                this.Database.AddInParameter(cmd, MakerCheckerEmailTemplateFactory.FieldNames.EmailTemplateId, System.Data.DbType.Int32, item.EmailTemplateId);
                var affectedRows = this.Database.ExecuteNonQuery(cmd);
                if (affectedRows == 0) this._logger.Log(ApplicationLogger.LogType.Warning, "Update operation with affected rows equal to zero.");
            }
            return item;
        }

        #region Implementation of IMakerCheckerEmailTemplateRepository

        /// <summary>
        /// Gets the email template.
        /// </summary>
        /// <param name="Role">The role of type <see cref="MakerCheckerRole"/></param>
        /// <returns></returns>
        public MakerCheckerEmailTemplate GetEmailTemplateByRole(MakerCheckerRole Role)
        {
            return this.GetEmailTemplateByRole(Role.RoleId);
        }

        /// <summary>
        /// Gets the email template.
        /// </summary>
        /// <param name="RoleId">The role id of type <see cref="System.Int32"/></param>
        /// <returns></returns>
        public MakerCheckerEmailTemplate GetEmailTemplateByRole(int RoleId)
        {
            StringBuilder query = new StringBuilder();
            query.AppendFormat("SELECT A.* FROM {0} as A INNER JOIN {1} as B ON A.{2} = B.{3} WHERE B.{4} = @{5}", MakerCheckerEmailTemplateFactory.TableNames.EmailTemplateTable, MakerCheckerEmailTemplateFactory.TableNames.EmailTemplateRoleTable, MakerCheckerEmailTemplateFactory.FieldNames.EmailTemplateId, MakerCheckerRoleFactory.FieldNames.RoleId, MakerCheckerRoleFactory.FieldNames.RoleId, MakerCheckerRoleFactory.FieldNames.RoleId);
            using (SqlCommand cmd = new SqlCommand(query.ToString()))
            {
                cmd.Parameters.AddWithValue(MakerCheckerRoleFactory.FieldNames.RoleId, RoleId);
                return this.BuildEntityFromSql(cmd);
            }
        }

        #endregion
    }
}
