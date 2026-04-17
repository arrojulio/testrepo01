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
    public class MakerCheckerChangesetSqlRepository:  SqlRepositoryBase<MakerCheckerChangeset>, IMakerCheckerChangesetRepository
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MakerCheckerUserSqlRepository"/> class.
        /// </summary>
        public MakerCheckerChangesetSqlRepository()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MakerCheckerUserSqlRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work of type <see cref="Bladex.Garantias.Infrastructure.IUnitOfWork"/></param>
        public MakerCheckerChangesetSqlRepository(IUnitOfWork unitOfWork) 
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
            return "APP_MAKERCHECKER_Changeset";
        }

        /// <summary>
        /// Returns the Key of the entity
        /// </summary>
        /// <returns>Primary Key of the entity.</returns>
        protected override string GetKeyFieldName()
        {
            return MakerCheckerChangesetFactory.FieldNames.ChangesetId;
        }

        /// <summary>
        /// Persists the new item.
        /// </summary>
        /// <param name="item">The item of type <see cref="Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker.MakerCheckerChangeset"/></param>
        /// <returns></returns>
        protected override MakerCheckerChangeset PersistNewItem(MakerCheckerChangeset item)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendFormat("INSERT INTO {0} ({1},{2},{3},{4},{5}) VALUES (NEWID(),@{2},@{3},@{4},@{5}) ", this.GetEntityName(),
                MakerCheckerChangesetFactory.FieldNames.ChangesetId,
                MakerCheckerChangesetFactory.FieldNames.MakerUserId,
                MakerCheckerChangesetFactory.FieldNames.ChangesetDate,
                MakerCheckerChangesetFactory.FieldNames.ChangesetCommitDate,
                MakerCheckerChangesetFactory.FieldNames.ChangesetComment);
            using (DbCommand cmd = this.Database.GetSqlStringCommand(strBuilder.ToString()))
            {
                this.Database.AddInParameter(cmd, MakerCheckerChangesetFactory.FieldNames.MakerUserId, System.Data.DbType.String, item.MakerUserId);
                this.Database.AddInParameter(cmd, MakerCheckerChangesetFactory.FieldNames.ChangesetDate, System.Data.DbType.DateTime, item.ChangesetDate);
                this.Database.AddInParameter(cmd, MakerCheckerChangesetFactory.FieldNames.ChangesetCommitDate, System.Data.DbType.DateTime, item.ChangesetCommitDate);
                this.Database.AddInParameter(cmd, MakerCheckerChangesetFactory.FieldNames.ChangesetComment, System.Data.DbType.String, string.IsNullOrEmpty(item.ChangesetComment) ? null : item.ChangesetComment.Trim());
                var affectedRows = this.Database.ExecuteNonQuery(cmd);
                if (affectedRows == 0) this._logger.Log(ApplicationLogger.LogType.Warning, "Insert operation with affected rows equal to zero.");
            }
            return item;
        }

        /// <summary>
        /// Persists the updated item.
        /// </summary>
        /// <param name="item">The item of type <see cref="Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker.MakerCheckerChangeset"/></param>
        /// <returns></returns>
        protected override MakerCheckerChangeset PersistUpdatedItem(MakerCheckerChangeset item)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendFormat("UPDATE {0} SET {1} = @{1}, {2} = @{2}, {3} = @{3}, {5} = @{5} WHERE {4} = @{4}", this.GetEntityName(),
                MakerCheckerChangesetFactory.FieldNames.MakerUserId,
                MakerCheckerChangesetFactory.FieldNames.ChangesetDate,
                MakerCheckerChangesetFactory.FieldNames.ChangesetCommitDate,
                MakerCheckerChangesetFactory.FieldNames.ChangesetId,
                MakerCheckerChangesetFactory.FieldNames.ChangesetComment);
            using (DbCommand cmd = this.Database.GetSqlStringCommand(strBuilder.ToString()))
            {
                this.Database.AddInParameter(cmd, MakerCheckerChangesetFactory.FieldNames.ChangesetId, System.Data.DbType.Guid, item.ChangesetId);
                this.Database.AddInParameter(cmd, MakerCheckerChangesetFactory.FieldNames.MakerUserId, System.Data.DbType.String, item.MakerUserId);
                this.Database.AddInParameter(cmd, MakerCheckerChangesetFactory.FieldNames.ChangesetDate, System.Data.DbType.DateTime, item.ChangesetDate);
                this.Database.AddInParameter(cmd, MakerCheckerChangesetFactory.FieldNames.ChangesetCommitDate, System.Data.DbType.DateTime, item.ChangesetCommitDate);
                this.Database.AddInParameter(cmd, MakerCheckerChangesetFactory.FieldNames.ChangesetComment, System.Data.DbType.String, string.IsNullOrEmpty(item.ChangesetComment) ? null : item.ChangesetComment.Trim());
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
            this.ChildCallbacks.Add(MakerCheckerChangesetFactory.FieldNames.MakerUserId, this.AppendMakerCheckerUser);
        }

        /// <summary>
        /// Appends the maker checker user.
        /// </summary>
        /// <param name="entity">The entity of type <see cref="Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker.MakerCheckerChangeset"/></param>
        /// <param name="foreignKey">The foreign key of type <see cref="System.Object"/></param>
        private void AppendMakerCheckerUser(DomainModel.DomainBase.Components.MakerChecker.MakerCheckerChangeset entity, object foreignKey)
        {
            IMakerCheckerUserRepository repository = RepositoryFactory.GetRepository<IMakerCheckerUserRepository, MakerCheckerUser>(this.UnitOfWork);
            entity.MakerUser = repository.FindBy(foreignKey);
        }

        private MakerCheckerUser GetMakerCheckerUser(string makerUserId)
        {
            IMakerCheckerUserRepository repository = RepositoryFactory.GetRepository<IMakerCheckerUserRepository, MakerCheckerUser>(this.UnitOfWork);
            return repository.FindBy(makerUserId);
        }


        #region IMakerCheckerChangesetRepository Members

        /// <summary>
        /// Gets the changeset summary.
        /// </summary>
        /// <returns></returns>
        public List<MakerCheckerChangesetSummary> GetChangesetSummary(MakerCheckerUser user)
        {


            //If Checker returns ChangeSetSummary Pending
            if (user.IsChecker || user.IsSuperUser)
            {
                return this.GetPendingChangeSet(user);
            }
            else if (user.IsMaker)
            {
                //Returns all changeset of the user         
                return this.GetAllChangeSetByUser(user);
            }
            return new List<MakerCheckerChangesetSummary>();


        }

        public List<MakerCheckerChangesetSummary> GetChangesetSummary()
        {
            List<MakerCheckerOperation> operations = new List<MakerCheckerOperation>();

            // Retrieve all changesets
            var changesets = this.GetAll();

            // Retrieve the operation repository
            IMakerCheckerOperationRepository operationRepository = RepositoryFactory.GetRepository<IMakerCheckerOperationRepository, MakerCheckerOperation>();
            // for each changeset, retrieve his operations and build a maker checker changeset summary object.
            // then return the resulting collection of summarie
            return changesets.Select(changeset => new MakerCheckerChangesetSummary(changeset, operationRepository.GetOperationsByChangeset(changeset))).ToList();
        }


        /// <summary>
        /// Gets the changeset summary.
        /// </summary>
        /// <param name="changesetId">The changeset id of type <see cref="System.Guid"/></param>
        /// <returns></returns>
        public MakerCheckerChangesetSummary GetChangesetSummary(Guid changesetId)
        {
            var changesetObj = this.FindBy(changesetId);
            // Retrieve the operation repository
            IMakerCheckerOperationRepository operationRepository = RepositoryFactory.GetRepository<IMakerCheckerOperationRepository, MakerCheckerOperation>();
            // for the changeset, retrieve his operations and build a maker checker changeset summary object.
            // then return the resulting summary.
            return new MakerCheckerChangesetSummary(changesetObj, operationRepository.GetOperationsByChangeset(changesetObj));

        }

        /// <summary>
        /// Gets the available changeset.
        /// </summary>
        /// <param name="makerUserId">The maker user id of type <see cref="System.String"/></param>
        /// <returns></returns>
        public MakerCheckerChangeset GetAvailableChangeset(string makerUserId)
        {            
            //#Ticket1320 : Reconexion sql
            MakerCheckerChangeset availableChangeset = GetSQLAvailableChangesetList("MakerUserId", makerUserId);
            //var availableChangeset = this.GetAll().FirstOrDefault(o => o.MakerUserId == makerUserId && !o.ChangesetCommitDate.HasValue);
                        
            if (availableChangeset == null)
            {                
                var changeset = new MakerCheckerChangeset() { MakerUserId = makerUserId, ChangesetDate = DateTime.Now };
                availableChangeset = this.Add(changeset);
            }
            return availableChangeset;
            
        }

        
        public MakerCheckerChangeset GetSQLAvailableChangesetList(string fieldName, string value)         
        {
            List<MakerCheckerChangeset> list = new List<MakerCheckerChangeset>();

            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendFormat("SELECT TOP 1 * FROM {0} WHERE {1}='{2}' AND ChangesetCommitDate IS NULL ORDER BY ChangesetDate DESC", GetEntityName(), fieldName, value);
            using (DbCommand cmd = this.Database.GetSqlStringCommand(strBuilder.ToString()))
            {

                using (IDataReader reader = this.Database.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        list.Add(new MakerCheckerChangeset
                        {
                            Key = DataHelper.GetGuid(reader["ChangeSetId"]),
                            ChangesetId = DataHelper.GetGuid(reader["ChangeSetId"]),
                            MakerUserId = DataHelper.GetString(reader["MakerUserId"]),
                            ChangesetDate = DataHelper.GetDateTime(reader["ChangesetDate"]),
                            ChangesetCommitDate = DataHelper.GetDateTime(reader["ChangesetCommitDate"]),
                            ChangesetComment = DataHelper.GetString(reader["ChangeSetComment"]),
                            MakerUser = GetMakerCheckerUser(DataHelper.GetString(reader["MakerUserId"]))
                        });
                    }
                }

            }

            return list.FirstOrDefault();
            
        }

        public int GetCountChangesetByUserAndId(string MakerUserId,Guid ChangesetId) 
        {
            int counter = 0;

            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendFormat("SELECT COUNT(*) Total  FROM {0} WHERE ChangesetId='{1}' AND MakerUserId='{2}'", GetEntityName(), ChangesetId.ToString(),MakerUserId);

            using (DbCommand cmd = this.Database.GetSqlStringCommand(strBuilder.ToString()))
            {

                using (IDataReader reader = this.Database.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        counter = DataHelper.GetInteger(reader["Total"]);                        
                    }
                }

            }

            return counter;
        }
        
        #endregion

        private List<MakerCheckerChangesetSummary> GetPendingChangeSet(MakerCheckerUser user)
        {
            StringBuilder strBuilder = new StringBuilder();
                        
            strBuilder.AppendFormat("SELECT * FROM [dbo].[UDF_GetPendingChangeSet] (null) WHERE ChangesetStatus = 1");
            List<MakerCheckerChangesetSummary> ret = new List<MakerCheckerChangesetSummary>();
       

            using (DbCommand cmd = this.Database.GetSqlStringCommand(strBuilder.ToString()))
            {
                using (IDataReader reader = this.Database.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        int operationStatus = DataHelper.GetInteger(reader["ChangesetStatus"]);
                        string makerUserId = DataHelper.GetString(reader["MakerUserId"]);
                        
                            MakerCheckerChangesetSummary item = new MakerCheckerChangesetSummary(new Guid(DataHelper.GetString(reader["ChangesetId"])),
                                DataHelper.GetString(reader["MakerUserId"]),
                                user,
                                DataHelper.GetDateTime(reader["ChangesetDate"]),
                                DataHelper.GetDateTime(reader["ChangesetCommitDate"]),
                                DataHelper.GetInteger(reader["TotalOperations"]),
                                DataHelper.GetInteger(reader["NewOperations"]),
                                DataHelper.GetInteger(reader["ApprovedOperations"]),
                                DataHelper.GetInteger(reader["RejectedOperations"]),
                                operationStatus == 1 ? MakerCheckerChangesetSummary.ChangesetStatusEnum.Pending : MakerCheckerChangesetSummary.ChangesetStatusEnum.Revised,
                                DataHelper.GetString(reader["ChangesetComment"]));
                        
                        item.GarantiaId = 0;
                        item.CustomerName = "";
                        item.Garante = "";
                        item.TipoGarantia = "";
                        item.ValorGarantia = 0;
                        
                        ret.Add(item);
                    }

                }
            }

            return ret.Where(o => o.ChangesetStatus == MakerCheckerChangesetSummary.ChangesetStatusEnum.Pending).ToList();
            
        }

        private List<MakerCheckerChangesetSummary> GetAllChangeSetByUser(MakerCheckerUser user)
        {
            StringBuilder strBuilder = new StringBuilder();
            
            strBuilder.AppendFormat("SELECT * FROM [dbo].[UDF_GetPendingChangeSet] (@UserId)");
            List<MakerCheckerChangesetSummary> ret = new List<MakerCheckerChangesetSummary>();

            using (DbCommand cmd = this.Database.GetSqlStringCommand(strBuilder.ToString()))
            {
                this.Database.AddInParameter(cmd, "@UserId", DbType.String, user.UserId);

                using (IDataReader reader = this.Database.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        int operationStatus = DataHelper.GetInteger(reader["ChangesetStatus"]);

                        MakerCheckerChangesetSummary item = new MakerCheckerChangesetSummary(new Guid(DataHelper.GetString(reader["ChangesetId"])),
                            DataHelper.GetString(reader["MakerUserId"]),
                            user,
                            DataHelper.GetDateTime(reader["ChangesetDate"]),
                            DataHelper.GetDateTime(reader["ChangesetCommitDate"]),
                            DataHelper.GetInteger(reader["TotalOperations"]),
                            DataHelper.GetInteger(reader["NewOperations"]),
                            DataHelper.GetInteger(reader["ApprovedOperations"]),
                            DataHelper.GetInteger(reader["RejectedOperations"]),
                            operationStatus == 1 ? MakerCheckerChangesetSummary.ChangesetStatusEnum.Pending : MakerCheckerChangesetSummary.ChangesetStatusEnum.Revised,
                            DataHelper.GetString(reader["ChangesetComment"]));

                        ret.Add(item);

                    }

                }
            }
            return ret.Where(o => o.ChangesetStatus == MakerCheckerChangesetSummary.ChangesetStatusEnum.Pending).ToList();
        }

    }
}
