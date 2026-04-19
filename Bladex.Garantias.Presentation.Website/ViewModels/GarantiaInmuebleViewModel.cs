using System.ComponentModel;
using Bladex.Garantias.Presentation.Website.Models;

namespace Bladex.Garantias.Presentation.Website.ViewModels
{
    /// <summary>
    /// The garantia inmueble view model class.
    /// </summary>
    [DisplayName("Garantia Inmueble")]
    public class GarantiaInmuebleViewModel:GarantiaBaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GarantiaInmuebleViewModel"/> class.
        /// </summary>
        public GarantiaInmuebleViewModel()
        {
            this.Garantia = new GarantiaInmuebleModel();
        }

        /// <summary>
        /// Gets or sets the garantia.
        /// </summary>
        /// <value>
        /// The garantia of type <see cref="Bladex.Garantias.Presentation.Website.Models.GarantiaInmuebleModel"/>
        /// </value>
        [DisplayName("Garantia InMueble")]
        public GarantiaInmuebleModel Garantia { get; set; }

    }
}
