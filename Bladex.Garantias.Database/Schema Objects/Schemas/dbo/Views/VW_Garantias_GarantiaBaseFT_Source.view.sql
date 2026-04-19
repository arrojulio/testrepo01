CREATE VIEW VW_Garantias_GarantiaBaseFT_Source
AS
SELECT     CONVERT(datetime, FLOOR(CONVERT(float,
                          (SELECT     MAX(BusinessDate) AS Expr1
                            FROM          dbo.GarantiaBase_History)))) AS BusinessDate, dbo.GarantiaBase.ID, dbo.GarantiaBase.ID_Atomo, dbo.GarantiaBase.FCCReference, 
                      dbo.GarantiaBase.IdentificadorGarantia, dbo.GarantiaBase.CodigoBanco, dbo.GarantiaBase.Cliente, dbo.GarantiaBase.Beneficiario, 
                      dbo.GarantiaBase.IdentificacionFideicomiso, dbo.GarantiaBase.FiduciariaSuper, dbo.GarantiaBase.Fiduciaria, Depositante.Nombre AS DepositanteNombre, 
                      dbo.GarantiaBase.Evaluador, Administrador.Nombre AS AdministradorNombre, dbo.GarantiaBase.Asegurador, Revisor.Nombre AS RevisorNombre, 
                      dbo.GarantiaBase.OrigenGarantia, dbo.GarantiaBase.PaisGarantia, dbo.GarantiaBase.TipoGarantiaSuper, dbo.GarantiaBase.TipoGarantiaBladex, 
                      dbo.GarantiaBase.getIdentificacionDocumentoGarantia, dbo.GarantiaBase.getNombreOrganismo, dbo.GarantiaBase.ValorInicial, 
                      dbo.GarantiaBase.getValorGarantiaSuperIntendencia, dbo.GarantiaBase.DescripcionDeLaGarantia, ISNULL(dbo.GarantiaBase.Garante, 'NA') AS Garante, 
                      dbo.GarantiaBase.AttachedToLine, ISNULL(dbo.GarantiaBase.FechaRegistroInicial, '1/1/1900') AS FechaRegistroInicial, 
                      ISNULL(dbo.GarantiaBase.FechaFormalizacion, '1/1/1900') AS FechaFormalizacion, ISNULL(dbo.GarantiaBase.FechaVencimientoRiesgo, '1/1/1900') 
                      AS FechaVencimientoRiesgo, ISNULL(dbo.GarantiaBase.FechaVencimientoGarantia, '1/1/1900') AS FechaVencimientoGarantia, 
                      ISNULL(dbo.GarantiaBase.FechaUltimaRevisionEvaluacion, '1/1/1900') AS FechaUltimaRevisionEvaluacion, 
                      ISNULL(dbo.GarantiaBase.FechaProximaRevisionEvaluacion, '1/1/1900') AS FechaProximaRevisionEvaluacion, dbo.GarantiaBase.FrequenciaRevision, 
                      ISNULL(dbo.GarantiaBase.FechaVencimientoSeguro, '1/1/1900') AS FechaVencimientoSeguro, dbo.GarantiaBase.CategoriaRiesgoGarantia, 
                      dbo.GarantiaBase.ReduccionDeRiesgoPorPais, dbo.GarantiaBase.Moneda, dbo.GarantiaBase.ValorNecesarioDeGarantia, 
                      dbo.GarantiaBase.getRatioCoberturaGarantia, dbo.GarantiaBase.PorcentajeAplicableMitigacionSuperInt, dbo.GarantiaBase.Comentarios, 
                      dbo.GarantiaBase.RatingGarante, dbo.GarantiaBase.ValorPolizaSeguro, dbo.GarantiaBase.NumeroPolizaSeguro, dbo.GarantiaBase.ValorMercado, 
                      dbo.GarantiaBase.InternalStatus, dbo.GarantiaBase.CategoriaSuperId, dbo.GarantiaBase.Source, dbo.GarantiaBase.IndAtomo, dbo.GarantiaPrenda.ID AS GarantiaPrendaId, 
                      dbo.GarantiaPrenda.Emisor AS GarantiaPrendaEmisor, dbo.GarantiaPrenda.TipoInstrumentoFinanciero AS GarantiaPrendaTipoInstrumentoFinanciero, 
                      dbo.GarantiaPrenda.CalificacionEmision AS GarantiaPrendaCalificacionEmision, dbo.GarantiaPrenda.CalificacionEmisor AS GarantiaPrendaCalificacionEmisor, 
                      dbo.GarantiaPrenda.PaisEmision AS GarantiaPrendaPaisEmision, dbo.GarantiaPrenda.IdentificadorPrenda AS GarantiaPrendaIdentificadorPrenda, 
                      dbo.GarantiaDeposito.ID AS GarantiaDepositoId, dbo.GarantiaDeposito.BancoLocalSuper AS GarantiaDepositoBancoLocalSuper, 
                      dbo.GarantiaDepositoOtroBanco.ID AS GarantiaDepositoOtroBancoId, dbo.GarantiaDepositoOtroBanco.BancoSuper AS GarantiaDepositoOtroBancoBancoSuper, 
                      dbo.GarantiaInmueble.ID AS GarantiaInmuebleId, dbo.GarantiaInmueble.AseguradorSuper AS GarantiaInmuebleAseguradorSuper, 
                      dbo.GarantiaInmueble.ValorEvaluacionVentaRapida AS GarantiaInmuebleValorEvaluacionVentaRapida, 
                      dbo.GarantiaInmueble.ValorAvaluo AS GarantiaInmuebleValorAvaluo, 
                      dbo.GarantiaInmueble.InscripcionRegistroPublico AS GarantiaInmuebleInscripcionRegistroPublico, 
                      dbo.GarantiaInmueble.NumeroDeFinca AS GarantiaInmuebleNumeroDeFinca, dbo.GarantiaMueble.ID AS GarantiaMuebleId, 
                      dbo.GarantiaMueble.AseguradorSuper AS GarantiaMuebleAseguradorSuper, dbo.GarantiaOtra.ID AS GarantiaOtraId, dbo.GarantiaOtra.Emisor AS GarantiaOtraEmisor, 
                      CASE WHEN isnull(dbo.GarantiaBase.getValorGarantiaSuperIntendencia, 0) - isnull(dbo.UDF_Garantias_ValorGarantia(dbo.GarantiaBase.ID), 0) < 0 THEN (- 1.0) 
                      * (isnull(dbo.GarantiaBase.getValorGarantiaSuperIntendencia, 0) - isnull(dbo.UDF_Garantias_ValorGarantia(dbo.GarantiaBase.ID), 0)) 
                      ELSE 0 END AS ValorInsuficienciaGarantia
