using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Bladex.Garantias.Presentation.Website.ViewModels;

namespace Bladex.Garantias.Presentation.Website.Models
{
    /// <summary>
    /// IGarantiaDepositoModel interface.
    /// </summary>
    public interface IGarantiaDepositoModel
    {
        //IEnumerable<SelectListItem> BancoLocalSuperSelectList { get; set; }

        /// <summary>
        /// Gets or sets the banco local super.
        /// </summary>
        /// <value>
        /// The banco local super of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.BancosViewModel"/>
        /// </value>
        BancosViewModel BancoLocalSuper { get; set; }
        
        int? selectedOperationId
        {
            get;
            set;
        }
              
    }
}
