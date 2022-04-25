using MarvelousReportMicroService.DAL.Entities;

namespace MarvelousReportMicroService.DAL.Repositories
{
    public interface IServiceRepository
    {
        Task AddService(Service model);
        Task UpdateService(Service model);
        Task<int?> GetServiceIdIfExsist(int id);
        Task<List<Service>> GetServicesSortedByCountLeads();
    }
}