using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Infrastructure.RepositoryFramework;

namespace Bladex.Garantias.DomainModel.Repositories
{
    public interface IGarantiaOtraRepository : IGarantiaBaseRepository<GarantiaOtra>
    {
        List<DomainModel.DomainBase.Summary.GarantiaOtraSummary> GetAllGarantiasOtrasSQL();
        List<DomainModel.DomainBase.Summary.GarantiaOtraSummary> GetAllByFianzasAvalesNoBancariosSQL(string fianzaAvalID);
        List<DomainModel.DomainBase.Summary.GarantiaOtraSummary> GetAllAndNotFianzasAvalesNoBancariosSQL(string fianzaAvalID);
        string GetNroReferencia(int garantiaId);
        string GetIdentificacionDocumentoGarantia(int garantiaId);
        void UpdateIdentificacionDocumentoGarantia(int garantiaId, string valor);
    }
}
