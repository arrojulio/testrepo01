using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Bladex.Garantias.Infrastructure.DomainBase;

namespace Bladex.Garantias.DomainModel.DomainBase.Components.Autocomplete
{
    /// <summary>
    /// The autocomplete value class.
    /// </summary>
    public class AutocompleteValue : EntityBase
    {
        /// <summary>
        /// Gets or sets the row id.
        /// </summary>
        /// <value>
        /// The row id of type <see cref="System.Int32"/>
        /// </value>
        public int RowId { get { return this.GetKeyAs<int>(); } set { this.Key = value; } }
        /// <summary>
        /// Gets or sets the HTML control id.
        /// </summary>
        /// <value>
        /// The HTML control id of type <see cref="System.String"/>
        /// </value>
        public string HtmlControlId { get; set; }
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value of type <see cref="System.String"/>
        /// </value>
        [Required]
        [StringLength(50, ErrorMessage = "No se debe exceder los 50 caracteres.")]
        public string Value { get; set; }

    }
}
