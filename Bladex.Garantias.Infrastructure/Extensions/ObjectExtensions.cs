using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace Bladex.Garantias.Infrastructure.Extensions
{
    /// <summary>
    /// The object extensions class.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Converts any object to an <see cref="ExpandoObject"/>
        /// </summary>
        /// <param name="value">The value of type <see cref="System.Object"/></param>
        /// <returns><see cref="ExpandoObject"/></returns>
        public static dynamic ToDynamic(this object value)
        {
            IDictionary<string, object> expando = new ExpandoObject();

            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(value.GetType()))
                expando.Add(property.Name, property.GetValue(value));

            return expando as ExpandoObject;
        }
    }
}
