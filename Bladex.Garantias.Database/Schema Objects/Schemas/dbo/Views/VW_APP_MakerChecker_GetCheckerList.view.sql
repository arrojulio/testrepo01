CREATE VIEW [dbo].[VW_APP_MakerChecker_GetCheckerList]
AS
	--SELECT logName AS UserID FROM dbo.APP_MakerChecker_CheckerUser
	SELECT UserId as UserID FROM APP_MakerChecker_User
	WHERE RoleId = 2