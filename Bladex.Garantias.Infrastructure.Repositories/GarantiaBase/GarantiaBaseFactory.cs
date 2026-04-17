using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Infrastructure.EntityFactoryFramework;

namespace Bladex.Garantias.Infrastructure.Repositories.GarantiaBase
{
    /// <summary>
    /// The garantia base factory class.
    /// </summary>
    internal class GarantiaBaseFactory : IEntityFactory<Bladex.Garantias.DomainModel.DomainBase.GarantiaBase>
    {
        #region Field Names

        /// <summary>
        /// The field names class.
        /// </summary>
        internal static class FieldNames
        {
            public const string GarantiaBaseId = "ID";
            public const string GarantiaBaseID_Atomo = "ID_Atomo";
            public const string GarantiaBaseFCCReference = "FCCReference";
            public const string GarantiaBaseIdentificadorGarantia = "IdentificadorGarantia";
            public const string GarantiaBaseCodigoBanco = "CodigoBanco";
            public const string GarantiaBaseClienteId = "Cliente";
            public const string GarantiaBaseBeneficiario = "Beneficiario";
            public const string GarantiaBaseIdentificacionFideicomiso = "IdentificacionFideicomiso";
            public const string GarantiaBaseFiduciariaSuperId = "FiduciariaSuper";
            public const string GarantiaBaseFiduciaria = "Fiduciaria";
            public const string GarantiaBaseDepositanteId = "Depositante";
            public const string GarantiaBaseEvaluadorId = "Evaluador";
            public const string GarantiaBaseAdministradorId = "Administrador";
            public const string GarantiaBaseAseguradorId = "Asegurador";
            public const string GarantiaBaseRevisorId = "Revisor";
            public const string GarantiaBaseOrigenGarantia = "OrigenGarantia";
            public const string GarantiaBasePaisGarantiaId = "PaisGarantia";
            public const string GarantiaBaseTipoGarantiaSuperId = "TipoGarantiaSuper";
            public const string GarantiaBaseTipoGarantiaBladexId = "TipoGarantiaBladex";
            public const string GarantiaBasegetIdentificacionDocumentoGarantia = "GetIdentificacionDocumentoGarantia";
            public const string GarantiaBasegetNombreOrganismo = "GetNombreOrganismo";
            public const string GarantiaBaseValorInicial = "ValorInicial";
            public const string GarantiaBasegetValorGarantiaSuperIntendencia = "GetValorGarantiaSuperIntendencia";
            public const string GarantiaBaseDescripcionDeLaGarantia = "DescripcionDeLaGarantia";
            public const string GarantiaBaseGaranteId = "Garante";
            public const string GarantiaBaseAttachedToLine = "AttachedToLine";
            public const string GarantiaBaseFechaRegistroInicial = "FechaRegistroInicial";
            public const string GarantiaBaseFechaFormalizacion = "FechaFormalizacion";
            public const string GarantiaBaseFechaVencimientoRiesgo = "FechaVencimientoRiesgo";
            public const string GarantiaBaseFechaVencimientoGarantia = "FechaVencimientoGarantia";
            public const string GarantiaBaseFechaUltimaRevisionEvaluacion = "FechaUltimaRevisionEvaluacion";
            public const string GarantiaBaseFechaProximaRevisionEvaluacion = "FechaProximaRevisionEvaluacion";
            public const string GarantiaBaseFrequenciaRevisionId = "FrequenciaRevision";
            public const string GarantiaBaseFechaVencimientoSeguro = "FechaVencimientoSeguro";
            public const string GarantiaBaseCategoriaRiesgoGarantiaId = "CategoriaRiesgoGarantia";
            public const string GarantiaBaseReduccionDeRiesgoPorPais = "ReduccionDeRiesgoPorPais";
            public const string GarantiaBaseMonedaId = "Moneda";
            public const string GarantiaBaseValorNecesarioDeGarantia = "ValorNecesarioDeGarantia";
            
