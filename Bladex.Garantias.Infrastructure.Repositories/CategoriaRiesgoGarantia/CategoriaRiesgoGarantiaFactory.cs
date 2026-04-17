using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Infrastructure.EntityFactoryFramework;

namespace Bladex.Garantias.Infrastructure.Repositories.CategoriaRiesgoGarantia
{
    internal class CategoriaRiesgoGarantiaFactory : IEntityFactory<Bladex.Garantias.DomainModel.DomainBase.CategoriaRiesgoGarantia>
    {
        #region Field Names

        internal static class FieldNames
        {
            public const string Id = "ID";
            public const string Nombre = "Nombre";
            public const string Grupo = "Grupo";

        }

        #endregion

        #region IEntityFactory<CategoriaRiesgoGarantia> Members

        public DomainModel.DomainBase.CategoriaRiesgoGarantia BuildEntity(System.Data.IDataReader reader)
        {
            var calificacionesRiesgo = new DomainModel.DomainBase.CategoriaRiesgoGarantia();
            calificacionesRiesgo.Key = reader[FieldNames.Id].ToString();
            calificacionesRiesgo.Nombre = reader[FieldNames.Nombre].ToString();

            return calificacionesRiesgo;
        }

        #endregion
    }
}
