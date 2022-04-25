using MarvelousReportMicroService.DAL.Entities;

namespace MarvelousReportMicroService.DAL.Repositories
{
    public interface IServiceRepository
    {
        Task AddService(Service model);
        Task<List<Service>> GetServicesSortedByCountLeads();
    }
}