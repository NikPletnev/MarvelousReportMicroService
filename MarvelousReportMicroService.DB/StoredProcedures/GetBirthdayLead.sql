CREATE PROCEDURE [dbo].[GetBirthdayLead]
	@Day tinyint,
	@Month tinyint
AS
	SELECT l.ExternalId,
		   l.[Name],
		   l.LastName,
		   l.BirthDate,
		   l.Email,
		   l.Phone,
		   l.City
	from [dbo].[Lead] l
	where l.BirthDay = @Day and l.BirthMonth = @Month and l.IsBanned = 0
