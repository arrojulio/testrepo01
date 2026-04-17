using System.Security.Principal;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Bladex.Garantias.Presentation.Website.Components.Attributes
{
    /// <summary>
    /// The authorization attribute class.
    /// </summary>
    public class AuthorizationAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        public AuthorizationAttribute()
            : base()
        {
            _authorize = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationAttribute"/> class.
        /// </summary>
        /// <param name="shouldAuthorize">if set to <c>true</c> [should authorize].</param>
        public AuthorizationAttribute(bool shouldAuthorize)
        {
            _authorize = false;
        }

        public AuthorizationAttribute(string role)
            : base()
        {
            base.Roles = role;
            _authorize = true;
        }

        public AuthorizationAttribute(string[] roles)
            : base()
        {
            base.Roles = string.Join(",", roles);
            _authorize = true;
        }

        public AuthorizationAttribute(bool isAuthorized, string[] roles)
            : base()
        {
            base.Roles = string.Join(",", roles);
            _authorize = isAuthorized;
        }

        private readonly bool _authorize;


        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!_authorize) return true;
            if (httpContext.User.Identity.IsAuthenticated)
            {
                var authCookie = httpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie != null)
                {
                    var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                    var roles = ticket.UserData.Split(',');
                    var identity = new GenericIdentity(ticket.Name);
                    httpContext.User = new GenericPrincipal(identity, roles);
                }
            }
            return base.AuthorizeCore(httpContext);
        }
    }
}