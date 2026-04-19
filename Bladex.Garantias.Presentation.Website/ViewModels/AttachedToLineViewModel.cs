using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bladex.Garantias.Presentation.Website.ViewModels
{

    /// <summary>
    /// The attached to line view model class.
    /// </summary>
    public class AttachedToLineViewModel : BaseViewModel
    {
        /// <summary>
        /// Gets or sets the attached to line.
        /// </summary>
        /// <value>
        /// The attached to line of type <see cref="bool"/>
        /// </value>
        public bool? AttachedToLine { get; set; }
        /// <summary>
        /// Gets or sets the customer id.
        /// </summary>
        /// <value>
        /// The customer id of type <see cref="System.String"/>
        /// </value>
        public string CustomerId { get; set; }
        /// <summary>
        /// Gets or sets the customer control id.
        /// </summary>
        /// <value>
        /// The customer control id of type <see cref="System.String"/>
        /// </value>
        public string CustomerControlId { get; set; }

        public override string Nombre
        {
            get
            {
                return this.AttachedToLine.GetValueOrDefault().ToString();
            }
        }
    }
}