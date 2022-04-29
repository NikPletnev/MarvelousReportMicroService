using MarvelousReportMicroService.DAL.Configuration;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using System.Data;

namespace MarvelousReportMicroService.DAL.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        public string ConnectionString { get; set; }

        public BaseRepository(IOptions<DbConfiguration> options)
        {
            ConnectionString = options.Value.ConnectionString;
        }

        protected IDbConnection ProvideConnection() => new SqlConnection(ConnectionString);
    }
}