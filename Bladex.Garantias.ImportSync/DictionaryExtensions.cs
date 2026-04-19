namespace Bladex.Garantias.ImportSync
{
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Dictionary Extensions
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Writes the formatted log.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        /// <returns>string Message</returns>
        public static string WriteFormattedLog(this IDictionary<string, bool> dictionary)
        {
            var builder = new StringBuilder();
            builder.AppendLine("Total: " + dictionary.Count + " | ");
            foreach (var obj in dictionary)
            {
                builder.Append(string.Format("ID: {0} - Success: {1} | ", obj.Key, obj.Value));
            }

            return builder.ToString();
        }
    }
}