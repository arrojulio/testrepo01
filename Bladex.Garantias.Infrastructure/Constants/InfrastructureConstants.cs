using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Bladex.Garantias.Infrastructure.Constants
{
    public static class InfrastructureConstants
    {
        public const string _EMAIL_SENDER_ADDRESS = "novaris@bladex.com";
        public static string GetEmailHost()
        {
            if (ConfigurationManager.AppSettings["Intelledata.Common.Email.Host"] != null)
                return ConfigurationManager.AppSettings["Intelledata.Common.Email.Host"];
            return string.Empty;
        }

        public static bool GetEmailUseSsl()
        {
            bool useSsl = false;
            if (ConfigurationManager.AppSettings["Intelledata.Common.Email.UseSsl"] != null && bool.TryParse(ConfigurationManager.AppSettings["Intelledata.Common.Email.UseSsl"], out useSsl))
                return useSsl;
            return useSsl;
        }

        public static int GetEmailPort()
        {
            int port = -1;
            if (ConfigurationManager.AppSettings["Intelledata.Common.Email.Port"] != null && int.TryParse(ConfigurationManager.AppSettings["Intelledata.Common.Email.Port"], out port))
                return port;
            return -1;
        }

        public static string GetEmailUsername()
        {
            if (ConfigurationManager.AppSettings["Intelledata.Common.Email.Username"] != null)
                return ConfigurationManager.AppSettings["Intelledata.Common.Email.Username"];
            return string.Empty;
        }
        public static string GetEmailPassword()
        {
            if (ConfigurationManager.AppSettings["Intelledata.Common.Email.Password"] != null)
                return ConfigurationManager.AppSettings["Intelledata.Common.Email.Password"];
            return string.Empty;
        }
        public static string GetEmailDomain()
        {
            if (ConfigurationManager.AppSettings["Intelledata.Common.Email.Domain"] != null)
                return ConfigurationManager.AppSettings["Intelledata.Common.Email.Domain"];
            return string.Empty;
        }

    }
}
