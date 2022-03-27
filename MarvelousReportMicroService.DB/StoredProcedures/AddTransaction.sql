CREATE PROCEDURE [dbo].[AddTransaction]
	@ExternalId integer,
	@Amount decimal,
	@AccountId integer,
	@Type tinyint,
	@Currency tinyint,
	@Date date,
    @Rate smallint

AS
	insert into [dbo].[Transaction] ([ExternalId], [Amount], [AccountId], [Type], [Date], [Currency], [Rate])
	values (@ExternalId, @Amount, @AccountId, @Type, @Currency, @Date, @Rate)
