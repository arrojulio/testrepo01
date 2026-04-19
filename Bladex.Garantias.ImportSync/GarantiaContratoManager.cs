using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Application.Facades;
using Bladex.Garantias.DomainModel.DomainBase;

namespace Bladex.Garantias.ImportSync
{
    /// <summary>
    /// Maneja las operaciones sobre <see cref="GarantiaContrato"/>
    /// </summary>
    public class GarantiaContratoManager : IGarantiaContratoManager
    {
        /// <summary>
        ///   Identificador para DealReferences vacios.
        /// </summary>
        public const string _DEAL_REFERENCE_EMPTY_IDENTIFIER = "NA";
        #region IGarantiaContratoManager Members

        /// <summary>
        /// Does the update.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="garantiaId">The garantia id.</param>
        public void DoUpdate(DomainModel.DomainBase.IMPORT_TH_ATOMO_GARANTIAS source, int garantiaId)
        {
            GarantiaContrato garantiaContrato = ServiceFacade.Instance.GarantiaContratoService.GetByGarantiaIdDealReference(garantiaId, source.numero_prestamo);
            if (garantiaContrato == null)
            {
                ServiceFacade.Instance.SyncLogService.AddSyncLog(new SyncLog(SyncLog.Status_SUCCESS, string.Format("No se encontro GarantiaContrato (GarantiaId: {0} Deal Reference: {1}) en el proceso de actualizacion. Generando GarantiaContrato",garantiaId, source.numero_prestamo)));
                this.DoInsert(source, garantiaId);
            }
            else
            {
                
                //NO DEBE ACTUALIZARCE EL DEAL REFERENCE YA QUE ES CLAVE SECUNDARIA
                //garantiaContrato.DealReference = source.numero_prestamo.Trim();
                garantiaContrato.NetBalancePrincipal = source.VALOR_GARANTIA;
                garantiaContrato.FechaRegistroInicial = source.DEAL_STAT_FROM_DT;
                garantiaContrato.FechaVencimientoRiesgo = source.DEAL_CLOSURE_DATE;
                DateTime fechaVencimiento;
                if(DateTime.TryParse(source.FECHA_VENCIMIENTO, out fechaVencimiento))
                    garantiaContrato.FechaVencimientoGarantia = fechaVencimiento;
                
                ServiceFacade.Instance.GarantiaContratoService.Save(garantiaContrato);
            }

        }

        /// <summary>
        /// Does the insert.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="garantiaId">The garantia id.</param>
        public void DoInsert(DomainModel.DomainBase.IMPORT_TH_ATOMO_GARANTIAS source, int garantiaId)
        {
            // Valido que el contrato no sea NA.
            if (source.numero_prestamo.Trim() == _DEAL_REFERENCE_EMPTY_IDENTIFIER) return;

            GarantiaContrato garantiaContrato = ServiceFacade.Instance.GarantiaContratoService.GetByGarantiaIdDealReference(garantiaId, source.numero_prestamo) ?? new GarantiaContrato();

            garantiaContrato.GarantiaId = garantiaId;
            garantiaContrato.NetBalancePrincipal = source.VALOR_GARANTIA;
            garantiaContrato.DealReference = source.numero_prestamo;
            garantiaContrato.DealReference = garantiaContrato.DealReference.Trim();
            garantiaContrato.FechaRegistroInicial = source.DEAL_STAT_FROM_DT;
            garantiaContrato.FechaVencimientoRiesgo = source.DEAL_CLOSURE_DATE;
            DateTime fechaVencimiento;
            if (DateTime.TryParse(source.FECHA_VENCIMIENTO, out fechaVencimiento))
                garantiaContrato.FechaVencimientoGarantia = fechaVencimiento;
            garantiaContrato.PorcUtilization = new decimal(100);
            ServiceFacade.Instance.GarantiaContratoService.Save(garantiaContrato);
        }

        /// <summary>
        /// Does the delete.
        /// </summary>
        /// <param name="dealRef">The deal ref.</param>
        /// <param name="garantiaId">The garantia id.</param>
        public void DoDelete(string dealRef, int garantiaId)
        {
            var garantiaContrato = ServiceFacade.Instance.GarantiaContratoService.GetByGarantiaIdDealReference(garantiaId, dealRef);
            if (garantiaContrato != null)
            {
                ServiceFacade.Instance.GarantiaContratoService.Delete(garantiaContrato,null,null);
            }
        }

        #endregion
    }
}
