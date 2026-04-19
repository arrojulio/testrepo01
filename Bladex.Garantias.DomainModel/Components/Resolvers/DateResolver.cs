using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using AutoMapper;

namespace Bladex.Garantias.DomainModel.Components.Resolvers
{
    /// <summary>
    /// The date resolver class.
    /// </summary>
    public class DateResolver : ValueResolver<string, DateTime>
    {
        ///<summary>Parses source value as date</summary>
        protected override DateTime ResolveCore(string source)
        {
            return DateTime.Parse(source, CultureInfo.CurrentCulture, DateTimeStyles.None);
        }
    }
}
