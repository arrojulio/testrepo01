using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Bladex.Garantias.Application.Facades;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Presentation.Website.Models;
using Telerik.Web.Mvc.UI;

namespace Bladex.Garantias.Presentation.Website.ViewModels
{
    /// <summary>
    /// The garantia index view model class.
    /// </summary>
    public class GarantiaIndexViewModel
    {
        /// <summary>
        ///   <see cref="System.Collections.Generic.List&lt;Bladex.Garantias.DomainModel.DomainBase.CategoriaSuper&gt;"/>
        /// </summary>
        private List<CategoriaSuper> _CategoriaSuperList;
        /// <summary>
        /// Gets or sets the categoria super list.
        /// </summary>
        /// <value>
        /// The categoria super list of type <see cref="System.Collections.Generic.List&lt;Bladex.Garantias.DomainModel.DomainBase.CategoriaSuper&gt;"/>
        /// </value>
        public List<CategoriaSuper> CategoriaSuperList 
        {
            get
            {
                return _CategoriaSuperList;
            }
            set 
            {
                _CategoriaSuperList = value;
                generateMenu();
            }
        }
        /// <summary>
        /// Gets or sets the menu.
        /// </summary>
        /// <value>
        /// The menu of type <see cref="System.Collections.Generic.List&lt;Bladex.Garantias.Presentation.Website.ViewModels.GarantiasMenu&gt;"/>
        /// </value>
        public List<GarantiasMenu> Menu { get; set; }

        /// <summary>
        /// Used to retrieve the controller name.
        /// </summary>
        /// <param name="categoriaSuper"><see cref="CategoriaSuper"/> used to decide the controller name.</param>
        /// <returns>Returns the controller name of the <see cref="GarantiaBase"/></returns>
        public string GetControllerName(CategoriaSuper categoriaSuper)
        {
            GarantiaResolver resolver = new GarantiaResolver(categoriaSuper);
            return resolver.Controller;
        }

        /// <summary>
        /// Generates the menu.
        /// </summary>
        private void generateMenu()
        {
            this.Menu = new List<GarantiasMenu>();
            foreach (CategoriaSuper category in this.CategoriaSuperList)
            {
                GarantiasMenu menu = new GarantiasMenu();
                menu.Value = category.Key.ToString();
                menu.Label = category.Nombre;
                menu.ChildrenItems = new List<GarantiasMenuItem>();
                menu.ChildrenItems.AddRange(generateMenuItem(category));
                this.Menu.Add(menu);
            }
            this.Menu.OrderBy(o => o.Label);
            this.Menu.Add(new GarantiasMenu() { IsParent = true, Value = "", Label = "Garantias Desvinculadas", ChildrenItems = new List<GarantiasMenuItem>() 
            { 
                new GarantiasMenuItem() { ControllerName = "Garantia", Label = "Ver el listado de garantias desvinculadas", ActionName = "ListUnknown", Value = "" },
                new GarantiasMenuItem() { ControllerName = "Garantia", Label = "Desvincular tipos de garantías super", ActionName = "DisassociateGuarantees", Value = "" } 
            }});
            this.Menu.Add(new GarantiasMenu() { IsParent= true,  Value = "", Label = "Todas las garantias", ChildrenItems = new List<GarantiasMenuItem>() { new GarantiasMenuItem() { ControllerName="Garantia", Label="Ver el listado completo de garantias", ActionName="List", Value="" } } });

        }

        /// <summary>
        /// Generates the menu item.
        /// </summary>
        /// <param name="category">The category of type <see cref="Bladex.Garantias.DomainModel.DomainBase.CategoriaSuper"/></param>
        /// <returns></returns>
        private IEnumerable<GarantiasMenuItem> generateMenuItem(CategoriaSuper category)
        {
            List<GarantiasMenuItem> result = new List<GarantiasMenuItem>();
            GarantiasMenuItem indexItem = new GarantiasMenuItem();
            GarantiasMenuItem createItem = new GarantiasMenuItem();
            GarantiaResolver resolver = new GarantiaResolver(category);
            indexItem.ActionName = resolver.IndexAction;
            indexItem.ControllerName = resolver.Controller;
            indexItem.Value = category.Key.ToString();
            indexItem.Label = string.Format("Ver listado de {0}", category.Nombre);
            createItem.ActionName = resolver.CreateAction;
            createItem.ControllerName = resolver.Controller;
            createItem.Value = category.Key.ToString();
            createItem.Label = string.Format("Ingresar nueva {0}", category.Nombre);
            
            // Si es Otras Garantias, inserto en nuevo item en el menu para poder ver los avales.
            if (category.Key.ToString() == CategoriaSuper.OTRAS_ID)
            {
                GarantiasMenuItem indexItem2 = new GarantiasMenuItem();
                indexItem2.ActionName = "IndexAvales";
                indexItem2.ControllerName = resolver.Controller;
                indexItem2.Value = category.Key.ToString();
                indexItem2.Label = string.Format("Ver listado de {0}", ServiceFacade.Instance.TipoGarantiaSuperService.GetFianzasYAvalesNoBancarios().Nombre);
                result.Add(indexItem2);
            }
            result.Add(indexItem);
            // Verifico permisos de escritura del usuario
            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated && HttpContext.Current.User.IsInRole("Power User") && !HttpContext.Current.User.IsInRole("Checker"))
            {
                // Si la categoria es read only no inserto la opcion para crear.
                if (!category.IsReadOnly)
                    result.Add(createItem);
            }
            return result;
        }
    }

    /// <summary>
    /// The garantias menu class.
    /// </summary>
    public class GarantiasMenu
    {
        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>
        /// The label of type <see cref="System.String"/>
        /// </value>
        public string Label { get; set; }
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value of type <see cref="System.String"/>
        /// </value>
        public string Value { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is selected.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is selected; otherwise, <c>false</c>.
        /// </value>
        public bool IsSelected { get; set; }
        /// <summary>
        /// Gets or sets the children items.
        /// </summary>
        /// <value>
        /// The children items of type <see cref="System.Collections.Generic.List&lt;GarantiasMenuItem&gt;"/>
        /// </value>
        public List<GarantiasMenuItem> ChildrenItems { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is parent.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is parent; otherwise, <c>false</c>.
        /// </value>
        public bool IsParent { get; set; }

    }

    /// <summary>
    /// The garantias menu item class.
    /// </summary>
    public class GarantiasMenuItem
    {
        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>
        /// The label of type <see cref="System.String"/>
        /// </value>
        public string Label { get; set; }
        /// <summary>
        /// Gets or sets the name of the controller.
        /// </summary>
        /// <value>
        /// The name of the controller.
        /// </value>
        public string ControllerName { get; set; }
        /// <summary>
        /// Gets or sets the name of the action.
        /// </summary>
        /// <value>
        /// The name of the action.
        /// </value>
        public string ActionName { get; set; }
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value of type <see cref="System.String"/>
        /// </value>
        public string Value { get; set; }
    }
}