using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.Components.TypeConverters;

namespace Bladex.Garantias.DomainModel.DomainBase
{
    /// <summary>
    /// Indica si la <see cref="GarantiaBase"/> existe en el Atomo.
    /// </summary>
    [TypeConverter(typeof(PascalCaseTypeConverter))]
    public enum IndicadorAtomoEnum 
    {
        /// 1 - ATOMO
        /// 2 - PIGNORADOS
        /// 3 - NO ESTA EN ATOMO
        Atomo = 1, Pignorados = 2, NoEstaEnAtomo = 3
    }
}
