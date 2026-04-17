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
    [DisplayName("Garantia Mueble")]
    public class GarantiaMuebleMetadata : GarantiaBaseMetadata, IGarantiaMuebleModel
    {
        #region Implementation of IGarantiaMuebleModel
                
        [DisplayName("Asegurador Super")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]        
        public AseguradorasViewModel AseguradorSuper { get; set; }
        [DisplayName("Fecha Inicial Avaluo")]
        [Required]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")] 
        public DateTime? FechaInicialAvaluo { get; set; }
        [DisplayName("Fecha Vencimiento Avaluo")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        [Required]
        public DateTime? FechaVencimientoAvaluo { get; set; }
        [DisplayName("Valor Total Avaluo")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        [Required]
        [DisplayFormat(DataFormatString = "c", NullDisplayText = "", ApplyFormatInEditMode = false)]
        [UIHint("Money")]
        [DataType(DataType.Currency)]
        public decimal ValorTotalAvaluo { get; set; }

        //public IEnumerable<SelectListItem> AseguradorSuperSelectList { get; set; }

        #endregion
    }
}
