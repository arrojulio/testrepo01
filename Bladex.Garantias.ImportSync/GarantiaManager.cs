using System;
using System.Collections.Generic;
using System.Linq;
using Bladex.Garantias.Infrastructure.Logging;

namespace Bladex.Garantias.ImportSync
{
    using Application.Facades;
    using DomainModel.Components;
    using DomainModel.DomainBase;
    using DomainModel.Services;

    /// <summary>
    /// Encargado de realizar las operaciones de sincronizacion entre TEINSA y NOVARIS.
    /// </summary>
    public class GarantiaManager : IGarantiaManager
    {
        /// <summary>
        ///   Almacena los codigos utilizados para representar a los walking customers.
        /// </summary>
        public static readonly string[] WalkingCustomerCodes = new string[] { "TRNPART00", "TRNPART01", "TRNPART02" };

        /// <summary>
        /// Occurs when [do insert completed].
        /// </summary>
        public event EventHandler<GarantiaEventArgs> DoInsertCompleted;
        
        /// <summary>
        /// Occurs when [do update completed].
        /// </summary>
        public event EventHandler<GarantiaEventArgs> DoUpdateCompleted;
        
        /// <summary>
        /// Occurs when [do delete completed].
        /// </summary>
        public event EventHandler<GarantiaEventArgs> DoDeleteCompleted;

        /// <summary>
        /// Last id que fue insertado en Garantia.
        /// Esta propiedad es actualizada por DoInsert(). En caso de error este id se torna -1
        /// </summary>
        /// <value>
        /// The last id added.
        /// </value>      
        public int LastIdAdded { get; set; }

        /// <summary>
        /// Does the update.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        public void DoUpdate(IMPORT_TH_ATOMO_GARANTIAS source, dynamic target)
        {
            if (target == null)
            {
                return;
            }

            /////////////////////////////////////////////////////////////////
            // Colocar aqui los campos a realizar actualizaciones
            /////////////////////////////////////////////////////////////////                        
            // Build GarantiaBase object.
            target = DoUpdateBase(source, target);
            // Update the specialization, passing dynamic object as parameter. It expects any GarantiaBase hierarchy. Avoid the If (target is GarantiaDeposito) then...
            if (target is GarantiaBase)
            {
                this.DoUpdate(source, target);
            }
            else
            {
                ServiceFacade.Instance.SyncLogService.AddSyncLog(new SyncLog(SyncLog.Status_FAIL, string.Format("No se puede actualizar la garantia Id {0} dado que no se pudo encontrar su especializacion", target.IdentificadorGarantia)));
            }
        }

        /// <summary>
        /// Does the insert.
        /// </summary>
        /// <param name="source">The source.</param>
        public void DoInsert(IMPORT_TH_ATOMO_GARANTIAS source)
        {
            // No hace falta hacer el metodo transaccional, ya que se esta realizando en el SVC de GarantiaCommonService.
            // using (TransactionScope scope = new TransactionScope())
            // {
                try
                {
                    if (source == null)
                    {
                        throw new ApplicationException("Registro fuente de Teinsa en DoInsert es null");
                    }
                    if (source.TIPO_GARANTIA.Length != 4)
                    {
                        throw new ApplicationException("TIPO_GARANTIA de Teinsa es inválido (" + source.TIPO_GARANTIA + ")");
                    }

                    string categoriaSuperId = source.TIPO_GARANTIA.Substring(0, 2);

                    if (categoriaSuperId == CategoriaSuper.MUEBLE_ID) 
                    {
                        // 01
                        this.DoInsertMueble(source);
                    }
                    else if (categoriaSuperId == CategoriaSuper.INMUEBLE_ID) 
                    {
                        // 02
                        this.DoInsertInmueble(source);
                    }
                    else if (categoriaSuperId == CategoriaSuper.DEPOSITO_ID) 
                    {
                        // 03
                        this.DoInsertDeposito(source);
                    }
                    else if (categoriaSuperId == CategoriaSuper.DEPOSITO_OTROS_ID) 
                    {
                        // 04
                        this.DoInsertDepositoOtros(source);
                    }
                    else if (categoriaSuperId == CategoriaSuper.PRENDARIA_ID) 
                    {
                        // 05
                        this.DoInsertPrendaria(source);
                    }
                    else if (categoriaSuperId == CategoriaSuper.OTRAS_ID) 
                    {
                        // 06
                        this.DoInsertOtras(source);
                    }
                    else
                    {
                        throw new ApplicationException(string.Format("Invalid categoriaSuperId (TIPOGARANTIA) {0} ({1}", categoriaSuperId, source.TIPO_GARANTIA));
                    }

                    // scope.Complete();
                }
                catch (Exception ex)
                {
                    this.LastIdAdded = -1;
                    ServiceFacade.Instance.SyncLogService.AddSyncLog(new SyncLog(SyncLog.Status_FAIL, string.Format("Exception: {0} - Message: {1}\nStackStrace:\n{2}", ex.GetType().Name, ex.Message, ex.StackTrace)));
                    throw ex;
                }

            // }
        }

