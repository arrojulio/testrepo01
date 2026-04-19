-- =============================================
CREATE FUNCTION [dbo].[UDF_Garantias_ValorGarantia] 
(
	@GarantiaId int

)
RETURNS money
AS
BEGIN
	DECLARE @Result money
	-- Calc Necessary Guarantee Value
	SELECT @Result = (SELECT SUM((ISNULL(NetBalancePrincipal, 0) * ISNULL(PorcUtilization,0)) / 100 ) FROM GarantiaContrato
	WHERE GarantiaId = @GarantiaId)

	-- Return the Necessary Guarantee Value
	RETURN @Result

END