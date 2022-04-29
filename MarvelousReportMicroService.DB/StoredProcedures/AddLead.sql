CREATE PROCEDURE [dbo].[AddLead]
	@ExternalId integer,
	@Name varchar(20),
	@LastName varchar(20),
	@BirthDay tinyint,
	@BirthMounth tinyint,
	@BirthDate date,
	@Email varchar(30),
	@Phone varchar(13),
	@Password varchar(70),
	@Role tinyint,
	@IsBanned bit,
	@City varchar(30)
AS
	insert into [dbo].[Lead] 
	([ExternalId], 
	[Name], 
	[LastName], 
	[BirthDay], 
	[BirthMonth], 
	[BirthDate], 
	[Email],
	[Phone],
	[Password],
	[Role],
	[IsBanned],
	[City])
values (@ExternalId, @Name, @LastName, @BirthDay, @BirthMounth, @BirthDate, @Email, @Phone, @Password, @Role, @IsBanned, @City)




