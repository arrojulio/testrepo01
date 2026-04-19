CREATE VIEW dbo.VW_Garantias_ProcessDates
AS
SELECT     (SELECT     TOP (1) BusinessDate
                       FROM          dbo.Import_Oracle_Param_BusinessDate) AS BusinessDate,
                          (SELECT     MAX(BusinessDate) AS Expr1
                            FROM          dbo.TH_ATOMO_GARANTIAS_History) AS LastAtomoDate