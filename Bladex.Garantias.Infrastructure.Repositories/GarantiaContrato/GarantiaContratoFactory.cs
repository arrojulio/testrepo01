using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Infrastructure.EntityFactoryFramework;
using System.Data;

namespace Bladex.Garantias.Infrastructure.Repositories.GarantiaContrato
{
    /// <summary>
    /// The garantia contrato factory class.
    /// </summary>
    internal class GarantiaContratoFactory : IEntityFactory<Bladex.Garantias.DomainModel.DomainBase.GarantiaContrato>
    {
        #region Field Names

        /// <summary>
        /// The field names class.
        /// </summary>
        internal static class FieldNames
        {
            public const string Id = "ID";
            public const string DealReference = "DealReference";
            public const string PorcUtilization = "PorcUtilization";
            public const string GarantiaId = "GarantiaId";
            public const string FechaRegistroInicial = "FechaRegistroInicial";
            public const string FechaVencimientoGarantia = "FechaVencimientoGarantia";
            public const string FechaVencimientoRiesgo = "FechaVencimientoRiesgo";
            public const string NetBalancePrincipal = "NetBalancePrincipal";
            public const string ProductGroupId = "ProductGroupId";
            public const string CustomerId = "CustomerId";
        }

        /// <summary>
        /// The table names class.
        /// </summary>
        internal static class TableNames
        {
            public const string GarantiaContratoTable = "GarantiaContrato";
            public const string GarantiaContratoDealReferences = "vw_GarantiaContrato_DealReference";
        }

        #endregion

        #region IEntityFactory<Project> Members

        /// <summary>
        /// Builds the entity.
        /// </summary>
        /// <param name="reader">The reader of type <see cref="System.Data.IDataReader"/></param>
        /// <returns></returns>
        public Bladex.Garantias.DomainModel.DomainBase.GarantiaContrato BuildEntity(IDataReader reader)
        {

            var garantiaContrato = AutoMapper.Mapper.Map<System.Data.IDataReader, Bladex.Garantias.DomainModel.DomainBase.GarantiaContrato>(reader);

            return garantiaContrato;

        }

        /// <summary>
        /// Builds the second entity.
        /// </summary>
        /// <param name="reader">The reader of type <see cref="System.Data.IDataReader"/></param>
        /// <returns></returns>
        public Bladex.Garantias.DomainModel.DomainBase.GarantiaContratoDealReference BuildSecondEntity(IDataReader reader)
        {
            AutoMapper.Mapper.CreateMap<System.Data.IDataReader, Bladex.Garantias.DomainModel.DomainBase.GarantiaContratoDealReference>();
            var entity = AutoMapper.Mapper.Map<System.Data.IDataReader, Bladex.Garantias.DomainModel.DomainBase.GarantiaContratoDealReference>(reader);
            return entity;
        }

        #endregion
    }
}
