using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bladex.Garantias.DomainModel.DomainBase
{
    public class GarantiaBaseRow
    {
        public int Key { get; set; }
        public string IdentificadorGarantia { get; set; }
        public string Cliente { get; set; }
        public string CategoriaGarantia { get; set; }
        public string CategoriaGarantiaId { get; set; }
        public string TipoGarantia { get; set; }
        public double ValorInicial { get; set; }
        public double ValorMercado { get; set; }
        public bool IsReadOnly { get; set; }
    }    
}
