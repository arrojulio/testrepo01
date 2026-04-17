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
    [DisplayName("Garantia Prenda")]
    public class GarantiaPrendaMetadata : GarantiaBaseMetadata, IGarantiaPrendaModel
    {
        #region Implementation of IGarantiaPrendaModel

        /// <summary>
        /// Identificador de Prenda
        /// </summary>
        [DisplayName("Identificador de Prenda")]
        [StringLength(100, ErrorMessage = "No se deben exceder los 100 caracteres.")]
        [DataType(DataType.Text)]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        [UIHint("Autocomplete")]
        [Required]
        public string IdentificadorPrenda { get; set; }

        [DisplayName("Emisor / Custodio o Depositario")]
        [Required]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        public ActorViewModel Emisor { get; set; }

        [DisplayName("Tipo Instrumento Financiero")]
        [Required]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        public InstrumentoFinancieroViewModel TipoInstrumentoFinanciero { get; set; }

        [DisplayName("Calificaciones Riesgo Emision")]
        [Required]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        public CalificacionesRiesgoViewModel CalificacionesRiesgoEmision { get; set; }

        [DisplayName("Calificaciones Riesgo Emisor")]
        [Required]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        public CalificacionesRiesgoViewModel CalificacionesRiesgoEmisor { get; set; }

        [DisplayName("Pais Emision")]
        [Required]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        public PaisViewModel PaisEmision { get; set; }
        [DisplayName("Fecha Inicial Avaluo")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        [Required]
        public DateTime? FechaInicialAvaluo { get; set; }
        [DisplayName("Fecha Vencimiento Avaluo")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        [Required]
        public DateTime? FechaVencimientoAvaluo { get; set; }
        [DisplayName("Valor Total Avaluo")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        [Required]
        public decimal ValorTotalAvaluo { get; set; }

        #endregion
    }
}
