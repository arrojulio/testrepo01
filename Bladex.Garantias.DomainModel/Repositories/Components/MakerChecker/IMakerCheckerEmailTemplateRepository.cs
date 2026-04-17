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
    public interface IMakerCheckerEmailTemplateRepository : IRepository<MakerCheckerEmailTemplate>
    {
        /// <summary>
        /// Gets the email template.
        /// </summary>
        /// <param name="Role">The role of type <see cref="MakerCheckerRole"/></param>
        /// <returns></returns>
        MakerCheckerEmailTemplate GetEmailTemplateByRole(MakerCheckerRole Role);

        /// <summary>
        /// Gets the email template.
        /// </summary>
        /// <param name="RoleId">The role id of type <see cref="System.Int32"/></param>
        /// <returns></returns>
        MakerCheckerEmailTemplate GetEmailTemplateByRole(int RoleId);
    }
}
