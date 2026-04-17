using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Bladex.Garantias.Presentation.Website.ViewModels;

namespace Bladex.Garantias.Presentation.Website.Models
{
    /// <summary>
    /// IGarantiaMuebleModel interface.
    /// </summary>
    public interface IGarantiaMuebleModel
    {
        /// <summary>
        /// Gets or sets the asegurador super.
        /// </summary>
        /// <value>
        /// The asegurador super of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.AseguradorasViewModel"/>
        /// </value>
        AseguradorasViewModel AseguradorSuper { get; set; }
        //IEnumerable<SelectListItem> AseguradorSuperSelectList { get; set; }

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
