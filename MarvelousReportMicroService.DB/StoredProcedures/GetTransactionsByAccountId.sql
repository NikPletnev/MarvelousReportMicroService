create procedure [dbo].[GetTransactionsByAccountId]
	@AccountId integer
as
	select Id, Amount, [Type], AccountId, [Date], Currency from [dbo].[Transaction]
	where AccountId = @AccountId