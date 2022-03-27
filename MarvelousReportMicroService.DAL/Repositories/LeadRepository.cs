using MarvelousReportMicroService.DAL.Configuration;
using MarvelousReportMicroService.DAL.Entities;
using MarvelousReportMicroService.DAL.Helpers;
using MarvelousReportMicroService.DAL.Models;
using Microsoft.Extensions.Options;
using System.Data;
using Dapper;
using SqlKata.Execution;
using SqlKata;
using MarvelousReportMicroService.DAL.Enums;

namespace MarvelousReportMicroService.DAL.Repositories
{
    public class LeadRepository : BaseRepository, ILeadRepository
    {
        private readonly QueryFactory _qeryFactory;

        public LeadRepository(IOptions<DbConfiguration> options, QueryFactory queryFactory) : base(options)
        {
            _qeryFactory = queryFactory;
        }

        public async Task<List<Lead>> GetAllLeads()
        {
            using IDbConnection connection = ProvideConnection();

            var leads = (await connection.
                QueryAsync<Lead>(
                Queries.GetAllLeads,
                commandType: CommandType.StoredProcedure)).ToList();

            return leads;
        }

        public List<Lead> GetLeadByParameters(LeadSearch lead)
        {
            var query = new Query("Lead");
            string lastQueryName = "";
            Query nestedQuery = null;

            foreach (var prop in lead.GetType().GetProperties())
            {
                if (prop.GetValue(lead) != null && prop.Name != "StartBirthDate" && prop.Name != "EndBirthDate")
                {
                    if (nestedQuery != null)
                    {
                        nestedQuery = new Query(lastQueryName).WhereLike(prop.Name.Replace("name", ""), prop.GetValue(lead).ToString()).From(nestedQuery).As(prop.Name);

                    }
                    else
                    {
                        nestedQuery = new Query("Lead").WhereLike(prop.Name.Replace("name", ""), prop.GetValue(lead).ToString()).As(prop.Name);
                    }
                    lastQueryName = prop.Name;
                }
            }
            //if (lead.StartBirthDate != null)
            //{
            //    nestedQuery = new Query(lastQueryName)
            //        .Where("BirthDay", ">", lead.StartBirthDate.Value.Day)
            //        .Where("BirthMonth", ">", lead.StartBirthDate.Value.Month)
            //        .Where("BirthYear", ">", lead.StartBirthDate.Value.Year)
            //        .From(nestedQuery).As("StartBirthDate");
            //    lastQueryName = "StartBirthDate";
            //}
            //if (lead.EndBirthDate != null)
            //{
            //    nestedQuery = new Query(lastQueryName)
            //        .Where("BirthDay", "<", lead.EndBirthDate.Value.Day)
            //        .Where("BirthMonth", "<", lead.EndBirthDate.Value.Month)
            //        .Where("BirthYear", "<", lead.EndBirthDate.Value.Year)
            //        .From(nestedQuery).As("EndBirthDate");
            //    lastQueryName = "EndBirthDate";
            //}


            IEnumerable<Lead> leads = _qeryFactory.Query(lastQueryName).From(nestedQuery).Get<Lead>();

            return (List<Lead>)leads;
        }

        public async Task<List<Lead>> GetLeadsByOffsetANdFetchParameters(LeadSerchWithOffsetAndFetch lead)
        {
            using IDbConnection connection = ProvideConnection();

            var leads = (await connection.
                QueryAsync<Lead>(
                Queries.GetLeadsByOffsetAndFetchParameters,
                new
                {
                    lead.Offset,
                    lead.Fetch
                },
                commandType: CommandType.StoredProcedure)).ToList();

            return leads;
        }

        public async Task<List<Lead>> GetLeadsByServiceId(int serviceId)
        {
            using IDbConnection connection = ProvideConnection();

            var Leads =
               (await connection
                   .QueryAsync<Lead>(
                   Queries.GetLeadsByServiceId,
                   new { serviceId },
                   commandType: CommandType.StoredProcedure)).ToList();

            return Leads;
        }


        public async Task AddLead(Lead lead)
        {
            using IDbConnection connection = ProvideConnection();

            await connection
                   .QueryAsync<Lead>(
                   Queries.AddLead
                   , new
                   {
                       Externalid = lead.Id,
                       lead.Name,
                       lead.LastName,
                       lead.BirthDay,
                       BirthMounth = lead.BirthMonth,
                       lead.BirthYear,
                       lead.Email,
                       lead.Phone,
                       lead.Password,
                       lead.Role,
                       lead.IsBanned,
                       lead.City
                   }
                   , commandType: CommandType.StoredProcedure);
        }

    }
}