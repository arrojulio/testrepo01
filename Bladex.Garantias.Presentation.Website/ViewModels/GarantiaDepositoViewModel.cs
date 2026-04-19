using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bladex.Garantias.Application.Facades;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Presentation.Website.Models;


namespace Bladex.Garantias.Presentation.Website.ViewModels
{

    /// <summary>
    /// The garantia deposito view model class.
    /// </summary>
    public class GarantiaDepositoViewModel : GarantiaBaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GarantiaDepositoViewModel"/> class.
        /// </summary>
        public GarantiaDepositoViewModel()
        {
            this.Garantia = new GarantiaDepositoModel();
        }

        #region GarantiaBase Fields
        /// <summary>
        /// Gets or sets the garantia.
        /// </summary>
        /// <value>
        /// The garantia of type <see cref="Bladex.Garantias.Presentation.Website.Models.GarantiaDepositoModel"/>
        /// </value>
        [DisplayName("Depósito Pignorado en el Banco")]
        public GarantiaDepositoModel Garantia { get; set; }

        //[HiddenInput]
        //[ReadOnly(true)]
        //public string Key { get; set; }

        //[DisplayName("Referencia a FCC")]
        //public string FCCReference { get; set; }

        //[DisplayName("Identificador de la garantia")]
        //public string IdentificadorGarantia { get; set; }

        //[DisplayName("Codigo del Banco")]
        //public string CodigoBanco { get; set; }

        //[DisplayName("Cliente")]
        //public string ClienteKey { get; set; }

        

        //[DisplayName("Identificador del Fideicomiso")]
        //public string IdentificacionFideicomiso { get; set; }

        //[DisplayName("Fiduciaria Super")]
        //public FiduciariasViewModel FiduciariaSuperSelected { get; set; }

        //[DisplayName("Fiduciaria Bladex")]
        //public string FiduciariaBladex { get; set; }

        //[DisplayName("Depositante")]
        //public ActorViewModel DepositanteSelected { get; set; }

        //[DisplayName("Evaluador")]
        //public ActorViewModel EvaluadorSelected { get; set; }

        //[DisplayName("Administrador")]
        //public ActorViewModel AdministradorSelected { get; set; }

        //[DisplayName("Asegurador")]
        //public ActorViewModel AseguradorSelected { get; set; }

        //[DisplayName("Revisor")]
        //public ActorViewModel RevisorSelected { get; set; }

        //[DisplayName("Origen de la garantia")]
        //public string OrigenGarantia { get; set; }

        //[DisplayName("Pais de la garantia")]
        //public PaisViewModel PaisGarantiaSelected { get; set; }

        //[DisplayName("Tipo de garantia Super")]
        //public TipoGarantiaSuperViewModel TipoGarantiaSuperSelected { get; set; }

        //[DisplayName("Tipo de garantia Bladex")]
        //public TipoGarantiaBladexViewModel TipoGarantiaBladexSelected { get; set; }

        //[DisplayName("Valor inicial")]
        //[DataType(DataType.Currency)]
        //public string ValorInicial { get; set; }

        //[DisplayName("Descripcion de la garantia")]
        //public string DescripcionDeLaGarantia { get; set; }

        //[DisplayName("Garante")]
        //[ScaffoldColumn(false)]
        //public ClienteViewModel GaranteSelected { get; set; }

        //[DisplayName("Registro inicial")]
        //[UIHint("DateTime")]
        //[DataType(DataType.Date)]
        //public DateTime? FechaRegistroInicial { get; set; }

        //[DisplayName("Formalizacion")]
        //[UIHint("DateTime")]
        //[DataType(DataType.Date)]
        //public DateTime FechaFormalizacion { get; set; }

        //[DisplayName("Vencimiento de riesgo")]
        //[UIHint("DateTime")]
        //[DataType(DataType.Date)]
        //public DateTime FechaVencimientoRiesgo { get; set; }

