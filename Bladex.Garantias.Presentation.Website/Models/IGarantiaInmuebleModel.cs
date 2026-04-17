using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Bladex.Garantias.Presentation.Website.ViewModels;

namespace Bladex.Garantias.Presentation.Website.Models
{
    /// <summary>
    /// IGarantiaInmuebleModel interface.
    /// </summary>
    public interface IGarantiaInmuebleModel
    {
        //IEnumerable<SelectListItem> AseguradorSuperSelectList { get; set; }
        /// <summary>
        /// Gets or sets the asegurador super.
        /// </summary>
        /// <value>
        /// The asegurador super of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.AvaluadorasViewModel"/>
        /// </value>
        AvaluadorasViewModel AseguradorSuper { get; set; }

        /// <summary>
        /// (Distressed Sale Value)
        /// </summary>
        decimal ValorEvaluacionVentaRapida
        {
            get;
            set;
        }

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

        /// <summary>
        /// Valor de Avaluo
        /// </summary>
        decimal ValorAvaluo
        {
            get;
            set;
        }

        /// <summary>
        /// Inscripcion Registro Publico
        /// </summary>
        string InscripcionRegistroPublico
        {
            get;
            set;
        }

        /// <summary>
        /// Numero de Finca
        /// </summary>
        string NumeroDeFinca
        {
            get;
            set;
        }
    }
}
