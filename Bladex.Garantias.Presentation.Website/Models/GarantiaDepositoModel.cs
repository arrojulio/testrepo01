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


    /// <summary>
    /// The garantia deposito model class.
    /// </summary>
    [MetadataType(typeof(GarantiaDepositoMetadata))]
    public class GarantiaDepositoModel : GarantiaBaseModel, IGarantiaDepositoModel
    {
        public override void Init()
        {
            this.BancoLocalSuper = new BancosViewModel();
        }

        //public IEnumerable<SelectListItem> BancoLocalSuperSelectList { get; set; }

        public BancosViewModel BancoLocalSuper { get; set; }
                
   
    }
}