        /// <summary>
        /// Desactivo la garantia base. no hace falta que se elimine
        /// </summary>
        /// <param name="target">Garantia para ser eliminada</param>
        public void DoDelete(GarantiaBase target)
        {
            if (target == null)
            {
                return;
            }

            InternalStatus status = ServiceFacade.Instance.InternalStatusService.GetDeletedStatus();
            if (status == null)
            {
                throw new ApplicationException("Cannot setup status for garantia");
            }

            ServiceFacade.Instance.GarantiaService.SetInternalStatus(target.Key, status);
            var result = ServiceFacade.Instance.GarantiaService.GetGarantiaById(target.Key);
            if (this.DoDeleteCompleted != null)
            {
                this.DoDeleteCompleted.Invoke(this, new GarantiaEventArgs(result, null));
            }
        }

        #region Private Methods

        /// <summary>
        /// Converts the date.
        /// </summary>
        /// <param name="dateStr">The date STR.</param>
        /// <returns> Convert to DateTime(year, month, day);</returns>
        private static DateTime? ConvertDate(string dateStr)
        {
            DateTime? result = default(DateTime?);
            if (string.IsNullOrEmpty(dateStr))
            {
                return result;
            }

            if (dateStr.Trim().Length != 8)
            {
                return result;
            }

            try
            {
                int year = int.Parse(dateStr.Substring(0, 4));
                int month = int.Parse(dateStr.Substring(4, 2));
                int day = int.Parse(dateStr.Substring(6, 2));

                result = new DateTime(year, month, day);
            }
            catch (Exception e)
            {
                // TODO: Notificar del error                
                result = default(DateTime?);
            }

            return result;
        }

        #endregion

        #region Update
        
        /// <summary>
        /// Does the update base.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="garantia">The garantia.</param>
        /// <returns>Garantia Base Object</returns>
        private static GarantiaBase DoUpdateBase(IMPORT_TH_ATOMO_GARANTIAS source, GarantiaBase garantia)
        {
            garantia.ValorInicial = source.VALOR_INICIAL;
            garantia.SetValorGarantiaSuperIntendencia(source.VALOR_INICIAL);

            // Incorporado a partir de la mejora 711. El mapeo de fechas ha cambiado, por lo tanto se agrega funcionalidad para 
            // actualizar los campos de registros ya insertados.
            if (!garantia.FechaUltimaRevisionEvaluacion.HasValue)
            {
                garantia.FechaUltimaRevisionEvaluacion = ConvertDate(source.FECHA_ULTIMA_ACT);
            }

            if (!garantia.FechaVencimientoGarantia.HasValue)
            {
                garantia.FechaVencimientoGarantia = ConvertDate(source.FECHA_VENCIMIENTO);
            }

            if (!garantia.FechaFormalizacion.HasValue)
            {
                garantia.FechaFormalizacion = source.EFFECTIVE_DATE;
            }

            if ((string)garantia.InternalStatus.Key == InternalStatus.DELETED_ID)
            {
                garantia.InternalStatus.Key = InternalStatus.ACTIVE_ID;
            }

            IndicadorAtomoEnum? res = IndicadorAtomoResolver.Resolve(source.IND_ATOMO);
            garantia.IndAtomo = res.HasValue ? res.Value : IndicadorAtomoEnum.NoEstaEnAtomo;

            garantia.ID_Atomo = source.ID;

            if (!string.IsNullOrEmpty(source.CODIGO_BANCO))
            {
                garantia.CodigoBanco = source.CODIGO_BANCO.Trim();
            }
            else if (string.IsNullOrEmpty(garantia.CodigoBanco))
            {
                garantia.CodigoBanco = "027";
            }

            return garantia;
        }

