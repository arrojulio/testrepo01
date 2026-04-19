using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Infrastructure.EntityFactoryFramework;

namespace Bladex.Garantias.Infrastructure.Repositories.InternalStatus
{
    internal class InternalStatusFactory : IEntityFactory<Bladex.Garantias.DomainModel.DomainBase.InternalStatus>
    {
        #region Field Names
        internal static string TableName = "InternalStatus";
        internal static class FieldNames
        {
            public const string InternalStatusId = "ID";
            public const string InternalStatusName = "Nombre";
        }

        #endregion

        #region IEntityFactory<Status> Members
        public DomainModel.DomainBase.InternalStatus BuildEntity(System.Data.IDataReader reader)
        {
            var status = new DomainModel.DomainBase.InternalStatus();
            status.Key = reader[FieldNames.InternalStatusId].ToString();
            status.Nombre = reader[FieldNames.InternalStatusName].ToString();
            return status;
        }

        #endregion
    }
}
