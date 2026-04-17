using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Infrastructure.EntityFactoryFramework;

namespace Bladex.Garantias.Infrastructure.Repositories.Status
{
    internal class StatusFactory : IEntityFactory<Bladex.Garantias.DomainModel.DomainBase.Status>
    {
        #region Field Names
        internal static string TableName = "Status";
        
        internal static class FieldNames
        {
            public const string StatusId = "ID";
            public const string StatusName = "Nombre";
        }

        #endregion

        #region IEntityFactory<Status> Members
        public DomainModel.DomainBase.Status BuildEntity(System.Data.IDataReader reader)
        {
            var status = new DomainModel.DomainBase.Status();
            status.Key = reader[FieldNames.StatusId].ToString();
            status.Nombre = reader[FieldNames.StatusName].ToString();
            return status;
        }

        #endregion
    }
}
