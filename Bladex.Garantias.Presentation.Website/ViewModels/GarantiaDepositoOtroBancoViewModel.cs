using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Bladex.Garantias.Application.Facades;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Presentation.Website.Models;

namespace Bladex.Garantias.Presentation.Website.ViewModels
{
    /// <summary>
    /// The garantia deposito otro banco view model class.
    /// </summary>
    [DisplayName("Depósito Pignorado en otro Banco")]
    public class GarantiaDepositoOtroBancoViewModel : GarantiaBaseViewModel
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GarantiaDepositoOtroBancoViewModel"/> class.
        /// </summary>
        public GarantiaDepositoOtroBancoViewModel()
        {
            this.Garantia = new GarantiaDepositoOtroBancoModel();
        }


        /// <summary>
        /// Gets or sets the garantia.
        /// </summary>
        /// <value>
        /// The garantia of type <see cref="Bladex.Garantias.Presentation.Website.Models.GarantiaDepositoOtroBancoModel"/>
        /// </value>
        [DisplayName("Depósito Pignorado otro Banco")]
        public GarantiaDepositoOtroBancoModel Garantia { get; set; }
    }
}
