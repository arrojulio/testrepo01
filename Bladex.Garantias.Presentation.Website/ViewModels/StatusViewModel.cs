using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bladex.Garantias.Presentation.Website.ViewModels
{
    /// <summary>
    /// the Status ViewModel class
    /// </summary>
    public class StatusViewModel : BaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StatusViewModel"/> class.
        /// </summary>
        public StatusViewModel()
        {
            this.Key = string.Empty;
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