using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Infrastructure.EntityFactoryFramework;

namespace Bladex.Garantias.Infrastructure.Repositories.CalificacionesRiesgo
{
    internal class CalificacionesRiesgoFactory : IEntityFactory<Bladex.Garantias.DomainModel.DomainBase.CalificacionesRiesgo>
    {
        #region Field Names

        internal static class FieldNames
        {
            public const string CalificacionesRiesgoId = "ID";
            public const string CalificacionesRiesgoFitch = "Fitch";
            public const string CalificacionesRiesgoMoodys = "Moodys";
            public const string CalificacionesRiesgoOrden = "Orden";
            public const string CalificacionesRiesgoSyP = "SyP";

        }

        #endregion

        #region IEntityFactory<CalificacionesRiesgo> Members

        public DomainModel.DomainBase.CalificacionesRiesgo BuildEntity(System.Data.IDataReader reader)
        {
            var calificacionesRiesgo = new DomainModel.DomainBase.CalificacionesRiesgo();
            calificacionesRiesgo.Key = reader[FieldNames.CalificacionesRiesgoId].ToString();
            calificacionesRiesgo.Fitch = reader[FieldNames.CalificacionesRiesgoFitch].ToString();
            calificacionesRiesgo.Moodys = reader[FieldNames.CalificacionesRiesgoMoodys].ToString();
            calificacionesRiesgo.Orden = DataHelper.GetInteger(reader[FieldNames.CalificacionesRiesgoOrden].ToString());
            calificacionesRiesgo.SnP = reader[FieldNames.CalificacionesRiesgoSyP].ToString();
            return calificacionesRiesgo;
        }

        #endregion
    }
}
