using MarvelousReportMicroService.DAL.Configuration;
using MarvelousReportMicroService.DAL.Entityes;
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

        public List<Lead> GetAllLeads()
        {
            using IDbConnection connection = ProvideConnection();

            return connection.
                Query<Lead>(
                Queries.GetAllLeads,
                commandType: CommandType.StoredProcedure)
                .ToList();
        }

        public List<Lead> GetLeadByParameters(LeadSearch lead)
        {
            lead.Name = GenerateParamString.Generate(lead.NameParam, lead.Name);
            lead.NameParam = null;
            lead.LastName = GenerateParamString.Generate(lead.LastNameParam, lead.LastName);
            lead.LastNameParam = null;
            lead.Email = GenerateParamString.Generate(lead.EmailParam, lead.Email);
            lead.EmailParam = null;
            lead.Phone = GenerateParamString.Generate(lead.PhoneParam, lead.Phone);
            lead.PhoneParam = null;

            var query = new Query("Lead");
            string lastQueryName = "";
            Query nestedQuery = null;

            foreach (var prop in lead.GetType().GetProperties())
            {
                if (prop.GetValue(lead) != null)
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

            IEnumerable<Lead> leads = _qeryFactory.Query(lastQueryName).From(nestedQuery).Get<Lead>();

            return (List<Lead>)leads;
        }


        public List<Lead> GetLeadsByOffsetANdFetchParameters(LeadSerchWithOffsetAndFetch lead)
        {
            using IDbConnection connection = ProvideConnection();

            return connection.
                Query<Lead>(
                Queries.GetLeadsByOffsetAndFetchParameters
                , new
                {
                    lead.Offset,
                    lead.Fetch
                }
                , commandType: CommandType.StoredProcedure)
                .ToList();
        }

        private Query GetSqlKataQurry(string table, string column, string param)
        {
            return new Query(table).WhereLike(column, param);
        }
    }
}