using MarvelousReportMicroService.DAL.Models;

namespace MarvelousReportMicroService.DAL.Repositories
{
    public interface ITransactionFeeRepository
    {
        Task AddTransactionFee(TransactionFee feeModel);
    }
}