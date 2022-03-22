CREATE PROCEDURE [dbo].[GetServicePayTransactionsByLeadIdBetweenDate]
	@LeadId integer,
	@StartDate datetime,
	@EndDate datetime
AS
	SELECT T.[ExternalId] as Id, T.[Amount], T.[AccountId], T.[Currency], T.[Date], T.[Type] from [Transaction] as T
	inner join Account as A
	on A.ExternalId = T.AccountId
	where A.LeadId = @LeadId and T.Type = 4 and T.[Date] >= @StartDate and T.[Date] <= @EndDate