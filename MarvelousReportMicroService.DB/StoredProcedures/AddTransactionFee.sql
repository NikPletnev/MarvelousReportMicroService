CREATE PROCEDURE [dbo].[AddTransactionFee]
	@TransactionId int,
	@AmountComission decimal
AS
	insert into [dbo].[TransactionFee]
	([TransactionId],
	[AmountComission])
	values(
	@TransactionId,
	@AmountComission)