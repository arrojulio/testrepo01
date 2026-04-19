CREATE VIEW [dbo].[VW_Garantias_Report_Insuficiencia]
AS
SELECT 
	*,
	dbo.UDF_Garantias_ValorGarantia(ID) AS NecessaryGarValueUSD, 
	dbo.UDF_Garantias_ValorPorcCoberturaGarantia(GarantiaContratoId) AS PorcCoberturaGarantia
FROM         dbo.VW_Garantias_GarantiaContratoFT AS FT
WHERE     (ID IN
                          (SELECT DISTINCT GB.GarantiaId
                            FROM          (SELECT     GarantiaId, SUM(NetBalancePrincipal) AS NetBalancePrincipalAcc
                                                    FROM          dbo.GarantiaContrato AS GC
                                                    GROUP BY GarantiaId) AS GC_1 INNER JOIN
                                                       (SELECT     ID AS GarantiaId, getValorGarantiaSuperIntendencia
                                                         FROM          dbo.GarantiaBase) AS GB ON GC_1.GarantiaId = GB.GarantiaId AND GC_1.NetBalancePrincipalAcc > GB.getValorGarantiaSuperIntendencia))