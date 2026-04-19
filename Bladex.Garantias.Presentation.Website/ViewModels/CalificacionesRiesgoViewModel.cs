using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Application.Facades;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Infrastructure.DomainBase;

namespace Bladex.Garantias.Presentation.Website.ViewModels
{
    /// <summary>
    /// The calificaciones riesgo view model class.
    /// </summary>
    public class CalificacionesRiesgoViewModel : BaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CalificacionesRiesgoViewModel"/> class.
        /// </summary>
        public CalificacionesRiesgoViewModel()
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
        /// Gets or sets the orden.
        /// </summary>
        /// <value>
        /// The orden of type <see cref="System.Int32"/>
        /// </value>
        public int Orden
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the moodys.
        /// </summary>
        /// <value>
        /// The moodys of type <see cref="System.String"/>
        /// </value>
        public string Moodys
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the fitch.
        /// </summary>
        /// <value>
        /// The fitch of type <see cref="System.String"/>
        /// </value>
        public string Fitch
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the sn P.
        /// </summary>
        /// <value>
        /// The sn P of type <see cref="System.String"/>
        /// </value>
        public string SnP
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the nombre.
        /// </summary>
        public override string Nombre
        {
            get { return this.ToString(); }
        }
        public IEnumerable<System.Web.Mvc.SelectListItem> List { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0} - Moodys: {1}, Fitch: {2}, Snp: {3}", Orden, Moodys, Fitch, SnP);
        }

    }
}
