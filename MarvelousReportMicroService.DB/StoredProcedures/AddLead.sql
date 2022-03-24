CREATE PROCEDURE [dbo].[AddLead]
	@ExternalId integer,
	@Name varchar,
	@LastName varchar,
	@BirthDay tinyint,
	@BirthMount tinyint,
	@BirthYear smallint,
	@Email varchar,
	@Phone varchar,
	@Password varchar,
	@Role tinyint,
	@IsBanned bit,
	@City varchar
AS
	insert into [dbo].[Lead] 
	([ExternalId], 
	[Name], 
	[LastName], 
	[BirthDay], 
	[BirthMonth], 
	[BirthYear], 
	[Email],
	[Phone],
	[Password],
	[Role],
	[IsBanned],
	[City])
values (@ExternalId, @Name, @LastName, @BirthDay, @BirthMount, @BirthYear, @Email, @Phone, @Password, @Role, @IsBanned, @City)




