using MarvelousReportMicroService.DAL.Configuration;
using MarvelousReportMicroService.DAL.Entityes;
using MarvelousReportMicroService.DAL.Helpers;
using Microsoft.Extensions.Options;
using System.Data;
using Dapper;

namespace MarvelousReportMicroService.DAL.Repositories
{
    public class TransactionRepository : BaseRepository, ITransactionRepository
    {
        public TransactionRepository(IOptions<DbConfiguration> options) : base(options)
        {

        }

        public async Task<List<Transaction>> GetTransactionsBetweenDatesByLeadId(int LeadId, DateTime startDate, DateTime finishDate)
        {
            using IDbConnection connection = ProvideConnection();

            var transactions =
                (await connection
                    .QueryAsync<Transaction>(
                    Queries.GetTransactionsBetweenDatesByLeadId
                    , new { LeadId, startDate, finishDate }
                    , commandType: CommandType.StoredProcedure)).ToList();

            return transactions;
        }

        public async Task<List<Transaction>> GetTransactionsByAccountId(int accountId)
        {
            using IDbConnection connection = ProvideConnection();

            var transactions =
                (await connection
                    .QueryAsync<Transaction>(
                    Queries.GetTransactionsByAccountId
                    , new { accountId }
                    , commandType: CommandType.StoredProcedure)).ToList();

            return transactions;
        }

        public async Task<List<Transaction>> GetServicePayTransactionsByLeadIdBetweenDate
            (int LeadId, DateTime startDate, DateTime endDate)
        {
            using IDbConnection connection = ProvideConnection();

            var transactions =
               (await connection
                   .QueryAsync<Transaction>(
                   Queries.GetServicePayTransactionsByLeadIdBetweenDate
                   , new 
                   {
                       LeadId,
                       startDate,
                       endDate
                   }
                   , commandType: CommandType.StoredProcedure)).ToList();

            return transactions;
        }
    }
}