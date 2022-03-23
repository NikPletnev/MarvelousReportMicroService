CREATE PROCEDURE [dbo].[GetServicesSortedByCountLeads]
AS
	SELECT s.ExternalId,
	min(s.[Name]) [Name],
	min(s.[Description]) [Description],
	min(s.Price) Price,
	min(s.[Type]) [Type],
	count(sl.LeadId) as [members count]
from [ServiceToLead] as sl
	inner join [Service] as s
	on s.ExternalId = sl.ServiceId
	group by s.ExternalId
	order by [members count] desc