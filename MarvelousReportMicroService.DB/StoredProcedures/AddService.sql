CREATE PROCEDURE [dbo].[AddService]
	@ExternalId int,
	@Name varchar,
	@Type int,
	@Description varchar,
	@Price decimal,
	@IsDeleted bit
AS
	insert into [dbo].[Service] 
    ([ExternalId],
    [Name], 
    [Type], 
    [Description],
    [Price],
    [IsDeleted])
    values 
    (@ExternalId,
    @Name,
    @Type,
    @Description,
    @Price,
    @IsDeleted)