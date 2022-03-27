CREATE PROCEDURE [dbo].[GetLeadsByServiceId]
	@ServiceId integer
AS
	SELECT 
	l.ExternalId Id,
	l.[Name],
	l.LastName,
	l.Email,
	l.Phone,
	l.[Role],
	l.BirthDay,
	l.BirthMonth,
	l.BirthYear,
	l.City,
	l.[Password],
	l.IsBanned from [dbo].[ServiceToLead] as s
	inner join [dbo].[Lead] as l
	on l.ExternalId = s.LeadId
