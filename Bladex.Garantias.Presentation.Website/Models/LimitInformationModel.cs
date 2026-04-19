using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bladex.Garantias.Presentation.Website.Models
{

    /// <summary>
    /// Wrapper del objeto retornado por los web services de CPER para informacion de limite de cliente.
    /// </summary>
    public class LimitInformationModel
    {
        /// <summary>
        /// Gets or sets the definition id.
        /// </summary>
        /// <value>
        /// The definition id of type <see cref="System.Int32"/>
        /// </value>
        public int DefinitionId { get; set; }
        /// <summary>
        /// Gets or sets the customer id.
        /// </summary>
        /// <value>
        /// The customer id of type <see cref="System.String"/>
        /// </value>
        public string CustomerId { get; set; }
        /// <summary>
        /// Gets or sets the matrix id.
        /// </summary>
        /// <value>
        /// The matrix id of type <see cref="System.Int32"/>
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
        /// The comments of type <see cref="System.String"/>
        /// </value>
        public string Comments { get; set; }
        /// <summary>
        /// Gets or sets the limit value.
        /// </summary>
        /// <value>
        /// The limit value of type <see cref="System.String"/>
        /// </value>
        public string LimitValue { get; set; }
        /// <summary>
        /// Gets or sets the last limit.
        /// </summary>
        /// <value>
        /// The last limit of type <see cref="System.String"/>
        /// </value>
        public string LastLimit { get; set; }
        /// <summary>
        /// Gets or sets the expiration date.
        /// </summary>
        /// <value>
        /// The expiration date of type <see cref="System.String"/>
        /// </value>
        public string ExpirationDate { get; set; }
        /// <summary>
        /// Gets or sets the matrix state id.
        /// </summary>
        /// <value>
        /// The matrix state id of type <see cref="System.Int32"/>
        /// </value>
        public int MatrixStateId { get; set; }
        /// <summary>
        /// Gets or sets the matrix type id.
        /// </summary>
        /// <value>
        /// The matrix type id of type <see cref="System.Int32"/>
        /// </value>
        public int MatrixTypeId { get; set; }
        /// <summary>
        /// Gets or sets the matrix state description.
        /// </summary>
        /// <value>
        /// The matrix state description of type <see cref="System.String"/>
        /// </value>
        public string MatrixStateDescription {get;set;}
        /// <summary>
        /// Gets or sets the matrix type description.
        /// </summary>
        /// <value>
        /// The matrix type description of type <see cref="System.String"/>
        /// </value>
        public string MatrixTypeDescription {get;set;}
    }
}

