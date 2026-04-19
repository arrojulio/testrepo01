using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using AutoMapper;

namespace Bladex.Garantias.DomainModel.Components.Resolvers
{
    /// <summary>
    /// The money resolver class.
    /// </summary>
    public class MoneyResolver : ValueResolver<string, decimal>
    {
        ///<summary>Parses source value as currency</summary>
        protected override decimal ResolveCore(string source)
        {
            return decimal.Parse(source, NumberStyles.Currency, CultureInfo.CurrentCulture);
        }
    }
}
