using System;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Bladex.Garantias.DomainModel.Components.TypeConverters
{
    public class PascalCaseTypeConverter : EnumConverter
    {
        // Overrides the CanConvertFrom method of TypeConverter.
        // The ITypeDescriptorContext interface provides the context for the
        // conversion. Typically, this interface is used at design time to 
        // provide information about the design-time container.
        public PascalCaseTypeConverter(Type type) : base(type)
        {
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context,
           Type sourceType)
        {

            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }
        // Overrides the ConvertFrom method of TypeConverter.
        public override object ConvertFrom(ITypeDescriptorContext context,
           CultureInfo culture, object value)
        {
            if (value is string)
            {
                return base.ConvertFrom(context, culture, value);
                return value.ToString().Replace(" ", "");
            }
            return base.ConvertFrom(context, culture, value);
        }
        // Overrides the ConvertTo method of TypeConverter.
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return Regex.Replace(value.ToString(), "([a-z](?=[A-Z])|[A-Z](?=[A-Z][a-z]))", "$1 ");
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
    
}
