namespace Bladex.Garantias.ImportSync.Client
{
    using System;
    using Infrastructure.Logging;

    /// <summary>
    /// Clase encargada de controlar el sincronizador de TEINSA.
    /// </summary>
    public class ImportSyncManager
    {
        /// <summary>
        /// Import Sync
        /// </summary>
        private ImportSync _importSync;

        /// <summary>
        /// Logger 
        /// </summary>
        private ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportSyncManager"/> class.
        /// </summary>
        public ImportSyncManager()
        {
            var garantiaManager = new GarantiaManager();
            var garantiaContratoManager = new GarantiaContratoManager();

            this._logger = Bladex.Garantias.Infrastructure.Logging.ApplicationLogger.Instance;
            this._importSync = new ImportSync(garantiaManager, garantiaContratoManager, this._logger);

            this._importSync.OnDoDeletesCompleted += this._importSync_OnDoDeletesCompleted;
            this._importSync.OnDoInsertsCompleted += this._importSync_OnDoInsertsCompleted;
            this._importSync.OnDoUpdatesCompleted += this._importSync_OnDoUpdatesCompleted;
            this._importSync.OnSynchronizationEnded += this._importSync_OnSynchronizationEnded;
            this._importSync.OnSynchronizationStarted += this._importSync_OnSynchronizationStarted;
        }
        
        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            this._importSync.Sync();
        }

        /// <summary>
        /// Handles the OnSynchronizationStarted event of the _importSync control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Bladex.Garantias.ImportSync.ImportSyncStartEventArgs"/> instance containing the event data.</param>
        private void _importSync_OnSynchronizationStarted(object sender, ImportSyncStartEventArgs e)
        {
            string fechaCorte = e.FechaDeCorte.HasValue ? e.FechaDeCorte.Value.ToShortDateString() : "(no date)";
            string retrievingMessage = string.Format("{0} - [START] -  Retrieving records for {1}. ", DateTime.Now, fechaCorte);
            
            Console.WriteLine(retrievingMessage);            
            Console.WriteLine(string.Format("{0} - [START] - {1} records to import.", DateTime.Now, e.RecordsToImport));

            this._logger.Info(string.Format("[START] - Retrieving records for {0}.", fechaCorte));
            this._logger.Info(string.Format("[START] - {0} records to import.", e.RecordsToImport));
        }

        /// <summary>
        /// Handles the OnSynchronizationEnded event of the _importSync control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Bladex.Garantias.ImportSync.ImportSyncEndEventArgs"/> instance containing the event data.</param>
        private void _importSync_OnSynchronizationEnded(object sender, ImportSyncEndEventArgs e)
        {
            Console.WriteLine(DateTime.Now + " - " + string.Format("[END] - Synchronization process {0}. {1}", e.SyncStatus, !string.IsNullOrEmpty(e.Message) ? "Message: " + e.Message : string.Empty));
            Console.WriteLine(DateTime.Now + " - " + string.Format("[END] - {0} records processed for date {1}", e.RecordsToImport, e.FechaDeCorte.HasValue ? e.FechaDeCorte.Value.ToShortDateString() : "(no date)"));
            switch (e.SyncStatus)
            {
                case ImportSyncEndEventArgs.ImportSyncEndStatus.Success:
                    this._logger.Info(string.Format("[END] - Synchronization process {0}.", e.SyncStatus));
                    break;
                case ImportSyncEndEventArgs.ImportSyncEndStatus.Error:
                    this._logger.Error(string.Format("[END] - Synchronization process {0}.", e.SyncStatus));
                    break;
                default:
                    this._logger.Info(string.Format("[END] - Synchronization process ended with {0} status.", e.SyncStatus));
                    break;
            }

            this._logger.Info(string.Format("[END] - {0} records processed for date {1}", e.RecordsToImport, e.FechaDeCorte.HasValue ? e.FechaDeCorte.Value.ToShortDateString() : "(no date)"));
        }

        /// <summary>
        /// Handles the OnDoUpdatesCompleted event of the _importSync control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Bladex.Garantias.ImportSync.ImportSyncEventArgs"/> instance containing the event data.</param>
        private void _importSync_OnDoUpdatesCompleted(object sender, ImportSyncEventArgs e)
        {
            Console.WriteLine(DateTime.Now + " - " + string.Format("[UPDATE] - {0} records.", e.RecordsAffected.Count));
            this._logger.Info(string.Format("[UPDATE] - {0} records.", e.RecordsAffected.Count));
        }

        /// <summary>
        /// Handles the OnDoInsertsCompleted event of the _importSync control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Bladex.Garantias.ImportSync.ImportSyncEventArgs"/> instance containing the event data.</param>
        private void _importSync_OnDoInsertsCompleted(object sender, ImportSyncEventArgs e)
        {
            Console.WriteLine(DateTime.Now + " - " + string.Format("[INSERT] - {0} records.", e.RecordsAffected.Count));
            this._logger.Info(string.Format("[INSERT] - {0} records.", e.RecordsAffected.Count));
        }

        /// <summary>
        /// Handles the OnDoDeletesCompleted event of the _importSync control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Bladex.Garantias.ImportSync.ImportSyncEventArgs"/> instance containing the event data.</param>
        private void _importSync_OnDoDeletesCompleted(object sender, ImportSyncEventArgs e)
        {
            Console.WriteLine(DateTime.Now + " - " + string.Format("[DELETE] - {0} records.", e.RecordsAffected.Count));
            this._logger.Info(string.Format("[DELETE] - {0} records.", e.RecordsAffected.Count));
        }        
    }
}