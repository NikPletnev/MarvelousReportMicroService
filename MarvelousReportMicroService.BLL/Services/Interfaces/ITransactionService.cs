using MarvelousReportMicroService.BLL.Models;

namespace MarvelousReportMicroService.BLL.Services
{
    public interface ITransactionService
    {
        List<TransactionModel> GetTransactionsBetweenDatesByLeadId(int id, DateTime startDate, DateTime finishDate);
        List<TransactionModel> GetTransactionsByAccountId(int id);
    }
}