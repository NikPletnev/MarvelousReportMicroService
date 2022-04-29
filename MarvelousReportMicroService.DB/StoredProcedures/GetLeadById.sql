CREATE PROCEDURE [dbo].[GetLeadById]
   @Id int
AS
    SELECT ExternalId as Id
	From [dbo].[Lead] 
	where [ExternalId] = @Id
