using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Bladex.Garantias.Presentation.Website.ViewModels;

namespace Bladex.Garantias.Presentation.Website.Models
{
    /// <summary>
    /// IGarantiaDepositoOtroBancoModel interface.
    /// </summary>
    public interface IGarantiaDepositoOtroBancoModel
    {
        //IEnumerable<SelectListItem> BancoSuperSelectList { get; set; }
        /// <summary>
        /// Gets or sets the banco super.
        /// </summary>
        /// <value>
        /// The banco super of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.BancosViewModel"/>
        /// </value>
        BancosViewModel BancoSuper { get; set; }
    }
}
