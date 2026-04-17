using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bladex.Garantias.Presentation.Website.Models
{
    public class GarantiaContratoMetadata : IGarantiaContratoModel
    {
        #region Implementation of IGarantiaContratoModel

        [DisplayName("Contrato N°")]
        public int ID { get; set; }
        
        [DisplayName("Deal Reference")]
        [Required(ErrorMessage="Debe seleccionarse un contrato.")]
        public string DealReference { get; set; }

        [DisplayName("Fecha Registro Inicial")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? FechaRegistroInicial { get; set; }

        [DisplayName("Fecha Vencimiento Garantia")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? FechaVencimientoGarantia { get; set; }

        [DisplayName("Fecha Vencimiento Riesgo")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? FechaVencimientoRiesgo { get; set; }

        [DisplayName("% Cobertura Garantía")]
        //[UIHint("Percentage")]
        [DisplayFormat(DataFormatString = "{0:f10}")]
        [Required()]
        public decimal PorcUtilization { get; set; }

        [DisplayName("Nro Identificador Garantia")]
        public int? GarantiaId { get; set; }

        [DisplayName("Net Balance Principal")]
        [DataType(DataType.Currency)]
        [UIHint("Money")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal? NetBalancePrincipal { get; set; }

        #endregion
    }
}