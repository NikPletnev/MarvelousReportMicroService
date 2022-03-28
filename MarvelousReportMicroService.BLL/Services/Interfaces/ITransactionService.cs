using MarvelousReportMicroService.BLL.Models;

namespace MarvelousReportMicroService.BLL.Services
{
    public interface ITransactionService
    {
        Task<List<TransactionModel>> GetServicePayTransactionsByLeadIdBetweenDate(int id, DateTime startDate, DateTime endDate);
        Task<List<TransactionModel>> GetTransactionsBetweenDatesByLeadId(int id, DateTime startDate, DateTime finishDate);
        Task<int> GetCountLeadTransactionWithoutWithdrawal(int leadId, DateTime startDate);
        Task<List<TransactionModel>> GetTransactionsByAccountId(int id);
        Task AddTransaction(TransactionModel model);
        Task<List<TransactionModel>> GetLeadTransactionsForTheLastMonth(int leadId);
    }
}