        //[DisplayName("Vencimiento de garantia")]
        //[UIHint("DateTime")]
        //[DataType(DataType.Date)]
        //public DateTime FechaVencimientoGarantia { get; set; }

        //[DisplayName("Ultima revision de la evaluacion")]
        //[UIHint("DateTime")]
        //[DataType(DataType.Date)]
        //public DateTime FechaUltimaRevisionEvaluacion { get; set; }

        //[DisplayName("Proxima revision de la evaluacion")]
        //[UIHint("DateTime")]
        //[DataType(DataType.Date)]
        //public DateTime FechaProximaRevisionEvaluacion { get; set; }

        //[DisplayName("Frecuencia de revision")]
        //public FrecuenciasViewModel FrequenciaRevisionSelected { get; set; }

        //[DisplayName("Vencimiento del seguro")]
        //[DataType(DataType.Date)]
        //public DateTime FechaVencimientoSeguro { get; set; }

        //[DisplayName("Categoria del riesgo de la garantia")]
        //public CategoriaRiesgoGarantiaViewModel CategoriaRiesgoGarantiaSelected { get; set; }

        //[DisplayName("Reduccion de riesgo por pais?")]
        //public bool ReduccionDeRiesgoPorPais { get; set; }

        //[DisplayName("Moneda")]
        //public MonedasViewModel MonedaSelected { get; set; }

        //[DisplayName("Valor necesario de garantia")]
        //[DataType(DataType.Currency)]
        //public string ValorNecesarioDeGarantia { get; set; }

        //[DisplayName("Valor evaluacion de venta rapida")]
        //[DataType(DataType.Currency)]
        //public string ValorEvaluacionVentaRapida { get; set; }

        //[DisplayName("Mitigacion Super Intendencia (%)")]
        //public decimal PorcentajeAplicableMitigacionSuperInt { get; set; }

        //[DisplayName("Avales")]
        //public List<AvalViewModel> AvalListSelected { get; set; }

        //[DisplayName("Comentarios")]
        //[DataType(DataType.MultilineText)]
        //public string Comentarios { get; set; }

        //[DisplayName("Rating del garante")]
        //public CalificacionesRiesgoViewModel RatingGaranteSelected { get; set; }

        //[DisplayName("Valor poliza de seguro")]
        //[DataType(DataType.Currency)]
        //public string ValorPolizaSeguro { get; set; }

        //[DisplayName("N° de poliza de seguro")]
        //public string NumeroPolizaSeguro { get; set; }

        //[DisplayName("Valor de mercado")]
        //[DataType(DataType.Currency)]
        //public string ValorMercado { get; set; }

        //[DisplayName("Categoria super")]
        //public CategoriaSuperViewModel CategoriaSuperSelected { get; set; }

        //[DisplayName("Attached to line")]
        //public bool? AttachedToLine { get; set; }

        //[DisplayName("Id del atomo")]
        //public int ID_Atomo { get; set; }

        ////[DisplayName("Fiduciaria")]
        ////public FiduciariasViewModel FiduciariasSelected {get;set;}

        //[DisplayName("Instrumento financiero")]
        //public InstrumentoFinancieroViewModel InstrumentoFinancieroSelected { get; set; }

        //[DisplayName("Calificacion de riesgo")]
        //public CalificacionesRiesgoViewModel CalificacionesRiesgoSelected { get; set; }

        //[DisplayName("Frecuencia")]
        //public FrecuenciasViewModel FrencuenciasSelected { get; set; }

        ////[DisplayName("Monedas")]
        ////public MonedasViewModel MonedasSelected { get; set; }

        //[DisplayName("Avaluadora")]
        //public AvaluadorasViewModel AvaluadorasSelected { get; set; }

        //[DisplayName("Bancos")]
        //[UIHint("BancosViewModel")]
        //public List<BancosViewModel> BancosListSelected { get; set; }

        //[DisplayName("Actor")]
        //public ActorViewModel ActorSelected { get; set; }

        //[DisplayName("Status")]
        //public StatusViewModel StatusSelected { get; set; }

        

        #endregion

    }
}