using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bladex.Garantias.Infrastructure.Logging
{
    /// <summary>
    /// ILogger interface.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Infoes the specified message.
        /// </summary>
        /// <param name="message">The message of type <see cref="System.String"/></param>
        void Info(string message);

        /// <summary>
        /// Warns the specified message.
        /// </summary>
        /// <param name="message">The message of type <see cref="System.String"/></param>
        void Warn(string message);
        /// <summary>
        /// Warns the specified message.
        /// </summary>
        /// <param name="message">The message of type <see cref="System.String"/></param>
        /// <param name="x">The x of type <see cref="System.Exception"/></param>
        void Warn(string message, Exception x);

        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message of type <see cref="System.String"/></param>
        void Debug(string message);

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message of type <see cref="System.String"/></param>
        void Error(string message);
        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message of type <see cref="System.String"/></param>
        /// <param name="x">The x of type <see cref="System.Exception"/></param>
        void Error(string message, Exception x);
        /// <summary>
        /// Errors the specified x.
        /// </summary>
        /// <param name="x">The x of type <see cref="System.Exception"/></param>
        void Error(Exception x);

        /// <summary>
        /// Fatals the specified message.
        /// </summary>
        /// <param name="message">The message of type <see cref="System.String"/></param>
        void Fatal(string message);
        /// <summary>
        /// Fatals the specified message.
        /// </summary>
        /// <param name="message">The message of type <see cref="System.String"/></param>
        /// <param name="x">The x of type <see cref="System.Exception"/></param>
        void Fatal(string message, Exception x);
        /// <summary>
        /// Fatals the specified x.
        /// </summary>
        /// <param name="x">The x of type <see cref="System.Exception"/></param>
        void Fatal(Exception x);

        /// <summary>
        /// Logs the specified log type.
        /// </summary>
        /// <param name="logType">Type of the log.</param>
        /// <param name="message">The message of type <see cref="System.String"/></param>
        /// <param name="parameters">The parameters of type <see cref="object"/></param>
        /// <param name="ex">The ex of type <see cref="System.Exception"/></param>
        void Log(ApplicationLogger.LogType logType, string message, IDictionary<string, object> parameters = null, Exception ex = null);
    }
}
