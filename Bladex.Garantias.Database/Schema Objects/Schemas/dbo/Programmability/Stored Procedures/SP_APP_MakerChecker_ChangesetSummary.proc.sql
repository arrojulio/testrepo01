CREATE PROCEDURE [dbo].[SP_APP_MakerChecker_ChangesetSummary]
AS
BEGIN

SELECT 
C.ChangesetID, 
C.MakerUserID,
C.ChangesetDate,
C.ChangesetCommitDate,
COUNT(OperationID) as TotalOperations, 
SUM(CASE WHEN OperationStatusID = 1 THEN 1 ELSE 0 END) as NewOperations,
SUM(CASE WHEN OperationStatusID = 2 THEN 1 ELSE 0 END) as ApprovedOperations,
SUM(CASE WHEN OperationStatusID = 3 THEN 1 ELSE 0 END) as RejectedOperations,
CASE WHEN (SUM(CASE WHEN OperationStatusID = 1 THEN 1 ELSE 0 END)) > 0 THEN 'Pending' ELSE 'Revised' END as ChangesetStatus
FROM APP_MAKERCHECKER_CHANGESET as C LEFT OUTER JOIN APP_MAKERCHECKER_Operation as O
ON C.ChangesetID = O.ChangesetID
WHERE C.ChangesetCommitDate IS NOT NULL
GROUP BY C.ChangesetID, C.MakerUserID, C.ChangesetDate, C.ChangesetCommitDate

ORDER BY 9, 4

END