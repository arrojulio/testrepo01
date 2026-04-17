namespace Bladex.Garantias.ImportSync.Client
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Console Class used to run the <see cref="ImportSyncManager"/>
    /// </summary>
    public class Program
    {
        static int Main(string[] args)
        {
            try
            {
                Console.Title = string.Format("TEINSA - Garantias | Synchronization Tool v{0}. Novaris LLC", typeof(Bladex.Garantias.ImportSync.ImportSync).Assembly.GetName().Version.ToString(4));
            }
            catch (Exception ex)
            {
                Console.Title = string.Format("TEINSA - Garantias | Synchronization Tool. Novaris LLC");
            }

            // App Setup
            try
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-us");
                System.Threading.Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-us");
                Bladex.Garantias.Infrastructure.Bootstrapper.Setup();
                Bladex.Garantias.Infrastructure.Repositories.Bootstrapper.Setup();
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine(ex.GetType().Name + ": " + ex.Message + "\n" + ex.StackTrace);
                Console.ReadLine();
#endif
                return (int)ExitCode.SetupError;
            }

            try
            {
                // Sync Manager execution
                ImportSyncManager manager = new ImportSyncManager();
                manager.Run();
#if DEBUG
                Console.ReadLine();
#endif
                return (int)ExitCode.Success;
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine(ex.GetType().Name + ": " + ex.Message + "\n" + ex.StackTrace);
                Console.ReadLine();
#endif
                return (int)ExitCode.Error;
            }
        }

    }

    /// <summary>
    /// Wrapper for Error Codes
    /// </summary>
    enum ExitCode : int
    {
        Success = 0,
        Error = 1,
        SetupError = 2
    }
}
