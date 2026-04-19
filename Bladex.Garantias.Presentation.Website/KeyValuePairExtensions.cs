using System;
using System.Collections.Generic;
using Bladex.Garantias.Infrastructure.Extensions;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Bladex.Garantias.Presentation.Website.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Bladex.Garantias.Presentation.Website
{
    /// <summary>
    /// The HTML label extensions class.
    /// </summary>
    public static class KeyValuePairExtensions
    {
        /// <summary>
        /// Gets the key formatted.
        /// </summary>
        /// <param name="kv">The kv of type <see cref="System.Collections.Generic.KeyValuePair&lt;System.String,System.Object&gt;"/></param>
        /// <returns></returns>
        public static string GetKeyFormatted(this KeyValuePair<string, object> kv)
        {
            return string.Format("{0}", kv.Key.ConvertToTitleCase());
        }

        /// <summary>
        /// Gets the value formatted.
        /// </summary>
        /// <param name="kv">The kv of type <see cref="System.Collections.Generic.KeyValuePair&lt;System.String,System.Object&gt;"/></param>
        /// <returns></returns>
        public static string GetValueFormatted(this KeyValuePair<string, object> kv)
        {
            if (kv.Value == null)
                return "(empty)";
            else if (kv.Value is BaseViewModel && !string.IsNullOrEmpty(((BaseViewModel)kv.Value).Nombre))
                return ((BaseViewModel)kv.Value).Nombre;
            else if (kv.Value is DateTime)
                return ((DateTime)kv.Value).ToShortDateString();
            else
                return string.IsNullOrEmpty(kv.Value.ToString()) ? "(empty)" : kv.Value.ToString();
        }       
        
    }


}