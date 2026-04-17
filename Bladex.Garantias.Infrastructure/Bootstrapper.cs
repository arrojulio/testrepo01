namespace Bladex.Garantias.Infrastructure
{
    public static class Bootstrapper
    {
        public static void Setup()
        {
            SetupLoggingFramework();
        }

        private static void SetupLoggingFramework()
        {
            //log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo("log4net.config"));
            //log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo("./log4net.config"));
            log4net.Config.XmlConfigurator.Configure();
        }


    }


}
