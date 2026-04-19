using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bladex.Garantias.DomainModel.DomainBase.Summary
{
    public class GarantiaMuebleSummary
    {
        public GarantiaMuebleSummary()
        {
        }

        public int? Key { get; set; }

        public string IdentificacionDocumentoGarantia { get; set; }

        public string Cliente { get; set; }

        public virtual string CategoriaSuper { get; set; }

        public virtual string TipoGarantiaSuper { get; set; }

        public virtual decimal ValorPolizaSeguro { get; set; }

        public virtual decimal ValorInicial { get; set; }

        public virtual int InternalStatus { get; set; }

        public virtual bool IsReadOnly { get; set; }
    }
}
