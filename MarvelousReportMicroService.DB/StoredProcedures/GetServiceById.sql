CREATE PROCEDURE [dbo].[GetServiceById]
   @Id int
AS
    SELECT ExternalId as Id
	From [dbo].[Service] 
	where [ExternalId] = @Id
