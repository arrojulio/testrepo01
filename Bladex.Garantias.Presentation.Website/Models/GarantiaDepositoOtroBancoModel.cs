using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Web.Mvc;
using System.Linq;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Presentation.Website.ViewModels;

namespace Bladex.Garantias.Presentation.Website.Models
{

    [MetadataType(typeof(GarantiaDepositoOtroBancoMetadata))]
    public class GarantiaDepositoOtroBancoModel : GarantiaBaseModel, IGarantiaDepositoOtroBancoModel
    {
        public override void Init()
        {
            this.BancoSuper = new BancosViewModel();
        }

        //public IEnumerable<SelectListItem> BancoSuperSelectList { get; set; }
        [Required]
        public BancosViewModel BancoSuper { get; set; }

    }
}