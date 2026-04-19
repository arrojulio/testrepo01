using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase;

namespace Bladex.Garantias.ImportSync
{
    /// <summary>
    /// <see cref="EventArgs"/> implementation.
    /// </summary>
    public class GarantiaEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GarantiaEventArgs"/> class.
        /// </summary>
        /// <param name="garantia">The garantia.</param>
        /// <param name="source">The source.</param>
        public GarantiaEventArgs(GarantiaBase garantia, IMPORT_TH_ATOMO_GARANTIAS source)
        {
            this.Garantia = garantia;
            this.Source = source;
        }

        /// <summary>
        /// <see cref="IMPORT_TH_ATOMO_GARANTIAS"/>
        /// </summary>
        public IMPORT_TH_ATOMO_GARANTIAS Source;
        /// <summary>
        /// <see cref="GarantiaBase"/>
        /// </summary>
        public GarantiaBase Garantia;
    }
}
