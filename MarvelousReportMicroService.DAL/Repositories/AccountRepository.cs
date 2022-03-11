using MarvelousReportMicroService.DAL.Entityes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using MarvelousReportMicroService.DAL.Helpers;
using Microsoft.Extensions.Options;
using MarvelousReportMicroService.DAL.Configuration;

namespace MarvelousReportMicroService.DAL.Repositories
{
    public class AccountRepository : BaseRepository, IAccountRepository
    {
        public AccountRepository(IOptions<DbConfiguration> options) : base(options)
        {

        }

        public decimal GetAccountBalance(int id)
        {
            using IDbConnection connection = ProvideConnection();

            return connection
                .QuerySingle<decimal>
                (
                 Queries.GetAccountBalance,
                 new { Id = id },
                commandType: CommandType.StoredProcedure
                );
        }
    }
}
