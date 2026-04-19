using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Web.Mvc;
using System.Linq;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Presentation.Website.ViewModels;

namespace Bladex.Garantias.Presentation.Website.Models
{

    
    [MetadataType(typeof(GarantiaMuebleMetadata))]
    public class GarantiaMuebleModel : GarantiaBaseModel, IGarantiaMuebleModel
    {
        public override void Init()
        {
            this.AseguradorSuper = new AseguradorasViewModel();
        }

        #region Implementation of IGarantiaMuebleModel
        [Required]
        public AseguradorasViewModel AseguradorSuper { get; set; }
        public DateTime? FechaInicialAvaluo { get; set; }
        public DateTime? FechaVencimientoAvaluo { get; set; }
        public decimal ValorTotalAvaluo { get; set; }
        //public IEnumerable<SelectListItem> AseguradorSuperSelectList { get; set; }

        #endregion
    }
}