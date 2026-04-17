using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bladex.Garantias.Application.Facades;

namespace Bladex.Garantias.Presentation.Website.ViewModels
{
    /// <summary>
    /// The cliente view model class.
    /// </summary>
    public class ClienteViewModel : BaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClienteViewModel"/> class.
        /// </summary>
        public ClienteViewModel()
        {
            this.Key = string.Empty;
        }

        /// <summary>
        /// Gets or sets the list.
        /// </summary>
        public IEnumerable<SelectListItem> List { get; set; }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key of type <see cref="System.String"/>
        /// </value>
        [Required(ErrorMessage="Debe seleccionar un cliente")]
        [DataType(DataType.Text, ErrorMessage="Debe seleccionar un cliente")]
        [StringLength(10, MinimumLength=2)]
        public string Key
        { get; set; }
        /// <summary>
        /// Nombre del cliente
        /// </summary>
        /// <value>
        /// The nombre of type <see cref="System.String"/>
        /// </value>
        public override string Nombre
        {
            get;
            set;
        }

        /// <summary>
        /// Rating de riesgo
        /// </summary>
        /// <value>
        /// The rating of type <see cref="System.String"/>
        /// </value>
        public string Rating
        {
            get;
            set;
        }

        /// <summary>
        /// Grupo Economico
        /// </summary>
        /// <value>
        /// The grupo economico of type <see cref="System.String"/>
        /// </value>
        public string GrupoEconomico
        {
            get;
            set;
        }

        /// <summary>
        /// Codigo del pais
        /// </summary>
        public PaisViewModel Pais
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the national id.
        /// </summary>
        /// <value>
        /// The national id of type <see cref="System.String"/>
        /// </value>
        [Display(Name="RUC Code", Description="RUC Code")]
        [DisplayName("RUC Code")]
        public string NationalId
        { get; set; }

        public bool IsInternal
        { get; set; }
        //public IEnumerable<SelectListItem> Pais { get; set; }

        ///// <summary>
        ///// Listado de clientes
        ///// </summary>
        //[Display(Nombre="Cliente", Description="Seleccion de cliente", Prompt="Select Cliente", ShortName="Cliente")]
        //public SelectList ClienteList { get; set; }

        //public string ClienteSelected
        //{
        //    get;
        //    set;
        //}

        //protected override IEnumerable<Infrastructure.DomainBase.EntityBase> GetDataSource()
        //{
        //    return ServiceFacade.Instance.ClienteService.GetAll();
        //}

        //protected override string GetDataTextField()
        //{
        //    return "Nombre";
        //}
    }
        
}