CREATE VIEW [dbo].[vw_GarantiaContrato_DealReference]
AS
SELECT 
	FIC_MIS_DATE, 
	DEAL_REF as DealReference, 
	PRODUCT_GROUP as ProductGroupId, 
	CUSTOMER_NO as CustomerId, 
	GLOBAL_LINE_DESC as GlobalLineDesc,
	DEAL_STAT_FROM_DT as FechaRegistroInicial, 
	EFFECTIVE_DATE as FechaVencimientoGarantia, 
	MAX(DEAL_CLOSURE_DATE) as FechaVencimientoRiesgo, 
	SUM(NET_BALANCE) as NetBalancePrincipal
FROM IMPORT_Finance_TransactionalInfo_FT
WHERE PRODUCT_GROUP IN (SELECT DISTINCT ProductGroupCode FROM GarantiaContrato_ProductFilter)
GROUP BY FIC_MIS_DATE, DEAL_REF, PRODUCT_GROUP, CUSTOMER_NO, GLOBAL_LINE_DESC, DEAL_STAT_FROM_DT, EFFECTIVE_DATE