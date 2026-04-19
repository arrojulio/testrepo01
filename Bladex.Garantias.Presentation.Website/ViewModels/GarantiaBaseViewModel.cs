using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bladex.Garantias.Application.Facades;
using Bladex.Garantias.Infrastructure.UI;

namespace Bladex.Garantias.Presentation.Website.ViewModels
{
    /// <summary>
    /// The garantia base view model class.
    /// </summary>
    public class GarantiaBaseViewModel : IGarantiaView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GarantiaBaseViewModel"/> class.
        /// </summary>
        public GarantiaBaseViewModel()
        {
            this.BancoSuperSelected = new BancosViewModel();
            this.ClienteSelected = new ClienteViewModel();
            this.PaisGarantiaSelected = new PaisViewModel();
            this.RegionSelected = new RegionViewModel();
            this.AvaluadorasSelected = new AvaluadorasViewModel();
            this.AdministradorSelected = new ActorViewModel();
            this.AseguradorSelected = new ActorViewModel();
            this.BancosListSelected = new List<BancosViewModel>() { new BancosViewModel() };
            this.CalificacionesRiesgoSelected = new CalificacionesRiesgoViewModel();
            this.CategoriaRiesgoGarantiaSelected = new CategoriaRiesgoGarantiaViewModel();
            this.CategoriaSuperSelected = new CategoriaSuperViewModel();
            this.DepositanteSelected = new ActorViewModel();
            this.EvaluadorSelected = new ActorViewModel();
            this.FrencuenciasSelected = new FrecuenciasViewModel();
            this.FrequenciaRevisionSelected = new FrecuenciasViewModel();
            this.GaranteSelected = new ClienteViewModel();
            this.InstrumentoFinancieroSelected = new InstrumentoFinancieroViewModel();
            this.MonedaSelected = new MonedasViewModel();
            this.RatingGaranteSelected = new CalificacionesRiesgoViewModel();
            this.RevisorSelected = new ActorViewModel();
            this.InternalStatusSelected = new InternalStatusViewModel();
            this.StatusSelected = new StatusViewModel();
            this.TipoGarantiaBladexSelected = new TipoGarantiaBladexViewModel();
            this.TipoGarantiaSuperSelected = new TipoGarantiaSuperViewModel();
            this.Comentarios = string.Empty;
        }
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key of type <see cref="System.String"/>
        /// </value>
        [HiddenInput]
        [ReadOnly(true)]
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the business error.
        /// </summary>
        /// <value>
        /// The business error of type <see cref="System.String"/>
        /// </value>
        public string BusinessError { get; set; }

        /// <summary>
        /// Gets or sets the FCC reference.
        /// </summary>
        /// <value>
        /// The FCC reference of type <see cref="System.String"/>
        /// </value>
        [DisplayName("Referencia a FCC")]
        public string FCCReference { get; set; }

        /// <summary>
        /// Gets or sets the identificador garantia.
        /// </summary>
        /// <value>
        /// The identificador garantia of type <see cref="System.String"/>
        /// </value>
        [DisplayName("Identificador de la garantia")]
        public string IdentificadorGarantia { get; set; }

        /// <summary>
        /// Gets or sets the codigo banco.
        /// </summary>
        /// <value>
        /// The codigo banco of type <see cref="System.String"/>
        /// </value>
        [DisplayName("Codigo del Banco")]
        public string CodigoBanco { get; set; }

        /// <summary>
        /// Gets or sets the cliente selected.
        /// </summary>
        /// <value>
        /// The cliente selected of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.ClienteViewModel"/>
        /// </value>
        [DisplayName("Cliente")]
        public ClienteViewModel ClienteSelected { get; set; }

        /// <summary>
        /// Gets or sets the identificacion fideicomiso.
        /// </summary>
        /// <value>
        /// The identificacion fideicomiso of type <see cref="System.String"/>
        /// </value>
        [DisplayName("Identificador del Fideicomiso")]
        public string IdentificacionFideicomiso { get; set; }

