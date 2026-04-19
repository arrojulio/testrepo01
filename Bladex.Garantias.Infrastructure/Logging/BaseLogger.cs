using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;

namespace Bladex.Garantias.Infrastructure.Logging
{
    public abstract class BaseLogger
    {
        private const string _CONFIGURATION_APPLICATION_VERSION = "ApplicationVersion";

        /// <summary>
        /// Prepares the message.
        /// </summary>
        /// <param name="message">The message of type <see cref="System.String"/></param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        protected virtual string PrepareMessage(string message, IDictionary<string, object> parameters)
        {
            message = string.Concat(GetApplicationVersion(), " ", this.GetCallingMethod(), " ", message);
            message = AppendMessageFromParameters(parameters, message);
            return message;
        }

        /// <summary>
        /// Gets the application version.
        /// </summary>
        /// <returns></returns>
        protected static string GetApplicationVersion()
        {
            string version = ConfigurationManager.AppSettings[_CONFIGURATION_APPLICATION_VERSION];
            return string.IsNullOrEmpty(version) ? string.Empty : string.Concat("[", version, "]");
        }

        /// <summary>
        /// Appends the message from parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="originalMessage">The original message of type <see cref="System.String"/></param>
        /// <returns></returns>
        protected static string AppendMessageFromParameters(IDictionary<string, object> parameters, string originalMessage)
        {
            if (parameters != null && parameters.Count > 0)
            {
                StringBuilder strBuilder = new StringBuilder();
                parameters.ToList().ForEach(o => strBuilder.AppendFormat("[{0}: {1}] ", o.Key, o.Value));
                originalMessage = string.Concat(originalMessage, " Parameters: ", strBuilder.ToString());
            }
            return originalMessage;
        }

        /// <summary>
        /// Gets the calling method.
        /// </summary>
        /// <returns></returns>
        protected virtual string GetCallingMethod()
        {
            MethodBase callingMethodObj = new StackFrame(this.GetStackTraceLevel(), false).GetMethod();
            string callingMethod = string.Format("{0}.{1}", callingMethodObj.DeclaringType, callingMethodObj.Name);
            return callingMethod;
        }

        protected virtual int GetStackTraceLevel()
        {
            return 3;
        }
    }
}
