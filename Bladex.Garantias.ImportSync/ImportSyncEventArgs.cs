using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bladex.Garantias.ImportSync
{
    /// <summary>
    /// Event Argument class that represents the status of the synchronization process.
    /// </summary>
    public class ImportSyncEventArgs : EventArgs
    {
        /// <summary>
        /// <see cref="ImportSyncEventArgs"/> constructor.
        /// </summary>
        /// <param name="recordsAffected">object with synchronization information.</param>
        public ImportSyncEventArgs(IDictionary<string,bool> recordsAffected)
        {
            _recordsAffected = recordsAffected;
        }

        private readonly IDictionary<string, bool> _recordsAffected;

        /// <summary>
        /// Gets the records affected.
        /// </summary>
        public IDictionary<string,bool> RecordsAffected
        {
            get { return _recordsAffected; }
        }
    }
}
