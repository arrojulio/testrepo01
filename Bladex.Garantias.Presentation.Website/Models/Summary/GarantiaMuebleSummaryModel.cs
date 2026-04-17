using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Bladex.Garantias.DomainModel.DomainBase;

namespace Bladex.Garantias.Presentation.Website.Models.Summary
{
    [MetadataType(typeof(GarantiaMuebleSummaryMetadata))]
    public class GarantiaMuebleSummaryModel
    {
        public int? Key { get; set; }        
        
        public string IdentificacionDocumentoGarantia { get; set; }
        
        public Cliente Cliente
        {
            get;
            set;
        }

        /// <summary>
        /// Guarda la categoria de la super. es importante ya que toda la logica de campo se rige de este valor.
        /// </summary>
        public virtual CategoriaSuper CategoriaSuper
        {
            get;
            set;
        }
        
        public virtual TipoGarantiaSuper TipoGarantiaSuper
        {
            get;
            set;
        }

        
        public virtual Decimal ValorPolizaSeguro
        {
            get;
            set;
        }

        public virtual Decimal ValorInicial
        {
            get;
            set;
        }
    }
}