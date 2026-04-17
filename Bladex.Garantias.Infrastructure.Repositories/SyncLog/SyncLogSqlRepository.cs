using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.Infrastructure.DomainBase;
using System.Data;

namespace Bladex.Garantias.Infrastructure.Repositories.SyncLog
{
    public class SyncLogSqlRepository : SqlRepositoryBase<DomainModel.DomainBase.SyncLog>, ISyncLogRepository
    {
         #region Private Members

        #endregion

        #region Public Constructors

        public SyncLogSqlRepository()
            : this(null)
        {
        }

        public SyncLogSqlRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        #endregion

        #region IRepository Members

        #endregion

        #region BuildChildCallbacks

        protected override void BuildChildCallbacks()
        {
            
        }

        #endregion

        #region Unit of Work Implementation

        public override EntityBase PersistNewItem(EntityBase item)
        {
            DomainModel.DomainBase.SyncLog synclog = item as DomainModel.DomainBase.SyncLog;
            if (synclog != null)
            {
                return this.PersistNewItem(synclog);
            }
            // TODO: We should throw some exception here if the cast is not valid.
            return item;
        }

        public override EntityBase PersistUpdatedItem(EntityBase item)
        {
            DomainModel.DomainBase.SyncLog synclog = item as DomainModel.DomainBase.SyncLog;
            if (synclog != null)
            {
                return this.PersistUpdatedItem(synclog);
            }
            // TODO: We should throw some exception here if the cast is not valid.
            return item;
        }

        public override void PersistDeletedItem(EntityBase item)
        {
            DomainModel.DomainBase.SyncLog synclog = item as DomainModel.DomainBase.SyncLog;
            if (synclog != null)
            {
                this.PersistDeletedItem(synclog);
            }
            // TODO: We should throw some exception here if the cast is not valid.
        }

        protected override DomainModel.DomainBase.SyncLog PersistNewItem(DomainModel.DomainBase.SyncLog item)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Format("INSERT INTO {0} ({1},{2},{3},{4},{5},{6},{7}) ",
                this.GetEntityName(),
                SyncLogFactory.FieldNames.TimeStamp,
                SyncLogFactory.FieldNames.Status,
                SyncLogFactory.FieldNames.FechaCorte,
                SyncLogFactory.FieldNames.ItemsAdded,
                SyncLogFactory.FieldNames.ItemsUpdated,
                SyncLogFactory.FieldNames.ItemsDeleted,
                SyncLogFactory.FieldNames.Message
                ));
            builder.Append(string.Format("VALUES ({0},{1},{2},{3},{4},{5},{6})",
                DataHelper.GetSqlValue(item.TimeStamp),
                DataHelper.GetSqlValue(item.Status),
                DataHelper.GetSqlValue(item.FechaCorte),
                DataHelper.GetSqlValue(item.ItemsAdded),
                DataHelper.GetSqlValue(item.ItemsUpdated),
                DataHelper.GetSqlValue(item.ItemsDeleted),
                DataHelper.GetSqlValue(item.Message)
                ));
            object newKey = this.Database.ExecuteScalar(this.Database.GetSqlStringCommand(builder.ToString()));
            item.Key = newKey;
            return item;
        }

        protected override DomainModel.DomainBase.SyncLog PersistUpdatedItem(DomainModel.DomainBase.SyncLog item)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("UPDATE {0} SET ", this.GetEntityName());

            builder.Append(string.Format("{0} = {1}",
                SyncLogFactory.FieldNames.TimeStamp,
                DataHelper.GetSqlValue(item.TimeStamp)));
            builder.Append(string.Format(", {0} = {1}",
                SyncLogFactory.FieldNames.Status,
                DataHelper.GetSqlValue(item.Status)));
            builder.Append(string.Format(", {0} = {1}",
                SyncLogFactory.FieldNames.FechaCorte,
                DataHelper.GetSqlValue(item.FechaCorte)));
            builder.Append(string.Format(", {0} = {1}",
                SyncLogFactory.FieldNames.ItemsAdded,
                DataHelper.GetSqlValue(item.ItemsAdded)));
            builder.Append(string.Format(", {0} = {1}",
                SyncLogFactory.FieldNames.ItemsUpdated,
                DataHelper.GetSqlValue(item.ItemsUpdated)));
            builder.Append(string.Format(", {0} = {1}",
                SyncLogFactory.FieldNames.ItemsDeleted,
                DataHelper.GetSqlValue(item.ItemsDeleted)));
            builder.Append(string.Format(", {0} = {1}",
                SyncLogFactory.FieldNames.Message,
                DataHelper.GetSqlValue(item.Message)));


            builder.Append(" ");
            builder.Append(this.BuildBaseWhereClause(item.Key));

            this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(builder.ToString()));
            return item;
        }

        protected override void PersistDeletedItem(DomainModel.DomainBase.SyncLog item)
        {
            // We could delete related objects here, and then, call the base method to delete the entity.
            base.PersistDeletedItem(item);
        }

        #endregion

        #region Private Callback and Helper Methods
       
        #endregion

        #region Implementation of ISyncLogRepository

        /// <summary>
        /// Retorna el ultimo Sync Log. NULL si esta vacio el log
        /// </summary>
        /// <returns>Ultimo SyncLog o NULL si no existen</returns>
        public DomainModel.DomainBase.SyncLog GetLastSyncLog()
        {
            using (IDataReader reader = this.ExecuteReader(String.Format("SELECT TOP 1 * FROM [{0}] ORDER BY [{1}] DESC", this.GetEntityName(), SyncLogFactory.FieldNames.TimeStamp)))
            {
                if (reader != null && reader.Read())
                {
                    return this.BuildEntityFromReader(reader);
                }
                return null;
            }
        }


        public DomainModel.DomainBase.SyncLog GetLastSuccessLog()
        {
            using (IDataReader reader = this.ExecuteReader(String.Format("SELECT TOP 1 * FROM [{0}] WHERE [{1}] = '{2}' ORDER BY [{3}] DESC", this.GetEntityName(), SyncLogFactory.FieldNames.Status, DomainModel.DomainBase.SyncLog.Status_SUCCESS, SyncLogFactory.FieldNames.TimeStamp)))
            {
                if (reader != null && reader.Read())
                {
                    return this.BuildEntityFromReader(reader);
                }
                return null;
            }
        }

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
            return " WHERE P.[ID] = {0};";
        }

        /// <summary>
        /// Returns the entity table name.
        /// </summary>
        /// <returns>Entity Table Nombre</returns>
        protected override string GetEntityName()
        {
            return "SyncLog"; 
        }

        /// <summary>
        /// Returns the Key of the entity
        /// </summary>
        /// <returns>Primary Key of the entity.</returns>
        protected override string GetKeyFieldName()
        {
            return SyncLogFactory.FieldNames.Id;
        }
    
    }
}
