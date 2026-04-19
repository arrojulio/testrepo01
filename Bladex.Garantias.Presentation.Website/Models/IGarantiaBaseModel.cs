using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Presentation.Website.ViewModels;

namespace Bladex.Garantias.Presentation.Website.Models
{
    /// <summary>
    /// IGarantiaBaseModel interface.
    /// </summary>
    public interface IGarantiaBaseModel
    {
        #region Plain Values

        /// <summary>
        /// Gets or sets the business error.
        /// </summary>
        /// <value>
        /// The business error.
        /// </value>
        string BusinessError { get; set; }

        int? Key
        { get; set; }
        /// <summary>
        ///  referencia de FCC por migracion
        /// </summary>
        string FCCReference
        {
            get;
            set;
        }

        #region Properties mapped to methods

        string IdentificacionDocumentoGarantia { get; set; }

        string NombreOrganismo { get; set; }

        decimal ValorGarantiaSuperIntendencia { get; set; }

        double RatioCoberturaGarantia { get; set; }
        
        #endregion

        /// <summary>
        /// Nombre del beneficiario similar al cliente de la garantia.
        /// </summary>
        string Beneficiario
        {
            get;
            set;
        }

        ///// <summary>
        ///// Numero de identificación/registro/certificado/notario conforme la garantía
        ///// </summary>
        //string IdentificadorGarantia
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// Gets or sets the nro incidente workflow.
        /// </summary>
        /// <value>
        /// The nro incidente workflow of type <see cref="System.String"/>
        /// </value>
        string NroIncidenteWorkflow
        {
            get;
            set;
        }

        /// <summary>
        /// 027 - el numero que asigno la super 
        /// </summary>
        string CodigoBanco
        {
            get;
            set;
        }
        
        /// <summary>
        /// Cedula o RUC de la empresa (SUPER)
        /// </summary>
        string IdentificacionFideicomiso
        {
            get;
            set;
        }

        /// <summary>
        /// Informacion interna de la ficuciaria
        /// </summary>
        string FiduciariaBladex
        {
            get;
            set;
        }

        /// <summary>
        /// Local/Extrangero (si es panama es local)
        /// </summary>
        string OrigenGarantia
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the valor inicial.
        /// </summary>
        /// <value>
        /// The valor inicial.
        /// </value>
        decimal ValorInicial
        {
            get;
            set;
        }

        /// <summary>
        /// detalla el activo dado en garantía. Ej: , hipoteca: “inmueble industrial, ubicado … , tantos metros de área, etcétera..”, prenda de inventarios: “nro de sacas de café de 60kgs, tipo xxxx, bebida xxxx, etecétera”…
        /// </summary>
        string DescripcionDeLaGarantia
        {
            get;
            set;
        }

        /// <summary>
        /// Fecha de registro inicial de la garantía (Issue Date)
        /// </summary>
        DateTime? FechaRegistroInicial
        {
            get;
            set;
        }

        /// <summary>
        /// Fecha de formalizacion de la garantia (Efective Date)
        /// </summary>
        DateTime? FechaFormalizacion
        {
            get;
            set;
        }

        /// <summary>
        /// Fecha Vencimiento Riesgo Garantizado (ClousureDate)
        /// </summary>
        DateTime? FechaVencimientoRiesgo
        {
            get;
            set;
        }

        /// <summary>
        /// Fecha Vencimiento  de la Garantía (Expiry Date)
        /// </summary>
        DateTime? FechaVencimientoGarantia
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the fecha ultima revision evaluacion.
        /// </summary>
        /// <value>
        /// The fecha ultima revision evaluacion.
        /// </value>
        DateTime? FechaUltimaRevisionEvaluacion
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the fecha proxima revision evaluacion.
        /// </summary>
        /// <value>
        /// The fecha proxima revision evaluacion.
        /// </value>
        DateTime? FechaProximaRevisionEvaluacion
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the fecha vencimiento seguro.
        /// </summary>
        /// <value>
        /// The fecha vencimiento seguro.
        /// </value>
        DateTime? FechaVencimientoSeguro
        {
            get;
            set;
        }



        /// <summary>
        /// Gets or sets the reduccion de riesgo por pais.
        /// </summary>
        /// <value>
        /// The reduccion de riesgo por pais.
        /// </value>
        decimal ReduccionDeRiesgoPorPais
        {
            get;
            set;
        }

