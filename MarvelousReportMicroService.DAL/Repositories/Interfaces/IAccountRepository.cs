namespace MarvelousReportMicroService.DAL.Repositories
{
    public interface IAccountRepository
    {
        decimal GetAccountBalance(int id);
    }
}