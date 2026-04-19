CREATE VIEW VW_Garantias_GarantiaContratoFT
AS
SELECT     BusinessDate, ID, ID_Atomo, FCCReference, IdentificadorGarantia, CodigoBanco, Cliente, Beneficiario, IdentificacionFideicomiso, FiduciariaSuper, Fiduciaria, 
                      DepositanteNombre, Evaluador, AdministradorNombre, Asegurador, RevisorNombre, OrigenGarantia, PaisGarantia, TipoGarantiaSuper, TipoGarantiaBladex, 
                      getIdentificacionDocumentoGarantia, getNombreOrganismo, ValorInicial, getValorGarantiaSuperIntendencia, DescripcionDeLaGarantia, Garante, AttachedToLine, 
                      FechaRegistroInicial, FechaFormalizacion, FechaVencimientoRiesgo, FechaVencimientoGarantia, FechaUltimaRevisionEvaluacion, FechaProximaRevisionEvaluacion, 
                      FrequenciaRevision, FechaVencimientoSeguro, CategoriaRiesgoGarantia, ReduccionDeRiesgoPorPais, Moneda, ValorNecesarioDeGarantia, 
                      getRatioCoberturaGarantia, PorcentajeAplicableMitigacionSuperInt, Comentarios, RatingGarante, ValorPolizaSeguro, NumeroPolizaSeguro, ValorMercado, 
                      InternalStatus AS Status, CategoriaSuperId, Source, IndAtomo, GarantiaPrendaId, GarantiaPrendaEmisor, GarantiaPrendaTipoInstrumentoFinanciero, 
                      GarantiaPrendaCalificacionEmision, GarantiaPrendaCalificacionEmisor, GarantiaPrendaPaisEmision, GarantiaPrendaIdentificadorPrenda, GarantiaDepositoId, 
                      GarantiaDepositoBancoLocalSuper, GarantiaDepositoOtroBancoId, GarantiaDepositoOtroBancoBancoSuper, GarantiaInmuebleId, GarantiaInmuebleAseguradorSuper, 
                      GarantiaInmuebleValorEvaluacionVentaRapida, GarantiaInmuebleValorAvaluo, GarantiaInmuebleInscripcionRegistroPublico, GarantiaInmuebleNumeroDeFinca, 
                      GarantiaMuebleId, GarantiaMuebleAseguradorSuper, GarantiaOtraId, GarantiaOtraEmisor, LineaCreditoRef, NetBalancePrincipal, ValorInsuficienciaGarantia, 
                      GarantiaContratoId, DealReference
FROM         dbo.VW_Garantias_GarantiaContratoFT_Source
WHERE     (InternalStatus = 1)