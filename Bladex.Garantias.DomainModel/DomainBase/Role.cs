using Bladex.Garantias.Infrastructure.DomainBase;

namespace Bladex.Garantias.DomainModel.DomainBase
{
    /// <summary>
    /// The user role class.
    /// </summary>
    public class Role : EntityBase
    {
        /// <summary>
        /// Gets or sets the role id.
        /// </summary>
        /// <value>
        /// The role id of type <see cref="System.Int32"/>
        /// </value>
        public int RoleId
        {
            get { return this.GetKeyAs<int>(); }
            set { this.Key = value; }
        }

        /// <summary>
        /// Gets or sets the name of the role.
        /// </summary>
        /// <value>
        /// The name of the role.
        /// </value>
        public string RoleName
        {
            get;
            set;
        }

        public AvailableRoles RoleEnum
        {
            get 
            {
                if (this.GetKeyAs<int>() == (int)AvailableRoles.PowerUser) return AvailableRoles.PowerUser;
                if (this.GetKeyAs<int>() == (int)AvailableRoles.ReadOnly) return AvailableRoles.ReadOnly;
                if (this.GetKeyAs<int>() == (int)AvailableRoles.Admin) return AvailableRoles.Admin;
                return AvailableRoles.ReadOnly;
            }
        }

        public enum AvailableRoles { PowerUser = 1, ReadOnly = 2, Admin = 3}
    }
}
