using MarvelousReportMicroService.DAL.Entities;

namespace MarvelousReportMicroService.DAL.Repositories
{
    public interface ITransactionRepository
    {
        Task<List<Transaction>> GetServicePayTransactionsByLeadIdBetweenDate(int LeadId, DateTime startDate, DateTime endDate);
        Task<List<Transaction>> GetTransactionsBetweenDatesByLeadId(int id, DateTime startDate, DateTime finishDate);
        Task<List<Transaction>> GetTransactionsByAccountId(int accountId);

    }
}