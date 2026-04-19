using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Infrastructure.DomainBase;

namespace Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker
{
    /// <summary>
    /// The maker checker changeset summary class.
    /// </summary>
    public class MakerCheckerChangesetSummary : EntityBase
    {
        /// <summary>
        /// Gets or sets the changeset id.
        /// </summary>
        /// <value>
        /// The changeset id of type <see cref="System.Guid"/>
        /// </value>
        public Guid ChangesetId { get; set; }
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
        /// Gets or sets the total operations.
        /// </summary>
        /// <value>
        /// The total operations of type <see cref="System.Int32"/>
        /// </value>
        public int TotalOperations { get; set; }
        /// <summary>
        /// Gets or sets the new operations.
        /// </summary>
        /// <value>
        /// The new operations of type <see cref="System.Int32"/>
        /// </value>
        public int NewOperations { get; set; }
        /// <summary>
        /// Gets or sets the approved operations.
        /// </summary>
        /// <value>
        /// The approved operations of type <see cref="System.Int32"/>
        /// </value>
        public int ApprovedOperations { get; set; }
        /// <summary>
        /// Gets or sets the rejected operations.
        /// </summary>
        /// <value>
        /// The rejected operations of type <see cref="System.Int32"/>
        /// </value>
        public int RejectedOperations { get; set; }
        /// <summary>
        /// Gets or sets the changeset status.
        /// </summary>
        /// <value>
        /// The changeset status of type <see cref="Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker.MakerCheckerChangesetSummary.ChangesetStatusEnum"/>
        /// </value>
        public ChangesetStatusEnum ChangesetStatus { get; set; }

        /// <summary>
        /// Gets or sets the changeset comment.
        /// </summary>
        /// <value>
        /// The changeset comment of type <see cref="System.String"/>
        /// </value>
        public string ChangesetComment { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MakerCheckerChangesetSummary"/> class.
        /// </summary>
        public MakerCheckerChangesetSummary()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MakerCheckerChangesetSummary"/> class. This constructor calculates the total New, Approved and Rejected operations
        /// </summary>
        /// <param name="changeset">The changeset of type <see cref="Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker.MakerCheckerChangeset"/></param>
        /// <param name="operations">The operations of type <see cref="System.Collections.Generic.IEnumerable&lt;Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker.MakerCheckerOperation&gt;"/></param>
        public MakerCheckerChangesetSummary(MakerCheckerChangeset changeset, IEnumerable<MakerCheckerOperation> operations)
        {
            this.ChangesetId = changeset.ChangesetId;
            this.MakerUserId = changeset.MakerUserId;
            this.MakerUser = changeset.MakerUser;
            this.ChangesetDate = changeset.ChangesetDate;
            this.ChangesetCommitDate = changeset.ChangesetCommitDate;
            this.TotalOperations = operations.Count();
            this.NewOperations = operations.Where(o => o.OperationStatusId == (int)MakerCheckerOperationStatus.OperationStatus.New).Count();
            this.ApprovedOperations = operations.Where(o => o.OperationStatusId == (int)MakerCheckerOperationStatus.OperationStatus.Approved).Count();
            this.RejectedOperations = operations.Where(o => o.OperationStatusId == (int)MakerCheckerOperationStatus.OperationStatus.Rejected).Count();
            this.ChangesetStatus = this.NewOperations > 0 ? ChangesetStatusEnum.Pending : ChangesetStatusEnum.Revised;
            this.ChangesetComment = changeset.ChangesetComment;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="MakerCheckerChangesetSummary"/> class without any calculation
        /// </summary>
        /// <param name="ChangesetId"></param>
        /// <param name="MakerUserId"></param>
        /// <param name="MakerUser"></param>
        /// <param name="ChangesetDate"></param>
        /// <param name="ChangesetCommitDate"></param>
        /// <param name="TotalOperations"></param>
        /// <param name="NewOperations"></param>
        /// <param name="ApprovedOperations"></param>
        /// <param name="RejectedOperations"></param>
        /// <param name="ChangesetStatus"></param>
        /// <param name="ChangesetComment"></param>
        public MakerCheckerChangesetSummary(
            Guid ChangesetId,
            string MakerUserId,
            MakerCheckerUser MakerUser,
            DateTime ChangesetDate,
            DateTime? ChangesetCommitDate,
            int TotalOperations,
            int NewOperations,
            int ApprovedOperations,
            int RejectedOperations,
            ChangesetStatusEnum ChangesetStatus,
            string ChangesetComment)
        {
            this.ChangesetId = ChangesetId;
            this.MakerUserId = MakerUserId;
            this.MakerUser = MakerUser;
            this.ChangesetDate = ChangesetDate;
            this.ChangesetCommitDate = ChangesetCommitDate;
            this.TotalOperations = TotalOperations;
            this.NewOperations = NewOperations;
            this.ApprovedOperations = ApprovedOperations;
            this.RejectedOperations = RejectedOperations;
            this.ChangesetStatus = ChangesetStatus;
            this.ChangesetComment = ChangesetComment;

        }

        /// <summary>
        /// Represents the status of the changeset. Pending or Revised.
        /// </summary>
        public enum ChangesetStatusEnum
        { 
            Pending = 1, Revised = 2
        }

        public int? GarantiaId
        {
            get; set;
        }
        public string CustomerName
        {
            get; set;
        }

        public string Garante { get; set; }

        public string TipoGarantia { get; set; }
        public decimal? ValorGarantia {  get; set; }


    }
}
