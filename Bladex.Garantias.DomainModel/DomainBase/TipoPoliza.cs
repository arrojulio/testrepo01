using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Infrastructure.DomainBase;

namespace Bladex.Garantias.DomainModel.DomainBase
{
    public class TipoPoliza: EntityBase
    {
        public virtual string Nombre
        {
            get;
            set;
        }
    }
}
