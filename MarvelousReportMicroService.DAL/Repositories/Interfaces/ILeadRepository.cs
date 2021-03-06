using MarvelousReportMicroService.DAL.Entities;
using MarvelousReportMicroService.DAL.Models;

namespace MarvelousReportMicroService.DAL.Repositories
{
    public interface ILeadRepository
    {
        Task AddLead(Lead lead);
        Task<List<Lead>> GetAllLeads();
        List<Lead> GetLeadByParameters(LeadSearch lead);
        Task<List<LeadStatusUpdate>> GetLeadsByOffsetANdFetchParameters(LeadSerchWithOffsetAndFetch lead);
        Task<List<Lead>> GetLeadsByServiceId(int serviceId);
        Task<List<Lead>> GetBirthdayLead(int day, int month);
        Task<int> GetLeadsCountByRole(int role);
        Task UpdateLead(Lead lead);
        Task<int?> GetLeadIdIfExsist(int id);
        Task<List<Lead>> GetLeadsWithNegativeBalance();
    }
}