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

    [MetadataType(typeof(GarantiaInmuebleMetadata))]
    public class GarantiaInmuebleModel :GarantiaBaseModel, IGarantiaInmuebleModel
    {
        public override void Init()
        {
            this.AseguradorSuper = new AvaluadorasViewModel();
        }

        #region Implementation of IGarantiaInmuebleModel
        [Required]
        public AvaluadorasViewModel AseguradorSuper { get; set; }

        /// <summary>
        /// (Distressed Sale Value)
        /// </summary>
        public decimal ValorEvaluacionVentaRapida { get; set; }

        /// <summary>
        /// Fecha Inicial del Avaluo
        /// </summary>
        public DateTime? FechaInicialAvaluo
        { get; set; }

        /// <summary>
        /// Fecha Vencimiento Avaluo
        /// </summary>
        public DateTime? FechaVencimientoAvaluo
        { get; set; }

        /// <summary>
        /// Valor Total Avaluo
        /// </summary>
        public decimal ValorTotalAvaluo
        { get; set; }

        /// <summary>
        /// Valor de Avaluo
        /// </summary>
        public decimal ValorAvaluo { get; set; }

        /// <summary>
        /// Inscripcion Registro Publico
        /// </summary>
        [Required]
        [AllowHtml]
        [MaxLength(100)]
        public string InscripcionRegistroPublico { get; set; }

        /// <summary>
        /// Numero de Finca
        /// </summary>
        [Required]
        [MaxLength(100)]
        [AllowHtml]
        public string NumeroDeFinca { get; set; }

        #endregion
    }
}