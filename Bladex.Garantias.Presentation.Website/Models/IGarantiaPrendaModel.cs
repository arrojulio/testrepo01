using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Bladex.Garantias.Presentation.Website.ViewModels;

namespace Bladex.Garantias.Presentation.Website.Models
{
    /// <summary>
    /// IGarantiaPrendaModel interface.
    /// </summary>
    public interface IGarantiaPrendaModel
    {
        /// <summary>
        /// Gets or sets the calificaciones riesgo emision.
        /// </summary>
        /// <value>
        /// The calificaciones riesgo emision of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.CalificacionesRiesgoViewModel"/>
        /// </value>
        CalificacionesRiesgoViewModel CalificacionesRiesgoEmision { get; set; }

        /// <summary>
        /// Gets or sets the calificaciones riesgo emisor.
        /// </summary>
        /// <value>
        /// The calificaciones riesgo emisor of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.CalificacionesRiesgoViewModel"/>
        /// </value>
        CalificacionesRiesgoViewModel CalificacionesRiesgoEmisor { get; set; }
        
        ActorViewModel Emisor { get; set; }

        /// <summary>
        /// Gets or sets the tipo instrumento financiero.
        /// </summary>
        /// <value>
        /// The tipo instrumento financiero of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.InstrumentoFinancieroViewModel"/>
        /// </value>
        InstrumentoFinancieroViewModel TipoInstrumentoFinanciero { get; set; }
        
        /// <summary>
        /// Identificador de Prenda
        /// </summary>
        string IdentificadorPrenda
        { get; set; }

        /// <summary>
        /// Fecha Inicial del Avaluo
        /// </summary>
        DateTime? FechaInicialAvaluo
        { get; set; }

        /// <summary>
        /// Fecha Vencimiento Avaluo
        /// </summary>
        DateTime? FechaVencimientoAvaluo
        { get; set; }

        /// <summary>
        /// Valor Total Avaluo
        /// </summary>
        decimal ValorTotalAvaluo
        { get; set; }
    }
}
