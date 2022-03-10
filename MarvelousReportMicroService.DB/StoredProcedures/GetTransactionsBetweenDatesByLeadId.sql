﻿create procedure [dbo].[GetTransactionsBetweenDatesByLeadId]
	@LeadId integer,
	@StartDate Date,
	@FinishDate date
as
	select T.Id, T.AccountId, T.[Type], T.AccountId, T.[Date], T.Currency from [dbo].[Accounts] as A
	left join [dbo].[Transaction] as T
	on T.AccountId = A.Id
	where LeadId = @LeadId and T.[Date] >= @StartDate and T.[Date] <= @FinishDate