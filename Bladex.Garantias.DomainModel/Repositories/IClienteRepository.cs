using System.Collections.Generic;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Infrastructure.RepositoryFramework;

namespace Bladex.Garantias.DomainModel.Repositories
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        IList<Cliente> FindByName(string Name);
        IList<Cliente> FindByCodigoPais(string CodigoPais);
        IList<Cliente> FindByGrupoEconomico(string GrupoEconomico);
    }
}
