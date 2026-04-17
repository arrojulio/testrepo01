using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using Bladex.Garantias.DomainModel.DomainBase;

namespace Bladex.Garantias.DomainModel.Repositories
{
    /// <summary>
    /// IGarantiaContratoRepository interface.
    /// </summary>
    public interface IGarantiaContratoRepository : IRepository<GarantiaContrato>
    {
        /// <summary>
        /// Gets the by garantia id deal reference.
        /// </summary>
        /// <param name="GarantiaId">The garantia id of type <see cref="System.Int32"/></param>
        /// <param name="DealReference">The deal reference of type <see cref="System.String"/></param>
        /// <returns></returns>
        GarantiaContrato GetByGarantiaIdDealReference(int GarantiaId, string DealReference);
        /// <summary>
        /// Gets the by garantia id.
        /// </summary>
        /// <param name="GarantiaId">The garantia id of type <see cref="System.Int32"/></param>
        /// <returns></returns>
        IList<GarantiaContrato> GetByGarantiaId(int GarantiaId);

        void RefreshContratosIntradiarios();

        /// <summary>
        /// Determina si la asignacion Garantia - Contrato provisto posee el valor de cobertura establecido externamente
        /// </summary>
        /// <param name="GarantiaId"></param>
        /// <param name=""></param>
        /// <returns></returns>
        bool IsPorcUtilizationImported(int GarantiaId, string DealReference);


    }
}
