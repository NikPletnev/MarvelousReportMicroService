using MarvelousReportMicroService.DAL.Entityes;

namespace MarvelousReportMicroService.DAL.Repositories
{
    public interface ITransactionRepository
    {
        List<Transaction> GetTransactionsBetweenDatesByLeadId(int id, DateTime startDate, DateTime finishDate);
    }
}