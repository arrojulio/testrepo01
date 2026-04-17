CREATE VIEW vw_ApplicationLog_Errors AS
SELECT * FROM ApplicationLog
WHERE [Level] IN ('ERROR', 'FATAL', 'WARNING')