CREATE PROCEDURE [dbo].[AddTransaction]
	@ExternalId integer,
	@Amount decimal,
	@AccountId integer,
	@Type tinyint,
	@Currency tinyint,
	@Date date
AS
	insert into [dbo].[Transaction] ([ExternalId], [Amount], [AccountId], [Type], [Date], [Currency])
	values (@ExternalId, @Amount, @AccountId, @Type, @Currency, @Date)