        /// <summary>
        ///  (Necessary Value) Suma de todos las coberturas que tiene relacionadas de los prestamos
        /// </summary>
        decimal ValorNecesarioDeGarantia
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the porcentaje aplicable mitigacion super int.
        /// </summary>
        /// <value>
        /// The porcentaje aplicable mitigacion super int.
        /// </value>
        decimal PorcentajeAplicableMitigacionSuperInt
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the comentarios.
        /// </summary>
        /// <value>
        /// The comentarios.
        /// </value>
        string Comentarios
        {
            get;
            set;
        }



        /// <summary>
        /// Gets or sets the valor poliza seguro.
        /// </summary>
        /// <value>
        /// The valor poliza seguro.
        /// </value>
        decimal ValorPolizaSeguro
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the numero poliza seguro.
        /// </summary>
        /// <value>
        /// The numero poliza seguro.
        /// </value>
        string NumeroPolizaSeguro
        {
            get;
            set;
        }

        /// <summary>
        /// Valor de Mercado ultima revision
        /// </summary>
        decimal ValorMercado
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets a value indicating whether [attached to line].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [attached to line]; otherwise, <c>false</c>.
        /// </value>
        bool AttachedToLine
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets the Id atomo.
        /// </summary>
        /// <value>
        /// The Id atomo.
        /// </value>
        int ID_Atomo
        {
            get;
            set;
        }

        int? selectedOperationId 
        { 
            get; 
            set; 
        }

        #endregion

        #region Collections

        //IEnumerable<SelectListItem> ClienteSelectList { get; set; }

        /// <summary>
        /// Cliente quien aplica la garantia
        /// </summary>
        /// <value>
        /// The cliente of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.ClienteViewModel"/>
        /// </value>
        ClienteViewModel Cliente { get; set; }

        //IEnumerable<SelectListItem> GaranteSelectList { get; set; }


        /// <summary>
        /// Gets or sets the garante.
        /// </summary>
        /// <value>
        /// The garante of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.ClienteViewModel"/>
        /// </value>
        ClienteViewModel Garante { get; set; }

        // IEnumerable<SelectListItem> FiduciariaSuperSelectList { get; set; }

        /// <summary>
        /// Codigo catalogo SB56 (Fiduciarias)
        /// </summary>
        /// <value>
        /// The fiduciaria super of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.FiduciariasViewModel"/>
        /// </value>
        FiduciariasViewModel FiduciariaSuper { get; set; }

        // IEnumerable<SelectListItem> DepositanteSelectList { get; set; }

        /// <summary>
        /// Nombre Depositante/Custodio/Fiel
        /// </summary>
        /// <value>
        /// The depositante of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.ActorViewModel"/>
        /// </value>
        ActorViewModel Depositante { get; set; }

        // IEnumerable<SelectListItem> EvaluadorSelectList { get; set; }

        /// <summary>
        /// Nombre Evaluador
        /// </summary>
        /// <value>
        /// The evaluador of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.ActorViewModel"/>
        /// </value>
        ActorViewModel Evaluador { get; set; }

        //IEnumerable<SelectListItem> AdministradorSelectList { get; set; }

        /// <summary>
        /// Nombre Administrador
        /// </summary>
        /// <value>
        /// The administrador of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.ActorViewModel"/>
        /// </value>
        ActorViewModel Administrador { get; set; }

        //IEnumerable<SelectListItem> AseguradorSelectList { get; set; }

        /// <summary>
        /// Gets or sets the asegurador.
        /// </summary>
        /// <value>
        /// The asegurador of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.ActorViewModel"/>
        /// </value>
        ActorViewModel Asegurador { get; set; }

        //IEnumerable<SelectListItem> RevisorSelectList { get; set; }

        /// <summary>
        /// Gets or sets the revisor.
        /// </summary>
        /// <value>
        /// The revisor of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.ActorViewModel"/>
        /// </value>
        ActorViewModel Revisor { get; set; }

        //IEnumerable<SelectListItem> PaisGarantiaSelectList { get; set; }

        /// <summary>
        /// Se debe colocar donde se encuentra fisicamente la garantia o garante
        /// </summary>
        /// <value>
        /// The pais garantia of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.PaisViewModel"/>
        /// </value>
        PaisViewModel PaisGarantia { get; set; }

        /// <summary>
        /// Se debe colocar donde se encuentra fisicamente la garantia o garante
        /// </summary>
        /// <value>
        /// The CodRegion garantia of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.RegionViewModel"/>
        /// </value>
        RegionViewModel Region { get; set; }

