using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase;

namespace Bladex.Garantias.ImportSync
{
    /// <summary>
    /// Interface used to define the implementation of an GarantiaContratoManager
    /// </summary>
    public interface IGarantiaContratoManager
    {
        /// <summary>
        /// Does the update.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="garantiaId">The garantia id.</param>
        void DoUpdate(IMPORT_TH_ATOMO_GARANTIAS source, int garantiaId);
        /// <summary>
        /// Does the insert.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="garantiaId">The garantia id.</param>
        void DoInsert(IMPORT_TH_ATOMO_GARANTIAS source, int garantiaId);
        /// <summary>
        /// Does the delete.
        /// </summary>
        /// <param name="dealRef">The deal ref.</param>
        /// <param name="garantiaId">The garantia id.</param>
        void DoDelete(string dealRef, int garantiaId);
    }
}
