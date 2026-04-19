CREATE VIEW [dbo].[VW_Garantias_Report_Incompletas]
AS
SELECT     *,
	dbo.UDF_Garantias_ValorGarantia(ID) AS NecessaryGarValueUSD, 
	dbo.UDF_Garantias_ValorPorcCoberturaGarantia(GarantiaContratoId) AS PorcCoberturaGarantia
FROM         dbo.VW_Garantias_GarantiaContratoFT
WHERE     (IdentificadorGarantia IS NULL) OR
                      (OrigenGarantia IS NULL) OR
                      (ValorInicial IS NULL) OR
                      (FechaRegistroInicial IS NULL) OR
                      (FechaFormalizacion IS NULL) OR
                      (FechaVencimientoRiesgo IS NULL) OR
                      (FechaVencimientoGarantia IS NULL) OR
                      (FechaUltimaRevisionEvaluacion IS NULL) OR
                      (FechaProximaRevisionEvaluacion IS NULL) OR
                      (FechaVencimientoSeguro IS NULL) OR
                      (Garante IS NULL) OR
                      (Cliente IS NULL) OR
                      (PaisGarantia IS NULL) OR
                      (TipoGarantiaSuper IS NULL)