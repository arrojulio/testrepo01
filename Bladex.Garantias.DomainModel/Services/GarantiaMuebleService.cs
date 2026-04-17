using System.Collections.Generic;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.DomainModel.DomainBase.Summary;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using IGarantiaMuebleRepository = Bladex.Garantias.DomainModel.Repositories.IGarantiaMuebleRepository;

namespace Bladex.Garantias.DomainModel.Services
{
    public class GarantiaMuebleService : GarantiaCommonService<IGarantiaMuebleRepository, GarantiaMueble>
    {
        public List<GarantiaMuebleSummary> GetAllMuebleSQL()
        {
            return RepositoryFactory.GetRepository<IGarantiaMuebleRepository, GarantiaMueble>().GetAllMuebleSQL();
        }
    }
}
