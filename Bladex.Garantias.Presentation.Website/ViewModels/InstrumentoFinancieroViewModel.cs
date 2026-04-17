using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Application.Facades;
using Bladex.Garantias.DomainModel.DomainBase;

namespace Bladex.Garantias.Presentation.Website.ViewModels
{
    /// <summary>
    /// The instrumento financiero view model class.
    /// </summary>
    public class InstrumentoFinancieroViewModel : BaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InstrumentoFinancieroViewModel"/> class.
        /// </summary>
        public InstrumentoFinancieroViewModel()
        {
            this.Key = string.Empty;
        }

        /// <summary>
        /// Gets or sets the nombre.
        /// </summary>
        /// <value>
        /// The nombre of type <see cref="System.String"/>
        /// </value>
        public override string Nombre{get;set;}
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
