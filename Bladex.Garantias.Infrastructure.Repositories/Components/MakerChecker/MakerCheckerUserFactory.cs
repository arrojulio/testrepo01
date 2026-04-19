using Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker;
using Bladex.Garantias.Infrastructure.EntityFactoryFramework;
using System.Data;

namespace Bladex.Garantias.Infrastructure.Repositories.Components.MakerChecker
{
    /// <summary>
    /// The maker checker user factory class.
    /// </summary>
    internal class MakerCheckerUserFactory : IEntityFactory<MakerCheckerUser>
    {
        #region Field Names

        internal static class FieldNames
        {
            // TODO: Map SQL Column Names here.
            public const string UserId = "UserId";
            public const string Email = "UserEmail";
            public const string RoleId = "RoleId";
        }

        #endregion

        #region IEntityFactory<Project> Members

        /// <summary>
        /// Builds the entity.
        /// </summary>
        /// <param name="reader">The reader of type <see cref="System.Data.IDataReader"/></param>
        /// <returns></returns>
        public Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker.MakerCheckerUser BuildEntity(IDataReader reader)
        {
            var entity = new MakerCheckerUser {Key = reader[FieldNames.UserId].ToString(), UserId = reader[FieldNames.UserId].ToString(), Email = reader[FieldNames.Email].ToString()};
            return entity;
           
        }

        #endregion

    }
}
