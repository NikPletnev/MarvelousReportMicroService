namespace MarvelousReportMicroService.BLL.Services
{
    public interface IAccountService
    {
        Task<decimal> GetAccountBalance(int id);
    }
}