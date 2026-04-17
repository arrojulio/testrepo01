CREATE VIEW [dbo].[VW_Garantias_Report_Revision30]
AS
SELECT     *,
	dbo.UDF_Garantias_ValorGarantia(ID) AS NecessaryGarValueUSD, 
	dbo.UDF_Garantias_ValorPorcCoberturaGarantia(GarantiaContratoId) AS PorcCoberturaGarantia
FROM         dbo.VW_Garantias_GarantiaContratoFT
WHERE     (CONVERT(datetime, FLOOR(CONVERT(float, GETDATE()))) >= DATEADD(dd, - 30, FechaProximaRevisionEvaluacion))