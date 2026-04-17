using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using IGarantiaMuebleRepository = Bladex.Garantias.DomainModel.Repositories.IGarantiaMuebleRepository;

namespace Bladex.Garantias.DomainModel.Services
{
    public class GarantiaMuebleService : GarantiaCommonService<IGarantiaMuebleRepository, GarantiaMueble>
    {
        
    }
}
