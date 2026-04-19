using Bladex.Garantias.Infrastructure.DomainBase;

namespace Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker
{
    /// <summary>
    /// The maker checker role class.
    /// </summary>
    public class MakerCheckerRole : EntityBase
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

        public MakerCheckerAvailableRoles Role
        {
            get 
            {
                if (this.GetKeyAs<int>() == (int)MakerCheckerAvailableRoles.Checker) return MakerCheckerAvailableRoles.Checker;
                if (this.GetKeyAs<int>() == (int)MakerCheckerAvailableRoles.Maker) return MakerCheckerAvailableRoles.Maker;
                if (this.GetKeyAs<int>() == (int)MakerCheckerAvailableRoles.SuperUser) return MakerCheckerAvailableRoles.SuperUser;
                return MakerCheckerAvailableRoles.Maker;
            }
        }

        public enum MakerCheckerAvailableRoles { Maker = 1, Checker = 2, SuperUser = 3}
    }
}
