using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Bladex.Garantias.Presentation.Website.Components.Attributes;
using Bladex.Garantias.Presentation.Website.ViewModels;

namespace Bladex.Garantias.Presentation.Website.Models
{
    [DisplayName("Garantia Deposito")]
    public class GarantiaDepositoMetadata : GarantiaBaseMetadata,  IGarantiaDepositoModel
    {
        // TODO: MUESTRA TODOS LOS BANCOS
        #region Implementation of IGarantiaDepositoModel

        //public IEnumerable<SelectListItem> BancoLocalSuperSelectList { get; set; }
        
        [DisplayName("Banco Local Super")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        [Required]
        public BancosViewModel BancoLocalSuper { get; set; }

        #endregion
    }
}
