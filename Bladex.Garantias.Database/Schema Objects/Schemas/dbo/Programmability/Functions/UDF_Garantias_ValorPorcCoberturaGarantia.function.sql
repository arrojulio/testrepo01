-- =============================================
-- Author:		Juan Manuel Moyano
-- Create date: 08/10/2011
-- Description:	Funcion que dado el ID de una Garantia de Novaris, retorna el Porcentaje de Cobertura de Garantia
-- =============================================
CREATE FUNCTION [dbo].[UDF_Garantias_ValorPorcCoberturaGarantia] 
(
	-- Add the parameters for the function here
	@ContratoId int
)
RETURNS money
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result money

	-- Add the T-SQL statements to compute the return value here
	SELECT @Result = (SELECT PorcUtilization as PorcCoberturaGarantia  FROM GarantiaContrato
	WHERE Id = @ContratoId)

	-- Return the result of the function
	RETURN @Result

END