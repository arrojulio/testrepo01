using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Bladex.Garantias.Application.Facades;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Infrastructure.DomainBase;

namespace Bladex.Garantias.Presentation.Website.ViewModels
{
    /// <summary>
    /// The avaluadoras view model class.
    /// </summary>
    public class AvaluadorasViewModel : BaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AvaluadorasViewModel"/> class.
        /// </summary>
        public AvaluadorasViewModel()
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
        [StringLength(150, ErrorMessage = "No se deben exceder los 150 caracteres.")]
        public override string Nombre{get;set;}

        /// <summary>
        /// Gets or sets the list.
        /// </summary>
        
        public IEnumerable<System.Web.Mvc.SelectListItem> List { get; set; }
    }
}
