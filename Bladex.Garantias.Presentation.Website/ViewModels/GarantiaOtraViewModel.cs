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
    /// The garantia otra view model class.
    /// </summary>
    [DisplayName("Garantia Otra")]
    public class GarantiaOtraViewModel : GarantiaBaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GarantiaOtraViewModel"/> class.
        /// </summary>
        public GarantiaOtraViewModel()
            : base()
        {
            this.Garantia = new GarantiaOtraModel();
        }

        /// <summary>
        /// Gets or sets the garantia.
        /// </summary>
        /// <value>
        /// The garantia of type <see cref="Bladex.Garantias.Presentation.Website.Models.GarantiaOtraModel"/>
        /// </value>
        [DisplayName("Garantia Otra")]
        public GarantiaOtraModel Garantia { get; set; }

        
        
    }
}
