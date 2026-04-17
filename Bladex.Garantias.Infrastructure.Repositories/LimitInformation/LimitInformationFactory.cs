using Bladex.Garantias.Infrastructure.EntityFactoryFramework;

namespace Bladex.Garantias.Infrastructure.Repositories.LimitInformation
{
    internal class LimitInformationFactory : IEntityFactory<Bladex.Garantias.DomainModel.DomainBase.LimitInformation>
    {
        #region Field Names

        internal static class FieldNames
        {
            public const string DefinitionId = "DefinitionId";
            public const string CustomerId = "CustomerId";
            public const string MatrixId = "MatrixId";
            public const string MatrixName = "MatrixName";
            public const string Comments = "Comments";
            public const string LimitValue = "LimitValue";
            public const string LastLimit = "LastLimit";
            public const string ExpirationDate = "ExpirationDate";
            public const string MatrixStateId = "MatrixStateId";
            public const string MatrixStateDescription = "MatrixStateDescription";
            public const string MatrixTypeId = "MatrixTypeId";
            public const string MatrixTypeDescription = "MatrixTypeDescription";
        }

        #endregion

        #region IEntityFactory<InstrumentoFinanciero> Members
        public DomainModel.DomainBase.LimitInformation BuildEntity(System.Data.IDataReader reader)
        {
            var limitInformation = new DomainModel.DomainBase.LimitInformation();
            limitInformation.DefinitionId = DataHelper.GetInteger(reader[FieldNames.DefinitionId]);
            limitInformation.CustomerId = reader[FieldNames.CustomerId].ToString();
            
            // Mapeo el cliente
            limitInformation.Key = limitInformation.CustomerId;
            
            limitInformation.Comments = reader[FieldNames.Comments].ToString();
            limitInformation.ExpirationDate = DataHelper.GetDateTime(reader[FieldNames.ExpirationDate]);
            limitInformation.LastLimit = DataHelper.GetDecimal(reader[FieldNames.LastLimit]);
            limitInformation.LimitValue = DataHelper.GetDecimal(reader[FieldNames.LimitValue]);
            limitInformation.MatrixId = DataHelper.GetInteger(reader[FieldNames.MatrixId]);
            limitInformation.MatrixName = reader[FieldNames.MatrixName].ToString();
            limitInformation.MatrixStateId = DataHelper.GetInteger(reader[FieldNames.MatrixStateId]);
            limitInformation.MatrixStateDescription = reader[FieldNames.MatrixStateDescription].ToString();
            limitInformation.MatrixTypeId = DataHelper.GetInteger(reader[FieldNames.MatrixTypeId]);
            limitInformation.MatrixTypeDescription = reader[FieldNames.MatrixTypeDescription].ToString();
            return limitInformation;
        }

        #endregion
    }
}






