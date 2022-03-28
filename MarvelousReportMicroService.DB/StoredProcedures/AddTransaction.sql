CREATE PROCEDURE [dbo].[AddTransaction]
	@ExternalId int,
	@Amount decimal,
	@AccountId integer,
	@Type tinyint,
	@Currency tinyint,
	@Date datetime2,
    @Rate smallint,
	@RecipientId int

AS
	insert into [dbo].[Transaction] ([ExternalId], [Amount], [AccountId], [Type], [Date], [Currency], [Rate], [RecipientId])
	values (@ExternalId, @Amount, @AccountId, @Type, @Date, @Currency, @Rate, @RecipientId)