        /// <summary>
        /// Does the update.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        private void DoUpdate(IMPORT_TH_ATOMO_GARANTIAS source, GarantiaDeposito target)
        {
            //// Aca actualizo los campos especificos
            //if (!string.IsNullOrEmpty(source.NOMBRE_ORGANISMO))
            //{
            //    Bancos bcnObj = ServiceFacade.Instance.BancosService.GetByName(source.NOMBRE_ORGANISMO);
            //    if (bcnObj != null)
            //    {
            //        target.BancoLocalSuper = bcnObj;
            //    }
            //    else
            //    {
            //        target.BancoLocalSuper = BancosService.GetEmpty();
            //    }
            //}     


            //Ticket #1529 se agrega id de cliente                           
                if (!string.IsNullOrEmpty(source.cliente_prestamo))
                {
                    Cliente clObj = ServiceFacade.Instance.ClienteService.GetById(source.cliente_prestamo);
                    if (clObj != null)
                        target.Cliente = clObj;                    
                }
                
                // Salvo la garantia
                var result = ServiceFacade.Instance.GarantiaDepositoService.Save(target);
                if (this.DoUpdateCompleted != null)
                {
                    this.DoUpdateCompleted.Invoke(this, new GarantiaEventArgs(result, source));
                }        
        }

        /// <summary>
        /// Does the update.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        private void DoUpdate(IMPORT_TH_ATOMO_GARANTIAS source, GarantiaDepositoOtroBanco target)
        {              
            
                // Aca actualizo los campos especificos
                //Ticket #1529 se agrega id de cliente
                
                if (!string.IsNullOrEmpty(source.cliente_prestamo))
                {
                    Cliente clObj = ServiceFacade.Instance.ClienteService.GetById(source.cliente_prestamo);
                    
                    if (clObj!=null)
                        target.Cliente = clObj;
                }
                
                // Salvo la garantia
                var result = ServiceFacade.Instance.GarantiaDepositoOtroBancoService.Save(target);
                if (this.DoUpdateCompleted != null)
                {
                    this.DoUpdateCompleted.Invoke(this, new GarantiaEventArgs(result, source));
                }
            
        }

        /// <summary>
        /// Does the update.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        private void DoUpdate(IMPORT_TH_ATOMO_GARANTIAS source, GarantiaInmueble target)
        {            
            // Aca actualizo los campos especificos
            // Salvo la garantia
            var result = ServiceFacade.Instance.GarantiaInmuebleService.Save(target);
            if (this.DoUpdateCompleted != null)
            {
                this.DoUpdateCompleted.Invoke(this, new GarantiaEventArgs(result, source));
            }
        }

        /// <summary>
        /// Does the update.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        private void DoUpdate(IMPORT_TH_ATOMO_GARANTIAS source, GarantiaMueble target)
        {            
            // Aca actualizo los campos especificos
            // Salvo la garantia
            var result = ServiceFacade.Instance.GarantiaMuebleService.Save(target);
            if (this.DoUpdateCompleted != null)
            {
                this.DoUpdateCompleted.Invoke(this, new GarantiaEventArgs(result, source));
            }
        }

        /// <summary>
        /// Does the update.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        private void DoUpdate(IMPORT_TH_ATOMO_GARANTIAS source, GarantiaOtra target)
        {            
            source.cliente_garantia = source.cliente_garantia.Trim();                        
            
            //// Aca actualizo los campos especificos
            //if (target.Emisor == null)
            //{
            //    target.Emisor = ActorService.GetEmpty();
            //}
            //if (!string.IsNullOrEmpty(source.NOMBRE_ORGANISMO))
            //{
            //    // Seteo el nombre del organismo como key del actor, para darlo de alta en el catalogo de actores.
            //    target.Emisor = new Actor() { Key = source.NOMBRE_ORGANISMO, Nombre = source.NOMBRE_ORGANISMO, Pais = PaisService.GetEmpty() };
            //}

            // Salvo la garantia
            var result = ServiceFacade.Instance.GarantiaOtraService.Save(target);
            if (this.DoUpdateCompleted != null)
            {
                this.DoUpdateCompleted.Invoke(this, new GarantiaEventArgs(result, source));
            }
        }

