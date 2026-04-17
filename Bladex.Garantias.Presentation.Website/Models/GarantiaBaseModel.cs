using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Infrastructure.DomainBase;
using Bladex.Garantias.Presentation.Website.Components.Attributes;
using Bladex.Garantias.Presentation.Website.ViewModels;

namespace Bladex.Garantias.Presentation.Website.Models
{
    /// <summary>
    /// The garantia base model class.
    /// </summary>
    [MetadataType(typeof(GarantiaBaseMetadata))]
    public class GarantiaBaseModel : IGarantiaBaseModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GarantiaBaseModel"/> class.
        /// </summary>
        public GarantiaBaseModel()
        {
            this.Init();
        }

        /// <summary>
        /// Inits this instance.
        /// </summary>
        public virtual void Init()
        {
            this.Administrador = new ActorViewModel();
            this.Asegurador = new ActorViewModel();
            this.CategoriaRiesgoGarantia = new CategoriaRiesgoGarantiaViewModel();
            this.CategoriaSuper = new CategoriaSuperViewModel();
            this.Cliente = new ClienteViewModel();
            this.Depositante = new ActorViewModel();
            this.Evaluador = new ActorViewModel();
            this.FiduciariaSuper = new FiduciariasViewModel();
            this.FrecuenciaRevision = new FrecuenciasViewModel();
            this.Garante = new ClienteViewModel();
            this.Moneda = new MonedasViewModel();
            this.PaisGarantia = new PaisViewModel();
            this.Region = new RegionViewModel();
            this.TipoPoliza = new TipoPolizaViewModel();
            this.RatingGarante = new CalificacionesRiesgoViewModel();
            this.Revisor = new ActorViewModel();
            this.InternalStatus = new InternalStatusViewModel();
            this.Status = new StatusViewModel();
            this.InternalStatus = new InternalStatusViewModel();
            this.TipoGarantiaBladex = new TipoGarantiaBladexViewModel();
            this.TipoGarantiaSuper = new TipoGarantiaSuperViewModel();
            this.Source = default(GarantiaSourceEnum);
            this.IndAtomo = default(IndicadorAtomoEnum);
        }

        #region Implementation of IGarantiaBaseModel
        public string BusinessError { get; set; }
        public int? Key { get; set; }

        /// <summary>
        ///  referencia de FCC por migracion
        /// </summary>
        [MaxLength(50)]
        
        public string FCCReference { get; set; }
        [MaxLength(250)]
        [AllowHtml]
        public string Beneficiario { get; set; }
        /// <summary>
        /// Gets or sets the identificacion documento garantia.
        /// </summary>
        /// <value>
        /// The identificacion documento garantia of type <see cref="System.String"/>
        /// </value>
        [AllowHtml]
        public string IdentificacionDocumentoGarantia { get; set; }
        /// <summary>
        /// Gets or sets the nombre organismo.
        /// </summary>
        /// <value>
        /// The nombre organismo of type <see cref="System.String"/>
        /// </value>
        [MaxLength(250)]        
        public string NombreOrganismo { get; set; }
        /// <summary>
        /// Gets or sets the valor garantia super intendencia.
        /// </summary>
        /// <value>
        /// The valor garantia super intendencia of type <see cref="System.Decimal"/>
        /// </value>
        public decimal ValorGarantiaSuperIntendencia { get; set; }
        /// <summary>
        /// Gets or sets the ratio cobertura garantia.
        /// </summary>
        /// <value>
        /// The ratio cobertura garantia of type <see cref="System.Double"/>
        /// </value>
        public double RatioCoberturaGarantia { get; set; }

        ///// <summary>
        ///// Numero de identificación/registro/certificado/notario conforme la garantía
        ///// </summary>
        //public string IdentificadorGarantia { get; set; }

        /// <summary>
        /// Gets or sets the nro incidente workflow.
        /// </summary>
        /// <value>
        /// The nro incidente workflow of type <see cref="System.String"/>
        /// </value>
        [AllowHtml]
        public string NroIncidenteWorkflow { get; set; }

        /// <summary>
        /// 027 - el numero que asigno la super 
        /// </summary>
        [MaxLength(5)] 
        public string CodigoBanco { get; set; }

        /// <summary>
        /// Cedula o RUC de la empresa (SUPER)
        /// </summary>
        /// 
        [MaxLength(50)]
        [AllowHtml]
        public string IdentificacionFideicomiso { get; set; }

        /// <summary>
        /// Informacion interna de la ficuciaria
        /// </summary>
        [MaxLength(50)]
        [AllowHtml]
        public string FiduciariaBladex { get; set; }