        /// <summary>
        /// Gets or sets the fiduciaria super selected.
        /// </summary>
        /// <value>
        /// The fiduciaria super selected of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.FiduciariasViewModel"/>
        /// </value>
        [DisplayName("Fiduciaria Super")]
        public FiduciariasViewModel FiduciariaSuperSelected { get; set; }

        /// <summary>
        /// Gets or sets the fiduciaria bladex.
        /// </summary>
        /// <value>
        /// The fiduciaria bladex of type <see cref="System.String"/>
        /// </value>
        [DisplayName("Fiduciaria Bladex")]
        public string FiduciariaBladex { get; set; }

        /// <summary>
        /// Gets or sets the depositante selected.
        /// </summary>
        /// <value>
        /// The depositante selected of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.ActorViewModel"/>
        /// </value>
        [DisplayName("Depositante")]
        public ActorViewModel DepositanteSelected { get; set; }

        /// <summary>
        /// Gets or sets the evaluador selected.
        /// </summary>
        /// <value>
        /// The evaluador selected of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.ActorViewModel"/>
        /// </value>
        [DisplayName("Evaluador")]
        public ActorViewModel EvaluadorSelected { get; set; }

        /// <summary>
        /// Gets or sets the administrador selected.
        /// </summary>
        /// <value>
        /// The administrador selected of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.ActorViewModel"/>
        /// </value>
        [DisplayName("Administrador")]
        public ActorViewModel AdministradorSelected { get; set; }

        /// <summary>
        /// Gets or sets the asegurador selected.
        /// </summary>
        /// <value>
        /// The asegurador selected of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.ActorViewModel"/>
        /// </value>
        [DisplayName("Asegurador")]
        public ActorViewModel AseguradorSelected { get; set; }

        /// <summary>
        /// Gets or sets the revisor selected.
        /// </summary>
        /// <value>
        /// The revisor selected of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.ActorViewModel"/>
        /// </value>
        [DisplayName("Revisor")]
        public ActorViewModel RevisorSelected { get; set; }

        /// <summary>
        /// Gets or sets the origen garantia.
        /// </summary>
        /// <value>
        /// The origen garantia of type <see cref="System.String"/>
        /// </value>
        [DisplayName("Origen de la garantia")]
        public string OrigenGarantia { get; set; }

        /// <summary>
        /// Gets or sets the pais garantia selected.
        /// </summary>
        /// <value>
        /// The pais garantia selected of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.PaisViewModel"/>
        /// </value>
        [DisplayName("Pais de la garantia")]
        public PaisViewModel PaisGarantiaSelected { get; set; }

        /// <summary>
        /// Gets or sets the Región garantia selected.
        /// </summary>
        /// <value>
        /// The Región garantia selected of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.RegionViewModel"/>
        /// </value>
        [DisplayName("Cod Región")]
        public RegionViewModel RegionSelected { get; set; }

        /// <summary>
        /// Gets or sets the banco super selected.
        /// </summary>
        /// <value>
        /// The banco super selected of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.BancosViewModel"/>
        /// </value>
        [DisplayName("Banco Super de la garantia")]
        public BancosViewModel BancoSuperSelected { get; set; }

        /// <summary>
        /// Gets or sets the tipo garantia super selected.
        /// </summary>
        /// <value>
        /// The tipo garantia super selected of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.TipoGarantiaSuperViewModel"/>
        /// </value>
        [DisplayName("Tipo de garantia Super")]
        public TipoGarantiaSuperViewModel TipoGarantiaSuperSelected { get; set; }

        /// <summary>
        /// Gets or sets the tipo garantia bladex selected.
        /// </summary>
        /// <value>
        /// The tipo garantia bladex selected of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.TipoGarantiaBladexViewModel"/>
        /// </value>
        [DisplayName("Tipo de garantia Bladex")]
        public TipoGarantiaBladexViewModel TipoGarantiaBladexSelected { get; set; }

