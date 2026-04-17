using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Infrastructure.EntityFactoryFramework;

namespace Bladex.Garantias.Infrastructure.Repositories.Aval
{
    internal class AvalFactory : IEntityFactory<Bladex.Garantias.DomainModel.DomainBase.Aval>
    {
        #region Field Names

        internal static class FieldNames
        {
            public const string AvalId = "ID";
            public const string AvalNombre = "Nombre";
            public const string AvalPorcentajeCobertura = "PorcentajeCobertura";
            public const string AvalPaisId = "CountryID";
            public const string AvalEsCliente = "Escliente";
            public const string AvalTipoAvalId = "TipoAvalId";
            public const string AvalGarantiaId = "GarantiaId";
        }

        #endregion

        #region IEntityFactory<Aval> Members

        public DomainModel.DomainBase.Aval BuildEntity(System.Data.IDataReader reader)
        {
            var aval = new DomainModel.DomainBase.Aval();
            aval.Key = DataHelper.GetInteger(reader[FieldNames.AvalId]);
            aval.Nombre = reader[FieldNames.AvalNombre].ToString();
            aval.EsCliente = DataHelper.GetBoolean(reader[FieldNames.AvalEsCliente].ToString());
            aval.GarantiaId = DataHelper.GetInteger(reader[FieldNames.AvalGarantiaId].ToString());
            aval.PorcentajeCobertura = DataHelper.GetDouble(reader[FieldNames.AvalPorcentajeCobertura].ToString());
            return aval;

        }

        #endregion
    }
}
