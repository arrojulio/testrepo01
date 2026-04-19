using System;
using System.Collections.Generic;
using System.Linq;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Application.Facades;
using Bladex.Garantias.Infrastructure.Logging;

namespace Bladex.Garantias.ImportSync
{
    

    /// <summary>
    /// TEINSA Sincronizator.
    /// </summary>
    public class ImportSync
    {        
        /// <summary>
        /// Mantiene en memoria todos los registros de la tabla de contratos.
        /// Key: IMPORT_TH_ATOMO_GARANTIAS.ID 
        /// </summary>
        private Dictionary<int, IMPORT_TH_ATOMO_GARANTIAS> _teinsaImportThAtomoGarantiasRecords;

        /// <summary>
        /// Gets or sets the last sucess sync.
        /// </summary>
        /// <value>
        /// The last sucess sync.
        /// </value>
        private SyncLog lastSucessSync;

        /// <summary>
        /// Mantiene un diccionario de los FCCs (IDENTIFICACION GARANTIA TEINSA) codes import de Teinsa
        /// Nos sirve para buscar la existencia de FCC codes cuando estamos en los deletes
        /// Key: IMPORT_TH_ATOMO_GARANTIAS.IDENTIFICACION_GARANTIA
        /// </summary>
        private Dictionary<string, Queue<IMPORT_TH_ATOMO_GARANTIAS>> _teinsaImportThAtomoGarantiaRecordsByFcc;

        /// <summary>
        /// FCC, NovarisId   
        /// </summary>
        private IDictionary<string, int> _fccAndNovarisId;

        /// <summary>
        /// Fecha de Corte
        /// </summary>
        private DateTime? _fechaDeCorte;

        /// <summary>
        /// guaranteesUpdated
        /// </summary>
        private IDictionary<string, bool> _guarantesUpdated;

        /// <summary>
        /// guaranteesInserted
        /// </summary>
        private IDictionary<string, bool> _guarantesInserted;

        /// <summary>
        /// guaranteesDeleted
        /// </summary>
        private IDictionary<string, bool> _guarantesDeleted;

        /// <summary>
        /// Constructor standard.
        /// </summary>
        public ImportSync(): this(new GarantiaManager(), new GarantiaContratoManager())
        {            
        }

        /// <summary>
        /// Construnctor para IoC e Dependency Injection
        /// </summary>
        /// <param name="garantiaManager">The garantia manager.</param>
        /// <param name="garantiaContratoManager">The garantia contrato manager.</param>        
        public ImportSync(IGarantiaManager garantiaManager, IGarantiaContratoManager garantiaContratoManager) : this(garantiaManager, garantiaContratoManager,Bladex.Garantias.Infrastructure.Logging.ApplicationLogger.Instance)
        {            
        }        

        /// <summary>
        /// Construnctor para IoC e Dependency Injection
        /// </summary>
        /// <param name="garantiaManager">The garantia manager.</param>
        /// <param name="garantiaContratoManager">The garantia contrato manager.</param>
        /// <param name="loggingModule">The logging module.</param>        
        public ImportSync(IGarantiaManager garantiaManager, IGarantiaContratoManager garantiaContratoManager, ILogger loggingModule)
        {
            this.GarantiaManager = garantiaManager;
            this.GarantiaContratoManager = garantiaContratoManager;
            this.Logger = loggingModule;

            //this.GarantiaManager.DoDeleteCompleted += this.garantiaManager_DoDeleteCompleted;
            //this.GarantiaManager.DoInsertCompleted += this.garantiaManager_DoInsertCompleted;
            //this.GarantiaManager.DoUpdateCompleted += this.garantiaManager_DoUpdateCompleted;
        }
        
        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        public ILogger Logger { get; set; }

        #region Events Region


