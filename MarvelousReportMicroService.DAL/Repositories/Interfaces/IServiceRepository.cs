using MarvelousReportMicroService.DAL.Entities;

namespace MarvelousReportMicroService.DAL.Repositories
{
    public interface IServiceRepository
    {
        Task<List<Service>> GetServicesSortedByCountLeads();
    }
}