using MarvelousReportMicroService.BLL.Models;

namespace MarvelousReportMicroService.BLL.Services
{
    public interface IServiceService
    {
        Task<List<ServiceModel>> GetServicesSortedByCountLeads();
    }
}