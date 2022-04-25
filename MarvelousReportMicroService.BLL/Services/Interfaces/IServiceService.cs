using MarvelousReportMicroService.BLL.Models;

namespace MarvelousReportMicroService.BLL.Services
{
    public interface IServiceService
    {
        Task AddService(ServiceModel model);
        Task UpdateService(ServiceModel model);
        Task<int?> GetServiceIdIfExsist(int id);
        Task<List<ServiceModel>> GetServicesSortedByCountLeads();
    }
}