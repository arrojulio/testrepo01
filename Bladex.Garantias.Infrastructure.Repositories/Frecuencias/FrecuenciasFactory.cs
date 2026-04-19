using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.Components;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Infrastructure.EntityFactoryFramework;

namespace Bladex.Garantias.Infrastructure.Repositories.Frecuencias
{
    internal class FrecuenciasFactory : IEntityFactory<Bladex.Garantias.DomainModel.DomainBase.Frecuencias>
    {
        #region Field Names

        internal static class FieldNames
        {
            public const string FrecuenciasId = "ID";
            public const string FrecuenciasName = "Nombre";
            public const string FrecuenciasUnidad = "Unidad";
            public const string FrecuenciasValorFrecuencia = "ValorFrecuencia";
        }

        #endregion

        #region IEntityFactory<Frecuencias> Members

        public DomainModel.DomainBase.Frecuencias BuildEntity(System.Data.IDataReader reader)
        {
            var frecuencias = new DomainModel.DomainBase.Frecuencias();
            frecuencias.Key = reader[FieldNames.FrecuenciasId].ToString();
            frecuencias.Nombre = reader[FieldNames.FrecuenciasName].ToString();
            frecuencias.ValorFrecuencia = DataHelper.GetInteger(reader[FieldNames.FrecuenciasValorFrecuencia]);
            frecuencias.Unidad = UnidadFrecuenciaResolver.Resolve(reader[FieldNames.FrecuenciasUnidad].ToString());
            // Si no tengo unidad definida, utilizo dias.
            if (!frecuencias.Unidad.HasValue)
                frecuencias.Unidad = UnidadFrecuenciaEnum.Days;
            return frecuencias;
        }

        #endregion
    }
}
