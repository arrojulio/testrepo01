using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Infrastructure.EntityFactoryFramework;

namespace Bladex.Garantias.Infrastructure.Repositories.Monedas
{
    internal class MonedasFactory : IEntityFactory<Bladex.Garantias.DomainModel.DomainBase.Monedas>
    {
        #region Field Names

        internal static class FieldNames
        {
            public const string MonedasId = "ID";
            public const string MonedasName = "Nombre";

        }

        #endregion

        #region IEntityFactory<Monedas> Members

        public DomainModel.DomainBase.Monedas BuildEntity(System.Data.IDataReader reader)
        {
            var monedas = new DomainModel.DomainBase.Monedas();
            monedas.Key = reader[FieldNames.MonedasId].ToString();
            monedas.Nombre = reader[FieldNames.MonedasName].ToString();
            return monedas;
        }

        #endregion
    }
}