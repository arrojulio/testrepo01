using Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker;
using Bladex.Garantias.Infrastructure.EntityFactoryFramework;
using System.Data;

namespace Bladex.Garantias.Infrastructure.Repositories.Components.MakerChecker
{
    /// <summary>
    /// The maker checker role factory class.
    /// </summary>
    internal class MakerCheckerRoleFactory : IEntityFactory<MakerCheckerRole>
    {
        #region Field Names

        internal static class FieldNames
        {
            public const string RoleId = "RoleID";
            public const string RoleName = "RoleName";
        }

        #endregion

        #region IEntityFactory<Project> Members

        /// <summary>
        /// Builds the entity.
        /// </summary>
        /// <param name="reader">The reader of type <see cref="System.Data.IDataReader"/></param>
        /// <returns></returns>
        public Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker.MakerCheckerRole BuildEntity(IDataReader reader)
        {
            var entity = new MakerCheckerRole();
            entity.Key = DataHelper.GetInteger(reader[FieldNames.RoleId]);
            entity.RoleId = DataHelper.GetInteger(reader[FieldNames.RoleId]);
            entity.RoleName = reader[FieldNames.RoleName].ToString();
            return entity;
           
        }

        #endregion

    }
}
