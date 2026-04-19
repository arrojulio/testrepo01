using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bladex.Garantias.Infrastructure.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Convierte <see cref="System.DateTime"/> al formato requerido por Bladex.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToBladexFormat(this DateTime date)
        {
            return date.ToString("yyyyMMdd");
        }
    }
}
