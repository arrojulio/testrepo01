using System.Collections.Specialized;
using System;
using System.Linq;
using Bladex.Garantias.Application.Facades;

namespace Bladex.Garantias.Presentation.Website.Components.Authentication
{
    /// <summary>
    /// The custom role provider class.
    /// </summary>
    public class CustomRoleProvider : System.Web.Security.RoleProvider
    {

        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null) throw new ArgumentNullException("config");

            if (name == null || name.Length == 0)
                name = "CustomRoleProvider";

            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Custom Role provider");
            }

            // Initialize the abstract base class.
            base.Initialize(name, config);


            if (config["applicationName"] == null || config["applicationName"].Trim() == "")
            {
                pApplicationName = System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath;
            }
            else
            {
                pApplicationName = config["applicationName"];
            }

        }

        private string pApplicationName;


        public override string ApplicationName
        {
            get { return pApplicationName; }
            set { pApplicationName = value; }
        }

        public override void AddUsersToRoles(string[] usernames, string[] rolenames)
        {
            throw new NotImplementedException("The feature is not implemented.");
        }

        public override void CreateRole(string rolename)
        {
            throw new NotImplementedException("The feature is not implemented.");
        }

        public override bool DeleteRole(string rolename, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException("The feature is not implemented.");
        }

        public override string[] GetAllRoles()
        {
            return ServiceFacade.Instance.RoleService.GetAll().Select(o => o.RoleName).ToArray();
        }

        public override string[] GetRolesForUser(string username)
        {
            var user = ServiceFacade.Instance.UserService.GetById(username);
            if(user != null && user.Role != null)
                return new string[] { user.Role.RoleName };
            return new string[0];
        }

        public override string[] GetUsersInRole(string rolename)
        {
            var users = ServiceFacade.Instance.UserService.GetAll();
            var matchedUsers = users.Where(o => o.Role != null && o.Role.RoleName == rolename);
            if(matchedUsers != null) return matchedUsers.Select(o => o.UserId).ToArray();
            else return new string[0];
        }

        public override bool IsUserInRole(string username, string rolename)
        {
            var user = ServiceFacade.Instance.UserService.GetById(username);
            return (user != null && user.Role != null && user.Role.RoleName == rolename);
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] rolenames)
        {
            throw new NotImplementedException("The feature is not implemented.");
        }

        public override bool RoleExists(string rolename)
        {
            var roles = ServiceFacade.Instance.RoleService.GetAll();
            return (roles != null && roles.Count > 0 && roles.FirstOrDefault(o => o.RoleName == rolename) != null);
        }

        public override string[] FindUsersInRole(string rolename, string usernameToMatch)
        {
            if (this.IsUserInRole(usernameToMatch, rolename)) return new string[] { usernameToMatch };
            else return new string[0];
        }

    }
}