        /// <summary>
        /// DoInsertEventHandler
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Bladex.Garantias.ImportSync.ImportSyncEventArgs"/> instance containing the event data.</param>
        public delegate void DoInsertsEventHandler(object sender, ImportSyncEventArgs e);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Bladex.Garantias.ImportSync.ImportSyncEventArgs"/> instance containing the event data.</param>
        public delegate void DoUpdatesEventHandler(object sender, ImportSyncEventArgs e);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Bladex.Garantias.ImportSync.ImportSyncEventArgs"/> instance containing the event data.</param>
        public delegate void DoDeletesEventHandler(object sender, ImportSyncEventArgs e);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Bladex.Garantias.ImportSync.ImportSyncStartEventArgs"/> instance containing the event data.</param>
        public delegate void StartSyncEventHandler(object sender, ImportSyncStartEventArgs e);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Bladex.Garantias.ImportSync.ImportSyncEndEventArgs"/> instance containing the event data.</param>
        public delegate void EndSyncEventHandler(object sender, ImportSyncEndEventArgs e);
        
        /// <summary>
        /// Raises after the Insert process completes.
        /// </summary>
        public event DoInsertsEventHandler OnDoInsertsCompleted;
        
        /// <summary>
        /// Raises after the Update process completes.
        /// </summary>
        public event DoUpdatesEventHandler OnDoUpdatesCompleted;
        
        /// <summary>
        /// Raises after the Delete process completes.
        /// </summary>
        public event DoDeletesEventHandler OnDoDeletesCompleted;

        /// <summary>
        /// Raises before the synchronization starts.
        /// </summary>
        public event StartSyncEventHandler OnSynchronizationStarted;

        /// <summary>
        /// Raises after the synchronization ends.
        /// </summary>
        public event EndSyncEventHandler OnSynchronizationEnded;


        #endregion       
        
        /// <summary>
        /// Gets or sets the garantia manager.
        /// </summary>
        /// <value>
        /// The garantia manager.
        /// </value>
        public IGarantiaManager GarantiaManager { get; set; }
       
        /// <summary>
        /// Gets or sets the garantia contrato manager.
        /// </summary>
        /// <value>
        /// The garantia contrato manager.
        /// </value>
        public IGarantiaContratoManager GarantiaContratoManager { get; set; }
                
        /// <summary>
        /// Start Syncronization
        /// </summary>
        public void Sync()
        {
            try
            {
                this.PrepareResources();

                this.GetLastSuccSync();

                // Obtener registros Import
                this.ReadImportRecords();
                if (this._teinsaImportThAtomoGarantiasRecords == null || this._teinsaImportThAtomoGarantiasRecords.Count == 0)
                {
                    ServiceFacade.Instance.SyncLogService.AddSyncLog(new SyncLog(SyncLog.Status_SUCCESS, "No hay registros para sincronizar"));
                    return;
                }

                if (this.OnSynchronizationStarted != null)
                {
                    this.OnSynchronizationStarted.Invoke(this, new ImportSyncStartEventArgs(this._teinsaImportThAtomoGarantiasRecords.Count, this._fechaDeCorte));
                }
               
                // Aplicar Updates                
                this.DoUpdates();

                // Obtener Insert                
                this.DoInserts();

                // Obtener Deletes
                this.DoDeletes();

                // Sincronizo los contratos y borro los contratos que no han venido en TEINSA.
                // Desactivado luego de filtrar las garantias provenientes de TEINSA.
                //this.SynchronizeContracts();
                
                SyncLog log = new SyncLog(SyncLog.Status_SUCCESS, "Sincronizacion efectuada con exito.");
                log.FechaCorte = this._fechaDeCorte;
                log.TimeStamp = DateTime.Now;
                log.ItemsAdded = this._guarantesInserted.WriteFormattedLog();
                log.ItemsUpdated = this._guarantesUpdated.WriteFormattedLog();
                log.ItemsDeleted = this._guarantesDeleted.WriteFormattedLog();
                
                ServiceFacade.Instance.SyncLogService.AddSyncLog(log);

                if (this.OnSynchronizationEnded != null)
                {
                    this.OnSynchronizationEnded.Invoke(this, new ImportSyncEndEventArgs(this._guarantesInserted.Count + this._guarantesUpdated.Count + this._guarantesDeleted.Count, this._fechaDeCorte, ImportSyncEndEventArgs.ImportSyncEndStatus.Success));
                }
            }
            catch (Exception ex)
            {
                this.Logger.Error("General error at Sync process.", ex);
                if (this.OnSynchronizationEnded != null)
                {
                    this.OnSynchronizationEnded.Invoke(this, new ImportSyncEndEventArgs(this._guarantesInserted.Count + this._guarantesUpdated.Count + this._guarantesDeleted.Count, this._fechaDeCorte, ImportSyncEndEventArgs.ImportSyncEndStatus.Error) { Message = string.Format("Exception: {0}\nMessage:{1}\nStackTrace:\n{2}", ex.GetType().Name, ex.Message, ex.StackTrace) });
                }
            }           
        }

