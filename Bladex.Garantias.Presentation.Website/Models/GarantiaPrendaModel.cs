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
    /// The garantia prenda model class.
    /// </summary>
    [MetadataType(typeof(GarantiaPrendaMetadata))]
    public class GarantiaPrendaModel : GarantiaBaseModel, IGarantiaPrendaModel
    {
        public override void Init()
        {
            base.Init();
            this.Emisor = new ActorViewModel();
            this.CalificacionesRiesgoEmision = new CalificacionesRiesgoViewModel();
            this.CalificacionesRiesgoEmisor = new CalificacionesRiesgoViewModel();
            this.TipoInstrumentoFinanciero = new InstrumentoFinancieroViewModel();
            this.PaisEmision = new PaisViewModel();

        }

        #region Implementation of IGarantiaPrendaModel

        /// <summary>
        /// Gets or sets the calificaciones riesgo emision.
        /// </summary>
        /// <value>
        /// The calificaciones riesgo emision of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.CalificacionesRiesgoViewModel"/>
        /// </value>
        public CalificacionesRiesgoViewModel CalificacionesRiesgoEmision { get; set; }
        //public IEnumerable<SelectListItem> CalificacionesRiesgoEmisionSelectList { get; set; }
        /// <summary>
        /// Gets or sets the calificaciones riesgo emisor.
        /// </summary>
        /// <value>
        /// The calificaciones riesgo emisor of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.CalificacionesRiesgoViewModel"/>
        /// </value>
        public CalificacionesRiesgoViewModel CalificacionesRiesgoEmisor { get; set; }
        //public IEnumerable<SelectListItem> CalificacionesRiesgoEmisorSelectList { get; set; }
        public ActorViewModel Emisor { get; set; }
        //public IEnumerable<SelectListItem> EmisorSelectList { get; set; }
        /// <summary>
        /// Gets or sets the tipo instrumento financiero.
        /// </summary>
        /// <value>
        /// The tipo instrumento financiero of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.InstrumentoFinancieroViewModel"/>
        /// </value>
        public InstrumentoFinancieroViewModel TipoInstrumentoFinanciero { get; set; }

        /// <summary>
        /// Identificador de Prenda
        /// </summary>
        [MaxLength(100)]
        [AllowHtml]
        public string IdentificadorPrenda { get; set; }

        /// <summary>
        /// Gets or sets the pais emision.
        /// </summary>
        /// <value>
        /// The pais emision of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.PaisViewModel"/>
        /// </value>
        public PaisViewModel PaisEmision { get; set; }
        //public IEnumerable<SelectListItem> PaisEmisionSelectList { get; set; }
        public DateTime? FechaInicialAvaluo { get; set; }
        public DateTime? FechaVencimientoAvaluo { get; set; }
        public decimal ValorTotalAvaluo { get; set; }
        #endregion
    }
}