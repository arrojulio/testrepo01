-- =============================================
-- Author:		Nicolas Pascual
-- Create date: 06/03/2011
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SP_MasterRefresh_Garantias_Period] 

	-- Add the parameters for the stored procedure here
	@StartBusinessDate datetime,
	@EndBusinessDate datetime

AS
BEGIN
	
	DECLARE @Current datetime
	SET @Current = @StartBusinessDate

	WHILE (@Current <= @EndBusinessDate)
	BEGIN
		-- Set Business Date 
		EXECUTE [dbo].[SP_ETL_BusinessDate] @Current

		-- Fill Import Oracle Tables.
		DECLARE @Cmd varchar(255)
		SET @Cmd = 'dtexec /SQL "ETL GARANTIAS Import_Garantias"  /ser LOCALHOST /Reporting E '
		EXEC xp_cmdshell @Cmd
		
		SET @Current = DATEADD(dd,1,@Current)
	END
END