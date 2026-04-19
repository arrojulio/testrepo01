using Bladex.Garantias.Infrastructure.EntityFactoryFramework;
using System.Data;

namespace Bladex.Garantias.Infrastructure.Repositories.Role
{
    /// <summary>
    /// The role factory class.
    /// </summary>
    internal class RoleFactory : IEntityFactory<Bladex.Garantias.DomainModel.DomainBase.Role>
    {
        #region Field Names

        /// <summary>
        ///   <see cref="System.String"/>
        /// </summary>
        internal static string TableName = "Application_Role";
        /// <summary>
        /// The field names class.
        /// </summary>
        internal static class FieldNames
        {
            /// <summary>
            ///   <see cref="System.String"/>
            /// </summary>
            public const string RoleId = "RoleId";
            /// <summary>
            ///   <see cref="System.String"/>
            /// </summary>
            public const string RoleName = "RoleName";
        }

        #endregion

        #region IEntityFactory<Role> Members

        /// <summary>
        /// Builds the entity.
        /// </summary>
        /// <param name="reader">The reader of type <see cref="System.Data.IDataReader"/></param>
        /// <returns></returns>
        public Bladex.Garantias.DomainModel.DomainBase.Role BuildEntity(IDataReader reader)
        {
            var role = new DomainModel.DomainBase.Role {Key = (int)reader[FieldNames.RoleId], RoleName = reader[FieldNames.RoleName].ToString()};
            return role;
        }

        #endregion

    }
}
