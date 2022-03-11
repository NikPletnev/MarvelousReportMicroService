using MarvelousReportMicroService.DAL.Entityes;

namespace MarvelousReportMicroService.DAL.Repositories
{
    public interface ILeadRepository
    {
        List<Lead> GetAllLeads();
        List<Lead> GetLeadByParameters(Lead lead);
    }
}