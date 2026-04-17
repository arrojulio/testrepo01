using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

using System.ComponentModel.DataAnnotations;

namespace Bladex.Garantias.Presentation.Website.ViewModels
{

    public class AvalManagerViewModel : BaseViewModel
    {
        public AvalManagerViewModel() 
        {
            AvalList = new List<AvalViewModel>();
            TipoAvalCatalog = new TipoAvalViewModel();
            PaisCatalog = new PaisViewModel();
            hiddenAvales = "";
        }
        
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key of type <see cref="int"/>
        /// </value>
        [System.ComponentModel.DataAnnotations.Display(Name = "ID")]
        [DisplayName("ID")]
        public int? Key { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [es cliente].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [es cliente]; otherwise, <c>false</c>.
        /// </value>
        [System.ComponentModel.DataAnnotations.Display(Name = "Es Cliente?")]
        [DisplayName("Es Cliente?")]
        public bool EsCliente { get; set; }
        /// <summary>
        /// Gets or sets the nombre.
        /// </summary>
        /// <value>
        /// The nombre of type <see cref="System.String"/>
        /// </value>
        [System.ComponentModel.DataAnnotations.Display(Name = "Nombre")]
        [DisplayName("Nombre")]
        //[Required(ErrorMessage = "Debe seleccionar un nombre para el aval.")]
        [UIHint("Autocomplete")]
        public override string Nombre { get; set; }

        /// <summary>
        /// Gets or sets the porcentaje cobertura.
        /// </summary>
        /// <value>
        /// The porcentaje cobertura of type <see cref="System.Double"/>
        /// </value>
        [System.ComponentModel.DataAnnotations.UIHint("PercentageDouble")]
        [System.ComponentModel.DataAnnotations.Display(Name = "% de Cobertura")]
        [DisplayName("% de Cobertura")]
        //[Required]
        public double PorcentajeCobertura { get; set; }

        public List<AvalViewModel> AvalList { get; set; }
        public TipoAvalViewModel TipoAvalCatalog { get; set; }
        public PaisViewModel PaisCatalog { get; set; }

        public string hiddenAvales { get; set; }
        
    }
}