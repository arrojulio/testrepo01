using System;
using System.Collections.Generic;
using log4net;

namespace Bladex.Garantias.Infrastructure.Logging
{
    /// <summary>
    /// The application logger class.
    /// </summary>
    public sealed class ApplicationLogger : BaseLogger, ILogger
    {
        #region Singleton Pattern
        
        private static volatile ApplicationLogger _instance;
        private static readonly object syncRoot = new object();

        /// <summary>
        /// Variable de acceso a Singleton
        /// </summary>
        public static ApplicationLogger Instance
        {
            get
            {
                // Use 'Lazy initialization'
                if (_instance == null)
                {
                    lock (syncRoot)
                    {
                        if (null == _instance)
                            _instance = new ApplicationLogger();
                    }

                }
                return _instance;
            }
        }

        #endregion

        #region Class Implementation

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationLogger"/> class.
        /// </summary>
        public ApplicationLogger()
        {
            this._logger = log4net.LogManager.GetLogger(this.GetType());
        }

        /// <summary>
        ///   <see cref="log4net.ILog"/>
        /// </summary>
        private readonly ILog _logger;

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="logType">Type of the logging even.</param>
        /// <param name="message">The message of type <see cref="System.String"/></param>
        /// <param name="parameters">The parameters to include into the message. It can be null.</param>
        /// <param name="ex">The ex of type <see cref="System.Exception"/>. It can be null.</param>
        public void Log(LogType logType, string message, IDictionary<string, object> parameters = null, Exception ex = null)
        {
            message = PrepareMessage(message, parameters);
            switch (logType)
            {
                case LogType.Debug:
                    this._logger.Debug(message, ex ?? null);
                    break;
                case LogType.Information:
                    this._logger.Info(message, ex ?? null);
                    break;
                case LogType.Warning:
                    this._logger.Warn(message, ex ?? null);
                    break;
                case LogType.Error:
                    this._logger.Error(message, ex ?? null);
                    break;
                case LogType.Fatal:
                    this._logger.Fatal(message, ex ?? null);
                    break;
                default:
                    this._logger.Info(message, ex ?? null);
                    break;
            }
        }

        /// <summary>
        /// Enum to specify the type of message.
        /// </summary>
        public enum LogType
        { 
            Debug, Information, Warning, Error, Fatal
        }

        #endregion


        #region ILogger Members

        public void Info(string message)
        {
            this.Log(LogType.Information, message);
        }

        public void Warn(string message)
        {
            this.Log(LogType.Warning, message);
            
        }

        public void Warn(string message, Exception x)
        {
            this.Log(LogType.Warning, message, null, x);
        }

        public void Debug(string message)
        {
            this.Log(LogType.Debug, message);
        }

        public void Error(string message)
        {
            this.Log(LogType.Error, message);
        }

        public void Error(string message, Exception x)
        {
            this.Log(LogType.Error, message, null, x);
        }

        public void Error(Exception x)
        {
            this.Log(LogType.Error, LogUtility.BuildExceptionMessage(x), null, x);
        }

        public void Fatal(string message)
        {
            this.Log(LogType.Fatal, message);
        }

        public void Fatal(string message, Exception x)
        {
            this.Log(LogType.Fatal, message,null, x);
        }

        public void Fatal(Exception x)
        {
            this.Log(LogType.Fatal, LogUtility.BuildExceptionMessage(x), null, x);
        }

        #endregion
    }
}
