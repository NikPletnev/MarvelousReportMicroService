CREATE PROCEDURE [dbo].[GetAllLeads]

AS
	SELECT ExternalId as Id, Role, Email, Password
	From [dbo].[Lead] 
	where IsBanned = 0

