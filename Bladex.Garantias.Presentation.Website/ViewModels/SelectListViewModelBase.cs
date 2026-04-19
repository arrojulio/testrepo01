using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Bladex.Garantias.Infrastructure.DomainBase;

namespace Bladex.Garantias.Presentation.Website.ViewModels
{
    /// <summary>
    /// The select list view model base class.
    /// </summary>
    public abstract class SelectListViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelectListViewModelBase"/> class.
        /// </summary>
        protected SelectListViewModelBase():this(string.Empty)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectListViewModelBase"/> class.
        /// </summary>
        /// <param name="selectedValue">The selected value of type <see cref="System.String"/></param>
        protected SelectListViewModelBase(string selectedValue)
        {
            this.SelectedValue = selectedValue;
        }

        /// <summary>
        /// Gets the data source.
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerable<EntityBase> GetDataSource();

        /// <summary>
        /// Gets the selection list.
        /// </summary>
        public SelectList SelectionList
        {
            get
            {
                
                return new SelectList(this.GetDataSource(), this.GetDataValueField(), this.GetDataTextField(), this.SelectedValue);
                
            }
        }

        /// <summary>
        /// Gets or sets the selected value.
        /// </summary>
        /// <value>
        /// The selected value of type <see cref="System.String"/>
        /// </value>
        public string SelectedValue { get; set; }

        /// <summary>
        /// Gets the data value field.
        /// </summary>
        /// <returns></returns>
        protected virtual string GetDataValueField()
        {
            return "Key";
        }

        /// <summary>
        /// Gets the data text field.
        /// </summary>
        /// <returns></returns>
        protected virtual string GetDataTextField()
        {
            return "Nombre";
        }
    }
}
