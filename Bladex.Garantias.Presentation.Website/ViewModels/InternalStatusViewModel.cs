using System.Collections.Generic;

namespace Bladex.Garantias.Presentation.Website.ViewModels
{
    /// <summary>
    /// The status view model class.
    /// </summary>
    public class InternalStatusViewModel : BaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InternalStatusViewModel"/> class.
        /// </summary>
        public InternalStatusViewModel()
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
