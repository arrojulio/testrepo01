using System.Collections.Generic;
using Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker;

namespace Bladex.Garantias.Presentation.Website.ViewModels
{
    /// <summary>
    /// The maker checker detail view model class.
    /// </summary>
    public class MakerCheckerDetailViewModel
    {
        /// <summary>
        /// Gets or sets the changeset.
        /// </summary>
        /// <value>
        /// The changeset of type <see cref="Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker.MakerCheckerChangeset"/>
        /// </value>
        public MakerCheckerChangeset Changeset { get; set; }
        /// <summary>
        /// Gets or sets the operation.
        /// </summary>
        /// <value>
        /// The operation of type <see cref="Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker.MakerCheckerOperation"/>
        /// </value>
        public MakerCheckerOperation Operation { get; set; }
        /// <summary>
        /// Gets or sets the page.
        /// </summary>
        /// <value>
        /// The page of type <see cref="System.Int32"/>
        /// </value>
        public int Page { get; set; }
        /// <summary>
        /// Gets or sets the total pages.
        /// </summary>
        /// <value>
        /// The total pages of type <see cref="System.Int32"/>
        /// </value>
        public int TotalPages { get; set; }
        /// <summary>
        /// Gets or sets the original.
        /// </summary>
        /// <value>
        /// The original of type <see cref="System.Collections.Generic.Dictionary&lt;System.String,System.Object&gt;"/>
        /// </value>
        public IDictionary<string, object> Original { get; set; }
        /// <summary>
        /// Gets or sets the proposed.
        /// </summary>
        /// <value>
        /// The proposed of type <see cref="System.Collections.Generic.Dictionary&lt;System.String,System.Object&gt;"/>
        /// </value>
        public IDictionary<string, object> Proposed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [read only].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [read only]; otherwise, <c>false</c>.
        /// </value>
        public bool ReadOnly { get; set; }

    }
}
