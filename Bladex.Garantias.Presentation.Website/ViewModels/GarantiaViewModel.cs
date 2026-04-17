using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bladex.Garantias.Application.Facades;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Infrastructure.DomainBase;
using Bladex.Garantias.Infrastructure.UI;

namespace Bladex.Garantias.Presentation.Website.ViewModels
{
    /// <summary>
    /// The garantia view model class.
    /// </summary>
    public class GarantiaViewModel : IGarantiaView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GarantiaViewModel"/> class.
        /// </summary>
        public GarantiaViewModel() :this(string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GarantiaViewModel"/> class.
        /// </summary>
        /// <param name="categoriaSuperId">The categoria super id of type <see cref="System.String"/></param>
        public GarantiaViewModel(string categoriaSuperId)
        {
            if (!string.IsNullOrEmpty(categoriaSuperId))
            {
                Type gvmType = getInstanceOfGarantiaViewModel(categoriaSuperId);
                var obj = Activator.CreateInstance(gvmType);
                this.Garantia = (GarantiaBaseViewModel)obj;
            }
            this.TiposDeGarantias = ServiceFacade.Instance.GarantiaService.GetTiposDeGarantias().ToList();
            this.TiposDeGarantiasList = new SelectList(this.TiposDeGarantias, "Key", "Nombre");
            this.CategoriaSuperSelected = this.TiposDeGarantias.FirstOrDefault(o => o.Key.ToString() == categoriaSuperId);
        }


        /// <summary>
        /// Gets or sets the create action.
        /// </summary>
        /// <value>
        /// The create action of type <see cref="System.String"/>
        /// </value>
        public string CreateAction { get; set; }
        /// <summary>
        /// Gets or sets the edit action.
        /// </summary>
        /// <value>
        /// The edit action of type <see cref="System.String"/>
        /// </value>
        public string EditAction { get; set; }
        /// <summary>
        /// Gets or sets the tipos de garantias.
        /// </summary>
        /// <value>
        /// The tipos de garantias of type <see cref="System.Collections.Generic.List&lt;CategoriaSuper&gt;"/>
        /// </value>
        public List<Bladex.Garantias.DomainModel.DomainBase.CategoriaSuper> TiposDeGarantias { get; set; }
        /// <summary>
        /// Gets or sets the categoria super selected.
        /// </summary>
        /// <value>
        /// The categoria super selected of type <see cref="Bladex.Garantias.DomainModel.DomainBase.CategoriaSuper"/>
        /// </value>
        public Bladex.Garantias.DomainModel.DomainBase.CategoriaSuper CategoriaSuperSelected { get; set; }
        /// <summary>
        /// Gets or sets the tipos de garantias list.
        /// </summary>
        /// <value>
        /// The tipos de garantias list of type <see cref="System.Web.Mvc.SelectList"/>
        /// </value>
        public SelectList TiposDeGarantiasList { get; set; }
        /// <summary>
        /// Gets or sets the garantia.
        /// </summary>
        /// <value>
        /// The garantia of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.GarantiaBaseViewModel"/>
        /// </value>
        public GarantiaBaseViewModel Garantia { get; set; }


        /// <summary>
        /// Gets the instance of garantia view model.
        /// </summary>
        /// <param name="categoriaSuperId">The categoria super id of type <see cref="System.String"/></param>
        /// <returns></returns>
        private Type getInstanceOfGarantiaViewModel(string categoriaSuperId)
        {
            switch (categoriaSuperId)
            {
                case "01": this.CreateAction = "CreateGarantiaMueble"; return typeof(GarantiaMuebleViewModel); break;
                case "02": this.CreateAction = "CreateGarantiaInmueble"; return typeof(GarantiaInmuebleViewModel); break;
                case "03": this.CreateAction = "CreateGarantiaDeposito"; return typeof(GarantiaDepositoViewModel); break;
                case "04": this.CreateAction = "CreateGarantiaDepositoOtroBanco"; return typeof(GarantiaDepositoOtroBancoViewModel); break;
                case "05": this.CreateAction = "CreateGarantiaPrenda"; return typeof(GarantiaPrendaViewModel); break;
                case "06": this.CreateAction = "CreateGarantiaOtra"; return typeof(GarantiaOtraViewModel); break;
                default:
                    this.CreateAction = "Create";
                    return typeof(GarantiaBaseViewModel);
                    break;
            }
        }

        #region Implementation of IGarantiaView

        /// <summary>
        /// Saves the view
        /// </summary>
        public void Save()
        {
            (this.Garantia).Save();
        }

        /// <summary>
        /// Reset the state of the view.
        /// </summary>
        public void Reset()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Initialize the view
        /// </summary>
        public void Init()
        {
            if (this.Garantia == null) throw new ApplicationException("The GarantiaViewModel is not initialized.");
        }

        

        #endregion

        #region IGarantiaView Members


        //public void Load(object GarantiaId)
        //{
        //    var garantia = ServiceFacade.Instance.GarantiaService.GetGarantiaById(GarantiaId);
        //    this.EditAction = string.Format("Edit{0}", garantia.GetType().Name);
            
        //    Type destType = getInstanceOfGarantiaViewModel(garantia.CategoriaSuper.Key.ToString());
            
        //    object obj = Activator.CreateInstance(destType);
        //    this.Garantia = (GarantiaBaseViewModel)obj;
        //    this.Garantia.Load()
        //    AutoMapper.Mapper.DynamicMap(garantia, destType);
        //    var viewModel = AutoMapper.Mapper.DynamicMap(garantia, garantia.GetType(), destType);
        //    AutoMapper.Mapper.Map(garantia, this.Garantia, garantia.GetType(), destType);

        //    if (viewModel != this.Garantia)
        //    {

        //    }
        //}


        /// <summary>
        /// Loads the view with the guarantee specified by the key.
        /// </summary>
        /// <param name="garantia"><see cref="Bladex.Garantias.DomainModel.DomainBase.GarantiaBase"/></param>
        public virtual void Load(GarantiaBase garantia)
        {
            this.EditAction = string.Format("Edit{0}", garantia.GetType().Name);
            Type destType = getInstanceOfGarantiaViewModel(garantia.CategoriaSuper.Key.ToString());
            this.Garantia = (GarantiaBaseViewModel)Activator.CreateInstance(destType);
            this.Garantia = AutoMapper.Mapper.Map(garantia, this.Garantia);
        }

        #endregion
    }
}