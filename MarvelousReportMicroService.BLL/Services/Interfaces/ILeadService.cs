using MarvelousReportMicroService.BLL.Models;


namespace MarvelousReportMicroService.BLL.Services
{
    public interface ILeadService
    {
        Task AddLead(LeadModel model);
        Task<List<LeadModel>> GetAllLeads();
        List<LeadModel> GetLeadByParameters(LeadSearchModel model);
        Task<List<LeadModel>> GetLeadsByOffsetAndFetchParameters(LeadSerchWithOffsetAndFetchModel model);
        Task<List<LeadModel>> GetLeadsByServiceId(int serviceId);
    }
}