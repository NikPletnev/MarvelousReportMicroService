create PROCEDURE [dbo].[GetLeadsByOffsetAndFetchParameters]
	@Offset int,
	@Fetch int
AS
SELECT  
      [ExternalId] as [Id],
      [BirthDate],
      [Role],
      [Email]
  FROM [ReportDb].[dbo].[Lead] 
  Where [Role] != 1 and [IsBanned] != 1
  ORDER by [Id]
  OFFSET @Offset ROWS  Fetch next @Fetch rows only