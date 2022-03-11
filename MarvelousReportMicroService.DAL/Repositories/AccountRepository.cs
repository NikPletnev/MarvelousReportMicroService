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
    public class AccountRepository : BaseRepository
    {
        public AccountRepository(IOptions<DbConfiguration> options) : base(options)
        {

        }

        public decimal GetAccauntBalance(int id)
        {
            using IDbConnection connection = ProvideConnection();

            return connection
                .QuerySingle<decimal>
                (
                 Queries.GetAllLeads,
                 new { Id = id},
                commandType: CommandType.StoredProcedure
                );
        }
    }
}
