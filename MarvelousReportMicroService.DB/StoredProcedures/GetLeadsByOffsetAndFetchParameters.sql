CREATE PROCEDURE [dbo].[GetLeadsByOffsetAndFetchParameters]
	@Offset int,
	@Fetch int
AS
SELECT  [Id]
      ,[ExternalId]
      ,[Name]
      ,[LastName]
      ,[BirthDate]
      ,[Email]
      ,[Phone]
      ,[Password]
      ,[Role]
      ,[IsBanned]
      ,[City]
  FROM [ReportDb].[dbo].[Lead] ORDER by Id
  OFFSET @Offset ROWS  Fetch next @Fetch rows only 

