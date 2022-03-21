using MarvelousReportMicroService.DAL.Configuration;
using MarvelousReportMicroService.DAL.Helpers;
using Microsoft.Extensions.Options;
using System.Data;
using Dapper;

namespace MarvelousReportMicroService.DAL.Repositories
{
    public class AccountRepository : BaseRepository, IAccountRepository
    {
        public AccountRepository(IOptions<DbConfiguration> options) : base(options)
        {

        }

        public async Task<decimal> GetAccountBalance(int id)
        {
            using IDbConnection connection = ProvideConnection();

            var balance = await connection
                .QuerySingleAsync<decimal>
                (
                 Queries.GetAccountBalance,
                 new { Id = id },
                commandType: CommandType.StoredProcedure
                );

            return balance;
        }
    }
}