        /// <summary>
        /// Sincroniza los contratos de TEINSA. Los contratos que no vengan en teinsa seran eliminados de Garantia Contrato.
        /// </summary>
        private void SynchronizeContracts()
        {
            //JA: Se deasctiva la sincronizacion de contratos dado que se mueve el procedimiento a una rutina de SQL
            this.Logger.Info("Contracts sincronization disabled.");


        }                

        /// <summary>
        /// Handles the DoUpdateCompleted event of the garantiaManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Bladex.Garantias.ImportSync.GarantiaEventArgs"/> instance containing the event data.</param>
        private void garantiaManager_DoUpdateCompleted(object sender, GarantiaEventArgs e)
        {
            // Si no tengo el FCC, return.
            if (!this._teinsaImportThAtomoGarantiaRecordsByFcc.ContainsKey(e.Source.IDENTIFICACION_GARANTIA)) return;

            Queue<IMPORT_TH_ATOMO_GARANTIAS> queue = this._teinsaImportThAtomoGarantiaRecordsByFcc[e.Source.IDENTIFICACION_GARANTIA];
            while (queue.Count > 0)
            {
                this.GarantiaContratoManager.DoUpdate(queue.Dequeue(), (int)e.Garantia.Key);
            }
        }

        /// <summary>
        /// Handles the DoInsertCompleted event of the garantiaManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Bladex.Garantias.ImportSync.GarantiaEventArgs"/> instance containing the event data.</param>
        private void garantiaManager_DoInsertCompleted(object sender, GarantiaEventArgs e)
        {
            // Si no tengo el FCC, return.
            if (!this._teinsaImportThAtomoGarantiaRecordsByFcc.ContainsKey(e.Source.IDENTIFICACION_GARANTIA)) return;

            Queue<IMPORT_TH_ATOMO_GARANTIAS> queue = this._teinsaImportThAtomoGarantiaRecordsByFcc[e.Source.IDENTIFICACION_GARANTIA];
            while (queue.Count > 0)
            {
                this.GarantiaContratoManager.DoInsert(queue.Dequeue(), (int)e.Garantia.Key);
            }
        }

        /// <summary>
        /// Handles the DoDeleteCompleted event of the garantiaManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Bladex.Garantias.ImportSync.GarantiaEventArgs"/> instance containing the event data.</param>
        private void garantiaManager_DoDeleteCompleted(object sender, GarantiaEventArgs e)
        {
            // Si no tengo el FCC, return.
            if (!this._teinsaImportThAtomoGarantiaRecordsByFcc.ContainsKey(e.Source.IDENTIFICACION_GARANTIA)) return;
            Queue<IMPORT_TH_ATOMO_GARANTIAS> queue = this._teinsaImportThAtomoGarantiaRecordsByFcc[e.Source.IDENTIFICACION_GARANTIA];
            while (queue.Count > 0)
            {
                this.GarantiaContratoManager.DoDelete(queue.Dequeue().numero_prestamo, (int)e.Garantia.Key);
            }
        }

        /// <summary>
        /// Prepares the resources.
        /// </summary>
        private void PrepareResources()
        {
            try
            {
                this._guarantesDeleted = new Dictionary<string, bool>();
                this._guarantesInserted = new Dictionary<string, bool>();
                this._guarantesUpdated = new Dictionary<string, bool>();
            }
            catch
            {
            }
        }

        /// <summary>
        /// Gets the last succ sync.
        /// </summary>
        private void GetLastSuccSync()
        {
            this.lastSucessSync = ServiceFacade.Instance.SyncLogService.GetLastSuccessLog();
        }

