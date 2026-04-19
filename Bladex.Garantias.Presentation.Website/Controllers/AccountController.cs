using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Bladex.Garantias.Application.Facades;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.DomainModel.DomainBase.Components.Security;
using Bladex.Garantias.DomainModel.Services;
using Bladex.Garantias.Infrastructure.Logging;
using Bladex.Garantias.Presentation.Website.Components.Attributes;
using Bladex.Garantias.Presentation.Website.Components.Authentication;

namespace Bladex.Garantias.Presentation.Website.Controllers
{
    [HandleError]
    public class AccountController : Controller, IFormsAuthentication
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
        }

        public ActionResult Login(string UserId = null)
        {
            string user = UserId;
            if (string.IsNullOrEmpty(UserId))
            {
                var cookie = Request.Cookies.Get("UserId");
                if (cookie != null) user = cookie.Value;
                else throw new System.Security.SecurityException("The UserId is not specified. Please verify the current request and check your cookies settings.");
            }
            this.SignIn(user, false);
            return RedirectToAction("Index", "Garantia");
        }

        public ActionResult SignOff()
        {
            this.SignOut();
            return RedirectToAction("Index", "Garantia", new { UserId = Request.QueryString["UserId"] });
        }

        public void SignIn(string userName, bool createPersistentCookie)
        {
            var user = ServiceFacade.Instance.UserService.GetById(userName);
            if (user == null || user.Role == null)
            {
                InvalidUserException ex = new InvalidUserException(string.Format("The User with id {0} not found into the system.", userName));
                ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Warning, "Acceso al sistema sin envio de id de usuario o el usuario no existe.", new Dictionary<string, object>() { { "UserId", userName } }, ex);
                throw ex;
            }
            List<string> rolesList = new List<string> { user.Role.RoleName };
            var mcUser = ServiceFacade.Instance.MakerCheckerService.GetUser(userName);
            if (mcUser != null && mcUser.Role != null)
                rolesList.Add(mcUser.Role.RoleName);
            // Add the name of the user to the view bag.
            this.ViewBag.User = user;
            // Add the cookie to store the user name.
            Response.AppendCookie(new HttpCookie("UserId", user.UserId) { Expires = DateTime.Now.AddHours(2) });
            
            var rolesString = string.Join(",", rolesList);

            var authTicket = new FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.AddHours(2), createPersistentCookie, rolesString, "/");

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));
            if (authTicket.IsPersistent)
            {
                cookie.Expires = authTicket.Expiration;
            }
            if (User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RenewTicketIfOld(authTicket);
            }
            else
            {
                Response.AppendCookie(cookie);
            }
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}