        /// <summary>
        /// Does the update.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        private void DoUpdate(IMPORT_TH_ATOMO_GARANTIAS source, GarantiaPrenda target)
        {            
            // Aca actualizo los campos especificos
            // Salvo la garantia
            var result = ServiceFacade.Instance.GarantiaPrendaService.Save(target);
            if (this.DoUpdateCompleted != null)
            {
                this.DoUpdateCompleted.Invoke(this, new GarantiaEventArgs(result, source));
            }
        }
        #endregion

        #region Insert

        /// <summary>
        /// Does the insert base.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="garantia">The garantia.</param>
        /// <returns>Garantia Base</returns>
        private GarantiaBase DoInsertBase(IMPORT_TH_ATOMO_GARANTIAS source, GarantiaBase garantia)
        {
            garantia.CodigoBanco = !string.IsNullOrEmpty(source.CODIGO_BANCO) ? source.CODIGO_BANCO.Trim() : "027";
            garantia.ID_Atomo = source.ID;

            // Seteo la garantia proveniente de TEINSA
            garantia.Source = GarantiaSourceEnum.Teinsa;

            IndicadorAtomoEnum? res = IndicadorAtomoResolver.Resolve(source.IND_ATOMO);
            garantia.IndAtomo = res.HasValue ? res.Value : IndicadorAtomoEnum.NoEstaEnAtomo;

            garantia.IdentificacionFideicomiso = source.IDENTIFICACION_FIDEICOMISO;
            Fiduciarias fiduciaria = ServiceFacade.Instance.FiduciariasService.GetById(source.NOMBRE_FIDUCIARIA);
            if (fiduciaria == null)
            {
                ServiceFacade.Instance.SyncLogService.AddSyncLog(new SyncLog(SyncLog.Status_FAIL, string.Format("Warning: La Fiduciaria {0} no se encuentra en el catalogo de Novaris. Asignando Fiduciaria.GetEmpty()", source.NOMBRE_FIDUCIARIA)));
                fiduciaria = FiduciariasService.GetEmpty();
            }

            garantia.FiduciariaSuper = fiduciaria;
            
            // Cambio realizado y solicitado por Felix.
            garantia.Beneficiario = string.Empty; 

            garantia.OrigenGarantia = source.ORIGEN_GARANTIA;
            TipoGarantiaSuper tipoGarantia = ServiceFacade.Instance.TipoGarantiaSuperService.GetById(source.TIPO_GARANTIA);
            if (tipoGarantia == null)
            {
                ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Warning, "TipoGarantiaSuper no encontrado. Asignando TipoGarantiaSuperService.GetEmpty().", new Dictionary<string,object>{{"TIPO_GARANTIA",source.TIPO_GARANTIA}},null);
                tipoGarantia = TipoGarantiaSuperService.GetEmpty(garantia.CategoriaSuper);
            }

            garantia.TipoGarantiaSuper = tipoGarantia;

            garantia.FCCReference = source.IDENTIFICACION_GARANTIA;
            //garantia.SetIdentificacionDocumentoGarantia(source.IDENTIFICACION_GARANTIA);

            //Reconciliacion Garantias - Ticket 911
            //Se descubre que no se esta colocando como IdentificadorGarantia a la identificacion de Teinsa
            // Se coloca N/A sobre Nro. Incidente Workflow
            //garantia.IdentificadorGarantia = source.IDENTIFICACION_GARANTIA;
            garantia.NroIncidenteWorkflow = "N/A";

            // Ignorado por Work Item N° 704
            // if (!string.IsNullOrEmpty(source.NOMBRE_ORGANISMO))
            //// garantia.SetNombreOrganismo(source.NOMBRE_ORGANISMO.Trim());

            garantia.ValorInicial = source.VALOR_INICIAL;

            // Cargo el Valor Inicial en la funcion de la garantia.            
            garantia.SetValorGarantiaSuperIntendencia(source.VALOR_INICIAL);

