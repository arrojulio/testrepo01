using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Presentation.Website.Models;

namespace Bladex.Garantias.Presentation.Website.ViewModels
{
    /// <summary>
    /// The categoria super change view model class.
    /// </summary>
    public class CategoriaSuperChangeViewModel
    {
        /// <summary>
        /// Gets or sets the current categoria super id.
        /// </summary>
        /// <value>
        /// The current categoria super id of type <see cref="System.String"/>
        /// </value>
        public string CurrentCategoriaSuperId { get; set; }
        /// <summary>
        /// Gets or sets the new categoria super id.
        /// </summary>
        /// <value>
        /// The new categoria super id of type <see cref="System.String"/>
        /// </value>
        public string NewCategoriaSuperId { get; set; }
        /// <summary>
        /// Gets or sets the garantia.
        /// </summary>
        /// <value>
        /// The garantia of type <see cref="Bladex.Garantias.DomainModel.DomainBase.GarantiaBase"/>
        /// </value>
        public GarantiaBase Garantia { get; set; }
        /// <summary>
        /// Gets or sets the categoria super list.
        /// </summary>
        /// <value>
        /// The categoria super list of type <see cref="System.Web.Mvc.SelectList"/>
        /// </value>
        public SelectList CategoriaSuperList { get; set; }
        /// <summary>
        /// Gets or sets the categoria super selected.
        /// </summary>
        /// <value>
        /// The categoria super selected of type <see cref="System.Web.Mvc.SelectListItem"/>
        /// </value>
        public SelectListItem CategoriaSuperSelected { get; set; }

    }
}
