using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.Components.TypeConverters;

namespace Bladex.Garantias.DomainModel.DomainBase
{
    /// <summary>
    /// Representa la fuente de origen de una garantía.
    /// <example>
    /// Cuando es Interna, la garantía se ha generado mediante nuestro sistema.
    /// Cuando es Teinsa, la garantía proviene del Átomo de Teinsa.
    /// </example>
    /// </summary>
    [TypeConverter(typeof(PascalCaseTypeConverter))]
    public enum GarantiaSourceEnum
    {
        Interna=0, Teinsa=1
    }
}
