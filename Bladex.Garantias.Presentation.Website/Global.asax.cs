using System;
using System.Security;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using Bladex.Garantias.Presentation.Website.Components.Authentication;
using Bladex.Garantias.Presentation.Website.ViewModels;

namespace Bladex.Garantias.Presentation.Website
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Handles the AuthenticateRequest event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            if (User != null)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    this.Session_Start();
                }
            }
        }

        /// <summary>
        /// Session_s the end.
        /// </summary>
        protected void Session_End()
        {
            
        }

        /// <summary>
        /// Handles the security.
        /// </summary>
        private void handleSecurity()
        {
            // Si accedo con usuario, genero un cookie.
            if (this.Request.QueryString["UserId"] != null)
            {
                string userId = this.Request.QueryString["UserId"].ToString();
                this.Response.Cookies.Add(new HttpCookie("UserId", userId) { Expires = DateTime.Now.AddHours(2) });
                this.Application.Logger().Info(string.Format("User {0} logged into the system using querystring.", userId));
            }
            else
            {
                // Si no accedo con usuario, consulto cookie generada previamente.
                if (Request.Cookies != null && Request.Cookies["UserId"] != null && !string.IsNullOrEmpty(Request.Cookies["UserId"].Value))
                {
                    // Si existe, tomo el usuario y actualizo la expiracion de cookie.
                    string userId = Request.Cookies["UserId"].Value;
                    this.Response.Cookies.Add(new HttpCookie("UserId", userId) { Expires = DateTime.Now.AddHours(2) });
                    this.Application.Logger().Info(string.Format("User {0} logged into the system using cookie.", userId));
                }
                else
                {
                    // Si no existe, auth required.
                    // GarantiaBaseController se encarga de redireccionar a error page.

                }
            }
            if (Request.QueryString["SessionId"] != null)
            {
                string sessionId = Request.QueryString["SessionId"];
                this.Response.Cookies.Add(new HttpCookie("SessionId", sessionId) { Expires = DateTime.Now.AddHours(2) });
            }
            else
            {
                // Si no accedo con sessionId, consulto cookie generada previamente.
                if (Request.Cookies != null && Request.Cookies["SessionId"] != null && !string.IsNullOrEmpty(Request.Cookies["SessionId"].Value))
                {
                    // Si existe, tomo el sessionId y actualizo la expiracion de cookie.
                    string sessionId = Request.Cookies["SessionId"].Value;
                    this.Response.Cookies.Add(new HttpCookie("SessionId", sessionId) { Expires = DateTime.Now.AddHours(2) });
                }
                else
                {
                    // Si no existe, auth required.
                    // GarantiaBaseController se encarga de redireccionar a error page.

                }
            }
        }

        /// <summary>
        /// Session_s the start.
        /// </summary>
        protected void Session_Start()
        {
            this.handleSecurity();
        }

        /// <summary>
        /// Registers the routes.
        /// </summary>
        /// <param name="routes">The routes of type <see cref="System.Web.Routing.RouteCollection"/></param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            //var isisHandler = new IsisRoutingHandler();
            //routes.Add(new Route("isis", isisHandler));

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.js");
            routes.IgnoreRoute("{resource}.jpg");
            routes.IgnoreRoute("{resource}.png");
            routes.IgnoreRoute("{resource}.gif");
            routes.IgnoreRoute("{resource}.ico");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Garantia", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                "ToopTips",
                "{controller}/{action}/{id}",
                new { controller = "GarantiaBase", action = "GetTooltip", id = UrlParameter.Optional }
            );

            routes.MapRoute("UnhandledExceptions", "{controller}/{action}", new { controller = "GarantiaBase", action = "Error" });
            

        }

        /// <summary>
        /// Application_s the start.
        /// </summary>
        protected void Application_Start()
        {
            // ✅ INICIALIZAR LOG4NET PRIMERO
            log4net.Config.XmlConfigurator.Configure();
            
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            HtmlHelper.ClientValidationEnabled = true;
            HtmlHelper.UnobtrusiveJavaScriptEnabled = true;

            this.Application.Logger().Info(string.Format("Application starting at {0}", DateTime.Now));
            try
            {
                ModelMetadataProviders.Current = new CustomModelMetadataProvider();
            }
            catch (Exception ex)
            {
                this.Application.Logger().Error("Error wiring up the CustomModelMetadata providers.", ex);
                throw;
            }
            try
            {
                Bladex.Garantias.Infrastructure.Bootstrapper.Setup();
                Bladex.Garantias.Infrastructure.Repositories.Bootstrapper.Setup();
            }
            catch (Exception ex)
            {
                this.Application.Logger().Error("Error running Bootstrapper.", ex);
                throw;
            }
            try
            {
                Bootstrapper.Setup();
                this.Application.Logger().Info(string.Format("Application started at {0}", DateTime.Now));
                Application["StartTime"] = DateTime.Now;
            }
            catch (Exception ex)
            {
                this.Application.Logger().Error("Error running WebBootstrapper.", ex);
                throw;
            }
        }

        /// <summary>
        /// Registers the global filters.
        /// </summary>
        /// <param name="filters">The filters of type <see cref="System.Web.Mvc.GlobalFilterCollection"/></param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute
            {
                ExceptionType = typeof(Exception),
                // DbError.cshtml is a view in the Shared folder.
                View = "Error",
                Order = 2
            });
            filters.Add(new HandleErrorAttribute
            {
                ExceptionType = typeof(InvalidRoleException),
                // DbError.cshtml is a view in the Shared folder.
                View = "InvalidRole",
                Order = 3
            });
            filters.Add(new HandleErrorAttribute
            {
                ExceptionType = typeof(InvalidUserException),
                // DbError.cshtml is a view in the Shared folder.
                View = "InvalidUser",
                Order = 4
            });
            filters.Add(new HandleErrorAttribute
            {
                ExceptionType = typeof(SecurityException),
                // DbError.cshtml is a view in the Shared folder.
                View = "InvalidUser",
                Order = 5
            });
            filters.Add(new HandleErrorAttribute());
        }
    }


    public class AvalResolver : AutoMapper.ValueResolver<AvalViewModel, DomainModel.DomainBase.Aval>
    {

        protected override DomainModel.DomainBase.Aval ResolveCore(AvalViewModel source)
        {
            IAvalRepository repository = RepositoryFactory.GetRepository<IAvalRepository, DomainModel.DomainBase.Aval>();
            return repository.FindBy(source.Key);
        }
    }

    
}