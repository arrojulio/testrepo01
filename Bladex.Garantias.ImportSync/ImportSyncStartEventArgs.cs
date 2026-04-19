using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bladex.Garantias.ImportSync
{
    /// <summary>
    /// Event Argument class that represents the start of the synchronization process.
    /// </summary>
    public class ImportSyncStartEventArgs : EventArgs
    {
        /// <summary>
        /// <see cref="ImportSyncStartEventArgs"/> constructor.
        /// </summary>
        /// <param name="recordsToImport"><see cref="Int32"/> used to represent the number of records to process.</param>
        /// <param name="fechaDeCorte"><see cref="System.DateTime"/> that represents the date of records to process.</param>
        public ImportSyncStartEventArgs(int recordsToImport, DateTime? fechaDeCorte)
        {
            _recordsToImport = recordsToImport;
            _fechaDeCorte = fechaDeCorte;
        }

        private readonly DateTime? _fechaDeCorte;
        private readonly int _recordsToImport;

        /// <summary>
        /// Gets the fecha de corte.
        /// </summary>
        public DateTime? FechaDeCorte
        {
            get { return _fechaDeCorte; }
        }

        /// <summary>
        /// Gets the records to import.
        /// </summary>
        public int RecordsToImport
        {
            get { return _recordsToImport; }
        }
    }


    /// <summary>
    /// Event Argument class that represents the end of the synchronization process.
    /// </summary>
    public class ImportSyncEndEventArgs : ImportSyncStartEventArgs
    {
        /// <summary>
        /// <see cref="ImportSyncEndEventArgs"/> constructor.
        /// </summary>
        /// <param name="recordsProcessed"><see cref="Int32"/> used to represent the number of records processed.</param>
        /// <param name="fechaDeCorte"><see cref="DateTime"/> that represents the date of records to process.</param>
        /// <param name="syncStatus"><see cref="ImportSyncEndStatus"/> that represents the status of the import process.</param>
        public ImportSyncEndEventArgs(int recordsProcessed, DateTime? fechaDeCorte, ImportSyncEndStatus syncStatus) :base(recordsProcessed, fechaDeCorte)
        {
            this._syncStatus = syncStatus;
        }

        private readonly ImportSyncEndStatus _syncStatus;

        /// <summary>
        /// Synchronization status of <see cref="ImportSync"/>
        /// </summary>
        public ImportSyncEndStatus SyncStatus { get { return _syncStatus; } }
            
        /// <summary>
        /// Enum that represents the Synchronization status.
        /// </summary>
        public enum ImportSyncEndStatus
        { 
            Success, Error
        }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }
    }
}
