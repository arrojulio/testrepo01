using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Infrastructure.DomainBase;

namespace Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker
{
    /// <summary>
    /// The maker checker operation status class.
    /// </summary>
    public class MakerCheckerOperationStatus : EntityBase
    {
        /// <summary>
        /// Gets or sets the operation status id.
        /// </summary>
        /// <value>
        /// The operation status id of type <see cref="System.Int32"/>
        /// </value>
        public int OperationStatusId
        {
            get { return this.GetKeyAs<int>(); }
            set { this.Key = value; }
        }
        /// <summary>
        /// Gets or sets the operation status description.
        /// </summary>
        /// <value>
        /// The operation status description of type <see cref="System.String"/>
        /// </value>
        public string OperationStatusDescription { get; set; }

        /// <summary>
        /// The operation status enum.
        /// </summary>
        public enum OperationStatus
        { 
            New = 1, Approved = 2, Rejected = 3
        }

    }
}