        /// <summary>
        /// Gets or sets the valor inicial.
        /// </summary>
        /// <value>
        /// The valor inicial of type <see cref="System.String"/>
        /// </value>
        [DisplayName("Valor inicial")]
        [DataType(DataType.Currency)]
        public string ValorInicial { get; set; }

        /// <summary>
        /// Gets or sets the descripcion de la garantia.
        /// </summary>
        /// <value>
        /// The descripcion de la garantia of type <see cref="System.String"/>
        /// </value>
        [DisplayName("Descripcion de la garantia")]
        public string DescripcionDeLaGarantia { get; set; }

        /// <summary>
        /// Gets or sets the garante selected.
        /// </summary>
        /// <value>
        /// The garante selected of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.ClienteViewModel"/>
        /// </value>
        [DisplayName("Garante")]
        public ClienteViewModel GaranteSelected { get; set; }

        /// <summary>
        /// Gets or sets the fecha registro inicial.
        /// </summary>
        /// <value>
        /// The fecha registro inicial of type <see cref="DateTime"/>
        /// </value>
        [DisplayName("Registro inicial")]
        [UIHint("DateTime")]
        [DataType(DataType.Date)]
        public DateTime? FechaRegistroInicial { get; set; }

        /// <summary>
        /// Gets or sets the fecha formalizacion.
        /// </summary>
        /// <value>
        /// The fecha formalizacion of type <see cref="System.DateTime"/>
        /// </value>
        [DisplayName("Formalizacion")]
        [UIHint("DateTime")]
        [DataType(DataType.Date)]
        public DateTime FechaFormalizacion { get; set; }

        /// <summary>
        /// Gets or sets the fecha vencimiento riesgo.
        /// </summary>
        /// <value>
        /// The fecha vencimiento riesgo of type <see cref="System.DateTime"/>
        /// </value>
        [DisplayName("Vencimiento de riesgo")]
        [UIHint("DateTime")]
        [DataType(DataType.Date)]
        public DateTime FechaVencimientoRiesgo { get; set; }

        /// <summary>
        /// Gets or sets the fecha vencimiento garantia.
        /// </summary>
        /// <value>
        /// The fecha vencimiento garantia of type <see cref="System.DateTime"/>
        /// </value>
        [DisplayName("Vencimiento de garantia")]
        [UIHint("DateTime")]
        [DataType(DataType.Date)]
        public DateTime FechaVencimientoGarantia { get; set; }

        /// <summary>
        /// Gets or sets the fecha ultima revision evaluacion.
        /// </summary>
        /// <value>
        /// The fecha ultima revision evaluacion of type <see cref="System.DateTime"/>
        /// </value>
        [DisplayName("Ultima revision de la evaluacion")]
        [UIHint("DateTime")]
        [DataType(DataType.Date)]
        public DateTime FechaUltimaRevisionEvaluacion { get; set;}

        [DataType(DataType.Date)]
        [DisplayName("Fecha de Comienzo de Ejecucion (Effective Date)")]
        [UIHint("DateTime")]
        public DateTime FechaComienzoEjecucion
        {
            get;
            set;
        }

        [DataType(DataType.Date)]
        [DisplayName("Fecha de Cierre de Ejecucion (Effective Date)")]
        [UIHint("DateTime")]
        public DateTime FechaCierreEjecucion
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the fecha proxima revision evaluacion.
        /// </summary>
        /// <value>
        /// The fecha proxima revision evaluacion of type <see cref="System.DateTime"/>
        /// </value>
        [DisplayName("Proxima revision de la evaluacion")]
        [UIHint("DateTime")]
        [DataType(DataType.Date)]
        public DateTime FechaProximaRevisionEvaluacion { get; set; }

        /// <summary>
        /// Gets or sets the frequencia revision selected.
        /// </summary>
        /// <value>
        /// The frequencia revision selected of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.FrecuenciasViewModel"/>
        /// </value>
        [DisplayName("Frecuencia de revision")]
        public FrecuenciasViewModel FrequenciaRevisionSelected { get; set; }

