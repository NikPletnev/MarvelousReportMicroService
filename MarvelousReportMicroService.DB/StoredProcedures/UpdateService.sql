CREATE PROCEDURE [dbo].[UpdateService]
	@ExternalId int,
	@Name varchar,
	@Type int,
	@Description varchar,
	@Price decimal,
	@IsDeleted bit
AS
update [dbo].[Service]
set 
[Name] = @Name,
[Type] = @Type,
[Description] = @Description,
[Price] = @Price,
[IsDeleted] = @IsDeleted
Where ExternalId = @ExternalId