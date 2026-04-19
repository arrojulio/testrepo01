using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Bladex.Garantias.Application.Facades;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.DomainModel.DomainBase.Components.Security;
using Bladex.Garantias.Infrastructure.Logging;
using Bladex.Garantias.Presentation.Website.ViewModels;
using System.Web.Routing;

namespace Bladex.Garantias.Presentation.Website.Controllers
{
    /// <summary>
    /// The base controller class.
    /// </summary>
    [Components.Attributes.Authorization()]
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// Gets the logger.
        /// </summary>
        protected virtual ILogger Logger
        {
            get { return ApplicationLogger.For(this.GetType()); }
        }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>
        /// The user id.
        /// </value>
        protected virtual string UserId
        {
            get { return User.Identity.Name; }
            set { } 
        }

        /// <summary>
        /// Gets a value indicating whether the user logged is a read only user.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if the user is read only user; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsReadOnlyUser
        {
            get
            {
                var user = this.GetUser();
                if (user == null) return true;
                return !user.IsPowerUser && !user.IsAdmin;
            }
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <returns></returns>
        public User GetUser()
        {
            if (Session["User"] == null)
            {
                var user = ServiceFacade.Instance.UserService.GetById(User.Identity.Name);
                // If no user is found, create a new one with guest privileges.
                if (user == null)
                    user = new User() { UserId = string.Empty, UserName = "(not logged)", RoleId = -1, Role = new Role() { RoleId = (int)Role.AvailableRoles.ReadOnly, RoleName = "Guest" } };
                Session["User"] = user;
            }
            return Session["User"] as User;
        }

        /// <summary>
        /// Gets or sets the session id.
        /// </summary>
        /// <value>
        /// The session id.
        /// </value>
        protected virtual string SessionId
        {
            get;
            set;
        }

        /// <summary>
        /// Sets the error model.
        /// </summary>
        /// <param name="ex">The ex of type <see cref="System.Exception"/></param>
        protected void SetErrorModel(Exception ex)
        {
            if (this.ViewData == null || this.ViewData.Model == null) return;

            if (this.ViewData.Model is GarantiaDepositoOtroBancoViewModel)
            {
                (this.ViewData.Model as GarantiaDepositoOtroBancoViewModel).Garantia.BusinessError = ex.Message;
            }
            else if (this.ViewData.Model is GarantiaDepositoViewModel)
            {
                (this.ViewData.Model as GarantiaDepositoViewModel).Garantia.BusinessError = ex.Message;
            }
            else if (this.ViewData.Model is GarantiaInmuebleViewModel)
            {
                (this.ViewData.Model as GarantiaInmuebleViewModel).Garantia.BusinessError = ex.Message;
            }
            else if (this.ViewData.Model is GarantiaMuebleViewModel)
            {
                (this.ViewData.Model as GarantiaMuebleViewModel).Garantia.BusinessError = ex.Message;
            }
            else if (this.ViewData.Model is GarantiaOtraViewModel)
            {
                (this.ViewData.Model as GarantiaOtraViewModel).Garantia.BusinessError = ex.Message;
            }
            else if (this.ViewData.Model is GarantiaPrendaViewModel)
            {
                (this.ViewData.Model as GarantiaPrendaViewModel).Garantia.BusinessError = ex.Message;
            }
            ModelState.AddModelError("Garantia.BusinessError", ex);
            this.Logger.Error(ex);
        }

        /// <summary>
        /// Called before the action method is invoked.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool logoff = false;
            var currentUser = User.Identity.Name;
            log4net.MDC.Set("UserId", currentUser);
            var proposedUser = Request.QueryString["UserId"] != null ? Request.QueryString["UserId"] : string.Empty;

            if (!string.IsNullOrEmpty(proposedUser) && proposedUser != currentUser)
            {
                // invoke account signoff action
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Account" }, { "action", "SignOff" }, { "UserId", proposedUser } });
                return;
            }
            this.GetUser();
            if (filterContext.RequestContext.HttpContext.Request.Cookies["SessionId"] != null)
            {
                this.SessionId = filterContext.RequestContext.HttpContext.Request.Cookies["SessionId"].Value;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}