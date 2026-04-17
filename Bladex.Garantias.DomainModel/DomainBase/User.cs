using Bladex.Garantias.Infrastructure.DomainBase;

namespace Bladex.Garantias.DomainModel.DomainBase
{
    /// <summary>
    /// The user class.
    /// </summary>
    public class User : EntityBase
    {

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>
        /// The user id of type <see cref="System.String"/>
        /// </value>
        public string UserId
        {
            get { return this.GetKeyAs<string>(); }
            set { this.Key = value; }
        }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The role of type <see cref="Role"/>
        /// </value>
        public Role Role
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the role id.
        /// </summary>
        /// <value>
        /// The role id of type <see cref="System.Int32"/>
        /// </value>
        public int RoleId
        { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets a value indicating whether this user has power-user privileges
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is power-user; otherwise, <c>false</c>.
        /// </value>
        public bool IsPowerUser
        {
            get 
            {
                return (this.Role != null && this.Role.RoleEnum == Role.AvailableRoles.PowerUser);
            }
        }
        /// <summary>
        /// Returns a value indicating whether this user has admin privileges
        /// </summary>
        public bool IsAdmin
        {
            get
            {
                return (this.Role != null && this.Role.RoleEnum == Role.AvailableRoles.Admin);
            }
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(this.UserName)) return this.UserId;
            return this.UserName;
        }
    }
}