        /// <summary>
        /// Local/Extrangero (si es panama es local)
        /// </summary>
        [MaxLength(1)] 
        public string OrigenGarantia { get; set; }
        public decimal ValorInicial { get; set; }

        /// <summary>
        /// detalla el activo dado en garantía. Ej: , hipoteca: “inmueble industrial, ubicado … , tantos metros de área, etcétera..”, prenda de inventarios: “nro de sacas de café de 60kgs, tipo xxxx, bebida xxxx, etecétera”…
        /// </summary>
        [AllowHtml]
        public string DescripcionDeLaGarantia { get; set; }

        /// <summary>
        /// Fecha de registro inicial de la garantía (Issue Date)
        /// </summary>
        public DateTime? FechaRegistroInicial { get; set; }

        /// <summary>
        /// Fecha de formalizacion de la garantia (Efective Date)
        /// </summary>
        public DateTime? FechaFormalizacion { get; set; }

        /// <summary>
        /// Fecha Vencimiento Riesgo Garantizado (ClousureDate)
        /// </summary>
        public DateTime? FechaVencimientoRiesgo { get; set; }

        /// <summary>
        /// Fecha Vencimiento  de la Garantía (Expiry Date)
        /// </summary>
        public DateTime? FechaVencimientoGarantia { get; set; }
        public DateTime? FechaUltimaRevisionEvaluacion { get; set; }
        public DateTime? FechaProximaRevisionEvaluacion { get; set; }
        public DateTime? FechaVencimientoSeguro { get; set; }
        public decimal ReduccionDeRiesgoPorPais { get; set; }

        /// <summary>
        ///  (Necessary Value) Suma de todos las coberturas que tiene relacionadas de los prestamos
        /// </summary>
        public decimal ValorNecesarioDeGarantia { get; set; }

        
        public decimal PorcentajeAplicableMitigacionSuperInt { get; set; }
        [MaxLength(500)]
        [AllowHtml]
        public string Comentarios { get; set; }
        public decimal ValorPolizaSeguro { get; set; }
        [MaxLength(150)]
        [AllowHtml]
        public string NumeroPolizaSeguro { get; set; }

        /// <summary>
        /// Valor de Mercado ultima revision
        /// </summary>
        public decimal ValorMercado { get; set; }
        
        public int ID_Atomo { get; set; }
        //public IEnumerable<SelectListItem> ClienteSelectList { get; set; }

        /// <summary>
        /// Cliente quien aplica la garantia
        /// </summary>
        public ClienteViewModel Cliente { get; set; }


        public bool AttachedToLine { get; set; }

        //public IEnumerable<SelectListItem> GaranteSelectList { get; set; }
        public ClienteViewModel Garante { get; set; }
        //public IEnumerable<SelectListItem> FiduciariaSuperSelectList { get; set; }

        /// <summary>
        /// Codigo catalogo SB56 (Fiduciarias)
        /// </summary>        
        public FiduciariasViewModel FiduciariaSuper { get; set; }
        //public IEnumerable<SelectListItem> DepositanteSelectList { get; set; }

        /// <summary>
        /// Nombre Depositante/Custodio/Fiel
        /// </summary>
        public ActorViewModel Depositante { get; set; }
        //public IEnumerable<SelectListItem> EvaluadorSelectList { get; set; }

        /// <summary>
        /// Nombre Evaluador
        /// </summary>
        public ActorViewModel Evaluador { get; set; }
        //public IEnumerable<SelectListItem> AdministradorSelectList { get; set; }

        /// <summary>
        /// Nombre Administrador
        /// </summary>
        public ActorViewModel Administrador { get; set; }
        //public IEnumerable<SelectListItem> AseguradorSelectList { get; set; }

        
        public ActorViewModel Asegurador { get; set; }
        //public IEnumerable<SelectListItem> RevisorSelectList { get; set; }
        
        public ActorViewModel Revisor { get; set; }
        //public IEnumerable<SelectListItem> PaisGarantiaSelectList { get; set; }

        /// <summary>
        /// Se debe colocar donde se encuentra fisicamente la garantia o garante
        /// </summary>
        public PaisViewModel PaisGarantia { get; set; }
        //public IEnumerable<SelectListItem> TipoGarantiaSuperSelectList { get; set; }

        /// <summary>
        /// Corresponde a un codigo de una provincia de Panama unicamente.
        /// </summary>
        public RegionViewModel Region { get; set; }

        /// <summary>
        /// Corresponde a un tipo de poliza Individual/Colectiva.
        /// </summary>
        public TipoPolizaViewModel TipoPoliza { get; set; }