        /// <summary>
        /// Reads the import records.
        /// </summary>
        private void ReadImportRecords()
        {            
            this._fechaDeCorte = ServiceFacade.Instance.IMPORT_TH_ATOMO_GARANTIASService.GetLastFechaCorte();
            if (!this._fechaDeCorte.HasValue)
            {
                this._teinsaImportThAtomoGarantiasRecords = null;
                return;
            }            
            // Obtengo los registros provenientes del import de teinsa por fecha de corte.
            IList<IMPORT_TH_ATOMO_GARANTIAS> records = ServiceFacade.Instance.IMPORT_TH_ATOMO_GARANTIASService.GetByFechaCorte(this._fechaDeCorte.Value);
            if (records == null)
            {
                this._teinsaImportThAtomoGarantiasRecords = null;
                return;
            }

            // Coleccion de contratos.
            this._teinsaImportThAtomoGarantiasRecords = new Dictionary<int, IMPORT_TH_ATOMO_GARANTIAS>();

            // Grouping de contratos.
            this._teinsaImportThAtomoGarantiaRecordsByFcc = new Dictionary<string, Queue<IMPORT_TH_ATOMO_GARANTIAS>>();
            foreach (var record in records)
            {
                try
                {
                    this._teinsaImportThAtomoGarantiasRecords.Add(record.ID, record);
                }
                catch (ArgumentException argumentEx)
                {
                    // Ya existe un contrato con el mismo ID, no deberia ocurrir.
                }

                if (this._teinsaImportThAtomoGarantiaRecordsByFcc.ContainsKey(record.IDENTIFICACION_GARANTIA))
                {
                    // Agrego a la cola
                    this._teinsaImportThAtomoGarantiaRecordsByFcc[record.IDENTIFICACION_GARANTIA].Enqueue(record);
                }
                else
                {
                    // Genero la estructura para almacenar multiples contratos en orden de llegada.
                    this._teinsaImportThAtomoGarantiaRecordsByFcc.Add(
                                                  record.IDENTIFICACION_GARANTIA,
                                                  new Queue<IMPORT_TH_ATOMO_GARANTIAS>(new[] { record }));
                }
            }

            // Traer los TeinsaIds(FCCReference) de Garantia Base 
            // Diccionario <GarantiaBase.FCCReference, GarantiaBase.ID>
            this.GetTeinsaIdsFromGarantias();

            return;
        }

        /// <summary>
        /// Traer los Ids de Teinsa de las garantias existentes
        /// </summary>
        private void GetTeinsaIdsFromGarantias()
        {
            this._fccAndNovarisId = ServiceFacade.Instance.GarantiaService.GetFccReferences() ?? new Dictionary<string, int>();
        }

        /// <summary>
        /// Aplicar updates
        /// </summary>
        private void DoUpdates()
        {
            // Obtengo los registros cuyo contrato ya existe en la tabla de garantias.
            var registros = this._teinsaImportThAtomoGarantiasRecords.Where(o => this._fccAndNovarisId.ContainsKey(o.Value.IDENTIFICACION_GARANTIA)).ToList();
            
            foreach (var importReg in registros)
            {
                // Me fijo si ya actualice la garantia para no volver a hacerlo ya que import_reg se encuentra a nivel garantia-contrato
                if (!this._guarantesUpdated.ContainsKey(this._fccAndNovarisId[importReg.Value.IDENTIFICACION_GARANTIA].ToString()))
                {
                    GarantiaBase target = ServiceFacade.Instance.GarantiaService.FindById(this._fccAndNovarisId[importReg.Value.IDENTIFICACION_GARANTIA],null);
                   
                    if (target != null)
                    {
                        try
                        {                                                        
                            this.GarantiaManager.DoUpdate(importReg.Value, target);                            

                            // Agrego la garantia que ya actualice
                            this._guarantesUpdated.Add(this._fccAndNovarisId[importReg.Value.IDENTIFICACION_GARANTIA].ToString(), true);
                        }
                        catch (Exception ex)
                        {
                            ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Error, "Error al actualizar garantia desde el sincronizador.", new Dictionary<string, object> { { "IDENTIFICACION_GARANTIA", importReg.Value.IDENTIFICACION_GARANTIA }, { "GarantiaId", this._fccAndNovarisId[importReg.Value.IDENTIFICACION_GARANTIA] } }, ex);
                            this._guarantesUpdated.Add(this._fccAndNovarisId[importReg.Value.IDENTIFICACION_GARANTIA].ToString(), false);
                        }                        
                    }
                }
                    
                // Actualizar GarantiaContrato
                this.GarantiaContratoManager.DoUpdate(importReg.Value, this._fccAndNovarisId[importReg.Value.IDENTIFICACION_GARANTIA]);
            }

