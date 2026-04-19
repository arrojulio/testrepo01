using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Infrastructure.RepositoryFramework;

namespace Bladex.Garantias.DomainModel.Repositories
{
    public interface ILimitInformationRepository : IRepository<LimitInformation>
    {
        /// <summary>
        /// Retorna la informacion del limite definido por <see cref="definitionId"/> para el cliente <see cref="customerId"/>
        /// </summary>
        /// <param name="definitionId">ID de la definicion del limite</param>
        /// <param name="customerId">ID del cliente</param>
        /// <returns>Retorna la informacion del limite definido por <see cref="definitionId"/> para el cliente <see cref="customerId"/>. <see cref="LimitInformation"/></returns>
        LimitInformation FindBy(int definitionId, string customerId);
    }
}
