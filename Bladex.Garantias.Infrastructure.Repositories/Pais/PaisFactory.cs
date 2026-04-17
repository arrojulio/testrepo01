using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Infrastructure.EntityFactoryFramework;
using System.Data;

namespace Bladex.Garantias.Infrastructure.Repositories.Pais
{
    internal class PaisFactory : IEntityFactory<Bladex.Garantias.DomainModel.DomainBase.Pais>
    {
        #region Field Names

        internal static class FieldNames
        {
            // TODO: Map SQL Column Names here.
            public const string PaisId = "ID";
            public const string PaisNombre = "Nombre";
            public const string CodigoSuper = "CodigoSuper";
        }

        #endregion

        #region IEntityFactory<PaisId> Members

        public Bladex.Garantias.DomainModel.DomainBase.Pais BuildEntity(IDataReader reader)
        {
            var pais = new DomainModel.DomainBase.Pais();
            pais.Key = reader[FieldNames.PaisId].ToString();
            pais.Nombre = reader[FieldNames.PaisNombre].ToString();
            pais.CodigoSuper = reader[FieldNames.CodigoSuper].ToString();

            // Examples of retrieval of diffent data types
            // Nullable Dates
            //cliente.ContractDate = DataHelper.GetNullableDateTime(reader[FieldNames.ContractDate]);
            // Decimal types
            //cliente.Amount = DataHelper.GetDecimal(reader[FieldNames.Amount]);
            // Integer types
            //cliente.quantities = DataHelper.GetInteger(reader[FieldNames.Quantities]);
            // Another object (entity)
            //cliente.PaisId = new PaisId();
            //cliente.PaisId.Key = reader[FieldNames.CodigoPaisId].ToString();
            //cliente.PaisId.Nombre = reader[FieldNames.PaisNombre].ToString();


            return pais;
        }

        #endregion

    }
}
