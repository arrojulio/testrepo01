namespace Bladex.Garantias.Infrastructure.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;
    using DomainBase;

    /// <summary>
    /// Static helper class used by the factories when getting 
    /// data from ADO.NET objects (i.e. IDataReader)
    /// </summary>
    public static class DataHelper
    {
        #region Static Data Helper Methods

        /// <summary>
        /// Gets the date time.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DateTime GetDateTime(object value)
        {
            DateTime dateValue = DateTime.MinValue;
            if ((value != null) && (value != DBNull.Value))
            {
                dateValue = (DateTime)value;
            }
            return dateValue;
        }

        /// <summary>
        /// Gets the nullable date time.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DateTime? GetNullableDateTime(object value)
        {
            DateTime? dateTimeValue = null;
            DateTime dbDateTimeValue;
            if (value != null && !Convert.IsDBNull(value))
            {
                if (DateTime.TryParse(value.ToString(), out dbDateTimeValue))
                {
                    dateTimeValue = dbDateTimeValue;
                }
            }
            return dateTimeValue;
        }

        /// <summary>
        /// Gets the integer.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static int GetInteger(object value)
        {
            int integerValue = 0;
            if (value != null && !Convert.IsDBNull(value))
            {
                int.TryParse(value.ToString(), out integerValue);
            }
            return integerValue;
        }

        /// <summary>
        /// Gets the nullable integer.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static int? GetNullableInteger(object value)
        {
            int? integerValue = null;
            int parseIntegerValue = 0;
            if (value != null && !Convert.IsDBNull(value))
            {
                if (int.TryParse(value.ToString(), out parseIntegerValue))
                {
                    integerValue = parseIntegerValue;
                }
            }
            return integerValue;
        }

        /// <summary>
        /// Gets the decimal.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static decimal GetDecimal(object value)
        {
            decimal decimalValue = 0;
            if (value != null && !Convert.IsDBNull(value))
            {
                decimal.TryParse(value.ToString(), out decimalValue);
            }
            return decimalValue;
        }

        /// <summary>
        /// Gets the nullable decimal.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static decimal? GetNullableDecimal(object value)
        {
            decimal? decimalValue = null;
            decimal parseDecimalValue = 0;
            if (value != null && !Convert.IsDBNull(value))
            {
                if (decimal.TryParse(value.ToString(), out parseDecimalValue))
                {
                    decimalValue = parseDecimalValue;
                }
            }
            return decimalValue;
        }

        /// <summary>
        /// Gets the double.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static double GetDouble(object value)
        {
            double doubleValue = 0;
            if (value != null && !Convert.IsDBNull(value))
            {
                double.TryParse(value.ToString(), out doubleValue);
            }
            return doubleValue;
        }

        /// <summary>
        /// Gets the nullable double.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static double? GetNullableDouble(object value)
        {
            double? doubleValue = null;
            double parseDoubleValue = 0;
            if (value != null && !Convert.IsDBNull(value))
            {
                if (double.TryParse(value.ToString(), out parseDoubleValue))
                {
                    doubleValue = parseDoubleValue;
                }
            }

            return doubleValue;
        }

        /// <summary>
        /// Gets the GUID.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static Guid GetGuid(object value)
        {
            Guid guidValue = Guid.Empty;
            if (value != null && !Convert.IsDBNull(value))
            {
                try
                {
                    guidValue = new Guid(value.ToString());
                }
                catch
                {
                    // really do nothing, because we want to return a value for the guid = Guid.Empty;
                }
            }
            return guidValue;
        }

        /// <summary>
        /// Gets the nullable GUID.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static Guid? GetNullableGuid(object value)
        {
            Guid? guidValue = null;
            if (value != null && !Convert.IsDBNull(value))
            {
                try
                {
                    guidValue = new Guid(value.ToString());
                }
                catch
                {
                    // really do nothing, because we want to return a value for the guid = null;
                }
            }
            return guidValue;
        }

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string GetString(object value)
        {
            string stringValue = string.Empty;
            if (value != null && !Convert.IsDBNull(value))
            {
                stringValue = value.ToString().Trim();
            }
            return stringValue;
        }

        /// <summary>
        /// Gets the boolean.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool GetBoolean(object value)
        {
            bool bReturn = false;
            if (value != null && value != DBNull.Value)
            {
                bReturn = Convert.ToBoolean(value);
            }
            return bReturn;
        }

        /// <summary>
        /// Gets the nullable boolean.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool? GetNullableBoolean(object value)
        {
            bool? bReturn = null;
            if (value != null && value != DBNull.Value)
            {
                bReturn = (bool)value;
            }

            return bReturn;
        }

        /// <summary>
        /// Gets the enum value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="databaseValue">The database value.</param>
        /// <returns></returns>
        public static T GetEnumValue<T>(string databaseValue) where T : struct
        {
            T enumValue = default(T);

            object parsedValue = Enum.Parse(typeof(T), databaseValue);
            if (parsedValue != null)
            {
                enumValue = (T)parsedValue;
            }

            return enumValue;
        }

        /// <summary>
        /// Gets the byte array value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static byte[] GetByteArrayValue(object value)
        {
            byte[] arrayValue = null;
            if (value != null && value != DBNull.Value)
            {
                arrayValue = (byte[])value;
            }
            return arrayValue;
        }

        /// <summary>
        /// Entities the list to delimited.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        public static string EntityListToDelimited<T>(IList<T> entities) where T : EntityBase
        {
            StringBuilder builder = new StringBuilder(20);
            if (entities != null)
            {
                for (int i = 0; i < entities.Count; i++)
                {
                    if (i > 0)
                    {
                        builder.Append(",");
                    }
                    builder.Append(entities[i].Key.ToString());
                }
            }
            return builder.ToString();
        }

        /// <summary>
        /// Readers the name of the contains column.
        /// </summary>
        /// <param name="schemaTable">The schema table.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns></returns>
        public static bool ReaderContainsColumnName(DataTable schemaTable, string columnName)
        {
            bool containsColumnName = false;
            foreach (DataRow row in schemaTable.Rows)
            {
                if (row["ColumnName"].ToString().ToUpperInvariant() == columnName.ToUpperInvariant())
                {
                    containsColumnName = true;
                    break;
                }
            }
            return containsColumnName;
        }

        /// <summary>
        /// Gets the SQL value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static object GetSqlValue(object value)
        {
            if (value != null)
            {
                if (value is EntityBase)
                {
                    return GetSqlValue((value as EntityBase).Key);
                }
                else if (value is Guid)
                {
                    return GetSqlValue((Guid)value);
                }
                else if (value is String)
                {
                    return GetSqlValue(value.ToString());
                }
                else if (value is int)
                {
                    return GetSqlValue((int)value);
                }
                else if (value is decimal)
                {
                    return GetSqlValue((decimal)value);
                }
                else if (value is double)
                {
                    return GetSqlValue((double)value);
                }
                else if (value is bool)
                {
                    return GetSqlValue((bool)value);
                }
                else if (value is bool?)
                {
                    if (((bool?)value).HasValue)
                        return GetSqlValue(((bool?)value).Value);
                    else return "NULL";
                }
                else return value;
            }
            else
            {
                return "NULL";
            }
        }

        /// <summary>
        /// Gets the SQL value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static object GetSqlValue(decimal value)
        {
            return string.Format("{0}", value).Replace(",",".");

        }

        /// <summary>
        /// Gets the SQL value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static object GetSqlValue(double value)
        {
            return string.Format("{0}", value).Replace(",", "."); 

        }

        /// <summary>
        /// Gets the SQL value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static object GetSqlValue(int value)
        {
            return string.Format("{0}", value);            
        }

        /// <summary>
        /// Gets the SQL value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static object GetSqlValue(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return string.Format("N'{0}'", value.Replace("'", "''"));
            }
            else
            {
                return "NULL";
            }
        }

        /// <summary>
        /// Gets the SQL value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static object GetSqlValue(DateTime value)
        {
            if (value != null)
            {
                return string.Format("'{0}'", value.ToString());
            }
            else
            {
                return "NULL";
            }
        }

        /// <summary>
        /// Gets the SQL value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static object GetSqlValue(DateTime? value)
        {
            if (value.HasValue)
            {
                return DataHelper.GetSqlValue(value.Value);
            }
            else
            {
                return "NULL";
            }
        }

        /// <summary>
        /// Gets the SQL value.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public static object GetSqlValue(bool value)
        {
            return value ? "1" : "0";
        }

        /// <summary>
        /// Gets the SQL value.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public static object GetSqlValueEsCliente(bool value)
        {
            return value ? "1" : "0";
        }

        /// <summary>
        /// Gets the SQL value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static object GetSqlValue(Guid value)
        {
            if (value != null)
            {
                return string.Format("'{0}'", value.ToString());
            }
            else
            {
                return "NULL";
            }
        }

        /// <summary>
        /// Gets the SQL value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static object GetSqlValue(EntityBase value)
        {
            if (value == null) return "NULL";
            return GetSqlValue(value.Key);
        }

        /// <summary>
        /// Gets the SQL value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static object GetSqlValue(Enum value)
        {
            if (value != null)
            {
                return DataHelper.GetSqlValue(value.ToString());
            }
            else
            {
                return "NULL";
            }
        }

        /// <summary>
        /// Gets the enum as integer for SQL.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static object GetEnumAsIntegerForSql(Enum value)
        {
            if (value != null)
            {

                return (int)System.Enum.Parse(value.GetType(), value.ToString());
            }
            else
            {
                return "NULL";
            }
        }

        #endregion
    }
}
