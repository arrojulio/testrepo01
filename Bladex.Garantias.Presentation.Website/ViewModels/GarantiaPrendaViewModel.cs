using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Bladex.Garantias.Application.Facades;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Presentation.Website.Models;

namespace Bladex.Garantias.Presentation.Website.ViewModels
{
    /// <summary>
    /// The garantia prenda view model class.
    /// </summary>
    [DisplayName("Garantia Prenda")]
    public class GarantiaPrendaViewModel : GarantiaBaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GarantiaPrendaViewModel"/> class.
        /// </summary>
        public GarantiaPrendaViewModel()
            : base()
        {
            this.Garantia = new GarantiaPrendaModel();
        }

        /// <summary>
        /// Gets or sets the garantia.
        /// </summary>
        /// <value>
        /// The garantia of type <see cref="Bladex.Garantias.Presentation.Website.Models.GarantiaPrendaModel"/>
        /// </value>
        [DisplayName("Garantia Prenda")]
        public GarantiaPrendaModel Garantia { get; set; }
                
    }
}
