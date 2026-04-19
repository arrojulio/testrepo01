using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Infrastructure.EntityFactoryFramework;

namespace Bladex.Garantias.Infrastructure.Repositories.TipoGarantiaBladex
{
    internal class TipoGarantiaBladexFactory : IEntityFactory<Bladex.Garantias.DomainModel.DomainBase.TipoGarantiaBladex>
    {
        #region Field Names

        internal static class FieldNames
        {
            public const string TipoGarantiaBladexId = "ID";
            public const string TipoGarantiaBladexName = "Nombre";
        }

        #endregion

        #region IEntityFactory<TipoGarantiaBladex> Members

        public DomainModel.DomainBase.TipoGarantiaBladex BuildEntity(System.Data.IDataReader reader)
        {
            var tipoGarantiaBladex = new DomainModel.DomainBase.TipoGarantiaBladex();
            tipoGarantiaBladex.Key = reader[FieldNames.TipoGarantiaBladexId].ToString();
            tipoGarantiaBladex.Nombre = reader[FieldNames.TipoGarantiaBladexName].ToString();
            return tipoGarantiaBladex;
        }

        #endregion
    }
}
