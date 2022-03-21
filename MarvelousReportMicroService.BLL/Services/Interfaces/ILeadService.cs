using MarvelousReportMicroService.BLL.Models;


namespace MarvelousReportMicroService.BLL.Services
{
    public interface ILeadService
    {
        Task<List<LeadModel>> GetAllLeads();
        List<LeadModel> GetLeadByParameters(LeadSearchModel model);
        Task<List<LeadModel>> GetLeadsByOffsetAndFetchParameters(LeadSerchWithOffsetAndFetchModel model);
    }
}