            public const string GarantiaBasegetRatioCoberturaGarantia = "GetRatioCoberturaGarantia";
            public const string GarantiaBasePorcentajeAplicableMitigacionSuperInt = "PorcentajeAplicableMitigacionSuperInt";
            public const string GarantiaBaseComentarios = "Comentarios";
            public const string GarantiaBaseRatingGaranteId = "RatingGarante";
            public const string GarantiaBaseValorPolizaSeguro = "ValorPolizaSeguro";
            public const string GarantiaBaseNumeroPolizaSeguro = "NumeroPolizaSeguro";
            public const string GarantiaBaseValorMercado = "ValorMercado";
            public const string GarantiaBaseInternalStatusId = "InternalStatus";
            public const string GarantiaBaseStatusId = "Status";
            public const string GarantiaBaseCategoriaSuperId = "CategoriaSuperId";
            public const string GarantiaBaseSource = "Source";
            public const string GarantiaBaseIndAtomo = "IndAtomo";
            public const string GarantiaBaseFechaComienzoEjecucion = "FechaComienzoEjecucion";
            public const string GarantiaBaseFechaCierreEjecucion = "FechaCierreEjecucion";
            public const string GarantiaBaseRegion = "Region";
            public const string GarantiaBaseTipoPoliza = "TipoPoliza";
        }

        #endregion

        #region IEntityFactory<GarantiaBase> Members

        /// <summary>
        /// Returns an instance of a specialization of GarantiaBase.
        /// </summary>
        /// <param name="categoriaSuperId">Key of <see cref="CategoriaSuper"/> entity used to determinate the required instance.</param>
        /// <returns>Specialization of GarantiaBase.</returns>
        private DomainModel.DomainBase.GarantiaBase getInstanceOfGarantia(string categoriaSuperId)
        {
            // 01	Garantía Hipotecaria Mueble
            // 02	Garantía Hipotecaria Inmueble
            // 03	Depósitos Pignorados en el Banco
            // 04	Depósitos Pignorados en Otros Bancos
            // 05	Garantía Prendaria
            // 06	Otras Garantías
            switch (categoriaSuperId)
            {
                case "01": return new DomainModel.DomainBase.GarantiaMueble(); break;
                case "02": return new DomainModel.DomainBase.GarantiaInmueble(); break;
                case "03": return new DomainModel.DomainBase.GarantiaDeposito(); break;
                case "04": return new DomainModel.DomainBase.GarantiaDepositoOtroBanco(); break;
                case "05": return new DomainModel.DomainBase.GarantiaPrenda(); break;
                case "06": return new DomainModel.DomainBase.GarantiaOtra(); break;
                default:
                    throw new ArgumentOutOfRangeException("categoriaSuperId", string.Format("The categoriaSuperId value {0} is not mapped to any specialization of class GarantiaBase.", categoriaSuperId));
                    break;
            }
        }

