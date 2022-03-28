using MarvelousReportMicroService.DAL.Entities;
using MarvelousReportMicroService.DAL.Models;

namespace MarvelousReportMicroService.DAL.Repositories
{
    public interface ILeadRepository
    {
        Task AddLead(Lead lead);
        Task<List<Lead>> GetAllLeads();
        List<Lead> GetLeadByParameters(LeadSearch lead);
        Task<List<Lead>> GetLeadsByOffsetANdFetchParameters(LeadSerchWithOffsetAndFetch lead);
        Task<List<Lead>> GetLeadsByServiceId(int serviceId);
        Task<int> GetLeadsCountByRole(int role);

    }
}