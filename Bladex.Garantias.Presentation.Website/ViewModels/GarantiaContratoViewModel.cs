using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bladex.Garantias.Presentation.Website.Models;

namespace Bladex.Garantias.Presentation.Website.ViewModels
{
    /// <summary>
    /// The garantia contrato view model class.
    /// </summary>
    public class GarantiaContratoViewModel
    {
        /// <summary>
        /// Gets or sets the garantia contrato model.
        /// </summary>
        /// <value>
        /// The garantia contrato model of type <see cref="Bladex.Garantias.Presentation.Website.Models.GarantiaContratoModel"/>
        /// </value>
        public GarantiaContratoModel GarantiaContratoModel { get; set; }
        /// <summary>
        /// Gets or sets the customer id.
        /// </summary>
        /// <value>
        /// The customer id of type <see cref="System.String"/>
        /// </value>
        public string CustomerId { get; set; }
        /// <summary>
        /// Gets or sets the name of the customer.
        /// </summary>
        /// <value>
        /// The name of the customer.
        /// </value>
        public string CustomerName { get; set; }
        /// <summary>
        /// Gets or sets the deal reference list.
        /// </summary>
        /// <value>
        /// The deal reference list of type <see cref="System.Collections.Generic.List&lt;Bladex.Garantias.Presentation.Website.ViewModels.DealReferenceSelectionViewModel&gt;"/>
        /// </value>
        public List<DealReferenceSelectionViewModel> DealReferenceList { get; set; }

        // Ticket #78343321 - Garantias - % de cobertura

        /// <summary>
        /// Relacionado cuando el % de cobertura proviene de un sistema externo
        /// </summary>
        public bool IsReadOnly { get; set; }
    }

    /// <summary>
    /// The deal reference selection view model class.
    /// </summary>
    public class DealReferenceSelectionViewModel
    {
        /// <summary>
        /// Gets or sets the deal reference.
        /// </summary>
        /// <value>
        /// The deal reference of type <see cref="System.String"/>
        /// </value>
        public string DealReference { get; set; }

        /// <summary>
        /// Gets or sets the grupo economico.
        /// </summary>
        /// <value>
        /// The grupo economico of type <see cref="System.String"/>
        /// </value>
        public string GrupoEconomico { get; set; }
        /// <summary>
        /// Gets or sets the product group.
        /// </summary>
        /// <value>
        /// The product group of type <see cref="System.String"/>
        /// </value>
        public string ProductGroup { get; set; }

        /// <summary>
        /// Gets or sets the net balance.
        /// </summary>
        /// <value>
        /// The net balance of type <see cref="System.String"/>
        /// </value>
        public decimal NetBalance { get; set; }

        public string NetBalanceFormatted { get; set; }

        /// <summary>
        /// Gets or sets the fecha registro inicial.
        /// </summary>
        /// <value>
        /// The fecha registro inicial of type <see cref="System.String"/>
        /// </value>
        public string FechaRegistroInicial { get; set; }

        /// <summary>
        /// Gets or sets the fecha vencimiento garantia.
        /// </summary>
        /// <value>
        /// The fecha vencimiento garantia of type <see cref="System.String"/>
        /// </value>
        public string FechaVencimientoGarantia { get; set; }

        /// <summary>
        /// Gets or sets the fecha vencimiento riesgo.
        /// </summary>
        /// <value>
        /// The fecha vencimiento riesgo of type <see cref="System.String"/>
        /// </value>
        public string FechaVencimientoRiesgo { get; set; }

    }
}