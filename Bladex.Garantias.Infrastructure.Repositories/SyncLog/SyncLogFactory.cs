using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Infrastructure.EntityFactoryFramework;
using System.Data;

namespace Bladex.Garantias.Infrastructure.Repositories.SyncLog
{
    internal class SyncLogFactory : IEntityFactory<Bladex.Garantias.DomainModel.DomainBase.SyncLog>
    {
        #region Field Names

        internal static class FieldNames
        {
            public const string Id = "ID";
            public const string TimeStamp = "TimeStamp";
            public const string Status = "Status";
            public const string FechaCorte = "FechaCorte";
            public const string ItemsAdded = "ItemsAdded";
            public const string ItemsUpdated = "ItemsUpdated";
            public const string ItemsDeleted = "ItemsDeleted";
            public const string Message = "Message";

        }

        #endregion

        #region IEntityFactory<PaisId> Members

        public Bladex.Garantias.DomainModel.DomainBase.SyncLog BuildEntity(IDataReader reader)
        {
            Bladex.Garantias.DomainModel.DomainBase.SyncLog synclog = new DomainModel.DomainBase.SyncLog(reader[SyncLogFactory.FieldNames.Status].ToString(), reader[FieldNames.Message].ToString());
            synclog.FechaCorte = DataHelper.GetNullableDateTime(reader[FieldNames.FechaCorte]);
            synclog.ItemsAdded = reader[FieldNames.ItemsAdded].ToString();
            synclog.ItemsDeleted = reader[FieldNames.ItemsDeleted].ToString();
            synclog.ItemsUpdated = reader[FieldNames.ItemsUpdated].ToString();
            synclog.Key = DataHelper.GetNullableInteger(reader[FieldNames.Id]);
            synclog.TimeStamp = DataHelper.GetDateTime(reader[FieldNames.TimeStamp]);
            return synclog;
        }

        #endregion
    }
}
