using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
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
    public class MakerCheckerOperationSqlRepository : SqlRepositoryBase<MakerCheckerOperation>, IMakerCheckerOperationRepository
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MakerCheckerUserSqlRepository"/> class.
        /// </summary>
        public MakerCheckerOperationSqlRepository()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MakerCheckerUserSqlRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work of type <see cref="Bladex.Garantias.Infrastructure.IUnitOfWork"/></param>
        public MakerCheckerOperationSqlRepository(IUnitOfWork unitOfWork)
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
            return "APP_MAKERCHECKER_Operation";
        }

        /// <summary>
        /// Returns the Key of the entity
        /// </summary>
        /// <returns>Primary Key of the entity.</returns>
        protected override string GetKeyFieldName()
        {
            return MakerCheckerOperationFactory.FieldNames.OperationId;
        }

        /// <summary>
        /// Persists the new item.
        /// </summary>
        /// <param name="item">The item of type <see cref="Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker.MakerCheckerOperation"/></param>
        /// <returns></returns>
        protected override MakerCheckerOperation PersistNewItem(MakerCheckerOperation item)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendFormat("INSERT INTO {0} ({1},{2},{3},{4},{5},{6},{7},{8},{9}) VALUES (@{1},@{2},@{3},@{4},@{5},@{6},@{7},@{8},@{9}) SELECT @@IDENTITY", this.GetEntityName(), MakerCheckerOperationFactory.FieldNames.ChangesetId, MakerCheckerOperationFactory.FieldNames.CheckerUserId, MakerCheckerOperationFactory.FieldNames.CheckerDate, MakerCheckerOperationFactory.FieldNames.MakerDate, MakerCheckerOperationFactory.FieldNames.OperationStatusId, MakerCheckerOperationFactory.FieldNames.ItemId, MakerCheckerOperationFactory.FieldNames.Item, MakerCheckerOperationFactory.FieldNames.Comment, MakerCheckerOperationFactory.FieldNames.ItemType);
            using (DbCommand cmd = this.Database.GetSqlStringCommand(strBuilder.ToString()))
            {
                this.Database.AddInParameter(cmd, MakerCheckerOperationFactory.FieldNames.ChangesetId, System.Data.DbType.Guid, item.ChangesetId);
                this.Database.AddInParameter(cmd, MakerCheckerOperationFactory.FieldNames.CheckerUserId, System.Data.DbType.String, string.IsNullOrEmpty(item.CheckerUserId) ? null : item.CheckerUserId.Trim());
                this.Database.AddInParameter(cmd, MakerCheckerOperationFactory.FieldNames.CheckerDate, System.Data.DbType.DateTime, item.CheckerDate);
                this.Database.AddInParameter(cmd, MakerCheckerOperationFactory.FieldNames.MakerDate, System.Data.DbType.DateTime, item.MakerDate);
                this.Database.AddInParameter(cmd, MakerCheckerOperationFactory.FieldNames.OperationStatusId, System.Data.DbType.Int32, item.OperationStatusId);
                this.Database.AddInParameter(cmd, MakerCheckerOperationFactory.FieldNames.ItemId, System.Data.DbType.Int32, item.ItemId);
                this.Database.AddInParameter(cmd, MakerCheckerOperationFactory.FieldNames.Item, System.Data.DbType.String, item.Item);
                this.Database.AddInParameter(cmd, MakerCheckerOperationFactory.FieldNames.Comment, System.Data.DbType.String, string.IsNullOrEmpty(item.Comment) ? null : item.Comment.Trim());
                this.Database.AddInParameter(cmd, MakerCheckerOperationFactory.FieldNames.ItemType, System.Data.DbType.String, item.ItemType);
                var affectedRows = this.Database.ExecuteScalar(cmd);
                item.Key = Convert.ToInt32(affectedRows);
            }
            return item;
        }

        /// <summary>
        /// Persists the updated item.
        /// </summary>
        /// <param name="item">The item of type <see cref="Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker.MakerCheckerOperation"/></param>
        /// <returns></returns>
        protected override MakerCheckerOperation PersistUpdatedItem(MakerCheckerOperation item)
        {
            try
            {
                StringBuilder strBuilder = new StringBuilder();
                // Handle concurrency based on CheckerUserId assignation
                /*
                strBuilder.AppendLine("BEGIN TRANSACTION ");                
                strBuilder.AppendLine(string.Format("IF( (SELECT COUNT(*) FROM {0} WHERE {1} = @{1} AND ({2} IS NOT NULL)) > 0) ", this.GetEntityName(), MakerCheckerOperationFactory.FieldNames.OperationId, MakerCheckerOperationFactory.FieldNames.CheckerUserId));
                strBuilder.AppendLine("BEGIN ");
                strBuilder.AppendLine("DECLARE @ConcurrencyMessage nvarchar(max) ");
                strBuilder.AppendFormat("SET @ConcurrencyMessage = (SELECT 'The user ' + ISNULL(O.[{0}],'(no user)') + ' has marked the operation as ' + LOWER(OS.{1}) + ' in ' + CONVERT(NVARCHAR(MAX),ISNULL(O.{2},'')) FROM {3} as O INNER JOIN [APP_MakerChecker_OperationStatus] as OS ON O.{4} = OS.{5} WHERE O.{6} = @{6}) ",
                                        MakerCheckerOperationFactory.FieldNames.CheckerUserId,
                                        MakerCheckerOperationStatusFactory.FieldNames.OperationStatusDescription,
                                        MakerCheckerOperationFactory.FieldNames.CheckerDate,
                                        this.GetEntityName(),
                                        MakerCheckerOperationFactory.FieldNames.OperationStatusId,
                                        MakerCheckerOperationStatusFactory.FieldNames.OperationStatusId,
                                        MakerCheckerOperationFactory.FieldNames.OperationId
                    );

                strBuilder.AppendLine("RAISERROR(@ConcurrencyMessage, 16, 1) ");
                strBuilder.AppendLine("ROLLBACK TRANSACTION ");
                strBuilder.AppendLine("RETURN ");
                strBuilder.AppendLine("END ");
                strBuilder.AppendLine("ELSE ");
                strBuilder.AppendLine("BEGIN ");*/
                strBuilder.AppendFormat("UPDATE {0} SET {1} = @{1}, {2} = @{2}, {3} = @{3}, {4} = @{4}, {5} = @{5}, {6} = @{6}, {7} = @{7}, {8} = @{8} WHERE {9} = @{9} ", this.GetEntityName(),
                                        MakerCheckerOperationFactory.FieldNames.ChangesetId,
                                        MakerCheckerOperationFactory.FieldNames.CheckerUserId,
                                        MakerCheckerOperationFactory.FieldNames.CheckerDate,
                                        MakerCheckerOperationFactory.FieldNames.MakerDate,
                                        MakerCheckerOperationFactory.FieldNames.OperationStatusId,
                                        MakerCheckerOperationFactory.FieldNames.ItemId,
                                        MakerCheckerOperationFactory.FieldNames.Item,
                                        MakerCheckerOperationFactory.FieldNames.Comment,
                                        MakerCheckerOperationFactory.FieldNames.OperationId);
                /*strBuilder.AppendLine("COMMIT TRANSACTION ");
                strBuilder.AppendLine("END ");*/

                using (DbCommand cmd = this.Database.GetSqlStringCommand(strBuilder.ToString()))
                {
                    this.Database.AddInParameter(cmd, MakerCheckerOperationFactory.FieldNames.ChangesetId, System.Data.DbType.Guid, item.ChangesetId);
                    this.Database.AddInParameter(cmd, MakerCheckerOperationFactory.FieldNames.OperationId, System.Data.DbType.Int32, item.OperationId);
                    this.Database.AddInParameter(cmd, MakerCheckerOperationFactory.FieldNames.CheckerUserId, System.Data.DbType.String, string.IsNullOrEmpty(item.CheckerUserId) ? null : item.CheckerUserId.Trim());
                    this.Database.AddInParameter(cmd, MakerCheckerOperationFactory.FieldNames.CheckerDate, System.Data.DbType.DateTime, item.CheckerDate);
                    this.Database.AddInParameter(cmd, MakerCheckerOperationFactory.FieldNames.MakerDate, System.Data.DbType.DateTime, item.MakerDate);
                    this.Database.AddInParameter(cmd, MakerCheckerOperationFactory.FieldNames.OperationStatusId, System.Data.DbType.Int32, item.OperationStatusId);
                    this.Database.AddInParameter(cmd, MakerCheckerOperationFactory.FieldNames.ItemId, System.Data.DbType.Int32, item.ItemId);
                    this.Database.AddInParameter(cmd, MakerCheckerOperationFactory.FieldNames.Item, System.Data.DbType.String, item.Item);
                    this.Database.AddInParameter(cmd, MakerCheckerOperationFactory.FieldNames.Comment, System.Data.DbType.String, string.IsNullOrEmpty(item.Comment) ? null : item.Comment.Trim());
                    var affectedRows = this.Database.ExecuteNonQuery(cmd);
                    if (affectedRows == 0) this._logger.Log(ApplicationLogger.LogType.Warning, "Update operation with affected rows equal to zero.");
                }
                return item;
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.State == 1 && sqlEx.Number == 50000)
                    throw new DBConcurrencyException(sqlEx.Message);
                throw;
            }
        }

        /// <summary>
        /// Builds the child callbacks.
        /// </summary>
        protected override void BuildChildCallbacks()
        {
            this.ChildCallbacks.Add(MakerCheckerOperationFactory.FieldNames.CheckerUserId, this.AppendMakerCheckerUser);
            this.ChildCallbacks.Add(MakerCheckerOperationFactory.FieldNames.OperationStatusId, this.AppendMakerCheckerOperationStatus);
            this.ChildCallbacks.Add(MakerCheckerOperationFactory.FieldNames.ChangesetId, this.AppendMakerCheckerChangeset);
        }

        /// <summary>
        /// Appends the maker checker user.
        /// </summary>
        /// <param name="entity">The entity of type <see cref="Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker.MakerCheckerOperation"/></param>
        /// <param name="foreignKey">The foreign key of type <see cref="System.Object"/></param>
        private void AppendMakerCheckerUser(DomainModel.DomainBase.Components.MakerChecker.MakerCheckerOperation entity, object foreignKey)
        {
            IMakerCheckerUserRepository repository = RepositoryFactory.GetRepository<IMakerCheckerUserRepository, MakerCheckerUser>(this.UnitOfWork);
            entity.CheckerUser = repository.FindBy(foreignKey);
        }

        /// <summary>
        /// Appends the maker checker operation status.
        /// </summary>
        /// <param name="entity">The entity of type <see cref="Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker.MakerCheckerOperation"/></param>
        /// <param name="foreignKey">The foreign key of type <see cref="System.Object"/></param>
        private void AppendMakerCheckerOperationStatus(DomainModel.DomainBase.Components.MakerChecker.MakerCheckerOperation entity, object foreignKey)
        {
            IMakerCheckerOperationStatusRepository repository = RepositoryFactory.GetRepository<IMakerCheckerOperationStatusRepository, MakerCheckerOperationStatus>(this.UnitOfWork);
            entity.OperationStatus = repository.FindBy(foreignKey);
        }

        /// <summary>
        /// Appends the maker checker operation status.
        /// </summary>
        /// <param name="entity">The entity of type <see cref="Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker.MakerCheckerOperation"/></param>
        /// <param name="foreignKey">The foreign key of type <see cref="System.Object"/></param>
        private void AppendMakerCheckerChangeset(DomainModel.DomainBase.Components.MakerChecker.MakerCheckerOperation entity, object foreignKey)
        {
            IMakerCheckerChangesetRepository repository = RepositoryFactory.GetRepository<IMakerCheckerChangesetRepository, MakerCheckerChangeset>(this.UnitOfWork);
            entity.Changeset = repository.FindBy(foreignKey);
        }

        private MakerCheckerChangeset GetChangesetByGuid(Guid changesetId)
        {
            IMakerCheckerChangesetRepository repository = RepositoryFactory.GetRepository<IMakerCheckerChangesetRepository, MakerCheckerChangeset>(this.UnitOfWork);
            return repository.FindBy(changesetId);
        }

        private MakerCheckerOperationStatus GetOperationStatusByID(int operationStatusId)
        {
            IMakerCheckerOperationStatusRepository repository = RepositoryFactory.GetRepository<IMakerCheckerOperationStatusRepository, MakerCheckerOperationStatus>(this.UnitOfWork);
            return repository.FindBy(operationStatusId);
        }

        private MakerCheckerUser GetCheckerUserById(string userId)
        {
            IMakerCheckerUserRepository repository = RepositoryFactory.GetRepository<IMakerCheckerUserRepository, MakerCheckerUser>(this.UnitOfWork);
            if (!string.IsNullOrEmpty(userId))
                return repository.FindBy(userId);
            else
                return null;
        }

        #region IMakerCheckerOperationRepository Members

        public List<MakerCheckerOperation> GetOperationsByChangeset(MakerCheckerChangeset changeset)
        {
            return this.GetOperationsByChangeset(changeset.ChangesetId);
        }

        public List<MakerCheckerOperation> GetOperationsByChangeset(System.Guid changeset)
        {
            //#Ticket1320 : Reconexion sql
            //return this.GetAll().Where(o => o.ChangesetId == changeset).ToList();                        
            return GetSQLOperationsByFilter(changeset.ToString(), "ChangesetId");
        }

        public List<MakerCheckerOperation> GetOperationsByMaker(string makerUserId)
        {
            return this.GetAll().Where(o => o.Changeset.MakerUserId == makerUserId).ToList();
        }

        public List<MakerCheckerOperation> GetOperationsByChecker(string checkerUserId)
        {
            //#Ticket1320 : Reconexion sql
            //return this.GetAll().Where(o => o.CheckerUserId == checkerUserId).ToList();
            return GetSQLOperationsByFilter(checkerUserId, "CheckerUserId");
        }

        public List<MakerCheckerOperation> GetOperationsByItemId(int itemId)
        {
            //#Ticket1320 : Reconexion sql
            //return this.GetAll().Where(o => o.ItemId.HasValue && o.ItemId.Value == itemId).ToList();
            return GetSQLOperationsByFilter(itemId.ToString(), "ItemId");
        }

        public List<MakerCheckerOperation> GetSqlOperationsNotApproved()
        {
            List<MakerCheckerOperation> list = new List<MakerCheckerOperation>();

            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendFormat("SELECT DISTINCT O.OperationId,O.ChangesetId,O.CheckerDate,O.MakerDate,O.OperationStatusId,O.ItemId,O.Item,O.ItemType,O.CheckerUserId,O.Comment FROM {0} O WHERE O.OperationStatusId <> 2 ", GetEntityName());

            using (DbCommand cmd = this.Database.GetSqlStringCommand(strBuilder.ToString()))
            {

                using (IDataReader reader = this.Database.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        list.Add(new MakerCheckerOperation
                        {
                            Key = DataHelper.GetInteger(reader["OperationId"]),
                            OperationId = DataHelper.GetInteger(reader["OperationId"]),
                            ChangesetId = DataHelper.GetGuid(reader["ChangesetId"]),
                            CheckerDate = DataHelper.GetDateTime(reader["CheckerDate"]),
                            MakerDate = DataHelper.GetDateTime(reader["MakerDate"]),
                            OperationStatusId = DataHelper.GetInteger(reader["OperationStatusId"]),
                            ItemId = DataHelper.GetInteger(reader["ItemId"]),
                            Item = DataHelper.GetString(reader["Item"]),
                            ItemType = DataHelper.GetString(reader["ItemType"]),
                            CheckerUserId = DataHelper.GetString(reader["CheckerUserId"]),
                            Comment = DataHelper.GetString(reader["Comment"]),
                            OperationStatus = GetOperationStatusByID(DataHelper.GetInteger(reader["OperationStatusId"])),
                            Changeset = GetChangesetByGuid(DataHelper.GetGuid(reader["ChangesetId"])),
                            CheckerUser = GetCheckerUserById(DataHelper.GetString(reader["CheckerUserId"]))
                        });
                    }
                }

            }

            return list;
        }

        public MakerCheckerOperation GetSqlOperationNotApprovedById(int OperationId)
        {
            List<MakerCheckerOperation> list = new List<MakerCheckerOperation>();

            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendFormat("SELECT TOP 1 O.OperationId,O.ChangesetId,O.CheckerDate,O.MakerDate,O.OperationStatusId,O.ItemId,O.Item,O.ItemType,O.CheckerUserId,O.Comment FROM {0} O WHERE O.OperationId={1} ", GetEntityName(), OperationId);

            using (DbCommand cmd = this.Database.GetSqlStringCommand(strBuilder.ToString()))
            {

                using (IDataReader reader = this.Database.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        list.Add(new MakerCheckerOperation
                        {
                            Key = DataHelper.GetInteger(reader["OperationId"]),
                            OperationId = DataHelper.GetInteger(reader["OperationId"]),
                            ChangesetId = DataHelper.GetGuid(reader["ChangesetId"]),
                            CheckerDate = DataHelper.GetDateTime(reader["CheckerDate"]),
                            MakerDate = DataHelper.GetDateTime(reader["MakerDate"]),
                            OperationStatusId = DataHelper.GetInteger(reader["OperationStatusId"]),
                            ItemId = DataHelper.GetInteger(reader["ItemId"]),
                            Item = DataHelper.GetString(reader["Item"]),
                            ItemType = DataHelper.GetString(reader["ItemType"]),
                            CheckerUserId = DataHelper.GetString(reader["CheckerUserId"]),
                            Comment = DataHelper.GetString(reader["Comment"]),
                            OperationStatus = GetOperationStatusByID(DataHelper.GetInteger(reader["OperationStatusId"])),
                            Changeset = GetChangesetByGuid(DataHelper.GetGuid(reader["ChangesetId"])),
                            CheckerUser = GetCheckerUserById(DataHelper.GetString(reader["CheckerUserId"]))
                        });
                    }
                }

            }

            return list.FirstOrDefault();
        }

        public List<MakerCheckerOperation> GetSQLOperationsByFilter(string value, string fieldName)
        {
            List<MakerCheckerOperation> list = new List<MakerCheckerOperation>();

            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendFormat("SELECT DISTINCT O.OperationId,O.ChangesetId,O.CheckerDate,O.MakerDate,O.OperationStatusId,O.ItemId,O.Item,O.ItemType,O.CheckerUserId,O.Comment FROM {0} O WHERE O.{1}='{2}'", GetEntityName(), fieldName, value);
            using (DbCommand cmd = this.Database.GetSqlStringCommand(strBuilder.ToString()))
            {

                using (IDataReader reader = this.Database.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        list.Add(new MakerCheckerOperation
                        {
                            Key = DataHelper.GetInteger(reader["OperationId"]),
                            OperationId = DataHelper.GetInteger(reader["OperationId"]),
                            ChangesetId = DataHelper.GetGuid(reader["ChangesetId"]),
                            CheckerDate = DataHelper.GetDateTime(reader["CheckerDate"]),
                            MakerDate = DataHelper.GetDateTime(reader["MakerDate"]),
                            OperationStatusId = DataHelper.GetInteger(reader["OperationStatusId"]),
                            ItemId = DataHelper.GetInteger(reader["ItemId"]),
                            Item = DataHelper.GetString(reader["Item"]),
                            ItemType = DataHelper.GetString(reader["ItemType"]),
                            CheckerUserId = DataHelper.GetString(reader["CheckerUserId"]),
                            Comment = DataHelper.GetString(reader["Comment"]),
                            OperationStatus = GetOperationStatusByID(DataHelper.GetInteger(reader["OperationStatusId"])),
                            Changeset = GetChangesetByGuid(DataHelper.GetGuid(reader["ChangesetId"])),
                            CheckerUser = GetCheckerUserById(DataHelper.GetString(reader["CheckerUserId"]))
                        });
                    }
                }

            }

            return list;
        }

        /// <summary>
        /// Devuelve un listado de operaciones pendientes de revision
        /// </summary>
        /// <returns></returns>
        public List<MakerCheckerOperationSummary> GetPendingSummaryOperations()
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendFormat("SELECT * FROM dbo.[UDF_GetPendingOperations](null)");

            List<MakerCheckerOperationSummary> operationSummaries = new List<MakerCheckerOperationSummary>();
            using (DbCommand cmd = this.Database.GetSqlStringCommand(strBuilder.ToString()))
            {
                using (IDataReader reader = this.Database.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        MakerCheckerOperationSummary operationSummary = new MakerCheckerOperationSummary();
                        operationSummary.Key = DataHelper.GetInteger(reader["OperationID"]);
                        operationSummary.ChangesetId = DataHelper.GetGuid(reader["ChangesetId"]);
                        operationSummary.OperationId = DataHelper.GetInteger(reader["OperationID"]);
                        operationSummary.MakerUserId = DataHelper.GetString(reader["MakerUserId"]);
                        //operationSummary.MakerUser = GetCheckerUserById(operationSummary.MakerUserId);
                        operationSummary.GarantiaId = DataHelper.GetInteger(reader["ItemID"]);
                        operationSummary.ChangesetDate = DataHelper.GetDateTime(reader["ChangesetDate"]);
                        operationSummary.ChangesetCommitDate = DataHelper.GetDateTime(reader["ChangesetCommitDate"]);
                        operationSummary.ChangesetComment = DataHelper.GetString(reader["ChangesetComment"]);
                        //Add element to the list
                        operationSummaries.Add(operationSummary); 



                    }

                }
            }
            return operationSummaries;

        }

        public List<MakerCheckerOperationSummary> GetPendingSummaryOperationsByUser(string userId)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendFormat("SELECT * FROM dbo.[UDF_GetPendingOperations]('{0}')", userId);

            List<MakerCheckerOperationSummary> operationSummaries = new List<MakerCheckerOperationSummary>();
            using (DbCommand cmd = this.Database.GetSqlStringCommand(strBuilder.ToString()))
            {
                using (IDataReader reader = this.Database.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        MakerCheckerOperationSummary operationSummary = new MakerCheckerOperationSummary();
                        operationSummary.Key = DataHelper.GetInteger(reader["OperationID"]);
                        operationSummary.ChangesetId = DataHelper.GetGuid(reader["ChangesetID"]);
                        operationSummary.OperationId = DataHelper.GetInteger(reader["OperationID"]);
                        operationSummary.MakerUserId = DataHelper.GetString(reader["MakerUserID"]);
                        operationSummary.OperationStatus = DataHelper.GetInteger(reader["OperationStatusID"]);
                        //operationSummary.MakerUser = GetCheckerUserById(operationSummary.MakerUserId);
                        operationSummary.GarantiaId = DataHelper.GetInteger(reader["ItemID"]);
                        operationSummary.ChangesetDate = DataHelper.GetDateTime(reader["ChangesetDate"]);
                        operationSummary.ChangesetCommitDate = DataHelper.GetDateTime(reader["ChangesetCommitDate"]);
                        operationSummary.ChangesetComment = DataHelper.GetString(reader["ChangesetComment"]);


                        //Add element to the list
                        operationSummaries.Add(operationSummary);



                    }

                }
            }
            return operationSummaries;
        }

        #endregion
    }

}
