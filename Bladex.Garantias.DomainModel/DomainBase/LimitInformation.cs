using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Infrastructure.DomainBase;

namespace Bladex.Garantias.DomainModel.DomainBase
{
    /// <summary>
    /// Representa un limite de <see cref="Cliente"/> proveniente del modulo de CPER.
    /// Se popula mediante un servicio web ofrecido por CPER.
    /// </summary>
    public class LimitInformation : EntityBase
    {
        /// <summary>
        /// Gets or sets the definition id.
        /// </summary>
        /// <value>
        /// The definition id.
        /// </value>
        public int DefinitionId { get; set; }
        /// <summary>
        /// Gets or sets the customer id.
        /// </summary>
        /// <value>
        /// The customer id.
        /// </value>
        public string CustomerId { get; set; }
        /// <summary>
        /// Gets or sets the matrix id.
        /// </summary>
        /// <value>
        /// The matrix id.
        /// </value>
        public int MatrixId { get; set; }
        /// <summary>
        /// Gets or sets the name of the matrix.
        /// </summary>
        /// <value>
        /// The name of the matrix.
        /// </value>
        public string MatrixName { get; set; }
        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>
        /// The comments.
        /// </value>
        public string Comments { get; set; }
        /// <summary>
        /// Gets or sets the limit value.
        /// </summary>
        /// <value>
        /// The limit value.
        /// </value>
        public decimal LimitValue { get; set; }
        /// <summary>
        /// Gets or sets the last limit.
        /// </summary>
        /// <value>
        /// The last limit.
        /// </value>
        public decimal LastLimit { get; set; }
        /// <summary>
        /// Gets or sets the expiration date.
        /// </summary>
        /// <value>
        /// The expiration date.
        /// </value>
        public DateTime ExpirationDate { get; set; }
        /// <summary>
        /// Gets or sets the matrix state id.
        /// </summary>
        /// <value>
        /// The matrix state id.
        /// </value>
        public int MatrixStateId { get; set; }
        /// <summary>
        /// Gets or sets the matrix state description.
        /// </summary>
        /// <value>
        /// The matrix state description.
        /// </value>
        public string MatrixStateDescription { get; set; }
        /// <summary>
        /// Gets or sets the matrix type id.
        /// </summary>
        /// <value>
        /// The matrix type id.
        /// </value>
        public int MatrixTypeId { get; set; }

        /// <summary>
        /// Gets or sets the matrix type description.
        /// </summary>
        /// <value>
        /// The matrix type description.
        /// </value>
        public string MatrixTypeDescription { get; set; }
    }
}