            // Fechas de la Garantia

            // Se vuelve a asociar el campo dado el Task ID 983.
            garantia.FechaUltimaRevisionEvaluacion = ConvertDate(source.FECHA_ULTIMA_ACT);
            garantia.FechaVencimientoGarantia = ConvertDate(source.FECHA_VENCIMIENTO);
            garantia.FechaFormalizacion = source.EFFECTIVE_DATE;
            
            // Ahora es una formula.
            // garantia.FechaProximaRevisionEvaluacion = default(DateTime?);
                        
            // Se vuelve a asociar el campo dado el Task ID 1165
            garantia.FechaRegistroInicial = ConvertDate(source.FECHA_ULTIMA_ACT);
            
            // FechaVencimientoRiesgo: Aplica solo para algunas garantias.
            garantia.FechaVencimientoRiesgo = default(DateTime?);
            garantia.FechaVencimientoSeguro = default(DateTime?);
            garantia.FechaComienzoEjecucion = default(DateTime?);
            garantia.FechaCierreEjecucion = default(DateTime?);


            Cliente cliente = ServiceFacade.Instance.ClienteService.GetById(source.cliente_prestamo.Trim());
            if (cliente == null)
            {
                ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Warning, "Cliente no encontrado en el catalogo de Novaris. Asignando ClienteService.GetEmpty().", new Dictionary<string, object> { { "cliente_prestamo", source.cliente_prestamo.Trim()} }, null);
                cliente = ClienteService.GetEmpty();
            }

            garantia.Cliente = cliente;

