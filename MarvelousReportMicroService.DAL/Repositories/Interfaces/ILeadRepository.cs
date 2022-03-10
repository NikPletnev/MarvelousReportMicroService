using MarvelousReportMicroService.DAL.Entityes;

namespace MarvelousReportMicroService.DAL.Repositories
{
    public interface ILeadRepository
    {
        List<Lead> GetAllLeads();
    }
}