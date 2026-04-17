using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Infrastructure.EntityFactoryFramework;

namespace Bladex.Garantias.Infrastructure.Repositories.Bancos
{
    internal class BancosFactory : IEntityFactory<Bladex.Garantias.DomainModel.DomainBase.Bancos>
    {
        #region Field Names

        internal static class FieldNames
        {
            public const string BancosId = "ID";
            public const string BancosName = "Nombre";
            public const string BancosCategoria = "Categoria";
        }

        #endregion

        #region IEntityFactory<Bancos> Members

        public DomainModel.DomainBase.Bancos BuildEntity(System.Data.IDataReader reader)
        {
            var bancos = new DomainModel.DomainBase.Bancos();
            bancos.Key = reader[FieldNames.BancosId].ToString();
            bancos.Nombre = reader[FieldNames.BancosName].ToString();
            bancos.Categoria = reader[FieldNames.BancosCategoria].ToString();
            return bancos;
        }

        #endregion
    }
}
