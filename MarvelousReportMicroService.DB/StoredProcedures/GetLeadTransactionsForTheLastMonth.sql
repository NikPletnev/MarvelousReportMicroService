CREATE PROCEDURE [dbo].[GetLeadTransactionsForTheLastMonth]
 @leadId int,
 @startOfCurrentMonth DATETIME
as
SELECT
       t.Amount,
	   t.Rate
FROM [ReportDb].[dbo].[Transaction] as t
inner join [ReportDb].[dbo].[Account] as a on t.AccountId = a.ExternalId
WHERE t.Date >= DATEADD(month, -1, @startOfCurrentMonth) 
      AND t.Date < @startOfCurrentMonth
	  AND a.LeadId = @leadId
	  AND t.Type = 1
	  AND t.Type = 2