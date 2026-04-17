using Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker;
using Bladex.Garantias.Infrastructure.EntityFactoryFramework;
using System.Data;

namespace Bladex.Garantias.Infrastructure.Repositories.Components.MakerChecker
{
    /// <summary>
    /// The maker checker changeset factory class.
    /// </summary>
    internal class MakerCheckerChangesetFactory : IEntityFactory<MakerCheckerChangeset>
    {
        #region Field Names

        /// <summary>
        /// The field names class.
        /// </summary>
        internal static class FieldNames
        {
            public const string ChangesetId = "ChangesetId";
            public const string MakerUserId = "MakerUserId";
            public const string ChangesetDate = "ChangesetDate";
            public const string ChangesetCommitDate = "ChangesetCommitDate";
            public const string ChangesetComment = "ChangesetComment";
        }

        #endregion

        #region IEntityFactory<Project> Members

        /// <summary>
        /// Builds the entity.
        /// </summary>
        /// <param name="reader">The reader of type <see cref="System.Data.IDataReader"/></param>
        /// <returns></returns>
        public Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker.MakerCheckerChangeset BuildEntity(IDataReader reader)
        {
            var entity = new MakerCheckerChangeset();
            entity.Key = DataHelper.GetGuid(reader[FieldNames.ChangesetId]);
            entity.ChangesetId = DataHelper.GetGuid(reader[FieldNames.ChangesetId]);
            entity.ChangesetDate = DataHelper.GetDateTime(reader[FieldNames.ChangesetDate]);
            entity.ChangesetCommitDate = DataHelper.GetNullableDateTime(reader[FieldNames.ChangesetCommitDate]);
            entity.MakerUserId = DataHelper.GetString(reader[FieldNames.MakerUserId]);
            entity.ChangesetComment = DataHelper.GetString(reader[FieldNames.ChangesetComment]);
            return entity;
           
        }

        #endregion

    }
}
