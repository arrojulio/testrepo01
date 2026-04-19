using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bladex.Garantias.Presentation.Website.Models
{
    /// <summary>
    /// IGarantiaContratoModel interface.
    /// </summary>
    public interface IGarantiaContratoModel
    {
        int ID
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the deal reference.
        /// </summary>
        /// <value>
        /// The deal reference of type <see cref="System.String"/>
        /// </value>
        string DealReference
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the fecha registro inicial.
        /// </summary>
        /// <value>
        /// The fecha registro inicial of type <see cref="DateTime"/>
        /// </value>
        DateTime? FechaRegistroInicial
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the fecha vencimiento garantia.
        /// </summary>
        /// <value>
        /// The fecha vencimiento garantia of type <see cref="DateTime"/>
        /// </value>
        DateTime? FechaVencimientoGarantia
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the fecha vencimiento riesgo.
        /// </summary>
        /// <value>
        /// The fecha vencimiento riesgo of type <see cref="DateTime"/>
        /// </value>
        DateTime? FechaVencimientoRiesgo
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the porc utilization.
        /// </summary>
        /// <value>
        /// The porc utilization of type <see cref="System.Decimal"/>
        /// </value>
        decimal PorcUtilization
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the garantia id.
        /// </summary>
        /// <value>
        /// The garantia id of type <see cref="int"/>
        /// </value>
        int? GarantiaId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the net balance principal.
        /// </summary>
        /// <value>
        /// The net balance principal of type <see cref="decimal"/>
        /// </value>
        decimal? NetBalancePrincipal
        {
            get;
            set;
        }
    }
}
