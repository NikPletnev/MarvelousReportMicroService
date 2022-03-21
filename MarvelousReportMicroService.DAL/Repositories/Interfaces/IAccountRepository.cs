namespace MarvelousReportMicroService.DAL.Repositories
{
    public interface IAccountRepository
    {
        Task<decimal> GetAccountBalance(int id);
    }
}