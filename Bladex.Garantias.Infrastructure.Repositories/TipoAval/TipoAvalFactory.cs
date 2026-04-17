using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Infrastructure.EntityFactoryFramework;

namespace Bladex.Garantias.Infrastructure.Repositories.TipoAval
{
    internal class TipoAvalFactory : IEntityFactory<Bladex.Garantias.DomainModel.DomainBase.TipoAval>
    {
        #region Field Names

        internal static class FieldNames
        {
            public const string TipoAvalId = "ID";
            public const string TipoAvalName = "Nombre";
        }

        #endregion

        #region IEntityFactory<TipoAval> Members

        public DomainModel.DomainBase.TipoAval BuildEntity(System.Data.IDataReader reader)
        {
            var tipoAval = new DomainModel.DomainBase.TipoAval();
            tipoAval.Key = reader[FieldNames.TipoAvalId].ToString();
            tipoAval.Nombre = reader[FieldNames.TipoAvalName].ToString();
            return tipoAval;
        }

        #endregion
    }
}
