using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bladex.Garantias.Presentation.Website.Components.Attributes;
using Bladex.Garantias.Presentation.Website.ViewModels;

namespace Bladex.Garantias.Presentation.Website.Models
{
    [DisplayName("Garantia Inmueble")]
    public class GarantiaInmuebleMetadata : GarantiaBaseMetadata, IGarantiaInmuebleModel
    {
        #region Implementation of IGarantiaInmuebleModel

        //public IEnumerable<SelectListItem> AseguradorSuperSelectList { get; set; }

        /// <summary>
        /// Inscripcion Registro Publico
        /// </summary>
        [DataType(DataType.Text)]
        [DisplayName("Inscripcion Registro Publico")]
        [StringLength(100, ErrorMessage = "No se deben exceder los 100 caracteres.")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        [UIHint("Autocomplete")]
        [Required]
        public string InscripcionRegistroPublico { get; set; }

        /// <summary>
        /// Numero de Finca
        /// </summary>
        [DataType(DataType.Text)]
        [DisplayName("Numero de Finca")]
        [StringLength(100, ErrorMessage = "No se deben exceder los 100 caracteres.")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        [UIHint("Autocomplete")]
        [Required]
        public string NumeroDeFinca { get; set; }

        /// <summary>
        /// Valor de Avaluo
        /// </summary>
        [DataType(DataType.Currency)]
        [DisplayName("Valor de Avaluo")]
        [UIHint("Money")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        [Required]
        public decimal ValorAvaluo { get; set; }

        [DisplayName("Fecha Inicial Avaluo")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        [Required]
        public DateTime? FechaInicialAvaluo { get; set; }
        [DisplayName("Fecha Vencimiento Avaluo")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        [Required]
        public DateTime? FechaVencimientoAvaluo { get; set; }
        [DataType(DataType.Currency)]
        [DisplayName("Valor Total Avaluo")]
        [UIHint("Money")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        [Required]
        public decimal ValorTotalAvaluo { get; set; }

        /// <summary>
        /// (Distressed Sale Value)
        /// </summary>
        [DataType(DataType.Currency)]
        [DisplayName("Valor Evaluacion Venta Rapida (Distressed Sale Value)")]
        [UIHint("Money")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        [Required]
        public decimal ValorEvaluacionVentaRapida { get; set; }

        [DisplayName("Avaluador Super")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        [Required]
        public AvaluadorasViewModel AseguradorSuper { get; set; }

        #endregion
    }
}