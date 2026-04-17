using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Configuration;
using System.Web.Security;
using Bladex.Garantias.Application.Facades;
using Bladex.Garantias.DomainModel.DomainBase;

namespace Bladex.Garantias.Presentation.Website.Components.Authentication
{
    public class CustomMembershipProvider : MembershipProvider
    {
        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            if (name == null || name.Length == 0)
                name = "CustomMembershipProvider";

            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Custom Membership provider");
            }

            // Initialize the abstract base class.
            base.Initialize(name, config);

            pApplicationName = GetConfigValue(config["applicationName"], System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
        }

        private string GetConfigValue(string configValue, string defaultValue)
        {
            if (String.IsNullOrEmpty(configValue))
                return defaultValue;

            return configValue;
        }

        private string pApplicationName;

        public override string ApplicationName
        {
            get { return pApplicationName; }
            set { pApplicationName = value; }
        }

        public override bool EnablePasswordReset
        {
            get { return false; }
        }


        public override bool EnablePasswordRetrieval
        {
            get { return false; }
        }


        public override bool RequiresQuestionAndAnswer
        {
            get { return false; }
        }


        public override bool RequiresUniqueEmail
        {
            get { return false; }
        }


        public override int MaxInvalidPasswordAttempts
        {
            get { return 0; }
        }


        public override int PasswordAttemptWindow
        {
            get { return 0; }
        }


        public override MembershipPasswordFormat PasswordFormat
        {
            get { return MembershipPasswordFormat.Clear; }
        }


        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return 0; }
        }

        public override int MinRequiredPasswordLength
        {
            get { return 0; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { return string.Empty; }
        }

        public override bool ChangePassword(string username, string oldPwd, string newPwd)
        {
            throw new NotImplementedException("The feature is not implemented.");
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPwdQuestion, string newPwdAnswer)
        {
            throw new NotImplementedException("The feature is not implemented.");
        }

        public override MembershipUser CreateUser(string username,string password,string email,string passwordQuestion,string passwordAnswer,bool isApproved,object providerUserKey,out MembershipCreateStatus status)
        {
            throw new NotImplementedException("The feature is not implemented.");
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException("The feature is not implemented.");
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            MembershipUserCollection usersCollection = new MembershipUserCollection();
            var users = ServiceFacade.Instance.UserService.GetAll();
            totalRecords = users.Count;

            foreach(var user in users)
            {
                MembershipUser u = GetMembershipUser(user);
                usersCollection.Add(u);
            }
            return usersCollection;
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException("The feature is not implemented.");
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException("The feature is not implemented.");
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            return this.GetMembershipUser(ServiceFacade.Instance.UserService.GetById(username));
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException("The feature is not implemented.");
        }

        private MembershipUser GetMembershipUser(User user)
        {
            MembershipUser u = new MembershipUser(this.Name,
                                                  user.UserId,
                                                  user.UserId,
                                                  string.Empty,
                                                  string.Empty,
                                                  user.UserName,
                                                  true,
                                                  false,
                                                  DateTime.Now,
                                                  DateTime.Now,
                                                  DateTime.Now,
                                                  DateTime.Now,
                                                  DateTime.Now);

            return u;
        }

        public override bool UnlockUser(string username)
        {
            throw new NotImplementedException("The feature is not implemented.");
        }

        public override string GetUserNameByEmail(string email)
        {
            return string.Empty;
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException("The feature is not implemented.");
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException("The feature is not implemented.");
        }

        public override bool ValidateUser(string username, string password)
        {
            return ServiceFacade.Instance.UserService.GetById(username) != null;
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            MembershipUserCollection collection = new MembershipUserCollection();
            var user = ServiceFacade.Instance.UserService.GetById(usernameToMatch);
            if (user != null) collection.Add(GetMembershipUser(user));
            totalRecords = collection.Count;
            return collection;
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException("The feature is not implemented.");
        }
    }
}
