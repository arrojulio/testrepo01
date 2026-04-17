-- =============================================
-- Author:		Nicolas Pascual
-- Create date: 06/03/2011
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SP_ETL_BusinessDate] 
	
	@BusinessDate datetime
	
AS
BEGIN
	TRUNCATE TABLE Import_Oracle_Param_BusinessDate
	INSERT INTO Import_Oracle_Param_BusinessDate
	SELECT @BusinessDate
	DECLARE @Date nvarchar(max)
	SET @Date = CONVERT(nvarchar(150),CONVERT(nvarchar(2), MONTH(@BusinessDate)) + '/' + CONVERT(nvarchar(2), DAY(@BusinessDate)) + '/' + CONVERT(nvarchar(4), YEAR(@BusinessDate)))
	
	UPDATE [ETL_Parameter]
	SET [Value] = @Date
	WHERE ID = 'Today'
		
END