            Cliente garante = ServiceFacade.Instance.ClienteService.GetById(source.cliente_garantia.Trim());
            if (garante == null)
            {
                ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Warning, "Garante no encontrado en el catalogo de Novaris. Asignando ClienteService.GetEmpty().", new Dictionary<string, object> { { "cliente_garantia", source.cliente_garantia.Trim() } }, null);
                garante = ClienteService.GetEmpty();    
            }
            
            garantia.Garante = garante;

            garantia.Administrador = ActorService.GetEmpty();
            garantia.Asegurador = ActorService.GetEmpty();
            garantia.AttachedToLine = false;
            garantia.CategoriaRiesgoGarantia = CategoriaRiesgoGarantiaService.GetEmpty();
            garantia.Depositante = ActorService.GetEmpty();
            garantia.DescripcionDeLaGarantia = string.Empty;
            garantia.Evaluador = ActorService.GetEmpty();
            garantia.FiduciariaBladex = string.Empty;
            garantia.FrecuenciaRevision = FrecuenciasService.GetEmpty();
            garantia.Moneda = MonedasService.GetEmpty();
            garantia.NumeroPolizaSeguro = string.Empty;
            garantia.OrigenGarantia = source.ORIGEN_GARANTIA;
            garantia.PaisGarantia = PaisService.GetEmpty();
            garantia.PorcentajeAplicableMitigacionSuperInt = 100;
            garantia.RatingGarante = CalificacionesRiesgoService.GetEmpty();
            garantia.ReduccionDeRiesgoPorPais = 0;
            garantia.Revisor = ActorService.GetEmpty();
            garantia.InternalStatus = ServiceFacade.Instance.InternalStatusService.GetActiveStatus();
            garantia.Status = ServiceFacade.Instance.StatusService.GetNormalStatus();
            garantia.TipoGarantiaBladex = TipoGarantiaBladexService.GetEmpty();

            // Retorno la garantia para su posterior utilizacion
            return garantia;
        }

        /// <summary>
        /// Does the insert mueble.
        /// </summary>
        /// <param name="source">The source.</param>
        private void DoInsertMueble(IMPORT_TH_ATOMO_GARANTIAS source)
        {
            GarantiaMueble garantia = new GarantiaMueble();

            garantia = this.DoInsertBase(source, garantia) as GarantiaMueble;
            garantia.FechaRegistroInicial = source.DEAL_STAT_FROM_DT;
            garantia.FechaVencimientoRiesgo = source.DEAL_CLOSURE_DATE;
            if (garantia.AseguradorSuper == null)
            { 
                garantia.AseguradorSuper = AseguradorasService.GetEmpty(); 
            }
                            
            // Salvo la garantia
            var result = ServiceFacade.Instance.GarantiaMuebleService.Save(garantia);
            this.LastIdAdded = (int)result.Key;

            if (this.DoInsertCompleted != null)
            {
                this.DoInsertCompleted.Invoke(this, new GarantiaEventArgs(result, source));
            }
        }

        /// <summary>
        /// Does the insert inmueble.
        /// </summary>
        /// <param name="source">The source.</param>
        private void DoInsertInmueble(IMPORT_TH_ATOMO_GARANTIAS source)
        {
            GarantiaInmueble garantia = new GarantiaInmueble();
            
            garantia = this.DoInsertBase(source, garantia) as GarantiaInmueble;            
            garantia.FechaRegistroInicial = source.DEAL_STAT_FROM_DT;
            garantia.FechaVencimientoRiesgo = source.DEAL_CLOSURE_DATE;

            if (garantia.AseguradorSuper == null)
            {
                garantia.AseguradorSuper = AvaluadoraService.GetEmpty();
            }

            // Salvo la garantia
            var result = ServiceFacade.Instance.GarantiaInmuebleService.Save(garantia);
            this.LastIdAdded = (int)result.Key;
            if (this.DoInsertCompleted != null)
            {
                this.DoInsertCompleted.Invoke(this, new GarantiaEventArgs(result, source));
            }
        }

        /// <summary>
        /// Does the insert deposito.
        /// </summary>
        /// <param name="source">The source.</param>
        private void DoInsertDeposito(IMPORT_TH_ATOMO_GARANTIAS source)
        {
            GarantiaDeposito garantia = new GarantiaDeposito();

            garantia = this.DoInsertBase(source, garantia) as GarantiaDeposito;
            if (garantia.BancoLocalSuper == null)
            {
                garantia.BancoLocalSuper = BancosService.GetEmpty();
            }
                
            if (!string.IsNullOrEmpty(source.NOMBRE_ORGANISMO))
            {
                Bancos bcnObj = ServiceFacade.Instance.BancosService.GetByName(source.NOMBRE_ORGANISMO);
                if (bcnObj != null)
                {
                    garantia.BancoLocalSuper = bcnObj;
                }
                else
                {
                    garantia.BancoLocalSuper = BancosService.GetEmpty();
                }
            }

            // Salvo la garantia
            var result = ServiceFacade.Instance.GarantiaDepositoService.Save(garantia);
            this.LastIdAdded = (int)result.Key;
            if (this.DoInsertCompleted != null)
            {
                this.DoInsertCompleted.Invoke(this, new GarantiaEventArgs(result, source));
            }
        }

        /// <summary>
        /// Does the insert deposito otros.
        /// </summary>
        /// <param name="source">The source.</param>
        private void DoInsertDepositoOtros(IMPORT_TH_ATOMO_GARANTIAS source)
        {
            GarantiaDepositoOtroBanco garantia = new GarantiaDepositoOtroBanco();

            garantia = this.DoInsertBase(source, garantia) as GarantiaDepositoOtroBanco;

            if (garantia.BancoSuper == null)
            {
                garantia.BancoSuper = BancosService.GetEmpty();
            }

            // Salvo la garantia
            var result = ServiceFacade.Instance.GarantiaDepositoOtroBancoService.Save(garantia);
            this.LastIdAdded = (int)result.Key;
            if (this.DoInsertCompleted != null)
            {
                this.DoInsertCompleted.Invoke(this, new GarantiaEventArgs(result, source));
            }
        }

        /// <summary>
        /// Does the insert prendaria.
        /// </summary>
        /// <param name="source">The source.</param>
        private void DoInsertPrendaria(IMPORT_TH_ATOMO_GARANTIAS source)
        {
            GarantiaPrenda garantia = new GarantiaPrenda();

            garantia = this.DoInsertBase(source, garantia) as GarantiaPrenda;
            garantia.FechaRegistroInicial = source.DEAL_STAT_FROM_DT;
            garantia.FechaVencimientoRiesgo = source.DEAL_CLOSURE_DATE;
            garantia.TipoInstrumentoFinanciero = ServiceFacade.Instance.InstrumentoFinancieroService.GetById(source.TIPO_INSTRUMENTO) ?? InstrumentoFinancieroService.GetEmpty();
            garantia.CalificacionEmision = ServiceFacade.Instance.CalificacionesRiesgoService.GetById(source.CALIFICACION_EMISION) ?? CalificacionesRiesgoService.GetEmpty();
            garantia.CalificacionEmisor = ServiceFacade.Instance.CalificacionesRiesgoService.GetById(source.CALIFICACION_INSTRUMENTO) ?? CalificacionesRiesgoService.GetEmpty();
            garantia.PaisEmision = ServiceFacade.Instance.PaisService.GetById(source.PAIS_EMISION) ?? PaisService.GetEmpty();
            
            if (garantia.Emisor == null)
            {
                garantia.Emisor = ActorService.GetEmpty();
            }
                            
            // Salvo la garantia
            var result = ServiceFacade.Instance.GarantiaPrendaService.Save(garantia);            
            this.LastIdAdded = (int)result.Key;

            if (this.DoInsertCompleted != null)
            {
                this.DoInsertCompleted.Invoke(this, new GarantiaEventArgs(result, source));
            }
        }

        /// <summary>
        /// Does the Insert Otras Garantias.
        /// </summary>
        /// <param name="source">The source.</param>
        private void DoInsertOtras(IMPORT_TH_ATOMO_GARANTIAS source)
        {
            GarantiaOtra garantia = new GarantiaOtra();

            garantia = this.DoInsertBase(source, garantia) as GarantiaOtra;

            // Comportamiento agregado el 08/31/2011 - Solicitud de Felix.
            // Si obtengo un walking customer, lo doy de alta en el sistema.
            if ( new string[] { "0604", "0605" }.Contains(garantia.TipoGarantiaSuper.GetKeyAs<string>()) && WalkingCustomerCodes.Contains(source.cliente_garantia.Trim()))
            {
                string customerName = source.BENEFICIARIO.Trim();
                
                // Log del walking customer a generar.
                new SyncLogService().AddSyncLog(new SyncLog(SyncLog.Status_SUCCESS, string.Format("Walking Customer detectado. Generando registro para el garante {0}", customerName)));

                Cliente garante = ServiceFacade.Instance.ClienteService.GetByName(customerName);
                if (garante == null)
                {
                    new SyncLogService().AddSyncLog(new SyncLog(SyncLog.Status_SUCCESS,
                                                                string.Format("Generando registro para el garante {0}",
                                                                              customerName)));
                    garantia.Garante = new Cliente()
                                           {
                                               Nombre = customerName,
                                               Pais = PaisService.GetEmpty(),
                                               IsActive = true,
                                               IsInternal = true
                                           };
                }
                else
                {
                    new SyncLogService().AddSyncLog(new SyncLog(SyncLog.Status_SUCCESS, string.Format("Asignando registro del garante {0} - {1}", garante.Key, garante.Nombre)));
                    garantia.Garante = garante;
                }
            }

            garantia.FechaRegistroInicial = source.DEAL_STAT_FROM_DT;
            garantia.FechaVencimientoRiesgo = source.DEAL_CLOSURE_DATE;
            
            if (garantia.Avales != null && garantia.Avales.Count > 0)
            {
                foreach (Aval a in garantia.Avales)
                {
                    ServiceFacade.Instance.AvalService.Save(a);
                }
            }
           
            if (!string.IsNullOrEmpty(source.NOMBRE_ORGANISMO))
            {
                // Seteo el nombre del actor como key, para darlo de alta en nuestro catalogo.
                garantia.Emisor = new Actor() { Key = source.NOMBRE_ORGANISMO, Nombre = source.NOMBRE_ORGANISMO, Pais = PaisService.GetEmpty() };
            }
            else
            {
                garantia.Emisor = ActorService.GetEmpty();
            }

            // Salvo la garantia
            var result = ServiceFacade.Instance.GarantiaOtraService.Save(garantia);
            this.LastIdAdded = (int)result.Key;

            if (this.DoInsertCompleted != null)
            {
                this.DoInsertCompleted.Invoke(this, new GarantiaEventArgs(result, source));
            }
        }
        
        #endregion        
    }
}
