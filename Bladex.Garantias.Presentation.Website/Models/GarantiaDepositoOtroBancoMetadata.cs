using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Bladex.Garantias.Presentation.Website.Components.Attributes;
using Bladex.Garantias.Presentation.Website.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Bladex.Garantias.Presentation.Website.Models
{
    [DisplayName("Garantia Deposito Otro Banco")]
    public class GarantiaDepositoOtroBancoMetadata : GarantiaBaseMetadata, IGarantiaDepositoOtroBancoModel
    {
        #region Implementation of IGarantiaDepositoOtroBancoModel

        //public IEnumerable<SelectListItem> BancoSuperSelectList { get; set; }        
        [DisplayName("Banco Super")]
        [Required]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]        
        public BancosViewModel BancoSuper { get; set; }

        #endregion
    }
}
