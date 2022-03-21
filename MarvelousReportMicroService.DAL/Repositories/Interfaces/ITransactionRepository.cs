using MarvelousReportMicroService.DAL.Entityes;

namespace MarvelousReportMicroService.DAL.Repositories
{
    public interface ITransactionRepository
    {
        Task<List<Transaction>> GetTransactionsBetweenDatesByLeadId(int id, DateTime startDate, DateTime finishDate);
        Task<List<Transaction>> GetTransactionsByAccountId(int accountId);
    }
}