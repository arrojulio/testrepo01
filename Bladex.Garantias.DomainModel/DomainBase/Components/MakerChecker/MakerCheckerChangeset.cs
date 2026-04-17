using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Infrastructure.DomainBase;

namespace Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker
{
    /// <summary>
    /// The maker checker changeset class.
    /// </summary>
    public class MakerCheckerChangeset : EntityBase
    {
        /// <summary>
        /// Gets or sets the changeset id.
        /// </summary>
        /// <value>
        /// The changeset id of type <see cref="System.Guid"/>
        /// </value>
        public Guid ChangesetId
        {
            get { return this.GetKeyAs<Guid>(); }
            set { this.Key = value; }
        }
        /// <summary>
        /// Gets or sets the maker user id.
        /// </summary>
        /// <value>
        /// The maker user id of type <see cref="System.String"/>
        /// </value>
        public string MakerUserId { get; set; }
        /// <summary>
        /// Gets or sets the maker user.
        /// </summary>
        /// <value>
        /// The maker user of type <see cref="Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker.MakerCheckerUser"/>
        /// </value>
        public MakerCheckerUser MakerUser { get; set; }
        /// <summary>
        /// Gets or sets the changeset date.
        /// </summary>
        /// <value>
        /// The changeset date of type <see cref="System.DateTime"/>
        /// </value>
        public DateTime ChangesetDate { get; set; }
        /// <summary>
        /// Gets or sets the changeset commit date.
        /// </summary>
        /// <value>
        /// The changeset commit date of type <see cref="DateTime"/>
        /// </value>
        public DateTime? ChangesetCommitDate { get; set; }

        /// <summary>
        /// Gets or sets the changeset comment.
        /// </summary>
        /// <value>
        /// The changeset comment of type <see cref="System.String"/>
        /// </value>
        public string ChangesetComment { get; set; }
    }
}