        /// <summary>
        /// El tipo de poliza
        /// </summary>
        /// <value>
        /// The TipoPoliza garantia of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.TipoPolizaViewModel"/>
        /// </value>
        TipoPolizaViewModel TipoPoliza { get; set; }

        //IEnumerable<SelectListItem> TipoGarantiaSuperSelectList { get; set; }

        /// <summary>
        /// Catalogo SB59 *opcional en caso que las garantias no posea categorizacion. Si no posee codigo no se registra contablemente
        /// </summary>
        /// <value>
        /// The tipo garantia super of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.TipoGarantiaSuperViewModel"/>
        /// </value>
        TipoGarantiaSuperViewModel TipoGarantiaSuper { get; set; }

        //IEnumerable<SelectListItem> TipoGarantiaBladexSelectList { get; set; }

        /// <summary>
        /// Tipo de garantia asignada por bladex cuando no existe clasificacion
        /// Restriccion: TipoGarantiaSuper.Categoria = CategoriaSuper
        /// </summary>
        /// <value>
        /// The tipo garantia bladex of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.TipoGarantiaBladexViewModel"/>
        /// </value>
        TipoGarantiaBladexViewModel TipoGarantiaBladex { get; set; }

        //IEnumerable<SelectListItem> FrecuenciaRevisionSelectList { get; set; }

        /// <summary>
        /// Gets or sets the frecuencia revision.
        /// </summary>
        /// <value>
        /// The frecuencia revision of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.FrecuenciasViewModel"/>
        /// </value>
        FrecuenciasViewModel FrecuenciaRevision { get; set; }


        //IEnumerable<SelectListItem> CategoriaRiesgoGarantiaSelectList { get; set; }

        /// <summary>
        /// Catalogo Categoria Garantias Riesgo
        /// </summary>
        /// <value>
        /// The categoria riesgo garantia of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.CategoriaRiesgoGarantiaViewModel"/>
        /// </value>
        CategoriaRiesgoGarantiaViewModel CategoriaRiesgoGarantia { get; set; }

        //IEnumerable<SelectListItem> MonedaSelectList { get; set; }

        /// <summary>
        /// Gets or sets the moneda.
        /// </summary>
        /// <value>
        /// The moneda of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.MonedasViewModel"/>
        /// </value>
        MonedasViewModel Moneda { get; set; }

        // IEnumerable<SelectListItem> AvalListSelectList { get; set; }

        
        



        // IEnumerable<SelectListItem> RatingGaranteSelectList { get; set; }

        /// <summary>
        /// Tabla SB25
        /// </summary>
        /// <value>
        /// The rating garante of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.CalificacionesRiesgoViewModel"/>
        /// </value>
        CalificacionesRiesgoViewModel RatingGarante { get; set; }

        //IEnumerable<SelectListItem> CategoriaSuperSelectList{ get; set; }

        /// <summary>
        /// Guarda la categoria de la super. es importante ya que toda la logica de campo se rige de este valor.
        /// </summary>
        /// <value>
        /// The categoria super of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.CategoriaSuperViewModel"/>
        /// </value>
        CategoriaSuperViewModel CategoriaSuper { get; set; }

        //IEnumerable<SelectListItem> StatusSelectList { get; set; }

        /// <summary>
        /// Gets or sets the internal status.
        /// </summary>
        /// <value>
        /// The status of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.InternalStatusViewModel"/>
        /// </value>
        InternalStatusViewModel InternalStatus { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.StatusViewModel"/>
        /// </value>
        StatusViewModel Status { get; set; }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>
        /// The source of type <see cref="Bladex.Garantias.DomainModel.DomainBase.GarantiaSourceEnum"/>
        /// </value>
        GarantiaSourceEnum? Source { get; set; }

        /// <summary>
        /// Gets or sets the ind atomo.
        /// </summary>
        /// <value>
        /// The ind atomo of type <see cref="Bladex.Garantias.DomainModel.DomainBase.IndicadorAtomoEnum"/>
        /// </value>
        IndicadorAtomoEnum IndAtomo { get; set; }

        /// <summary>
        /// Gets or sets the fecha comienzo ejecucion.
        /// </summary>
        /// <value>
        /// The fecha comienzo ejecucion.
        /// </value>
        DateTime? FechaComienzoEjecucion
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the fecha cierre ejecucion.
        /// </summary>
        /// <value>
        /// The fecha cierre ejecucion.
        /// </value>
        DateTime? FechaCierreEjecucion
        {
            get;
            set;
        }

        #endregion

    }
}
