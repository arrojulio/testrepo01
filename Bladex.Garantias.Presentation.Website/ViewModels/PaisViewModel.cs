using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bladex.Garantias.Application.Facades;
using Bladex.Garantias.DomainModel.DomainBase;

namespace Bladex.Garantias.Presentation.Website.ViewModels
{
    /// <summary>
    /// The pais view model class.
    /// </summary>
    public class PaisViewModel : BaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaisViewModel"/> class.
        /// </summary>
        public PaisViewModel()
        {
            this.Key = "N/A";
        }

        /// <summary>
        /// Gets or sets the nombre.
        /// </summary>
        /// <value>
        /// The nombre of type <see cref="System.String"/>
        /// </value>
        public override string Nombre { get; set; }
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key of type <see cref="System.String"/>
        /// </value>
        public string Key { get; set; }
        /// <summary>
        /// Gets or sets the codigo super.
        /// </summary>
        /// <value>
        /// The codigo super of type <see cref="System.String"/>
        /// </value>
        public string CodigoSuper{get;set;}
        public IEnumerable<System.Web.Mvc.SelectListItem> List { get; set; }
        
    }
}