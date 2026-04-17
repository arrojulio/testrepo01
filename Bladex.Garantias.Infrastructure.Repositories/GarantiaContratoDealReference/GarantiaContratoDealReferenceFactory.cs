using Bladex.Garantias.Infrastructure.EntityFactoryFramework;
using System.Data;

namespace Bladex.Garantias.Infrastructure.Repositories.GarantiaContratoDealReference
{

    /// <summary>
    /// The garantia contrato deal reference factory class.
    /// </summary>
    internal class GarantiaContratoDealReferenceFactory : IEntityFactory<Bladex.Garantias.DomainModel.DomainBase.GarantiaContratoDealReference>
    {
        #region Field Names

        /// <summary>
        /// The field names class.
        /// </summary>
        internal static class FieldNames
        {
            public const string DealReference = "DealReference";
            public const string FechaRegistroInicial = "FechaRegistroInicial";
            public const string FechaVencimientoGarantia = "FechaVencimientoGarantia";
            public const string FechaVencimientoRiesgo = "FechaVencimientoRiesgo";
            public const string NetBalancePrincipal = "NetBalancePrincipal";
            public const string ProductGroupId = "ProductGroupId";
            public const string CustomerId = "CustomerId";
            public const string GlobalLineDescription = "GlobalLineDesc";
        }

        /// <summary>
        /// The table names class.
        /// </summary>
        internal static class TableNames
        {
            /// <summary>
            ///   <see cref="System.String"/>
            /// </summary>
            public const string GarantiaContratoDealReferences = "vw_GarantiaContrato_DealReference";
        }

        #endregion

        #region IEntityFactory<Project> Members

        /// <summary>
        /// Builds the entity.
        /// </summary>
        /// <param name="reader">The reader of type <see cref="System.Data.IDataReader"/></param>
        /// <returns></returns>
        public Bladex.Garantias.DomainModel.DomainBase.GarantiaContratoDealReference BuildEntity(IDataReader reader)
        {
            var entity = new Bladex.Garantias.DomainModel.DomainBase.GarantiaContratoDealReference {Key = DataHelper.GetInteger(reader[FieldNames.DealReference]), DealReference = DataHelper.GetString(reader[FieldNames.DealReference]), FechaRegistroInicial = DataHelper.GetNullableDateTime(reader[FieldNames.FechaRegistroInicial]), FechaVencimientoGarantia = DataHelper.GetNullableDateTime(reader[FieldNames.FechaVencimientoGarantia]), FechaVencimientoRiesgo = DataHelper.GetNullableDateTime(reader[FieldNames.FechaVencimientoRiesgo]), NetBalance = DataHelper.GetDecimal(reader[FieldNames.NetBalancePrincipal]), ProductGroup = DataHelper.GetString(reader[FieldNames.ProductGroupId]), GrupoEconomico = DataHelper.GetString(reader[FieldNames.GlobalLineDescription])};

            return entity;
        }

        #endregion
    }
}