        /// <summary>
        /// Catalogo SB59 *opcional en caso que las garantias no posea categorizacion. Si no posee codigo no se registra contablemente
        /// </summary>
        public TipoGarantiaSuperViewModel TipoGarantiaSuper { get; set; }
        //public IEnumerable<SelectListItem> TipoGarantiaBladexSelectList { get; set; }

        /// <summary>
        /// Tipo de garantia asignada por bladex cuando no existe clasificacion
        /// 
        /// Restriccion: TipoGarantiaSuper.Categoria = CategoriaSuper
        /// 
        /// </summary>
        public TipoGarantiaBladexViewModel TipoGarantiaBladex { get; set; }
        //public IEnumerable<SelectListItem> FrecuenciaRevisionSelectList { get; set; }
        public FrecuenciasViewModel FrecuenciaRevision { get; set; }
        //public IEnumerable<SelectListItem> CategoriaRiesgoGarantiaSelectList { get; set; }

        /// <summary>
        /// Catalogo Categoria Garantias Riesgo
        /// </summary>
        public CategoriaRiesgoGarantiaViewModel CategoriaRiesgoGarantia { get; set; }
        //public IEnumerable<SelectListItem> MonedaSelectList { get; set; }
        public MonedasViewModel Moneda { get; set; }
        
        /// <summary>
        /// Tabla SB25
        /// </summary>
        public CalificacionesRiesgoViewModel RatingGarante { get; set; }
        //public IEnumerable<SelectListItem> CategoriaSuperSelectList { get; set; }

        /// <summary>
        /// Guarda la categoria de la super. es importante ya que toda la logica de campo se rige de este valor.
        /// </summary>
        public CategoriaSuperViewModel CategoriaSuper { get; set; }
        //public IEnumerable<SelectListItem> StatusSelectList { get; set; }
        public InternalStatusViewModel InternalStatus { get; set; }
        public StatusViewModel Status { get; set; }
        public GarantiaSourceEnum? Source { get; set; }
        
        public IndicadorAtomoEnum IndAtomo { get; set; }


        /// <summary>
        /// Gets or sets the fecha comienzo ejecucion.
        /// </summary>
        /// <value>
        /// The fecha comienzo ejecucion.
        /// </value>
        public DateTime? FechaComienzoEjecucion
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
        public DateTime? FechaCierreEjecucion
        {
            get;
            set;
        }

        public int? selectedOperationId 
        {
            get; 
            set; 
        }

        #endregion

        /// <summary>
        /// Gets the properties.
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, object> GetProperties()
        {
            var properties = new Dictionary<string, object>();
            // try to get a metadata attribute if its specified.
            var metadataTypeAttr = (MetadataTypeAttribute) this.GetType().GetCustomAttributes(typeof(MetadataTypeAttribute), true).FirstOrDefault();
            // if not specified use the type of the current object else use the type of the metadata class.
            Type typeToInspect = metadataTypeAttr != null ? metadataTypeAttr.MetadataClassType : this.GetType();
            // iterate through type properties
            foreach (PropertyInfo propertyInfo in typeToInspect.GetProperties().Where(o => o.GetCustomAttributes(typeof(ExcludeAttribute), true).Count() == 0).OrderBy(x=> x.DeclaringType.FullName)) // .OrderBy(o => o.Name)    
            {
                // lookup for a display name attribute
                var displayName = (DisplayNameAttribute)propertyInfo.GetCustomAttributes(typeof(DisplayNameAttribute),true).FirstOrDefault();
                // lookup from a required attribute
                var isRequired = propertyInfo.GetCustomAttributes(typeof(RequiredAttribute), true).FirstOrDefault() == null ? false : true;
                // in case that we have a display name attribute use his value else use the property name.
                string fieldName = displayName != null ? displayName.DisplayName : propertyInfo.Name;
                // if the field is required, append an asterisk to the display name
                if (isRequired)
                    fieldName = string.Concat(fieldName, " (*)");
                // lookup for a data typa attribute to format the values
                var dataType = (DataTypeAttribute)propertyInfo.GetCustomAttributes(typeof(DataTypeAttribute), true).FirstOrDefault();
                object value = this.GetType().GetProperty(propertyInfo.Name).GetValue(this, null);

                // Agrega asterisco a campos que deben ir en requeridos pero no tienen atributo required.
                fieldName = this.GetFieldWithAsterisk(fieldName);

                // Fix si es Garantia en otro Banco u Otas Garantias, cambiar el label de "N° de Poliza", a "Ref. Deposito Otro Banco"
                //se modifca por req ticket #1624 solo se cambia cuando es deposito otro banco
                if ((propertyInfo.ReflectedType.FullName.Contains("GarantiaDepositoOtroBancoMetadata"))
                    && fieldName == "N° Poliza de Seguro (*)")
                {
                    fieldName = "Ref. Deposito Otro Banco";
                }

                // check for Enum values, check for datatype and Display format and add the property with his value to the dictionary.
                if(fieldName!="Inclusion de Avales adicionales" && fieldName!="Avales")
                    properties.Add(fieldName, value is IList ? (value as IList).Count.ToString() : value is Enum ? (value as Enum).ToString() : dataType != null && dataType.DisplayFormat != null ? string.Format(dataType.DisplayFormat.DataFormatString, value) : value);
            }

            return properties;
        }

