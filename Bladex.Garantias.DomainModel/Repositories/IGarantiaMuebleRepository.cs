using System.Collections.Generic;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.DomainModel.DomainBase.Summary;
using Bladex.Garantias.Infrastructure.RepositoryFramework;

namespace Bladex.Garantias.DomainModel.Repositories
{
    public interface IGarantiaMuebleRepository : IGarantiaBaseRepository<GarantiaMueble>
    {
        List<GarantiaMuebleSummary> GetAllMuebleSQL();
    }
}
