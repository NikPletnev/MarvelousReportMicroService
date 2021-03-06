create procedure [dbo].[GetCountLeadTransactionWithoutWithdrawal]
	@LeadId integer
AS
declare @Date date = dateadd(month, -2 , getdate())

	SELECT count(distinct t.[Date]) from [dbo].[Account] a
	inner join [dbo].[Transaction] t
	on t.AccountId = a.ExternalId
	where a.LeadId = @LeadId and (t.[Type] = 1 or t.[Type] = 3 or t.[Type] = 4) and t.[Date] >= @Date