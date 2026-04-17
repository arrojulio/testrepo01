using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Infrastructure.EntityFactoryFramework;

namespace Bladex.Garantias.Infrastructure.Repositories.GrupoRiesgoGarantia
{
    internal class GrupoRiesgoGarantiaFactory : IEntityFactory<Bladex.Garantias.DomainModel.DomainBase.GrupoRiesgoGarantia>
    {
        #region Field Names

        internal static class FieldNames
        {
            public const string GrupoRiesgoGarantiaId = "ID";
            public const string GrupoRiesgoGarantiaName = "Nombre";
        }

        #endregion

        #region IEntityFactory<GrupoRiesgoGarantia> Members

        public DomainModel.DomainBase.GrupoRiesgoGarantia BuildEntity(System.Data.IDataReader reader)
        {
            var grupoRiesgoGarantia = new DomainModel.DomainBase.GrupoRiesgoGarantia();
            grupoRiesgoGarantia.Key = reader[FieldNames.GrupoRiesgoGarantiaId].ToString();
            grupoRiesgoGarantia.Nombre = reader[FieldNames.GrupoRiesgoGarantiaName].ToString();
            return grupoRiesgoGarantia;
        }

        #endregion
    }
}
