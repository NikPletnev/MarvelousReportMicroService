using MarvelousReportMicroService.DAL.Configuration;
using MarvelousReportMicroService.DAL.Entities;
using MarvelousReportMicroService.DAL.Helpers;
using Microsoft.Extensions.Options;
using System.Data;
using Dapper;

namespace MarvelousReportMicroService.DAL.Repositories
{
    public class ServiceRepository : BaseRepository, IServiceRepository
    {
        public ServiceRepository(IOptions<DbConfiguration> options) : base(options)
        {

        }

        public async Task AddService(Service model)
        {
            using IDbConnection connection = ProvideConnection();

            await connection
                   .QueryAsync<Account>(
                   Queries.AddService
                   , new
                   {
                       Externalid = model.ExternalId,
                       model.Name,
                       model.Type,
                       model.Description,
                       model.Price,
                       model.IsDeleted
                   }
                   , commandType: CommandType.StoredProcedure);
        }

        public async Task<List<Service>> GetServicesSortedByCountLeads()
        {
            using IDbConnection connection = ProvideConnection();

            var leads = (await connection.
                QueryAsync<Service>(
                Queries.GetServicesSortedByCountLeads,
                commandType: CommandType.StoredProcedure)).ToList();

            return leads;
        }
    }
}
