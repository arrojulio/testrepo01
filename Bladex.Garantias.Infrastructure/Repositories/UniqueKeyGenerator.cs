using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bladex.Garantias.Infrastructure.Repositories
{
    /// <summary>
    /// The unique key generator class.
    /// </summary>
	public sealed class UniqueKeyGenerator
	{
        /// <summary>
        /// Generates the specified max lenght.
        /// </summary>
        /// <param name="maxLenght">The max lenght of type <see cref="System.Int32"/></param>
        /// <returns></returns>
        public static string Generate(int maxLenght)
        {
            string newKey = Guid.NewGuid().ToString("N").ToUpper();
            if (maxLenght > 32)
                maxLenght = 32;
            return newKey.Substring(0, maxLenght);
        }
	}
}
