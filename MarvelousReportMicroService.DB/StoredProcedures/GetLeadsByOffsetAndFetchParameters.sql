CREATE PROCEDURE [dbo].[GetLeadsByOffsetAndFetchParameters]
	@Offset int,
	@Fetch int
AS
SELECT  
      [ExternalId] as [Id],
      [Name],
      [LastName],
      [BirthDay],
      [BirthMonth],
      [BirthDate],
      [Email],
      [Phone],
      [Password],
      [Role],
      [IsBanned],
      [City]
  FROM [ReportDb].[dbo].[Lead] 
  Where [Role] != 1
  ORDER by [Id]
  OFFSET @Offset ROWS  Fetch next @Fetch rows only 