            // Global Event for reporting purposes
            if (this.OnDoUpdatesCompleted != null)
            {
                this.OnDoUpdatesCompleted.Invoke(this, new ImportSyncEventArgs(this._guarantesUpdated));
            }
        }

        /// <summary>
        /// Realiza los Inserts
        /// </summary>
        private void DoInserts()
        {
            foreach (var importReg in this._teinsaImportThAtomoGarantiasRecords.ToList().Where(import_reg => !this._fccAndNovarisId.ContainsKey(import_reg.Value.IDENTIFICACION_GARANTIA)))
            {
                // Me fijo si ya actualice la garantia para no volver a hacerlo ya que import_reg se encuentra a nivel garantia-contrato
                if (!this._guarantesInserted.ContainsKey(importReg.Value.IDENTIFICACION_GARANTIA))
                {
                    try
                    {
                        this.GarantiaManager.DoInsert(importReg.Value);
                        
                        // Agrego la garantia que ya actualice
                        this._guarantesInserted.Add(importReg.Value.IDENTIFICACION_GARANTIA, true);
                    }
                    catch (Exception ex)
                    {
                        // TODO: Agregar logging de errores
                        this.Logger.Error(string.Format("Error intentando actualizar garantia. ID: {0} - IDENTIFICACION_GARANTIA: {1}", importReg.Value.ID, importReg.Value.IDENTIFICACION_GARANTIA), ex);

                        ServiceFacade.Instance.SyncLogService.AddSyncLog(
                                                                         new SyncLog(
                                                                             SyncLog.Status_FAIL,
                                                                             string.Format(
                                                                                 "Error intentando actualizar garantia. ID: {0} - IDENTIFICACION_GARANTIA: {1} ",
                                                                                 importReg.Value.ID,
                                                                                         importReg.Value.IDENTIFICACION_GARANTIA) +
                                                                                     "(" + ex.GetType().Name + ": " +
                                                                                     ex.Message + ") " + ex.StackTrace));


                        // Agrego la garantia que ya actualice
                        this._guarantesInserted.Add(importReg.Value.IDENTIFICACION_GARANTIA, false);
                    }                    
                }
                    
                // Actualizar GarantiaContrato
                if (this.GarantiaManager.LastIdAdded > -1)
                {
                    this.GarantiaContratoManager.DoInsert(importReg.Value, this.GarantiaManager.LastIdAdded);
                }
            }

            // Global Event for reporting purposes
            if (this.OnDoInsertsCompleted != null)
            {
                this.OnDoInsertsCompleted.Invoke(this, new ImportSyncEventArgs(this._guarantesInserted));
            }
        }

        /// <summary>
        /// Realiza los deletes
        /// </summary>
        private void DoDeletes()
        {
            // Recorro el indice de garantias en novaris por FCC id 
            foreach (var garantiabaseReg in this._fccAndNovarisId)
            {
                // Me fijo si no existe en Teinsa
                if (!this._teinsaImportThAtomoGarantiaRecordsByFcc.ContainsKey(garantiabaseReg.Key))
                { 
                    // no existe el FCC id de Novaris en los registro import de Teinsa
                    // Procedo a su "eliminacion"
                    try
                    {
                        GarantiaBase target = ServiceFacade.Instance.GarantiaService.FindById(garantiabaseReg.Value,null);
                        // Elimino solo si la garantia es un Deposito Pignorado en el Banco.
                        if (CategoriaSuperResolver.Resolve(target).Key.ToString() == CategoriaSuper.DEPOSITO_ID)
                        {
                            this.GarantiaManager.DoDelete(target);
                            this._guarantesDeleted.Add(garantiabaseReg.Key, true);
                        }
                    }
                    catch (Exception ex)
                    {
                        this._guarantesDeleted.Add(garantiabaseReg.Key, false);
                    }
                }
            }

            // Global Event for reporting purposes
            if (this.OnDoDeletesCompleted != null)
            {
                this.OnDoDeletesCompleted.Invoke(this, new ImportSyncEventArgs(this._guarantesDeleted));
            }
        }
    }
}