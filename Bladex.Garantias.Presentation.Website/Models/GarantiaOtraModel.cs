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

    
    [MetadataType(typeof(GarantiaOtraMetadata))]
    public class GarantiaOtraModel : GarantiaBaseModel, IGarantiaOtraModel
    {
        public override void Init()
        {
            this.Emisor = new ActorViewModel();
            this.AvalList = new List<AvalViewModel>();
            this.AvalComponent = new AvalManagerViewModel();            
        }

        #region Implementation of IGarantiaOtraModel
        public List<AvalViewModel> AvalList { get; set; }
        public ActorViewModel Emisor { get; set; }
        [StringLength(100)]
        [AllowHtml]
        public string NroReferencia { get; set; }
        
        [UIHint("AvalManager")]
        public AvalManagerViewModel AvalComponent { get; set; }
        //public IEnumerable<SelectListItem> EmisorSelectList { get; set; }

        #endregion

    }
}