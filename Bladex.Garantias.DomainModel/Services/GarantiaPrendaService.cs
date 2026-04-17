using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using IGarantiaPrendaRepository = Bladex.Garantias.DomainModel.Repositories.IGarantiaPrendaRepository;

namespace Bladex.Garantias.DomainModel.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class GarantiaPrendaService : GarantiaCommonService<IGarantiaPrendaRepository, GarantiaPrenda>
    {
        
    }
}
