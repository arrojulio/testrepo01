using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Bladex.Garantias.Infrastructure.Extensions
{
    /// <summary>
    /// The object extensions class.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Used to convert an string like "camelCase" or "CAMELCASE" to "CamelCase".
        /// </summary>
        /// <param name="inputCamelCaseString">String to format</param>
        /// <returns>
        /// String formatted
        /// </returns>
        public static string ConvertToTitleCase(this string inputCamelCaseString)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(inputCamelCaseString.ToLower());
        }
    }
}
