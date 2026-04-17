

CREATE VIEW [dbo].[VW_Garantias_Report_SuperIntendencia]
AS
SELECT 
G.BusinessDate as BusinessDate,
dbo.UDF_Datetime_Format(G.BusinessDate) as FECHA_SIB,
CASE WHEN (G.TipoGarantiaSuper LIKE '03%') THEN '027' ELSE G.CodigoBanco END as CODIGO_BANCO,
CASE WHEN (G.TipoGarantiaSuper IN ('0301', '0401')) THEN Garante.NationalId ELSE Cliente.NationalId END as RUC_DEUDOR,
CASE WHEN (G.IdentificacionFideicomiso IS NULL) THEN 'NA' ELSE G.IdentificacionFideicomiso END as IDENTIFICACION_FIDEICOMISO,
CASE WHEN (G.FiduciariaSuper IS NULL) THEN 'NA' ELSE G.FiduciariaSuper END as NOMBRE_FIDUCIARIA,
G.OrigenGarantia as ORIGEN_GARANTIA,
G.TipoGarantiaSuper as TIPO_GARANTIA,
------------------------------------------------------------------
--Modificacion propuesta para solventar valores en blanco
--Julio Arro - 1-feb-2012
--G.getIdentificacionDocumentoGarantia as IDENTIFICACION_GARANTIA,
G.IdentificadorGarantia as IDENTIFICACION_GARANTIA,
------------------------------------------------------------------
CASE 
	WHEN (GMueble.ID IS NOT NULL) THEN GMueble.AseguradorSuper
	WHEN (GInmueble.ID IS NOT NULL) THEN GInmueble.AseguradorSuper
	WHEN (GDeposito.ID IS NOT NULL) THEN GDeposito.BancoLocalSuper
	WHEN (GDepositoOtroBanco.ID IS NOT NULL) THEN GDepositoOtroBanco.BancoSuper 
	ELSE G.getNombreOrganismo 
END as NOMBRE_ORGANISMO,
G.ValorInicial as valor_inicial,
G.getValorGarantiaSuperIntendencia as valor_garantia,
CASE WHEN (GPrenda.TipoInstrumentoFinanciero IS NULL) THEN 'NA' ELSE GPrenda.TipoInstrumentoFinanciero END as TIPO_INSTRUMENTO,
CASE WHEN (GPrenda.CalificacionEmisor IS NULL) THEN 'NA' ELSE GPrenda.CalificacionEmisor END as CALIFICACION_INSTRUMENTO,
CASE WHEN (GPrenda.CalificacionEmision IS NULL) THEN 'NA' ELSE GPrenda.CalificacionEmision END as CALIFICACION_EMISION,
-- Modified on 08/03/2011 by NP. Bug 1157.
--CASE WHEN (G.PaisGarantia IS NULL) THEN 'NA' ELSE G.PaisGarantia END as PAIS_EMISION,
CASE WHEN (PaisEmision.ID IS NULL) THEN 'NA' ELSE PaisEmision.CodigoSuper END as PAIS_EMISION,
dbo.UDF_Datetime_Format(G.FechaUltimaRevisionEvaluacion) as FECHA_ULTIMA_ACT,
dbo.UDF_Datetime_Format(G.FechaVencimientoGarantia) as FECHA_VENCIMIENTO,
dbo.UDF_Datetime_Format(G.FechaRegistroInicial) as FECHA_REGISTRO_INICIAL,
G.FCCReference,
G.ID
FROM GarantiaBase_History as G
LEFT OUTER JOIN Customer as Garante
ON G.Garante = Garante.ID
LEFT OUTER JOIN Customer as Cliente
ON G.Cliente = Cliente.ID
LEFT OUTER JOIN GarantiaPrenda as GPrenda
ON G.ID = GPrenda.ID
LEFT OUTER JOIN GarantiaMueble_History as GMueble
ON G.ID = GMueble.ID AND G.BusinessDate = GMueble.BusinessDate
LEFT OUTER JOIN GarantiaInmueble_History as GInmueble
ON G.ID = GInmueble.ID AND G.BusinessDate = GInmueble.BusinessDate
LEFT OUTER JOIN GarantiaDeposito_History as GDeposito
ON G.ID = GDeposito.ID AND G.BusinessDate = GDeposito.BusinessDate
LEFT OUTER JOIN GarantiaDepositoOtroBanco_History as GDepositoOtroBanco
ON G.ID = GDepositoOtroBanco.ID AND G.BusinessDate = GDepositoOtroBanco.BusinessDate
LEFT OUTER JOIN Pais as PaisEmision
ON G.PaisGarantia = PaisEmision.ID

WHERE G.Status = 1