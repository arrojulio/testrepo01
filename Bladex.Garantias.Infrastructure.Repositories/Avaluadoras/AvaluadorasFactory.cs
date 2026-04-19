using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Infrastructure.EntityFactoryFramework;

namespace Bladex.Garantias.Infrastructure.Repositories.Avaluadoras
{
    internal class AvaluadorasFactory : IEntityFactory<Bladex.Garantias.DomainModel.DomainBase.Avaluadoras>
    {
        #region Field Names

        internal static class FieldNames
        {
            public const string AvaluadorasId = "ID";
            public const string AvaluadorasName = "Nombre";
        }

        #endregion

        #region IEntityFactory<Avaluadoras> Members

        public DomainModel.DomainBase.Avaluadoras BuildEntity(System.Data.IDataReader reader)
        {
            var avaluadoras = new DomainModel.DomainBase.Avaluadoras();
            avaluadoras.Key = reader[FieldNames.AvaluadorasId].ToString();
            avaluadoras.Nombre = reader[FieldNames.AvaluadorasName].ToString();
            return avaluadoras;
        }

        #endregion
    }
}
