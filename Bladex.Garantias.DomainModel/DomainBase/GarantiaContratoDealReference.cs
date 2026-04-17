using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Infrastructure.DomainBase;

namespace Bladex.Garantias.DomainModel.DomainBase
{
    /// <summary>
    /// The garantia contrato deal reference class. Used to display information about the deal reference selection into the garantia contrato view.
    /// </summary>
    public class GarantiaContratoDealReference : EntityBase
    {
        /// <summary>
        /// Gets or sets the deal reference.
        /// </summary>
        /// <value>
        /// The deal reference of type <see cref="System.String"/>
        /// </value>
        public string DealReference { get { return this.GetKeyAs<string>(); } set { this.Key = value; } }
        
        /// <summary>
        /// Gets or sets the product group.
        /// </summary>
        /// <value>
        /// The product group of type <see cref="System.String"/>
        /// </value>
        public string ProductGroup { get; set; }

        /// <summary>
        /// Gets or sets the customer id.
        /// </summary>
        /// <value>
        /// The customer id of type <see cref="System.String"/>
        /// </value>
        public Cliente Customer { get; set; }

        /// <summary>
        /// Gets or sets the net balance.
        /// </summary>
        /// <value>
        /// The net balance of type <see cref="System.Decimal"/>
        /// </value>
        public decimal NetBalance { get; set; }


        /// <summary>
        /// Gets or sets the fecha registro inicial.
        /// </summary>
        /// <value>
        /// The fecha registro inicial of type <see cref="DateTime"/>
        /// </value>
        public DateTime? FechaRegistroInicial { get; set; }

        /// <summary>
        /// Gets or sets the fecha vencimiento riesgo.
        /// </summary>
        /// <value>
        /// The fecha vencimiento riesgo of type <see cref="DateTime"/>
        /// </value>
        public DateTime? FechaVencimientoRiesgo { get; set; }

        /// <summary>
        /// Gets or sets the fecha vencimiento garantia.
        /// </summary>
        /// <value>
        /// The fecha vencimiento garantia of type <see cref="DateTime"/>
        /// </value>
        public DateTime? FechaVencimientoGarantia { get; set; }

        /// <summary>
        /// Gets or sets the grupo economico.
        /// </summary>
        /// <value>
        /// The grupo economico of type <see cref="System.String"/>
        /// </value>
        public string GrupoEconomico { get; set; }

        public override string ToString()
        {
            StringBuilder strBuilder = new StringBuilder();
            if (this.Customer == null)
            {
                this.Customer = Bladex.Garantias.DomainModel.Services.ClienteService.GetEmpty();
            }
            strBuilder.AppendFormat("{0} - Registro Inicial: {1} - Vencimiento Riesgo: {2} - Vencimiento Garantia: {3} - Net Balance: {4} - Grupo Economico: {5} - Product Group: {6} - (Cliente: Id: {7} - Nombre: {8} - Grupo Economico: {9} - Global Line: {10}", this.DealReference, this.FechaRegistroInicial, this.FechaVencimientoRiesgo, this.FechaVencimientoGarantia, this.NetBalance, this.GrupoEconomico, this.ProductGroup, this.Customer.Key, this.Customer.Nombre, this.Customer.GrupoEconomico, this.Customer.GlobalLineDescription );
            return strBuilder.ToString();
        }
    }
}