        /// <summary>
        /// Builds the entity.
        /// </summary>
        /// <param name="reader">The reader of type <see cref="System.Data.IDataReader"/></param>
        /// <returns></returns>
        public DomainModel.DomainBase.GarantiaBase BuildEntity(System.Data.IDataReader reader)
        {
            var categoriaSuperValue = reader[FieldNames.GarantiaBaseCategoriaSuperId].ToString();

            var instanceOfGarantia = getInstanceOfGarantia(categoriaSuperValue);
            instanceOfGarantia.Key = DataHelper.GetInteger(reader[FieldNames.GarantiaBaseId]);
            instanceOfGarantia.ID_Atomo = DataHelper.GetInteger(reader[FieldNames.GarantiaBaseID_Atomo]);
            instanceOfGarantia.FCCReference = reader[FieldNames.GarantiaBaseFCCReference].ToString();
            instanceOfGarantia.NroIncidenteWorkflow = reader[FieldNames.GarantiaBaseIdentificadorGarantia].ToString();
            instanceOfGarantia.IdentificacionFideicomiso = reader[FieldNames.GarantiaBaseIdentificacionFideicomiso].ToString();
            instanceOfGarantia.OrigenGarantia = reader[FieldNames.GarantiaBaseOrigenGarantia].ToString();
            instanceOfGarantia.ValorInicial = DataHelper.GetDecimal(reader[FieldNames.GarantiaBaseValorInicial]);
            instanceOfGarantia.DescripcionDeLaGarantia = reader[FieldNames.GarantiaBaseDescripcionDeLaGarantia].ToString();
            instanceOfGarantia.AttachedToLine = DataHelper.GetNullableBoolean(reader[FieldNames.GarantiaBaseAttachedToLine]);
            instanceOfGarantia.FechaRegistroInicial = DataHelper.GetNullableDateTime(reader[FieldNames.GarantiaBaseFechaRegistroInicial]);
            instanceOfGarantia.FechaFormalizacion = DataHelper.GetNullableDateTime(reader[FieldNames.GarantiaBaseFechaFormalizacion]);
            instanceOfGarantia.FechaVencimientoRiesgo = DataHelper.GetNullableDateTime(reader[FieldNames.GarantiaBaseFechaVencimientoRiesgo]);
            instanceOfGarantia.FechaVencimientoGarantia = DataHelper.GetNullableDateTime(reader[FieldNames.GarantiaBaseFechaVencimientoGarantia]);
            instanceOfGarantia.FechaUltimaRevisionEvaluacion = DataHelper.GetNullableDateTime(reader[FieldNames.GarantiaBaseFechaUltimaRevisionEvaluacion]);
            instanceOfGarantia.NombreOrganismo = DataHelper.GetString(reader[FieldNames.GarantiaBasegetNombreOrganismo]);
            //instanceOfGarantia.FechaProximaRevisionEvaluacion = DataHelper.GetNullableDateTime(reader[FieldNames.GarantiaBaseFechaProximaRevisionEvaluacion]);
            instanceOfGarantia.FechaVencimientoSeguro = DataHelper.GetNullableDateTime(reader[FieldNames.GarantiaBaseFechaVencimientoSeguro]);
            instanceOfGarantia.ReduccionDeRiesgoPorPais = DataHelper.GetDecimal(reader[FieldNames.GarantiaBaseReduccionDeRiesgoPorPais]);
            instanceOfGarantia.FechaComienzoEjecucion = DataHelper.GetNullableDateTime(reader[FieldNames.GarantiaBaseFechaComienzoEjecucion]);
            instanceOfGarantia.FechaCierreEjecucion = DataHelper.GetNullableDateTime(reader[FieldNames.GarantiaBaseFechaCierreEjecucion]);
            //instanceOfGarantia.ValorNecesarioDeGarantia = DataHelper.GetDecimal(reader[FieldNames.GarantiaBaseValorNecesarioDeGarantia]);
            // Commented, ValorNecesarioDeGarantia replaced by UDF_Garantias_ValorGarantia
            
            instanceOfGarantia.PorcentajeAplicableMitigacionSuperInt = DataHelper.GetDecimal(reader[FieldNames.GarantiaBasePorcentajeAplicableMitigacionSuperInt]);
            instanceOfGarantia.Comentarios = reader[FieldNames.GarantiaBaseComentarios].ToString();
            instanceOfGarantia.ValorPolizaSeguro = DataHelper.GetDecimal(reader[FieldNames.GarantiaBaseValorPolizaSeguro]);
            instanceOfGarantia.NumeroPolizaSeguro = reader[FieldNames.GarantiaBaseNumeroPolizaSeguro].ToString();
            instanceOfGarantia.FiduciariaBladex = reader[FieldNames.GarantiaBaseFiduciaria].ToString();
            instanceOfGarantia.ValorMercado = DataHelper.GetDecimal(reader[FieldNames.GarantiaBaseValorMercado]);
            instanceOfGarantia.Beneficiario = DataHelper.GetString(reader[FieldNames.GarantiaBaseBeneficiario]);

            int? sourceInt = DataHelper.GetNullableInteger(reader[FieldNames.GarantiaBaseSource]);
            int? indAtomoInt = DataHelper.GetNullableInteger(reader[FieldNames.GarantiaBaseIndAtomo]);
            if (sourceInt.HasValue)
                instanceOfGarantia.Source = (GarantiaSourceEnum)sourceInt.Value;
            else
                instanceOfGarantia.Source = default(GarantiaSourceEnum?);
            if (indAtomoInt.HasValue)
                instanceOfGarantia.IndAtomo = (IndicadorAtomoEnum)indAtomoInt.Value;
            else
            {
                // Commented after Task N° 933
                // instanceOfGarantia.IndAtomo = default(IndicadorAtomoEnum?);
                instanceOfGarantia.IndAtomo = IndicadorAtomoEnum.NoEstaEnAtomo;
                
            }

            return instanceOfGarantia;
        }

        #endregion
    }
}
