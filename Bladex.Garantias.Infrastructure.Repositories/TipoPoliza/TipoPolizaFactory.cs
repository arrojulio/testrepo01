using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Infrastructure.EntityFactoryFramework;
using System.Data;

namespace Bladex.Garantias.Infrastructure.Repositories.TipoPoliza
{
    public class TipoPolizaFactory : IEntityFactory<Bladex.Garantias.DomainModel.DomainBase.TipoPoliza>
    {
        #region Field Names

        /// <summary>
        ///   <see cref="System.String"/>
        /// </summary>
        internal static string TableName = "TipoPoliza";
        /// <summary>
        /// The field names class.
        /// </summary>
        internal static class FieldNames
        {
            /// <summary>
            ///   <see cref="System.String"/>
            /// </summary>
            public const string TipoPolizaId = "TipoPolizaId";
            /// <summary>
            ///   <see cref="System.String"/>
            /// </summary>
            public const string TipoPolizaName = "TipoPolizaName";
        }

        #endregion

        #region IEntityFactory<TipoPoliza> Members

        /// <summary>
        /// Builds the entity.
        /// </summary>
        /// <param name="reader">The reader of type <see cref="System.Data.IDataReader"/></param>
        /// <returns></returns>
        public Bladex.Garantias.DomainModel.DomainBase.TipoPoliza BuildEntity(IDataReader reader)
        {
            var tipoPoliza = new DomainModel.DomainBase.TipoPoliza { Key = reader[FieldNames.TipoPolizaId].ToString(), Nombre = reader[FieldNames.TipoPolizaName].ToString() };
            return tipoPoliza;
        }

        #endregion
    }
}