        public string GetFieldWithAsterisk(string propertyName)
        {
            // Propiedades de la clase base. (comunes para todas las garantias)
            List<string> garantiaBaseFields = new List<string>() 
            { 
                "Nro Identificador Garantia", 
                "Identificacion Documento Garantia", 
                "Nombre Fiduciaria (Super)", 
                "Nombre Depositante/Custodio/Fiel",
                "Fecha Evaluacion / Renovacion",
                "N° Poliza de Seguro"
            };
            
            // Propiedades de GarantiaMueble
            garantiaBaseFields.Add("Asegurador Super");

            foreach (string property in garantiaBaseFields)
            {
                if (property == propertyName)
                {
                    propertyName = string.Format("{0} {1}", property, "(*)");
                }
            }

            return propertyName;
        }

        /// <summary>
        /// Gets the properties for the maker checker changeset viewer.
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, object> GetPropertiesForViewer()
        {
            var properties = new Dictionary<string, object>();
            // try to get a metadata attribute if its specified.
            var metadataTypeAttr = (MetadataTypeAttribute)this.GetType().GetCustomAttributes(typeof(MetadataTypeAttribute), true).FirstOrDefault();
            // if not specified use the type of the current object else use the type of the metadata class.
            Type typeToInspect = metadataTypeAttr != null ? metadataTypeAttr.MetadataClassType : this.GetType();
            // iterate through type properties
            foreach (PropertyInfo propertyInfo in typeToInspect.GetProperties().Where(o => o.GetCustomAttributes(typeof(ExcludeAttribute), true).Count() == 0 && o.GetCustomAttributes(typeof(ShowInViewerAttribute), true).Count() > 0).OrderBy(o => o.Name))
            {
                // lookup for a display name attribute
                var displayName = (DisplayNameAttribute)propertyInfo.GetCustomAttributes(typeof(DisplayNameAttribute), true).FirstOrDefault();
                // lookup from a required attribute
                var isRequired = propertyInfo.GetCustomAttributes(typeof(RequiredAttribute), true).FirstOrDefault() == null ? false : true;
                // in case that we have a display name attribute use his value else use the property name.
                string fieldName = displayName != null ? displayName.DisplayName : propertyInfo.Name;
                // if the field is required, append an asterisk to the display name
                if (isRequired)
                    fieldName = string.Concat(fieldName, " (*)");
                // lookup for a data typa attribute to format the values
                var dataType = (DataTypeAttribute)propertyInfo.GetCustomAttributes(typeof(DataTypeAttribute), true).FirstOrDefault();
                object value = this.GetType().GetProperty(propertyInfo.Name).GetValue(this, null);
                // check for Enum values, check for datatype and Display format and add the property with his value to the dictionary.
                properties.Add(fieldName, value is IList ? (value as IList).Count.ToString() : value is Enum ? (value as Enum).ToString() : dataType != null && dataType.DisplayFormat != null ? string.Format(dataType.DisplayFormat.DataFormatString, value) : value);
                //properties.Add("IsRequired", isRequired);
            }
            return properties;
        }

        /// <summary>
        /// Gets the display name attribute value of this class.
        /// </summary>
        /// <returns></returns>
        public string GetDisplayName()
        {
            MetadataTypeAttribute metadataType = (MetadataTypeAttribute)this.GetType().GetCustomAttributes(typeof(MetadataTypeAttribute), true).FirstOrDefault();
            Type typeToInspect = metadataType != null ? metadataType.MetadataClassType : this.GetType();
            DisplayNameAttribute displayNameAttr = (DisplayNameAttribute)typeToInspect.GetCustomAttributes(typeof(DisplayNameAttribute), true).FirstOrDefault();
            return displayNameAttr == null ? this.GetType().Name : displayNameAttr.DisplayName;
        }
    }
}