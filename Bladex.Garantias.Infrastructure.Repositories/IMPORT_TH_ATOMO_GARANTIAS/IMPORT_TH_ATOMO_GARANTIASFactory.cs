using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Infrastructure.EntityFactoryFramework;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.Infrastructure.RepositoryFramework;

namespace Bladex.Garantias.Infrastructure.Repositories.IMPORT_TH_ATOMO_GARANTIAS
{
    internal class IMPORT_TH_ATOMO_GARANTIASFactory : IEntityFactory<Bladex.Garantias.DomainModel.DomainBase.IMPORT_TH_ATOMO_GARANTIAS>
    {
        #region Field Names

        internal static class FieldNames
        {
            public const string ID = "ID";
            public const string Fecha_corte = "Fecha_corte";
            public const string FECHA_SIB = "FECHA_SIB";
            public const string CODIGO_BANCO = "CODIGO_BANCO";
            public const string RUC_DEUDOR = "RUC_DEUDOR";
            public const string IDENTIFICACION_FIDEICOMISO = "IDENTIFICACION_FIDEICOMISO";
            public const string NOMBRE_FIDUCIARIA = "NOMBRE_FIDUCIARIA";
            public const string ORIGEN_GARANTIA = "ORIGEN_GARANTIA";
            public const string TIPO_GARANTIA = "TIPO_GARANTIA";
            public const string IDENTIFICACION_GARANTIA = "IDENTIFICACION_GARANTIA";
            public const string NOMBRE_ORGANISMO = "NOMBRE_ORGANISMO";
            public const string VALOR_INICIAL = "VALOR_INICIAL";
            public const string VALOR_GARANTIA = "VALOR_GARANTIA";
            public const string TIPO_INSTRUMENTO = "TIPO_INSTRUMENTO";
            public const string CALIFICACION_INSTRUMENTO = "CALIFICACION_INSTRUMENTO";
            public const string CALIFICACION_EMISION = "CALIFICACION_EMISION";
            public const string PAIS_EMISION = "PAIS_EMISION";
            public const string FECHA_ULTIMA_ACT = "FECHA_ULTIMA_ACT";
            public const string FECHA_VENCIMIENTO = "FECHA_VENCIMIENTO";
            public const string id_sib = "id_sib";
            public const string numero_prestamo = "numero_prestamo";
            public const string cliente_garantia = "cliente_garantia";
            public const string nombre_cliente_garantia = "nombre_cliente_garantia";
            public const string cliente_prestamo = "cliente_prestamo";
            public const string nombre_cliente_prestamo = "nombre_cliente_prestamo";
            public const string APLICANTE = "APLICANTE";
            public const string BENEFICIARIO = "BENEFICIARIO";
            public const string ORIGEN_TRANS = "ORIGEN_TRANS";
            public const string IND_ATOMO = "IND_ATOMO";
            public const string DEAL_STAT_FROM_DT = "DEAL_STAT_FROM_DT";
            public const string DEAL_CLOSURE_DATE = "DEAL_CLOSURE_DATE";
            public const string EFFECTIVE_DATE = "EFFECTIVE_DATE";

        }

        #endregion

        #region IEntityFactory<GarantiaBase> Members

        public DomainModel.DomainBase.IMPORT_TH_ATOMO_GARANTIAS BuildEntity(System.Data.IDataReader reader)
        {
            //var import = AutoMapper.Mapper.Map<System.Data.IDataReader, Bladex.Garantias.DomainModel.DomainBase.IMPORT_TH_ATOMO_GARANTIAS>(reader);
            var import = new DomainModel.DomainBase.IMPORT_TH_ATOMO_GARANTIAS();
            import.APLICANTE = reader[FieldNames.APLICANTE].ToString().Trim();
            import.BENEFICIARIO = reader[FieldNames.BENEFICIARIO].ToString().Trim();
            import.CALIFICACION_EMISION = reader[FieldNames.CALIFICACION_EMISION].ToString().Trim();
            import.CALIFICACION_INSTRUMENTO = reader[FieldNames.CALIFICACION_INSTRUMENTO].ToString().Trim();
            import.cliente_garantia = reader[FieldNames.cliente_garantia].ToString().Trim();
            import.cliente_prestamo = reader[FieldNames.cliente_prestamo].ToString().Trim();
            import.CODIGO_BANCO = reader[FieldNames.CODIGO_BANCO].ToString().Trim();
            import.Fecha_corte = DataHelper.GetDateTime(reader[FieldNames.Fecha_corte]);
            import.FECHA_SIB = reader[FieldNames.FECHA_SIB].ToString();
            import.FECHA_ULTIMA_ACT = reader[FieldNames.FECHA_ULTIMA_ACT].ToString().Trim();
            import.FECHA_VENCIMIENTO = reader[FieldNames.FECHA_VENCIMIENTO].ToString().Trim();
            import.ID = DataHelper.GetInteger(reader[FieldNames.ID]);
            import.id_sib = reader[FieldNames.id_sib].ToString().Trim();
            import.IDENTIFICACION_FIDEICOMISO = reader[FieldNames.IDENTIFICACION_FIDEICOMISO].ToString().Trim();
            import.IDENTIFICACION_GARANTIA = DataHelper.GetString(reader[FieldNames.IDENTIFICACION_GARANTIA]);
            import.Key = import.ID;
            import.nombre_cliente_garantia = reader[FieldNames.nombre_cliente_garantia].ToString().Trim();
            import.nombre_cliente_prestamo = reader[FieldNames.nombre_cliente_prestamo].ToString().Trim();
            import.NOMBRE_FIDUCIARIA = reader[FieldNames.NOMBRE_FIDUCIARIA].ToString().Trim();
            import.NOMBRE_ORGANISMO = reader[FieldNames.NOMBRE_ORGANISMO].ToString().Trim();
            import.numero_prestamo = reader[FieldNames.numero_prestamo].ToString().Trim();
            import.ORIGEN_GARANTIA = reader[FieldNames.ORIGEN_GARANTIA].ToString().Trim();
            import.ORIGEN_TRANS = reader[FieldNames.ORIGEN_TRANS].ToString().Trim();
            import.PAIS_EMISION = reader[FieldNames.PAIS_EMISION].ToString().Trim();
            import.RUC_DEUDOR = reader[FieldNames.RUC_DEUDOR].ToString().Trim();
            import.TIPO_GARANTIA = reader[FieldNames.TIPO_GARANTIA].ToString().Trim();
            import.TIPO_INSTRUMENTO = reader[FieldNames.TIPO_INSTRUMENTO].ToString().Trim();
            import.VALOR_GARANTIA = DataHelper.GetDecimal(reader[FieldNames.VALOR_GARANTIA]);
            import.VALOR_INICIAL = DataHelper.GetDecimal(reader[FieldNames.VALOR_INICIAL]);
            import.IND_ATOMO = reader[FieldNames.IND_ATOMO].ToString().Trim();

            import.DEAL_CLOSURE_DATE = DataHelper.GetNullableDateTime(reader[FieldNames.DEAL_CLOSURE_DATE]);
            import.DEAL_STAT_FROM_DT = DataHelper.GetNullableDateTime(reader[FieldNames.DEAL_STAT_FROM_DT]);
            import.EFFECTIVE_DATE = DataHelper.GetNullableDateTime(reader[FieldNames.EFFECTIVE_DATE]);
            
            return import;

        }

        #endregion
    }
}
