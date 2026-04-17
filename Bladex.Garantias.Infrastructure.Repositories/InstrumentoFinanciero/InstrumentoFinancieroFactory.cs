using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Infrastructure.EntityFactoryFramework;

namespace Bladex.Garantias.Infrastructure.Repositories.InstrumentoFinanciero
{
    internal class InstrumentoFinancieroFactory : IEntityFactory<Bladex.Garantias.DomainModel.DomainBase.InstrumentoFinanciero>
    {
        #region Field Names

        internal static class FieldNames
        {
            public const string InstrumentoFinancieroId = "ID";
            public const string InstrumentoFinancieroName = "Nombre";
        }

        #endregion

        #region IEntityFactory<InstrumentoFinanciero> Members
        public DomainModel.DomainBase.InstrumentoFinanciero BuildEntity(System.Data.IDataReader reader)
        {
            var instrumentoFinanciero = new DomainModel.DomainBase.InstrumentoFinanciero();
            instrumentoFinanciero.Key = reader[FieldNames.InstrumentoFinancieroId].ToString();
            instrumentoFinanciero.Nombre = reader[FieldNames.InstrumentoFinancieroName].ToString();
            return instrumentoFinanciero;
        }

        #endregion
    }
}






