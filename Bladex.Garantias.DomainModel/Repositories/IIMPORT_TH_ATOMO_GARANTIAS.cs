using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using Bladex.Garantias.DomainModel.DomainBase;

namespace Bladex.Garantias.DomainModel.Repositories
{
    public interface IIMPORT_TH_ATOMO_GARANTIASRepository : IRepository<IMPORT_TH_ATOMO_GARANTIAS>
    {
        IList<IMPORT_TH_ATOMO_GARANTIAS> GetByFechaCorte(DateTime fechaCorte);
        DateTime GetLastFechaCorte();

    }
}

