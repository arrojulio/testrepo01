using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Infrastructure.EntityFactoryFramework;

namespace Bladex.Garantias.Infrastructure.Repositories.Fiduciarias
{
    internal class FiduciariasFactory : IEntityFactory<Bladex.Garantias.DomainModel.DomainBase.Fiduciarias>
    {
        #region Field Names

        internal static class FieldNames
        {
            public const string FiduciariasId = "ID";
            public const string FiduciariasName = "Nombre";
        }

        #endregion

        #region IEntityFactory<Fiduciarias> Members

        public DomainModel.DomainBase.Fiduciarias BuildEntity(System.Data.IDataReader reader)
        {
            var fiduciarias = new DomainModel.DomainBase.Fiduciarias();
            fiduciarias.Key = reader[FieldNames.FiduciariasId].ToString();
            fiduciarias.Nombre = reader[FieldNames.FiduciariasName].ToString();
            return fiduciarias;
        }

        #endregion
    }
}
