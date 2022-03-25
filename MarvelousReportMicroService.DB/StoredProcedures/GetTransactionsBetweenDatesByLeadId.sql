create procedure [dbo].[GetTransactionsBetweenDatesByLeadId]
	@LeadId integer,
	@StartDate Date,
	@FinishDate date
as
	select T.ExternalId, T.Amount, T.AccountId, T.[Type], T.AccountId, T.[Date], T.Currency, T.Rate from [dbo].[Account] as A
	left join [dbo].[Transaction] as T
	on T.AccountId = A.Id
	where LeadId = @LeadId and T.[Date] >= @StartDate and T.[Date] <= @FinishDate