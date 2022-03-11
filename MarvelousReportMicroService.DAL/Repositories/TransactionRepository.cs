using MarvelousReportMicroService.DAL.Configuration;
using MarvelousReportMicroService.DAL.Entityes;
using Microsoft.Extensions.Options;
using System.Data;
using Dapper;
using MarvelousReportMicroService.DAL.Helpers;

namespace MarvelousReportMicroService.DAL.Repositories
{
    public class TransactionRepository : BaseRepository, ITransactionRepository
    {
        public TransactionRepository(IOptions<DbConfiguration> options) : base(options)
        {

        }

        public List<Transaction> GetTransactionsBetweenDatesByLeadId(int LeadId, DateTime startDate, DateTime finishDate)
        {
            using IDbConnection connection = ProvideConnection();

            List<Transaction> transactions =
                connection
                    .Query<Transaction>(
                    Queries.GetTransactionsBetweenDatesByLeadId
                    , new { LeadId, startDate, finishDate }
                    , commandType: CommandType.StoredProcedure).ToList();

            return transactions;
        }

        public List<Transaction> GetTransactionsByAccountId(int accountId)
        {
            using IDbConnection connection = ProvideConnection();

            List<Transaction> transactions =
                connection
                    .Query<Transaction>(
                    Queries.GetTransactionsByAccountId
                    , new { accountId }
                    , commandType: CommandType.StoredProcedure).ToList();

            return transactions;
        }
    }
}