using Marvelous.Contracts.Enums;
using MarvelousReportMicroService.BLL.Models;


namespace MarvelousReportMicroService.BLL.Services
{
    public interface ILeadService
    {
        Task AddLead(LeadModel model);
        Task<List<LeadModel>> GetAllLeads();
        List<LeadModel> GetLeadByParameters(LeadSearchModel model);
        Task<List<LeadStatusUpdateModel>> GetLeadsByOffsetAndFetchParameters(LeadSerchWithOffsetAndFetchModel model);
        Task<List<LeadModel>> GetLeadsByServiceId(int serviceId);
        Task<List<LeadModel>> GetBirthdayLead(int day, int month);
        Task<int> GetLeadsCountByRole(Role role);
        Task<int?> GetLeadIdIfExist(int id);
        Task UpdateLead(LeadModel model);
        Task<List<LeadModel>> GetLeadsWithNegativeBalance();
    }
}