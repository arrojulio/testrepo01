using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Bladex.Garantias.Application.Facades;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Presentation.Website.Models;

namespace Bladex.Garantias.Presentation.Website.ViewModels
{
    /// <summary>
    /// The garantia mueble view model class.
    /// </summary>
    [DisplayName("Garantia Mueble")]
    public class GarantiaMuebleViewModel : GarantiaBaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GarantiaMuebleViewModel"/> class.
        /// </summary>
        public GarantiaMuebleViewModel() : base()
        {
            this.Garantia = new GarantiaMuebleModel();
        }

        /// <summary>
        /// Gets or sets the garantia.
        /// </summary>
        /// <value>
        /// The garantia of type <see cref="Bladex.Garantias.Presentation.Website.Models.GarantiaMuebleModel"/>
        /// </value>
        [DisplayName("Garantia Mueble")]
        public GarantiaMuebleModel Garantia { get; set; }

        
        //public override void Save()
        //{
        //    base.Save();
        //    var garantia = AutoMapper.Mapper.Map < GarantiaMuebleViewModel, Garantia > (this);
        //    ServiceFacade.Instance.GarantiaMuebleService.Save(garantia);
        //}
    }
}
