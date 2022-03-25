CREATE PROCEDURE [dbo].[GetServicesSortedByCountLeads]
AS
select s.Id, s.Name, s.Description, s.Price, s.Type, s.IsDeleted , Counts.Subscrubers
from 
(select ServiceId, COUNT(*) As Subscrubers
from [ServiceToLead]
group by ServiceId)
as Counts 
inner join [Service] as s on Counts.ServiceId = s.Id


