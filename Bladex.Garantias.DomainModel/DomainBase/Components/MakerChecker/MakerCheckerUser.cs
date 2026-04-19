using Bladex.Garantias.Infrastructure.DomainBase;

namespace Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker
{
    /// <summary>
    /// The maker checker user class.
    /// </summary>
    public class MakerCheckerUser : EntityBase
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
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email of type <see cref="System.String"/>
        /// </value>
        public string Email
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The role of type <see cref="MakerCheckerRole"/>
        /// </value>
        public MakerCheckerRole Role
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is checker.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is checker; otherwise, <c>false</c>.
        /// </value>
        public bool IsChecker
        {
            get
            {
                return (this.Role != null && this.Role.RoleId == (int)MakerCheckerRole.MakerCheckerAvailableRoles.Checker);
            }
        }

        public bool IsMaker
        {
            get
            {
                return (this.Role != null && this.Role.RoleId == (int)MakerCheckerRole.MakerCheckerAvailableRoles.Maker);
            }
        }


        public bool IsSuperUser
        {
            get
            {
                return (this.Role != null && this.Role.RoleId == (int)MakerCheckerRole.MakerCheckerAvailableRoles.SuperUser);
            }
        }

    }
}
