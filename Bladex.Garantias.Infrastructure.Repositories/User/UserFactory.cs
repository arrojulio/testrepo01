using Bladex.Garantias.Infrastructure.EntityFactoryFramework;
using System.Data;

namespace Bladex.Garantias.Infrastructure.Repositories.User
{
    /// <summary>
    /// The user factory class.
    /// </summary>
    internal class UserFactory : IEntityFactory<Bladex.Garantias.DomainModel.DomainBase.User>
    {
        #region Field Names

        /// <summary>
        ///   <see cref="System.String"/>
        /// </summary>
        internal static string TableName = "Application_User";
        /// <summary>
        /// The field names class.
        /// </summary>
        internal static class FieldNames
        {
            /// <summary>
            ///   <see cref="System.String"/>
            /// </summary>
            public const string UserId = "UserId";

            /// <summary>
            ///   <see cref="System.String"/>
            /// </summary>
            public const string UserName = "UserName";
            /// <summary>
            ///   <see cref="System.String"/>
            /// </summary>
            public const string RoleId = "RoleId";
        }

        #endregion

        #region IEntityFactory<User> Members

        /// <summary>
        /// Builds the entity.
        /// </summary>
        /// <param name="reader">The reader of type <see cref="System.Data.IDataReader"/></param>
        /// <returns></returns>
        public Bladex.Garantias.DomainModel.DomainBase.User BuildEntity(IDataReader reader)
        {
            var entity = new DomainModel.DomainBase.User {Key = reader[FieldNames.UserId].ToString(), UserName = reader[FieldNames.UserName].ToString(), RoleId = (int)reader[FieldNames.RoleId]};
            return entity;
        }

        #endregion

    }
}
