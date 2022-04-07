using MarvelousReportMicroService.DAL.Configuration;
using MarvelousReportMicroService.DAL.Entities;
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

        public async Task<int> GetCountLeadTransactionWithoutWithdrawal(int leadId)
        {
            using IDbConnection connection = ProvideConnection();

            return (await connection
                .QuerySingleAsync<int>(
                Queries.GetCountLeadTransactionWithoutWithdrawal,
                new
                {
                    leadId
                },
                commandType: CommandType.StoredProcedure));
        }

        public async Task AddTransaction(Transaction transaction)
        {
            using IDbConnection connection = ProvideConnection();

            await connection
                   .QueryAsync<Transaction>(
                   Queries.AddTransaction
                   , new
                   {
                       transaction.ExternalId,
                       transaction.Amount,
                       transaction.AccountId,
                       transaction.Type,
                       transaction.Currency,
                       transaction.Date,
                       transaction.Rate
                   }
                   , commandType: CommandType.StoredProcedure);
        }

        public async Task<List<Transaction>> GetLeadTransactionsForTheLastMonth(int leadId)
        {
            using IDbConnection connection = ProvideConnection();
            var startOfCurrentMonth = DateTime.Now;

            var transactions = (await connection.
                QueryAsync<Transaction>(
                Queries.GetLeadTransactionsForTheLastMonth,
                new { leadId, startOfCurrentMonth },
                commandType: CommandType.StoredProcedure)).ToList();

            return transactions;
        }
    }
}