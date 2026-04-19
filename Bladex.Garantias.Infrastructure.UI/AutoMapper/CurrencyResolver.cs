using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using AutoMapper;

namespace Bladex.Garantias.Infrastructure.UI.AutoMapper
{
    public class CurrencyResolver : ValueResolver<string, decimal>
    {

        ///<summary>Parses source value as currency</summary>

        protected override decimal ResolveCore(string source)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrWhiteSpace(source))
                return new decimal();
            return decimal.Parse(source, NumberStyles.Currency, CultureInfo.CurrentCulture);

        }

    }

}
