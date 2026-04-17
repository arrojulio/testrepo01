using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase;

namespace Bladex.Garantias.ImportSync
{
    /// <summary>
    /// Interface used to define the implementation of an GarantiaManager.
    /// </summary>
    public interface IGarantiaManager
    {

        /// <summary>
        /// Occurs when [do insert completed].
        /// </summary>
        event EventHandler<GarantiaEventArgs> DoInsertCompleted;
        /// <summary>
        /// Occurs when [do update completed].
        /// </summary>
        event EventHandler<GarantiaEventArgs> DoUpdateCompleted;
        /// <summary>
        /// Occurs when [do delete completed].
        /// </summary>
        event EventHandler<GarantiaEventArgs> DoDeleteCompleted;

        /// <summary>
        /// Does the update.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="target">The target. It should inherit GarantiaBase.</param>
        void DoUpdate(IMPORT_TH_ATOMO_GARANTIAS source, dynamic target);
        /// <summary>
        /// Does the insert.
        /// </summary>
        /// <param name="source">The source.</param>
        void DoInsert(IMPORT_TH_ATOMO_GARANTIAS source);
        /// <summary>
        /// Does the delete.
        /// </summary>
        /// <param name="target">The target.</param>
        void DoDelete(GarantiaBase target);

        /// <summary>
        /// Gets or sets the last id added.
        /// </summary>
        /// <value>
        /// The last id added.
        /// </value>
        int LastIdAdded
        {
            get;
            set;
        }

    }
}
