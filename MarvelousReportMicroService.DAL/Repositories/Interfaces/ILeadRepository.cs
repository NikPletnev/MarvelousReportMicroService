using MarvelousReportMicroService.DAL.Entityes;
using MarvelousReportMicroService.DAL.Models;

namespace MarvelousReportMicroService.DAL.Repositories
{
    public interface ILeadRepository
    {
        List<Lead> GetAllLeads();
        List<Lead> GetLeadByParameters(LeadSearch lead);
        Task<List<Lead>> GetLeadsByOffsetANdFetchParameters(LeadSerchWithOffsetAndFetch lead);
    }
}