FROM         dbo.GarantiaBase LEFT OUTER JOIN
                      dbo.GarantiaPrenda ON dbo.GarantiaBase.ID = dbo.GarantiaPrenda.ID LEFT OUTER JOIN
                      dbo.GarantiaDeposito ON dbo.GarantiaBase.ID = dbo.GarantiaDeposito.ID LEFT OUTER JOIN
                      dbo.GarantiaDepositoOtroBanco ON dbo.GarantiaBase.ID = dbo.GarantiaDepositoOtroBanco.ID LEFT OUTER JOIN
                      dbo.GarantiaInmueble ON dbo.GarantiaBase.ID = dbo.GarantiaInmueble.ID LEFT OUTER JOIN
                      dbo.GarantiaMueble ON dbo.GarantiaBase.ID = dbo.GarantiaMueble.ID LEFT OUTER JOIN
                      dbo.GarantiaOtra ON dbo.GarantiaBase.ID = dbo.GarantiaOtra.ID LEFT OUTER JOIN
                      dbo.Actor AS Depositante ON dbo.GarantiaBase.Depositante = Depositante.ID LEFT OUTER JOIN
                      dbo.Actor AS Revisor ON dbo.GarantiaBase.Revisor = Revisor.ID LEFT OUTER JOIN
                      dbo.Actor AS Administrador ON dbo.GarantiaBase.Administrador = Administrador.ID