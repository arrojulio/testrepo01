using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.Components.MakerChecker;
using Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker;
using Bladex.Garantias.Infrastructure.RepositoryFramework;

namespace Bladex.Garantias.DomainModel.Repositories.Components.MakerChecker
{
    /// <summary>
    /// IMakerCheckerChangesetRepository interface.
    /// </summary>
    public interface IMakerCheckerChangesetRepository : IRepository<MakerCheckerChangeset>
    {
        /// <summary>
        /// Gets the changeset summary.
        /// </summary>
        /// <returns></returns>
        List<MakerCheckerChangesetSummary> GetChangesetSummary();
        List<MakerCheckerChangesetSummary> GetChangesetSummary(MakerCheckerUser user);                
        MakerCheckerChangesetSummary GetChangesetSummary(Guid changesetId);
        MakerCheckerChangeset GetAvailableChangeset(string makerUserId);
        int GetCountChangesetByUserAndId(string MakerUserId, Guid ChangesetId);
    }
}
