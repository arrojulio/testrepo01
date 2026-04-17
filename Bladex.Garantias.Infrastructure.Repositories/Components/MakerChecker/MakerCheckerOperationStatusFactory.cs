using Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker;
using Bladex.Garantias.Infrastructure.EntityFactoryFramework;
using System.Data;

namespace Bladex.Garantias.Infrastructure.Repositories.Components.MakerChecker
{
    /// <summary>
    /// The maker checker operation status factory class.
    /// </summary>
    internal class MakerCheckerOperationStatusFactory : IEntityFactory<MakerCheckerOperationStatus>
    {
        #region Field Names

        internal static class FieldNames
        {
            public const string OperationStatusId = "OperationStatusId";
            public const string OperationStatusDescription = "OperationStatusDescription";
        }

        #endregion

        #region IEntityFactory<Project> Members

        /// <summary>
        /// Builds the entity.
        /// </summary>
        /// <param name="reader">The reader of type <see cref="System.Data.IDataReader"/></param>
        /// <returns></returns>
        public Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker.MakerCheckerOperationStatus BuildEntity(IDataReader reader)
        {
            var entity = new MakerCheckerOperationStatus();
            entity.Key = DataHelper.GetInteger(reader[FieldNames.OperationStatusId]);
            entity.OperationStatusId = DataHelper.GetInteger(reader[FieldNames.OperationStatusId]);
            entity.OperationStatusDescription = reader[FieldNames.OperationStatusDescription].ToString();
            return entity;
           
        }

        #endregion

    }
}
