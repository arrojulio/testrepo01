using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using AutoMapper;

namespace Bladex.Garantias.DomainModel.Components.Formatters
{
    /// <summary>
    /// The date formatter class.
    /// </summary>
    public class DateFormatter : IValueFormatter
    {
        #region IValueFormatter Members

        public string FormatValue(ResolutionContext context)
        {
            return string.Format(CultureInfo.CurrentCulture, "{0:d}", context.SourceValue);
        }

        #endregion
    }
}
