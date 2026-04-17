using System;
using System.Web;
using System.Web.Mvc;
using Elmah;

namespace Bladex.Garantias.Presentation.Website.Components.Attributes
{
    /// <summary>
    /// The handle error with elmah attribute class.
    /// </summary>
    public class HandleErrorWithElmahAttribute : System.Web.Mvc.HandleErrorAttribute
    {
        /// <summary>
        /// Called when [exception].
        /// </summary>
        /// <param name="context">The context of type <see cref="System.Web.Mvc.ExceptionContext"/></param>
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);

            var e = context.Exception;
            //if (!context.ExceptionHandled   // if unhandled, will be logged anyhow
            //    || RaiseErrorSignal(e)      // prefer signaling, if possible
            //    || IsFiltered(context))     // filtered?
            //    return;
            
            // Handle error even if is handled previously.
            LogException(e);


            // Handle error even if is handled previously.
            //if (RaiseErrorSignal(e)      // prefer signaling, if possible
            //    || IsFiltered(context))     // filtered?
            //    return;

            //LogException(e);
        }

        /// <summary>
        /// Raises the error signal.
        /// </summary>
        /// <param name="e">The e of type <see cref="System.Exception"/></param>
        /// <returns></returns>
        private static bool RaiseErrorSignal(Exception e)
        {
            var context = HttpContext.Current;
            if (context == null)
                return false;
            var signal = ErrorSignal.FromContext(context);
            if (signal == null)
                return false;
            signal.Raise(e, context);
            return true;
        }

        /// <summary>
        /// Determines whether the specified context is filtered.
        /// </summary>
        /// <param name="context">The context of type <see cref="System.Web.Mvc.ExceptionContext"/></param>
        /// <returns>
        ///   <c>true</c> if the specified context is filtered; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsFiltered(ExceptionContext context)
        {
            var config = context.HttpContext.GetSection("elmah/errorFilter")
                         as ErrorFilterConfiguration;

            if (config == null)
                return false;

            var testContext = new ErrorFilterModule.AssertionHelperContext(
                                      context.Exception, HttpContext.Current);

            return config.Assertion.Test(testContext);
        }

        /// <summary>
        /// Logs the exception.
        /// </summary>
        /// <param name="e">The e of type <see cref="System.Exception"/></param>
        private static void LogException(Exception e)
        {
            var context = HttpContext.Current;
            ErrorLog.GetDefault(context).Log(new Error(e, context));
        }
    }
}