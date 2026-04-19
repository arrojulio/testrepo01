-- =============================================
-- Author:		Nicolas Pascual
-- Create date: 06/03/2011
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SP_MasterRefresh_Garantias] 

	-- Add the parameters for the stored procedure here
	@ParamBusinessDate datetime
AS
BEGIN
	
	-- Set Business Date 
	DECLARE @Today datetime
	SET @Today = (SELECT CONVERT(datetime, FLOOR(CONVERT(float, getdate()))))
	IF(@ParamBusinessDate = @Today)
	BEGIN	
		SET @ParamBusinessDate = (SELECT DATEADD(dd, -1, @ParamBusinessDate))
	END
	EXECUTE [dbo].[SP_ETL_BusinessDate] @ParamBusinessDate

	-- Fill Import Oracle Tables.
	DECLARE @Cmd varchar(255)
	SET @Cmd = 'dtexec /SQL "ETL GARANTIAS Import_Garantias"  /ser LOCALHOST /Reporting E '
	EXEC xp_cmdshell @Cmd
	
	--EXEC dbo.SP_CPER_CalculationEngine_MasterExecute

END