        /// <summary>
        /// Gets or sets the fecha vencimiento seguro.
        /// </summary>
        /// <value>
        /// The fecha vencimiento seguro of type <see cref="System.DateTime"/>
        /// </value>
        [DisplayName("Vencimiento del seguro")]
        [DataType(DataType.Date)]
        public DateTime FechaVencimientoSeguro { get; set; }

        /// <summary>
        /// Gets or sets the categoria riesgo garantia selected.
        /// </summary>
        /// <value>
        /// The categoria riesgo garantia selected of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.CategoriaRiesgoGarantiaViewModel"/>
        /// </value>
        [DisplayName("Categoria del riesgo de la garantia")]
        public CategoriaRiesgoGarantiaViewModel CategoriaRiesgoGarantiaSelected { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [reduccion de riesgo por pais].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [reduccion de riesgo por pais]; otherwise, <c>false</c>.
        /// </value>
        [DisplayName("Reduccion de riesgo por pais?")]
        public bool ReduccionDeRiesgoPorPais { get; set; }

        /// <summary>
        /// Gets or sets the moneda selected.
        /// </summary>
        /// <value>
        /// The moneda selected of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.MonedasViewModel"/>
        /// </value>
        [DisplayName("Moneda")]
        public MonedasViewModel MonedaSelected { get; set; }

        /// <summary>
        /// Gets or sets the valor necesario de garantia.
        /// </summary>
        /// <value>
        /// The valor necesario de garantia of type <see cref="System.String"/>
        /// </value>
        [DisplayName("Valor necesario de garantia")]
        [DataType(DataType.Currency)]
        public string ValorNecesarioDeGarantia { get; set; }

        /// <summary>
        /// Gets or sets the porcentaje aplicable mitigacion super int.
        /// </summary>
        /// <value>
        /// The porcentaje aplicable mitigacion super int of type <see cref="System.Decimal"/>
        /// </value>
        [DisplayName("Mitigacion Super Intendencia (%)")]
        public decimal PorcentajeAplicableMitigacionSuperInt { get; set; }

        /// <summary>
        /// Gets or sets the comentarios.
        /// </summary>
        /// <value>
        /// The comentarios of type <see cref="System.String"/>
        /// </value>
        [DisplayName("Comentarios")]
        [DataType(DataType.MultilineText)]
        public string Comentarios { get; set; }

        /// <summary>
        /// Gets or sets the rating garante selected.
        /// </summary>
        /// <value>
        /// The rating garante selected of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.CalificacionesRiesgoViewModel"/>
        /// </value>
        [DisplayName("Rating del garante")]
        public CalificacionesRiesgoViewModel RatingGaranteSelected { get; set; }

        /// <summary>
        /// Gets or sets the valor poliza seguro.
        /// </summary>
        /// <value>
        /// The valor poliza seguro of type <see cref="System.String"/>
        /// </value>
        [DisplayName("Valor poliza de seguro")]
        [DataType(DataType.Currency)]
        public string ValorPolizaSeguro { get; set; }

        /// <summary>
        /// Gets or sets the numero poliza seguro.
        /// </summary>
        /// <value>
        /// The numero poliza seguro of type <see cref="System.String"/>
        /// </value>
        [DisplayName("N° de poliza de seguro")]
        public string NumeroPolizaSeguro { get; set; }

        /// <summary>
        /// Gets or sets the valor mercado.
        /// </summary>
        /// <value>
        /// The valor mercado of type <see cref="System.String"/>
        /// </value>
        [DisplayName("Valor de mercado")]
        [DataType(DataType.Currency)]
        public string ValorMercado { get; set; }

        /// <summary>
        /// Gets or sets the categoria super selected.
        /// </summary>
        /// <value>
        /// The categoria super selected of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.CategoriaSuperViewModel"/>
        /// </value>
        [DisplayName("Categoria super")]
        public CategoriaSuperViewModel CategoriaSuperSelected { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [attached to line].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [attached to line]; otherwise, <c>false</c>.
        /// </value>
        [DisplayName("Attached to line")]
        public bool AttachedToLine { get; set; }

        /// <summary>
        /// Gets or sets the I d_ atomo.
        /// </summary>
        /// <value>
        /// The I d_ atomo of type <see cref="System.Int32"/>
        /// </value>
        [DisplayName("Id del atomo")]
        public int ID_Atomo { get; set; }

        //[DisplayName("Fiduciaria")]
        //public FiduciariasViewModel FiduciariasSelected {get;set;}

        /// <summary>
        /// Gets or sets the instrumento financiero selected.
        /// </summary>
        /// <value>
        /// The instrumento financiero selected of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.InstrumentoFinancieroViewModel"/>
        /// </value>
        [DisplayName("Instrumento financiero")]
        public InstrumentoFinancieroViewModel InstrumentoFinancieroSelected { get; set; }

        /// <summary>
        /// Gets or sets the calificaciones riesgo selected.
        /// </summary>
        /// <value>
        /// The calificaciones riesgo selected of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.CalificacionesRiesgoViewModel"/>
        /// </value>
        [DisplayName("Calificacion de riesgo")]
        public CalificacionesRiesgoViewModel CalificacionesRiesgoSelected { get; set; }

        /// <summary>
        /// Gets or sets the frencuencias selected.
        /// </summary>
        /// <value>
        /// The frencuencias selected of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.FrecuenciasViewModel"/>
        /// </value>
        [DisplayName("Frecuencia")]
        public FrecuenciasViewModel FrencuenciasSelected { get; set; }

        //[DisplayName("Monedas")]
        //public MonedasViewModel MonedasSelected { get; set; }

        /// <summary>
        /// Gets or sets the avaluadoras selected.
        /// </summary>
        /// <value>
        /// The avaluadoras selected of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.AvaluadorasViewModel"/>
        /// </value>
        [DisplayName("Avaluadora")]
        public AvaluadorasViewModel AvaluadorasSelected {get;set;}

        /// <summary>
        /// Gets or sets the bancos list selected.
        /// </summary>
        /// <value>
        /// The bancos list selected of type <see cref="System.Collections.Generic.List&lt;Bladex.Garantias.Presentation.Website.ViewModels.BancosViewModel&gt;"/>
        /// </value>
        [DisplayName("Bancos")]
        [UIHint("BancosViewModel")]
        public List<BancosViewModel> BancosListSelected { get; set; }

        //[DisplayName("Actor")]
        //public ActorViewModel ActorSelected { get; set; }

        /// <summary>
        /// Gets or sets the internal status selected.
        /// </summary>
        /// <value>
        /// The internal status selected of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.InternalStatusViewModel"/>
        /// </value>
        [DisplayName("Internal Status")]
        public InternalStatusViewModel InternalStatusSelected { get; set; }

        /// <summary>
        /// Gets or sets the status selected.
        /// </summary>
        /// <value>
        /// The status selected of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.InternalStatusViewModel"/>
        /// </value>
        [DisplayName("Status")]
        public StatusViewModel StatusSelected { get; set; }

        #region IGarantiaView Members

        /// <summary>
        /// Saves the view
        /// </summary>
        public virtual void Save()
        {
            
        }

        /// <summary>
        /// Reset the state of the view.
        /// </summary>
        public virtual void Reset()
        {
            
        }

        /// <summary>
        /// Initialize the view
        /// </summary>
        public virtual void Init()
        {
            
        }

        

        #endregion

        #region IGarantiaView Members


        /// <summary>
        /// Loads the view with the guarantee specified by the key.
        /// </summary>
        /// <param name="garantia"><see cref="Bladex.Garantias.DomainModel.DomainBase.GarantiaBase"/></param>
        public virtual void Load(DomainModel.DomainBase.GarantiaBase garantia)
        {
            
        }

        #endregion
    }

    
}