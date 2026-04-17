using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Infrastructure.EntityFactoryFramework;

namespace Bladex.Garantias.Infrastructure.Repositories.Aseguradoras
{
    internal class AseguradorasFactory : IEntityFactory<Bladex.Garantias.DomainModel.DomainBase.Aseguradoras>
    {
        #region Field Names

        internal static class FieldNames
        {
            public const string AseguradorasId = "ID";
            public const string AseguradorasName = "Nombre";
        }

        #endregion

        #region IEntityFactory<Aseguradoras> Members

        public DomainModel.DomainBase.Aseguradoras BuildEntity(System.Data.IDataReader reader)
        {
            var aseguradoras = new DomainModel.DomainBase.Aseguradoras();
            aseguradoras.Key = reader[FieldNames.AseguradorasId].ToString();
            aseguradoras.Nombre = reader[FieldNames.AseguradorasName].ToString();
            return aseguradoras;
        }

        #endregion
    }
}
