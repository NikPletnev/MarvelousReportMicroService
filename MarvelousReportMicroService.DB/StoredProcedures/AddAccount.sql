CREATE PROCEDURE [dbo].[AddAccount]
	@ExternalId integer,
	@Name varchar(20),
    @CurrencyType TINYINT, 
    @LeadId INT,
    @LockDate DATETIME,
    @IsBlocked BIT
AS
	insert into [dbo].[Account] 
    ([ExternalId],
    [Name], 
    [CurrencyType], 
    [LeadId],
    [LockDate],
    [IsBlocked])
    values 
    (@ExternalId,
    @Name,
    @CurrencyType,
    @LeadId,
    @LockDate,
    @IsBlocked)
