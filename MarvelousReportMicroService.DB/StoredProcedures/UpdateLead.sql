CREATE PROCEDURE [dbo].[UpdateLead]
	@ExternalId integer,
	@Name varchar(20),
	@LastName varchar(20),
	@BirthDay tinyint,
	@BirthMonth tinyint,
	@BirthDate date,
	@Email varchar(30),
	@Phone varchar(13),
	@Password varchar(70),
	@Role tinyint,
	@IsBanned bit,
	@City varchar(30)
AS
update [dbo].[Lead]
set 
[Name] = @Name,
[LastName] = @LastName,
[BirthDay] = @BirthDay,
[BirthMonth] = @BirthMonth,
[BirthDate] = @BirthDate,
[Email] = @Email,
[Phone] = @Phone,
[Password] = @Password,
[Role] = @Role,
[IsBanned] = @IsBanned,
[City] = @City
Where ExternalId = @ExternalId