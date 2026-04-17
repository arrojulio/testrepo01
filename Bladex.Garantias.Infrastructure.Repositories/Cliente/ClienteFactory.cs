using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Infrastructure.EntityFactoryFramework;
using System.Data;

namespace Bladex.Garantias.Infrastructure.Repositories.Cliente
{
    internal class ClienteFactory : IEntityFactory<Bladex.Garantias.DomainModel.DomainBase.Cliente>
    {
        #region Field Names

        internal static class FieldNames
        {
            // TODO: Map SQL Column Names here.
            public const string ClienteId = "ID";
            public const string CodigoPaisId = "CountryID";
            public const string GrupoEconomico = "EconomicGroup";
            public const string Nombre = "Nombre";
            public const string ClienteRating = "Rating";
            public const string NationalId = "NationalId";
            public const string BusinessLineDescription = "BUSINESS_LINE_DESC_2";
            public const string AccountOfficer = "AccountOfficer";
            public const string GlobalLineDescription = "GLOBAL_LINE_DESC";
            public const string AuthStat = "AUTH_STAT";
            public const string RecordStart = "RECORD_STAT";
            public const string IsActive = "CUSTOMER_ACTIVE";
            public const string LimitExpDate = "LIMIT_EXP_DATE";
            public const string Internal = "Internal";
            
        }

        #endregion

        #region IEntityFactory<Project> Members

        public Bladex.Garantias.DomainModel.DomainBase.Cliente BuildEntity(IDataReader reader)
        {
            var cliente = new Bladex.Garantias.DomainModel.DomainBase.Cliente();

            cliente.Key = reader[FieldNames.ClienteId].ToString();
            cliente.NationalId = reader[FieldNames.NationalId].ToString();
            cliente.GrupoEconomico = reader[FieldNames.GrupoEconomico].ToString();
            cliente.Rating = reader[FieldNames.ClienteRating].ToString();
            cliente.Nombre = reader[FieldNames.Nombre].ToString();
            cliente.BusinessLineDescription = reader[FieldNames.BusinessLineDescription].ToString();
            cliente.AccountOfficer = reader[FieldNames.AccountOfficer].ToString();
            cliente.GlobalLineDescription = reader[FieldNames.GlobalLineDescription].ToString();
            cliente.IsActive = DataHelper.GetNullableBoolean(reader[FieldNames.IsActive]);
            cliente.AuthStat = reader[FieldNames.AuthStat].ToString();
            cliente.LimitExpDate = reader[FieldNames.LimitExpDate].ToString();
            cliente.RecordStat = reader[FieldNames.RecordStart].ToString();
            cliente.IsInternal = DataHelper.GetBoolean(reader[FieldNames.Internal]);
            return cliente;
           
        }

        #endregion

    }
}
