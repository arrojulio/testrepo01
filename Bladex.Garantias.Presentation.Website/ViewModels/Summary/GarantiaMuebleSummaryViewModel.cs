using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bladex.Garantias.Presentation.Website.Models.Summary;
using System.ComponentModel;

namespace Bladex.Garantias.Presentation.Website.ViewModels.Summary
{
    public class GarantiaMuebleSummaryViewModel
    {

          /// <summary>
        /// Initializes a new instance of the <see cref="GarantiaMuebleViewModel"/> class.
        /// </summary>
        public GarantiaMuebleSummaryViewModel()
            : base()
        {
            this.Garantia = new GarantiaMuebleSummaryModel();
        }

        /// <summary>
        /// Gets or sets the garantia.
        /// </summary>
        /// <value>
        /// The garantia of type <see cref="Bladex.Garantias.Presentation.Website.Models.GarantiaMuebleModel"/>
        /// </value>
        [DisplayName("Garantia Mueble")]
        public GarantiaMuebleSummaryModel Garantia { get; set; }
    }
}