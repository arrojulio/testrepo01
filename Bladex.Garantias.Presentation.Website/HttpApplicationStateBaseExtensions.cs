using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bladex.Garantias.Infrastructure.Logging;

namespace Bladex.Garantias.Presentation.Website
{
    /// <summary>
    /// The HTTP application state base extensions class.
    /// </summary>
    public static class HttpApplicationStateBaseExtensions
    {
        /// <summary>
        /// Loggers the specified application state.
        /// </summary>
        /// <param name="applicationState">State of the application.</param>
        /// <returns></returns>
        public static ILogger Logger(this System.Web.HttpApplicationStateBase applicationState)
        {
            if (applicationState["ILogger"] == null)
            {
                applicationState["ILogger"] = Bladex.Garantias.Infrastructure.Logging.ApplicationLogger.Instance;
            }
            return applicationState["ILogger"] as ILogger;
        }

        /// <summary>
        /// Loggers the specified application state.
        /// </summary>
        /// <param name="applicationState">State of the application.</param>
        /// <returns></returns>
        public static ILogger Logger(this System.Web.HttpApplicationState applicationState)
        {
            if (applicationState["ILogger"] == null)
            {
                applicationState["ILogger"] = Bladex.Garantias.Infrastructure.Logging.ApplicationLogger.Instance;
            }
            return applicationState["ILogger"] as ILogger;
        }
    }
}