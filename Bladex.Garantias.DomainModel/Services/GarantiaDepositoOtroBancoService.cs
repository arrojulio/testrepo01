using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using IGarantiaDepositoOtroBancoRepositoy = Bladex.Garantias.DomainModel.Repositories.IGarantiaDepositoOtroBancoRepository;

namespace Bladex.Garantias.DomainModel.Services
{
    public class GarantiaDepositoOtroBancoService : GarantiaCommonService<IGarantiaDepositoOtroBancoRepositoy, GarantiaDepositoOtroBanco>
    {
        
    }
}
