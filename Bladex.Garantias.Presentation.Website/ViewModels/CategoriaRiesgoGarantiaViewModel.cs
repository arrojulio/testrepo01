using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Application.Facades;

namespace Bladex.Garantias.Presentation.Website.ViewModels
{
    /// <summary>
    /// The categoria riesgo garantia view model class.
    /// </summary>
    public class CategoriaRiesgoGarantiaViewModel : BaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoriaRiesgoGarantiaViewModel"/> class.
        /// </summary>
        public CategoriaRiesgoGarantiaViewModel()
        {
            this.Key = string.Empty;
        }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key of type <see cref="System.String"/>
        /// </value>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the nombre.
        /// </summary>
        /// <value>
        /// The nombre of type <see cref="System.String"/>
        /// </value>
        public override string Nombre
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the grupo key.
        /// </summary>
        /// <value>
        /// The grupo key of type <see cref="System.String"/>
        /// </value>
        public string GrupoKey
        {
            get;
            set;
        }
        public IEnumerable<System.Web.Mvc.SelectListItem> List { get; set; }
    }
}
