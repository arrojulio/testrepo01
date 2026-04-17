using System;
using Bladex.Garantias.Infrastructure.DomainBase;

namespace Bladex.Garantias.DomainModel.DomainBase
{

    /// <summary>
    /// Representa un contrato asociado a una especializacion de <see cref="GarantiaBase"/>
    /// </summary>
    public class GarantiaContrato : EntityBase
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>
        /// The ID.
        /// </value>
        public virtual int ID
        {
            get
            {
                return this.GetKeyAs<int>();
            }
            set
            {
                if (value == 0)
                {
                    this.Key = null;
                }
                else
                {
                    this.Key = value;
                }
            }
        }
        /// <summary>
        /// Gets or sets the deal reference.
        /// </summary>
        /// <value>
        /// The deal reference.
        /// </value>
        public virtual string DealReference 
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the fecha registro inicial.
        /// </summary>
        /// <value>
        /// The fecha registro inicial.
        /// </value>
        public virtual DateTime? FechaRegistroInicial
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the fecha vencimiento garantia.
        /// </summary>
        /// <value>
        /// The fecha vencimiento garantia.
        /// </value>
        public virtual DateTime? FechaVencimientoGarantia
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the fecha vencimiento riesgo.
        /// </summary>
        /// <value>
        /// The fecha vencimiento riesgo.
        /// </value>
        public virtual DateTime? FechaVencimientoRiesgo
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the porc utilization.
        /// </summary>
        /// <value>
        /// The porc utilization.
        /// </value>
        public virtual decimal? PorcUtilization
        {
            get;
            set;
        }
        
        //public bool IsPorcUtilizationImported { get; set; }


        /// <summary>
        /// Gets or sets the garantia id.
        /// </summary>
        /// <value>
        /// The garantia id.
        /// </value>
        public virtual int? GarantiaId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the net balance principal.
        /// </summary>
        /// <value>
        /// The net balance principal.
        /// </value>
        public virtual decimal? NetBalancePrincipal
        {
            get;
            set;
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}", this.DealReference, this.NetBalancePrincipal.HasValue ? this.NetBalancePrincipal.Value.ToString("c") : new decimal(0).ToString("c"));
        }

    }
}
