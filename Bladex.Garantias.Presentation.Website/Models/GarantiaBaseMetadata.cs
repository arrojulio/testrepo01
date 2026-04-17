using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime;
using System.Web;
using System.Web.Mvc;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Presentation.Website.Components.Attributes;
using Bladex.Garantias.Presentation.Website.ViewModels;

namespace Bladex.Garantias.Presentation.Website.Models
{
    /// <summary>
    /// IMPORTANTE: El orden de las propiedades no debe aterarste, ya que esta arreglado para ser el mismo 
    /// que esta en las vistas de garantias, para mostrar de la misma forma en la tabla de maker&chaker.
    /// </summary>
    public class GarantiaBaseMetadata : IGarantiaBaseModel
    {
        #region Implementation of IGarantiaBaseModel

        [DisplayName("Errores")]
        [Exclude]
        public string BusinessError
        {
            get;
            set;
        }

        [DisplayName("Nro Identificador Garantia")]
        [DataObjectField(true, true, true, Int32.MaxValue)]
        [DataType(DataType.Text)]
        //[Editable(false, AllowInitialValue=false)]
        [Key()]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        [ShowInViewer]
        public int? Key { get; set; }

        [DisplayName("Identificacion Documento Garantia")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        public string IdentificacionDocumentoGarantia { get; set; }

        /// <summary>
        /// 027 - el numero que asigno la super 
        /// </summary>
        /// <summary>
        /// 027 - el numero que asigno la super 
        /// </summary>
        [Required]
        [DisplayName("Codigo Banco")]
        [DefaultValue("027")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        //TODO: confirmar si es necesario incorporar longitud de string
        [StringLength(5)]
        public string CodigoBanco { get; set; }

        [UIHint("Cliente")]
        [DisplayName("Cliente")]
        [Required]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        [ShowInViewer]
        public ClienteViewModel Cliente { get; set; }

        [UIHint("Garante")]
        [DisplayName("Garante")]
        [Required]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        [ShowInViewer]
        public ClienteViewModel Garante { get; set; }

        /// <summary>
        /// Cedula o RUC de la empresa (SUPER)
        /// </summary>
        [Required]
        [DisplayName("Identificacion Fideicomiso")]
        [DataType(DataType.Text)]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        [UIHint("Autocomplete")]
        //TODO: confirmar si es necesario incorporar longitud de string
        [StringLength(50)]
        public string IdentificacionFideicomiso { get; set; }

        [DisplayName("Nombre Fiduciaria (Super)")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        public FiduciariasViewModel FiduciariaSuper { get; set; }

        /// <summary>
        /// Informacion interna de la ficuciaria
        /// </summary>
        [Required]
        [DisplayName("Nombre Fiduciario (en el Exterior)")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        //[UIHint("Autocomplete")]
        //TODO: confirmar si es necesario incorporar longitud de string
        [StringLength(50)]
        public string FiduciariaBladex { get; set; }

        [Required]
        [DisplayName("Nombre Asegurador")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        public ActorViewModel Asegurador { get; set; }

        [DisplayName("Nombre Depositante/Custodio/Fiel")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        public ActorViewModel Depositante { get; set; }

        /// <summary>
        /// Local/Extrangero (si es panama es local)
        /// </summary>
        [DisplayName("Origen Garantia (Super) L (Local) | E Extranjera")]
        [StringLength(1)]
        [Required]
        [UIHint("DropdownOrigenGarantia")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        public string OrigenGarantia { get; set; }

        [DisplayName("Pais Garantía")]
        [Required]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        public PaisViewModel PaisGarantia { get; set; }

        [DisplayName("Región")]
        [Required]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        public RegionViewModel Region { get; set; }

        [DisplayName("Tipo Garantia (Super)")]
        [Required]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        [ShowInViewer]
        public TipoGarantiaSuperViewModel TipoGarantiaSuper { get; set; }

        [Required]
        [DisplayName("Moneda (Currency)")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        public MonedasViewModel Moneda { get; set; }

        [DisplayName("Valor Inicial")]
        [DataType(DataType.Currency)]
        [UIHint("Money")]
        [Required]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        [ShowInViewer]
        public decimal ValorInicial { get; set; }

        [Required]
        [DisplayName("Valor Garantia Super Int.")]
        [DataType(DataType.Currency)]
        //[ReadOnly(true)]
        [UIHint("ValorGarantiaSuperIntendencia")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        [ShowInViewer]
        public decimal ValorGarantiaSuperIntendencia { get; set; }

        /// <summary>
        /// Valor de Mercado ultima revision
        /// </summary>
        [Required]
        [DataType(DataType.Currency)]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        [DisplayFormat(DataFormatString = "c", NullDisplayText = "", ApplyFormatInEditMode = false)]
        [UIHint("Money")]
        [DisplayName("Valor de Mercado (Ultima Revision)")]
        public decimal ValorMercado { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        [DisplayName("Valor de la Poliza del Seguro")]
        [DisplayFormat(DataFormatString = "c", NullDisplayText = "", ApplyFormatInEditMode = false)]
        [UIHint("Money")]
        public decimal ValorPolizaSeguro { get; set; }

        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        [DisplayName("N° Poliza de Seguro")]
        //TODO: es necesario longitud (#66707785), sin embago en ticket #64305761 reportaron un contenido READONLY mayor a 50 (previo valor) 
        [StringLength(150)]

        public string NumeroPolizaSeguro { get; set; }

        [Required]
        [DisplayName("Tipo Póliza")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        public TipoPolizaViewModel TipoPoliza { get; set; }

        /// <summary>
        /// Fecha de registro inicial de la garantía (Issue Date)
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayName("Fecha Registro Inicial (Issue Date)")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        [Required]
        public DateTime? FechaRegistroInicial { get; set; }

        /// <summary>
        /// Fecha de formalizacion de la garantia (Efective Date)
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayName("Fecha Formalizacion (Effective Date)")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        [Required]
        public DateTime? FechaFormalizacion { get; set; }

        /// <summary>
        /// Fecha Vencimiento  de la Garantía (Expiry Date)
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayName("Fecha Vencimiento Garantia (Expiry Date)")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        [ShowInViewer]
        [Required]
        [UIHint("FechaVencimientoRiesgo")]
        public DateTime? FechaVencimientoGarantia { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Fecha Evaluacion / Renovacion")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        [UIHint("FechaUltimaRevisionEvaluacion")]
        public DateTime? FechaUltimaRevisionEvaluacion { get; set; }

        /// <summary>
        /// detalla el activo dado en garantía. Ej: , hipoteca: “inmueble industrial, ubicado … , tantos metros de área, etcétera..”, prenda de inventarios: “nro de sacas de café de 60kgs, tipo xxxx, bebida xxxx, etecétera”…
        /// </summary>
        [DataType(DataType.MultilineText)]
        [DisplayName("Descripcion de la Garantia")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]       
        public string DescripcionDeLaGarantia { get; set; }

        [DisplayName("ID Atomo Garantia")]
        [DataType(DataType.Text)]
        //[ReadOnly(true)]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        public int ID_Atomo { get; set; }

        /// <summary>
        ///  referencia de FCC por migracion
        /// </summary>
        [DisplayName("FCC Reference")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        [ShowInViewer]
        //TODO: confirmar si es necesario incorporar longitud de string
        [StringLength(50)]
        public string FCCReference { get; set; }

        [DisplayName("Attached to Line")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        public bool AttachedToLine { get; set; }

        /// <summary>
        /// Beneficiario de la garantia
        /// </summary>
        [DisplayName("Beneficiario")]
        [StringLength(105)]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        [UIHint("Autocomplete")]
        //TODO: confirmar si es necesario incorporar longitud de string
        public string Beneficiario { get; set; }

        [DisplayName("Nombre Evaluador")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        public ActorViewModel Evaluador { get; set; }

        [DisplayName("Nombre Administrador")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]        
        public ActorViewModel Administrador { get; set; }

        [DisplayName("Nombre Revisor/Validador")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        public ActorViewModel Revisor { get; set; }

        [DisplayName("Indicador de Atomo")]
        [EnumDataType(typeof(IndicadorAtomoEnum))]
        [UIHint("EnumControl")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        public IndicadorAtomoEnum IndAtomo { get; set; }

        [DisplayName("Tipo Garantia (Bladex)")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        [ShowInViewer]
        public TipoGarantiaBladexViewModel TipoGarantiaBladex { get; set; }

        [DisplayName("Categoria Riesgo Garantía (Risk Guarantee Category)")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        public CategoriaRiesgoGarantiaViewModel CategoriaRiesgoGarantia { get; set; }

        /// <summary>
        ///  (Necessary Value) Suma de todos las coberturas que tiene relacionadas de los prestamos
        /// </summary>
        [DataType(DataType.Currency)]
        [DisplayName("Valor Necesario de Garantia (Necessary Value)")]
        [UIHint("Money")]
        //[ReadOnly(true)]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        [ShowInViewer]
        public decimal ValorNecesarioDeGarantia { get; set; }

        /// <summary>
        /// Fecha Vencimiento Riesgo Garantizado (ClousureDate)
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayName("Fecha Vencimiento Riesgo (Closure Date)")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        public DateTime? FechaVencimientoRiesgo { get; set; }

        [DisplayName("Frecuencia Revisión/Evaluación/Validación (Review Frequency)")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        public FrecuenciasViewModel FrecuenciaRevision { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Proxima fecha Revision/Evaluacion/Validacion")]
        //[ReadOnly(true)]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        public DateTime? FechaProximaRevisionEvaluacion { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Fecha Vencimiento Seguro")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        public DateTime? FechaVencimientoSeguro { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Fecha de Comienzo de Ejecucion (Effective Date)")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        public DateTime? FechaComienzoEjecucion
        {
            get;
            set;
        }

        [DataType(DataType.Date)]
        [DisplayName("Fecha de Cierre de Ejecucion (Effective Date)")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        public DateTime? FechaCierreEjecucion
        {
            get;
            set;
        }

        [DisplayName("% Cobertura de Garantia")]
        [DataType(DataType.Text)]
        //[ReadOnly(true)]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        public double RatioCoberturaGarantia
        {
            get;
            set;
        }

        [DataType(DataType.MultilineText)]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        [StringLength(500)]
        public string Comentarios { get; set; }

        [DisplayName("Rating Garante (Calificadora)")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        public CalificacionesRiesgoViewModel RatingGarante { get; set; }

        [DisplayName("Status")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        public StatusViewModel Status { get; set; }

        //public IEnumerable<SelectListItem> StatusSelectList { get; set; }
        [DisplayName("Internal Status")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        public InternalStatusViewModel InternalStatus { get; set; }

        [DisplayName("Source")]
        [EnumDataType(typeof(GarantiaSourceEnum))]
        [UIHint("EnumControl")]
        //[ReadOnly(true)]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        public GarantiaSourceEnum? Source { get; set; }

        [DisplayName("% Reduccion Riesgo Pais")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        [UIHint("Percentage")]
        [Required]
        public decimal ReduccionDeRiesgoPorPais { get; set; }

        [DisplayName("Nombre del Organismo")]
        [Required]
        //[ReadOnly(true)]
        [UIHint("Autocomplete")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        //TODO: confirmar si es necesario incorporar longitud de string
        [StringLength(250)]
        public string NombreOrganismo { get; set; }

        [DisplayName("% Reduccion por Tipo Garantia")]
        [UIHint("Percentage")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        [Required]
        public decimal PorcentajeAplicableMitigacionSuperInt { get; set; }

        ///// <summary>
        ///// Numero de identificación/registro/certificado/notario conforme la garantía
        ///// </summary>
        //[DisplayName("Nro. Incidente Workflow")]
        //[DataType(DataType.Text)]
        //[StringLength(50)]
        //[Required()]
        //[IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        //public string IdentificadorGarantia { get; set; }


        /// <summary>
        /// Gets or sets the nro incidente workflow.
        /// </summary>
        /// <value>
        /// The nro incidente workflow of type <see cref="System.String"/>
        /// </value>
        [DisplayName("Nro. Incidente Workflow")]
        [DataType(DataType.Text)]
        [StringLength(50)]
        //[Required()]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        public string NroIncidenteWorkflow { get; set; }

        [DisplayName("Categoria Super")]
        [IsTooltip(true, "src:../../../Content/help-green.png", "alt:Tooltip", "class:tooltip")]
        [ShowInViewer]
        public CategoriaSuperViewModel CategoriaSuper { get; set; }

        public int? selectedOperationId
        {
            get;
            set;
        }
        
        #endregion
    }

    
}