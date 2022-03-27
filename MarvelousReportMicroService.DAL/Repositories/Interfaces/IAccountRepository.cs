using MarvelousReportMicroService.DAL.Entities;

namespace MarvelousReportMicroService.DAL.Repositories
{
    public interface IAccountRepository
    {
        Task AddAccount(Account account);
        Task<decimal> GetAccountBalance(int id);
    }
}