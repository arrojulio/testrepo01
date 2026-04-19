using Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker;
using Bladex.Garantias.Infrastructure.EntityFactoryFramework;
using System.Data;

namespace Bladex.Garantias.Infrastructure.Repositories.Components.MakerChecker
{
    /// <summary>
    /// The maker checker Operation factory class.
    /// </summary>
    internal class MakerCheckerOperationFactory : IEntityFactory<MakerCheckerOperation>
    {
        #region Field Names

        /// <summary>
        /// The field names class.
        /// </summary>
        internal static class FieldNames
        {
            public const string OperationId = "OperationId";
            public const string ChangesetId = "ChangesetId";
            public const string CheckerUserId = "CheckerUserId";
            public const string CheckerDate = "CheckerDate";
            public const string MakerDate = "MakerDate";
            public const string OperationStatusId = "OperationStatusId";
            public const string ItemId = "ItemId";
            public const string Item = "Item";
            public const string ItemType = "ItemType";
            public const string Comment = "Comment";
        }

        #endregion

        #region IEntityFactory<Project> Members

        /// <summary>
        /// Builds the entity.
        /// </summary>
        /// <param name="reader">The reader of type <see cref="System.Data.IDataReader"/></param>
        /// <returns></returns>
        public Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker.MakerCheckerOperation BuildEntity(IDataReader reader)
        {
            var entity = new MakerCheckerOperation();
            entity.Key = DataHelper.GetInteger(reader[FieldNames.OperationId]);
            entity.OperationId = DataHelper.GetInteger(reader[FieldNames.OperationId]);
            entity.ChangesetId = DataHelper.GetGuid(reader[FieldNames.ChangesetId]);
            entity.CheckerDate = DataHelper.GetNullableDateTime(reader[FieldNames.CheckerDate]);
            entity.CheckerUserId = DataHelper.GetString(reader[FieldNames.CheckerUserId]);
            entity.Comment = DataHelper.GetString(reader[FieldNames.Comment]);
            entity.ItemId = DataHelper.GetNullableInteger(reader[FieldNames.ItemId]);
            entity.MakerDate = DataHelper.GetDateTime(reader[FieldNames.MakerDate]);
            entity.OperationStatusId = DataHelper.GetInteger(reader[FieldNames.OperationStatusId]);
            entity.ItemType = reader[FieldNames.ItemType].ToString();
            entity.Item = reader[FieldNames.Item].ToString();
            return entity;
           
        }

        #endregion

    }
}
