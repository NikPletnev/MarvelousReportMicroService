CREATE PROCEDURE [dbo].[AddTransaction]
	@ExternalId int,
	@Amount decimal,
	@AccountId integer,
	@Type tinyint,
	@Currency tinyint,
	@Date datetime2,
    @Rate smallint
AS
	insert into [dbo].[Transaction] ([ExternalId], [Amount], [AccountId], [Type], [Date], [Currency], [Rate])
	values (@ExternalId, @Amount, @AccountId, @Type, @Date, @Currency, @Rate)
