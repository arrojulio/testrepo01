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
    /// IMakerCheckerOperationRepository interface.
    /// </summary>
    public interface IMakerCheckerOperationRepository : IRepository<MakerCheckerOperation>
    {
        /// <summary>
        /// Gets the operations by changeset.
        /// </summary>
        /// <param name="changeset">The changeset of type <see cref="Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker.MakerCheckerChangeset"/></param>
        /// <returns></returns>
        List<MakerCheckerOperation> GetOperationsByChangeset(MakerCheckerChangeset changeset);

        /// <summary>
        /// Gets the operations by changeset.
        /// </summary>
        /// <param name="changeset">The changeset of type <see cref="System.Guid"/></param>
        /// <returns></returns>
        List<MakerCheckerOperation> GetOperationsByChangeset(Guid changeset);

        List<MakerCheckerOperation> GetOperationsByMaker(string makerUserId);

        List<MakerCheckerOperation> GetOperationsByChecker(string checkerUserId);

        List<MakerCheckerOperation> GetOperationsByItemId(int itemId);

        List<MakerCheckerOperation> GetSQLOperationsByFilter(string value, string fieldName);

        MakerCheckerOperation GetSqlOperationNotApprovedById(int OperationId);

        /// <summary>
        /// Devuelve un listado de operaciones pendiente de revision.
        /// </summary>
        /// <returns></returns>
        List<MakerCheckerOperationSummary> GetPendingSummaryOperations();

        List<MakerCheckerOperationSummary> GetPendingSummaryOperationsByUser(string userId);


    }
}
