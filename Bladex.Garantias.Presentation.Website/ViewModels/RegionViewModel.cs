using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bladex.Garantias.Application.Facades;
using Bladex.Garantias.DomainModel.DomainBase;

namespace Bladex.Garantias.Presentation.Website.ViewModels
{
    /// <summary>
    /// The CodRegion view model class.
    /// </summary>
    public class RegionViewModel : BaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegionViewModel"/> class.
        /// </summary>
        public RegionViewModel()
        {
            this.Key = "NA";
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

        public IEnumerable<System.Web.Mvc.SelectListItem> List { get; set; }
    }
}