CREATE PROCEDURE [dbo].[GetLeadTransactionsForTheLastMonth]
 @leadId int,
 @startOfCurrentMonth DATETIME
as
SELECT t.ExternalId,
       t.Amount,
	   t.Type,
	   t.AccountId,
	   t.Date,
	   t.Currency,
	   t.Rate,
	   a.ExternalId as AccountId,
	   a.LeadId
FROM [ReportDb].[dbo].[Transaction] as t
inner join [ReportDb].[dbo].[Account] as a on t.AccountId = a.ExternalId
WHERE t.Date >= DATEADD(month, -1, @startOfCurrentMonth) 
      AND t.Date < @startOfCurrentMonth
	  AND a.LeadId = @leadId
	  AND t.Type = 1
	  AND t.Type = 2