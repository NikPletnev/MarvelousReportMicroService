using MarvelousReportMicroService.BLL.Models;

namespace MarvelousReportMicroService.BLL.Services
{
    public interface IServiceService
    {
        Task AddService(ServiceModel model);
        Task<List<ServiceModel>> GetServicesSortedByCountLeads();
    }
}