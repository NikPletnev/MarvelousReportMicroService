CREATE PROCEDURE [dbo].[GetLeadsCountByRole]
   @Role int
AS
	SELECT count(*) from [dbo].[Lead]
	where Role = @Role 

