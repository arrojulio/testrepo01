using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using Bladex.Garantias.DomainModel.DomainBase;

namespace Bladex.Garantias.DomainModel.Repositories
{
    /// <summary>
    /// IGarantiaContratoDealReferenceRepository interface.
    /// </summary>
    public interface IGarantiaContratoDealReferenceRepository : IRepository<GarantiaContratoDealReference>
    {
        /// <summary>
        /// Gets the garantia contrato deal reference by customer.
        /// </summary>
        /// <param name="customerId">The customer id of type <see cref="System.String"/></param>
        /// <param name="globalLineDescription">The global line description of type <see cref="System.String"/></param>
        /// <returns></returns>
        IEnumerable<Bladex.Garantias.DomainModel.DomainBase.GarantiaContratoDealReference> GetGarantiaContratoDealReferenceByCustomerOrEconomicGroup(string customerId, string globalLineDescription);
    }
}
