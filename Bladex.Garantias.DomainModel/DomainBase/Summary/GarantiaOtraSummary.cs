using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bladex.Garantias.DomainModel.DomainBase.Summary
{
    public class GarantiaOtraSummary
    {
        public GarantiaOtraSummary() 
        {
        
        }

        public int? Key { get; set; }

        public string IdentificacionDocumentoGarantia { get; set; }

        public string Cliente
        {
            get;
            set;
        }

        /// <summary>
        /// Guarda la categoria de la super. es importante ya que toda la logica de campo se rige de este valor.
        /// </summary>
        public virtual string CategoriaSuper
        {
            get;
            set;
        }

        public virtual string TipoGarantiaSuper
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


        public virtual int InternalStatus
        {
            get;
            set;
        }

        public virtual decimal ValorGarantiaSuperIntendencia
        {
            get;
            set;
        }

        public virtual bool IsReadOnly
        {
            get;
            set;
        }
    }
}
