using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Infrastructure.DomainBase;

namespace Bladex.Garantias.DomainModel.DomainBase.Components.AvalManager
{
    public class AvalManager :EntityBase
    {        
        public virtual List<Aval> AvalList { get; set; }
        public virtual TipoAval TipoAvalCatalog { get; set; }
        public virtual Pais PaisCatalog { get; set; }
        public virtual string hiddenAvales { get; set; }
    }
}
