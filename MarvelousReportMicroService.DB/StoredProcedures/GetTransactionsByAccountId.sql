CREATE PROCEDURE [dbo].[GetTransactionsByAccountId]
	@AccountId integer
as
	select A.[ExternalId], A.[Amount], A.[Type], A.[AccountId], A.[Date], A.[Currency] from [dbo].[Transaction] as A
	where A.AccountId = @AccountId