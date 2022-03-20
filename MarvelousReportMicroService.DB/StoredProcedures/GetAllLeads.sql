CREATE PROCEDURE [dbo].[GetAllLeads]

AS
	select
		[ExternalId] as Id,
		[Name], 
		[LastName],
		[BirthDate],
		[Email],
		[Phone],
		[Password],
		[Role],
		[IsBanned]
	from dbo.[Lead]