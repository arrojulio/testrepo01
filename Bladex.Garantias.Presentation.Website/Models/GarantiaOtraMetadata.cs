using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Bladex.Garantias.Presentation.Website.Components.Attributes;
using Bladex.Garantias.Presentation.Website.ViewModels;
using System.ComponentModel.DataAnnotations;
using Bladex.Garantias.DomainModel.DomainBase;

namespace Bladex.Garantias.Presentation.Website.Models
{
    [DisplayName("Garantia Otra")]
    public class GarantiaOtraMetadata : GarantiaBaseMetadata, IGarantiaOtraModel
    {
        #region Implementation of IGarantiaOtraModel
        
        [DisplayName("Nombre Emisor / Garante / Cedente")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        [Required]
        public ActorViewModel Emisor { get; set; }

        [DisplayName("Inclusion de Avales adicionales")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        public List<AvalViewModel> AvalList { get; set; }

        [DisplayName("Avales")]        
        public AvalManagerViewModel AvalComponent { get; set; }

        [DisplayName("Nro Referencia")]
        [Required]
        [StringLength(100)]
        public string NroReferencia { get; set; }
        #endregion
    }
}
