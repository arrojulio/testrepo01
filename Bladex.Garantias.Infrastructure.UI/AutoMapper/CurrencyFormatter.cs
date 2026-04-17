using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using AutoMapper;

namespace Bladex.Garantias.Infrastructure.UI.AutoMapper
{
    public class CurrencyFormatter : IValueFormatter
    {

        ///<summary>Formats source value as currency</summary>

        public string FormatValue(ResolutionContext context)
        {

            return string.Format(CultureInfo.CurrentCulture, "{0:c}", context.SourceValue);

        }

    }
}
