CREATE PROCEDURE [dbo].[GetLeadsWithNegativeBalance]
AS
select 
	l.[ExternalId], 
	l.[Name], 
	l.[LastName], 
	l.[BirthDay], 
	l.[BirthMonth], 
	l.[BirthDate], 
	l.[Email],
	l.[Phone],
	l.[Password],
	l.[Role],
	l.[IsBanned],
	l.[City] from [dbo].[Lead] l
where l.ExternalId in(
  select distinct a.LeadId from [dbo].[Account] a
  where a.Id in (
    select t.AccountId from [dbo].[Transaction] t
    group by t.AccountId
    having sum(t.Amount) < 0
    )
  )