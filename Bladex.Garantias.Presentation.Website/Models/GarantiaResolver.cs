using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bladex.Garantias.DomainModel.DomainBase;

namespace Bladex.Garantias.Presentation.Website.Models
{
    /// <summary>
    /// The garantia resolver class.
    /// </summary>
    public class GarantiaResolver
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GarantiaResolver"/> class.
        /// </summary>
        /// <param name="categoriaSuperId">The categoria super id of type <see cref="System.String"/></param>
        public GarantiaResolver(string categoriaSuperId)
        {
            this.CreateAction = "Create";
            this.UpdateAction = "Edit";
            this.DeleteAction = "Delete";
            this.ListAction = "List";
            this.IndexAction = "Index";

            switch (categoriaSuperId)
            {
                case "01": this.Controller = "GarantiaMueble"; break;
                case "02": this.Controller = "GarantiaInmueble"; break;
                case "03": this.Controller = "GarantiaDeposito"; this.CreateAction = string.Empty; this.UpdateAction = string.Empty; this.DeleteAction = string.Empty; break;
                case "04": this.Controller = "GarantiaDepositoOtroBanco"; break;
                case "05": this.Controller = "GarantiaPrenda"; break;
                case "06": this.Controller = "GarantiaOtra"; break;
                default:
                    throw new ArgumentOutOfRangeException("categoriaSuperId", string.Format("The Categoria Super ID {0} is not valid.", categoriaSuperId));
                    break;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GarantiaResolver"/> class.
        /// </summary>
        /// <param name="categoriaSuper">The categoria super of type <see cref="Bladex.Garantias.DomainModel.DomainBase.CategoriaSuper"/></param>
        public GarantiaResolver(CategoriaSuper categoriaSuper):this(categoriaSuper.Key.ToString())
        {

        }

        /// <summary>
        /// Gets the create action.
        /// </summary>
        public string CreateAction { get; private set; }

        /// <summary>
        /// Gets the update action.
        /// </summary>
        public string UpdateAction { get; private set; }

        /// <summary>
        /// Gets the delete action.
        /// </summary>
        public string DeleteAction { get; private set; }

        /// <summary>
        /// Gets the list action.
        /// </summary>
        public string ListAction { get; private set; }

        /// <summary>
        /// Gets the index action.
        /// </summary>
        public string IndexAction { get; private set; }

        /// <summary>
        /// Gets the controller.
        /// </summary>
        public string Controller
        {
            get;
            private set;
        }
    }
}