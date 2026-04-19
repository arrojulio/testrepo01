using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase;

namespace Bladex.Garantias.DomainModel.Components
{
    /// <summary>
    /// Clase utilizada para resolver los diferentes tipos de <see cref="UnidadFrecuenciaEnum"/> de <see cref="Frecuencias"/>.
    /// </summary>
    public static class UnidadFrecuenciaResolver
    {
        /// <summary>
        /// <see cref="Dictionary{String,UnidadFrecuenciaEnum}"></see> que mantiene los mapeos entre los valores de texto y los valores de <see cref="UnidadFrecuenciaEnum"/>
        /// </summary>
        private static readonly Dictionary<string, UnidadFrecuenciaEnum?> UnidadFrecuenciaValues = new Dictionary<string, UnidadFrecuenciaEnum?>() { 
            { "DD", UnidadFrecuenciaEnum.Days }, 
            { "MM", UnidadFrecuenciaEnum.Months }, 
            { "YY", UnidadFrecuenciaEnum.Years}, 
            { "NULL", default(UnidadFrecuenciaEnum?) } 
        };

        /// <summary>
        /// Dado un valor en texto, retorna un valor de <see cref="UnidadFrecuenciaEnum"/>.
        /// </summary>
        /// <param name="value"><see cref="String"/> con el valor de la unidad expresado en texto. (dd, mm, yy)</param>
        /// <returns><see cref="UnidadFrecuenciaEnum"/> correspondiente al valor en texto.</returns>
        public static UnidadFrecuenciaEnum? Resolve(string value)
        {
            return UnidadFrecuenciaValues.ContainsKey(value.ToUpper()) ? UnidadFrecuenciaValues[value.ToUpper()] : default(UnidadFrecuenciaEnum?);
        }
        /// <summary>
        /// Dado un valor <see cref="UnidadFrecuenciaEnum"/> retorna el valor de la unidad expresado en texto. (dd, mm, yy)
        /// </summary>
        /// <param name="value"><see cref="Nullable{UnidadFrecuenciaEnum}"/> que refleja la frecuencia.</param>
        /// <returns><see cref="String"/> representando el valor de <see cref="UnidadFrecuenciaEnum"/>.</returns>
        public static string Resolve(UnidadFrecuenciaEnum? value)
        {
            if (!value.HasValue) return "NULL";
            string result = string.Empty;
            switch (value)
            {
                case UnidadFrecuenciaEnum.Days:
                    result = "dd";
                    break;
                case UnidadFrecuenciaEnum.Months:
                    result = "mm";
                    break;
                case UnidadFrecuenciaEnum.Years:
                    result = "yy";
                    break;
                default:
                    result = "dd";
                    break;
            }

            return result;
        }

    }
}
