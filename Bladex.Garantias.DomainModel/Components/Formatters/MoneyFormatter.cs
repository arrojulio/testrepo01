using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using AutoMapper;

namespace Bladex.Garantias.DomainModel.Components.Formatters
{
    /// <summary>
    /// The money formatter class.
    /// </summary>
    public class MoneyFormatter : IValueFormatter
    {
        #region IValueFormatter Members

        ///<summary>Formats source value as currency</summary>
        public string FormatValue(ResolutionContext context)
        {
            return string.Format(CultureInfo.CurrentCulture, "{0:c}", context.SourceValue);
        }

        #endregion
    }
}
