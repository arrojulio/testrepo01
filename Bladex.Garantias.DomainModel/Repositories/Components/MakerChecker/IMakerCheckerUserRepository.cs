using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.Components.MakerChecker;
using Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker;
using Bladex.Garantias.Infrastructure.RepositoryFramework;

namespace Bladex.Garantias.DomainModel.Repositories.Components.MakerChecker
{
    /// <summary>
    /// IMakerCheckerCoreRepository interface.
    /// </summary>
    public interface IMakerCheckerUserRepository : IRepository<MakerCheckerUser>
    {
    }
}
