using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using IGarantiaInmuebleRepository = Bladex.Garantias.DomainModel.Repositories.IGarantiaInmuebleRepository;

namespace Bladex.Garantias.DomainModel.Services
{
    public class GarantiaInmuebleService : GarantiaCommonService<IGarantiaInmuebleRepository, GarantiaInmueble>
    {
        
    }
}
