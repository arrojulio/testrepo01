using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Bladex.Garantias.DomainModel.DomainBase;

namespace Bladex.Garantias.Infrastructure.UI.AutoMapper
{
    public class DateTimeTypeConverter : ITypeConverter<DateTime, DateTime?>
    {
        public DateTime? Convert(ResolutionContext context)
        {
            if (context.IsSourceValueNull)
                return default( DateTime? );
            if (context.SourceType == typeof(DateTime) || context.SourceType == typeof(DateTime?))
            {
                //1/1/0001 12:00:00 AM
                DateTime sourceDate = (DateTime)context.SourceValue;
                if (sourceDate.ToShortDateString() == "1/1/0001")
                    return default(DateTime?);
            }

            return (DateTime) context.SourceValue;

        }
    }

    public class EnumTypeConverter : ITypeConverter<Bladex.Garantias.DomainModel.DomainBase.IndicadorAtomoEnum, Bladex.Garantias.DomainModel.DomainBase.IndicadorAtomoEnum?>
    {
        #region Implementation of ITypeConverter<IndicadorAtomoEnum,IndicadorAtomoEnum?>

        public IndicadorAtomoEnum? Convert(ResolutionContext context)
        {
            if (context.IsSourceValueNull)
            {
                return default(IndicadorAtomoEnum?);
            }
            if (context.SourceType == typeof(IndicadorAtomoEnum) || context.SourceType == typeof(IndicadorAtomoEnum?))
            {
                IndicadorAtomoEnum source = (IndicadorAtomoEnum)context.SourceValue;
                return source;
            }
            return (IndicadorAtomoEnum)context.SourceValue;
        }

        #endregion
    }
}
