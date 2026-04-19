using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Infrastructure.RepositoryFramework;

namespace Bladex.Garantias.DomainModel.Repositories
{
    public interface IBancosRepository : IRepository<Bancos>
    {
        Bancos FindByName(string name);
    }
}
