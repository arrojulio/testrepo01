using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bladex.Garantias.Presentation.Website.Models
{
    [MetadataType(typeof(GarantiaContratoMetadata))]
    public class GarantiaContratoModel : IGarantiaContratoModel
    {
        #region Implementation of IGarantiaContratoModel

        public int ID { get; set; }
        public string DealReference { get; set; }
        public DateTime? FechaRegistroInicial { get; set; }
        public DateTime? FechaVencimientoGarantia { get; set; }
        public DateTime? FechaVencimientoRiesgo { get; set; }
        public decimal PorcUtilization { get; set; }
        //public bool IsPorcUtilizationImported { get; set; }

        public int? GarantiaId { get; set; }
        public decimal? NetBalancePrincipal { get; set; }
        
        #endregion
    }
}