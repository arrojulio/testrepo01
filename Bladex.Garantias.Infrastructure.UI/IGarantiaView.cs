using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bladex.Garantias.Infrastructure.UI
{
    /// <summary>
    /// Defines the behavior of GarantiaView
    /// </summary>
    public interface IGarantiaView
    {
        /// <summary>
        /// Saves the view
        /// </summary>
        void Save();
        /// <summary>
        /// Reset the state of the view.
        /// </summary>
        void Reset();
        /// <summary>
        /// Initialize the view
        /// </summary>
        void Init();
        /// <summary>
        /// Loads the view with the guarantee specified by the key.
        /// </summary>
        /// <param name="garantia"><see cref="Bladex.Garantias.DomainModel.DomainBase.GarantiaBase"/></param>
        void Load(Bladex.Garantias.DomainModel.DomainBase.GarantiaBase garantia);
    }
}
