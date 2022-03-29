using Dapper;
using MarvelousReportMicroService.DAL.Configuration;
using MarvelousReportMicroService.DAL.Entities;
using MarvelousReportMicroService.DAL.Helpers;
using MarvelousReportMicroService.DAL.Models;
using Microsoft.Extensions.Options;
using SqlKata;
using SqlKata.Execution;
using System.Data;

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
            string lastQueryName = "Lead";
            Query nestedQuery = null;
            string sign;
            foreach (var prop in lead.GetType().GetProperties())
            {
                if (prop.GetValue(lead) != null)
                {
                    if (prop.Name == nameof(LeadSearch.StartBirthDate) || prop.Name == nameof(LeadSearch.EndBirthDate))
                    {
                        sign = prop.Name == nameof(LeadSearch.StartBirthDate) ? ">" : "<";
                        nestedQuery = GetSqlKataBirthDateQuery((DateTime?)prop.GetValue(lead), sign, nestedQuery, lastQueryName).As(nameof(prop.Name));
                        lastQueryName = nameof(prop.Name);
                    }
                    else
                    {
                        if (nestedQuery != null)
                        {
                            nestedQuery = new Query(lastQueryName)
                                .WhereLike(prop.Name, prop.GetValue(lead)
                                .ToString()).From(nestedQuery)
                                .As(prop.Name);
                        }
                        else
                        {
                            nestedQuery = new Query("Lead")
                                .WhereLike(prop.Name, prop.GetValue(lead)
                                .ToString())
                                .As(prop.Name);
                        }
                        lastQueryName = prop.Name;
                    }

                }
            }
            IEnumerable<Lead> leads;
            if (nestedQuery != null)
            {
                leads = _qeryFactory.Query(lastQueryName).From(nestedQuery).Get<Lead>();
            }
            else
            {
                leads = _qeryFactory.Query(lastQueryName).Get<Lead>();
            }
            

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
                   Queries.AddLead,
                   new
                   {
                       Externalid = lead.Id,
                       lead.Name,
                       lead.LastName,
                       lead.BirthDay,
                       BirthMounth = lead.BirthMonth,
                       lead.BirthDate,
                       lead.Email,
                       lead.Phone,
                       lead.Password,
                       lead.Role,
                       lead.IsBanned,
                       lead.City
                   },
                   commandType: CommandType.StoredProcedure);
        }
        public async Task<int> GetLeadsCountByRole(int role)
        {
            using IDbConnection connection = ProvideConnection();

            var count = await connection
                   .QueryFirstAsync<int>(
                   Queries.GetLeadsCountByRole,
                   new { role },
                   commandType: CommandType.StoredProcedure);
            return count;
        }

        private Query GetSqlKataBirthDateQuery(DateTime? dateParam, string sign, Query nestedQuery, string lastQueryName)
        {
            var birthDateNestedQuery = new Query(lastQueryName).Where(nameof(Lead.BirthDate), sign, dateParam);
            if (nestedQuery != null)
            {
                birthDateNestedQuery = birthDateNestedQuery.From(nestedQuery);
            }
            return birthDateNestedQuery;
        }

        public async Task<List<Lead>> GetBirthdayLead(int day, int month)
        {
            using IDbConnection connection = ProvideConnection();

            var Leads =
               (await connection
                   .QueryAsync<Lead>(
                   Queries.GetBirthdayLead,
                   new { day, month },
                   commandType: CommandType.StoredProcedure)).ToList();

            return Leads;
        }
    }
}