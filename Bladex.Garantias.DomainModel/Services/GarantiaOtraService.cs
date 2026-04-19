using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using IGarantiaOtraRepository = Bladex.Garantias.DomainModel.Repositories.IGarantiaOtraRepository;
using Bladex.Garantias.DomainModel.DomainBase.Summary;

namespace Bladex.Garantias.DomainModel.Services
{
    public class GarantiaOtraService : GarantiaCommonService<IGarantiaOtraRepository, GarantiaOtra>
    {
        public List<GarantiaOtraSummary> GetAllByFianzasAvalesNoBancarios()
        {
            /*
            IList<GarantiaOtra> result = base.GetAll(null);
            List<GarantiaOtra> wErrors = result.Where(o => o.TipoGarantiaSuper.Key == null).ToList();            
            return result.Where(o => o.TipoGarantiaSuper.GetKeyAs<string>() == fianzasAvalesNoBancarios.GetKeyAs<string>()).ToList();
            */

            //#Ticket1320 : Reconexion sql
            TipoGarantiaSuperService svc = new TipoGarantiaSuperService();
            TipoGarantiaSuper fianzasAvalesNoBancarios = svc.GetFianzasYAvalesNoBancarios();
            return this.GarantiaOtraRepository.GetAllByFianzasAvalesNoBancariosSQL(fianzasAvalesNoBancarios.Key.ToString());
            
        }

        public string GetNroReferencia(int garantiaId) {
            return this.GarantiaOtraRepository.GetNroReferencia(garantiaId);
        }

        public string GetIdentificacionDocumentoGarantia(int garantiaId)
        {
            return this.GarantiaOtraRepository.GetIdentificacionDocumentoGarantia(garantiaId);
        }

        public void UpdateIdentificacionDocumentoGarantia(int garantiaId, string valor)
        {
            this.GarantiaOtraRepository.UpdateIdentificacionDocumentoGarantia(garantiaId, valor);
        }

        public List<GarantiaOtraSummary> GetAllAndNotFianzasAvalesNoBancarios()
        {
            /*
            IList<GarantiaOtra> result = base.GetAll(null);
            return result.Where(o => o.TipoGarantiaSuper.GetKeyAs<string>() != fianzasAvalesNoBancarios.GetKeyAs<string>()).ToList();
            */
            //#Ticket1320 : Reconexion sql
            TipoGarantiaSuperService svc = new TipoGarantiaSuperService();
            TipoGarantiaSuper fianzasAvalesNoBancarios = svc.GetFianzasYAvalesNoBancarios();
            return this.GarantiaOtraRepository.GetAllAndNotFianzasAvalesNoBancariosSQL(fianzasAvalesNoBancarios.Key.ToString());
          
        }

        public List<DomainModel.DomainBase.Summary.GarantiaOtraSummary> GetAllGarantiasOtrasSQL()
        {
            return this.GarantiaOtraRepository.GetAllGarantiasOtrasSQL();

        }
    }
}
