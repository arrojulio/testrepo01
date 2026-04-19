using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Application.Facades;
using Bladex.Garantias.DomainModel.DomainBase;

namespace Bladex.Garantias.Presentation.Website.ViewModels
{
    /// <summary>
    /// The monedas view model class.
    /// </summary>
    public class MonedasViewModel : BaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MonedasViewModel"/> class.
        /// </summary>
        public MonedasViewModel()
        {
            this.Key = string.Empty;
        }

        // USD - Default Value for Dropdowns
        public const string _DEFAULT_VALUE = "10";

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
        /// Gets or sets the list.
        /// </summary>
        public IEnumerable<System.Web.Mvc.SelectListItem> List { get; set; }
    }
}
