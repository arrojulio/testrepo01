-- =============================================
-- Author:		Intelledata - Nicolas Pascual
-- Create date: 06/27/2011
-- Description:	Toma un valor datetime y devuelve un valor string formateado yyyymmdd
-- =============================================
CREATE FUNCTION [dbo].[UDF_Datetime_Format] 
(
	@ParamDate datetime
)
RETURNS nvarchar(8)
AS
BEGIN
	IF(@ParamDate IS NULL)
	BEGIN 
		RETURN 'NA'
	END
	DECLARE @Result nvarchar(8)
	DECLARE @Month nvarchar(2), @Day nvarchar(2)
	
	SET @Month = (SELECT CONVERT(NVARCHAR(2),MONTH(@ParamDate)))
	IF(MONTH(@ParamDate) < 10) SET @Month = '0' + @Month

	SET @Day = (SELECT CONVERT(NVARCHAR(2),DAY(@ParamDate)))
	IF(DAY(@ParamDate) < 10) SET @Day = '0' + @Day

	SET @Result = (SELECT CONVERT(NVARCHAR(4),YEAR(@ParamDate)) + @Month + @Day)
	RETURN @Result

END