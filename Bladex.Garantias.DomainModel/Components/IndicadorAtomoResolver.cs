using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase;

namespace Bladex.Garantias.DomainModel.Components
{
    /// <summary>
    /// Clase utilizada para resolver los diferentes tipos de indicadores de atomo provenientes de TEINSA.
    /// </summary>
    public static class IndicadorAtomoResolver
    {
        /// <summary>
        /// <see cref="Dictionary{String,IndicadorAtomoEnum}"></see> que mantiene los mapeos entre los valores de TEINSA y los valores de Novaris.
        /// </summary>
        private static readonly Dictionary<string, IndicadorAtomoEnum?> TeinsaIndAtomoValues = new Dictionary<string, IndicadorAtomoEnum?>() { 
            { "ATOMO", IndicadorAtomoEnum.Atomo }, 
            { "PIGNORADOS", IndicadorAtomoEnum.Pignorados }, 
            { "NO ESTAN EN ATOMO", IndicadorAtomoEnum.NoEstaEnAtomo}, 
            { "NULL", default(IndicadorAtomoEnum?) } 
        };


        /// <summary>
        /// Dado un valor de TEINSA, retorna un valor de Novaris.
        /// </summary>
        /// <param name="teinsaIndAtomoValue"><see cref="String"/> con el valor de Ind_Atomo proveniente de TEINSA.</param>
        /// <returns><see cref="IndicadorAtomoEnum"/> correspondiente al valor de TEINSA.</returns>
        public static IndicadorAtomoEnum? Resolve(string teinsaIndAtomoValue)
        {
            return TeinsaIndAtomoValues.ContainsKey(teinsaIndAtomoValue.ToUpper()) ? TeinsaIndAtomoValues[teinsaIndAtomoValue.ToUpper()] : default(IndicadorAtomoEnum?);
            
        }
    }
}
