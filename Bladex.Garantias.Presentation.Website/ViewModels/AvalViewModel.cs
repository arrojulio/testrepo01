using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Bladex.Garantias.Application.Facades;
using Bladex.Garantias.DomainModel.DomainBase;

namespace Bladex.Garantias.Presentation.Website.ViewModels
{
    /// <summary>
    /// The aval view model class.
    /// </summary>
    public class AvalViewModel : BaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AvalViewModel"/> class.
        /// </summary>
        public AvalViewModel()
        {
            this.Key = default(int?);
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
        [System.ComponentModel.DataAnnotations.Display(Name="Es Cliente?")]
        [DisplayName("Es Cliente?")]
        public bool EsCliente {get;set;}
        /// <summary>
        /// Gets or sets the nombre.
        /// </summary>
        /// <value>
        /// The nombre of type <see cref="System.String"/>
        /// </value>
        [System.ComponentModel.DataAnnotations.Display(Name = "Nombre")]
        [DisplayName("Nombre")]
        [Required(ErrorMessage="Debe seleccionar un nombre para el aval.")]
        [UIHint("Autocomplete")]
        public override string Nombre{get;set;}

        /// <summary>
        /// Gets or sets the pais.
        /// </summary>
        /// <value>
        /// The pais of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.PaisViewModel"/>
        /// </value>
        [System.ComponentModel.DataAnnotations.Display(Name = "Pais")]
        [DisplayName("Pais")]
        [Required(ErrorMessage = "Debe seleccionar un pais para el aval.")]
        public PaisViewModel Pais {get;set;}

        /// <summary>
        /// Gets or sets the porcentaje cobertura.
        /// </summary>
        /// <value>
        /// The porcentaje cobertura of type <see cref="System.Double"/>
        /// </value>
        [System.ComponentModel.DataAnnotations.UIHint("PercentageDouble")]
        [System.ComponentModel.DataAnnotations.Display(Name = "% de Cobertura")]
        [DisplayName("% de Cobertura")]
        [Required]
        public double PorcentajeCobertura{get;set;}

        /// <summary>
        /// Gets or sets the tipo aval.
        /// </summary>
        /// <value>
        /// The tipo aval of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.TipoAvalViewModel"/>
        /// </value>
        [System.ComponentModel.DataAnnotations.Display(Name = "Tipo de Aval")]
        [DisplayName("Tipo de Aval")]
        [Required(ErrorMessage = "Debe seleccionar el tipo de aval.")]
        public TipoAvalViewModel TipoAval {get;set;}
        /// <summary>
        /// Gets or sets the garantia id.
        /// </summary>
        /// <value>
        /// The garantia id of type <see cref="int"/>
        /// </value>
        public int? GarantiaId { get; set; }
        /// <summary>
        /// Gets or sets the list.
        /// </summary>
        public IEnumerable<System.Web.Mvc.SelectListItem> List { get; set; }
    }
}
