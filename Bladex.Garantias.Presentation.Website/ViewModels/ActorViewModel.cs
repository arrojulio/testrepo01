using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bladex.Garantias.Application.Facades;
using Bladex.Garantias.DomainModel.DomainBase;

namespace Bladex.Garantias.Presentation.Website.ViewModels
{
    /// <summary>
    /// The actor view model class.
    /// </summary>
    public class ActorViewModel : BaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActorViewModel"/> class.
        /// </summary>
        public ActorViewModel()
        {
            this.Key = null;
            this._pais = new PaisViewModel();
        }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key of type <see cref="System.String"/>
        /// </value>
        public string Key { get; set; }
        /// <summary>
        /// Gets or sets the nombre.
        /// </summary>
        /// <value>
        /// The nombre of type <see cref="System.String"/>
        /// </value>
        public override string Nombre{get;set;}
        
        private PaisViewModel _pais;
        /// <summary>
        /// Gets or sets the pais.
        /// </summary>
        /// <value>
        /// The pais of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.PaisViewModel"/>
        /// </value>
        public PaisViewModel Pais
        {
            get { return _pais ?? ( _pais = new PaisViewModel() ); }
            set { _pais = value; }
        }
        /// <summary>
        /// Gets or sets the list.
        /// </summary>
        public IEnumerable<SelectListItem> List { get; set; }
        
    }


}