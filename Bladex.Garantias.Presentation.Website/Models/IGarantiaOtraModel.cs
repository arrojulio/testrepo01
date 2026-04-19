using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Bladex.Garantias.Presentation.Website.ViewModels;
using Bladex.Garantias.DomainModel.DomainBase;

namespace Bladex.Garantias.Presentation.Website.Models
{
    /// <summary>
    /// IGarantiaOtraModel interface.
    /// </summary>
    public interface IGarantiaOtraModel
    {
        /// <summary>
        /// Gets or sets the emisor.
        /// </summary>
        /// <value>
        /// The emisor of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.ActorViewModel"/>
        /// </value>
        ActorViewModel Emisor { get; set; }
        /// <summary>
        /// Gets or sets the aval list.
        /// </summary>
        List<AvalViewModel> AvalList { get; set; }
        
        AvalManagerViewModel AvalComponent { get; set; }

        string NroReferencia { get; set; }
                
    }
}
