using Bladex.Garantias.Infrastructure.DomainBase;
using System;

namespace Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker

{
    public class    MakerCheckerOperationSummary : EntityBase
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
        /// Gets or sets the changeset status.
        /// </summary>
        /// <value>
        /// The changeset status of type <see cref="Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker.MakerCheckerChangesetSummary.ChangesetStatusEnum"/>
        /// </value>
        //public ChangesetStatusEnum ChangesetStatus { get; set; }


        /// <summary>
        /// Changeset comment added by maker user when creating the operation.
        public string ChangesetComment { get; set; }

        /// <summary>
        /// Gets or sets the operation id.
        /// </summary>
        /// <value>
        /// The operation id of type <see cref="System.Int32"/>
        /// </value>
        public int OperationId
        {
            get { return this.GetKeyAs<int>(); }
            set { this.Key = value; }
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
        public decimal? ValorGarantia { get; set; }

        public int OperationStatus { get; set; }

        public int RelatedDeals { get; set; }

        public MakerCheckerOperationSummary(Guid changesetId, string makerUserId, MakerCheckerUser makerUser, DateTime changesetDate, DateTime? changesetCommitDate, int operationId, int? garantiaId, string customerName, string garante, string tipoGarantia, decimal? valorGarantia, int operationStatus, int relatedDeals, string changesetComment)
        {
            ChangesetId = changesetId;
            MakerUserId = makerUserId;
            MakerUser = makerUser;
            ChangesetDate = changesetDate;
            ChangesetComment = changesetComment;
            ChangesetCommitDate = changesetCommitDate;            
            OperationId = operationId;
            GarantiaId = garantiaId;
            CustomerName = customerName;
            Garante = garante;
            TipoGarantia = tipoGarantia;
            ValorGarantia = valorGarantia;
            OperationStatus = operationStatus;
            RelatedDeals = relatedDeals;

        }

        public MakerCheckerOperationSummary()
        {
        }

        public MakerCheckerOperationSummary(object key) : base(key)
        {
        }
    }
}
