using MarvelousReportMicroService.BLL.Models;

namespace MarvelousReportMicroService.BLL.Services
{
    public interface IAccountService
    {
        Task AddAccount(AccountModel model);
        Task<decimal> GetAccountBalance(int id);
    }
}