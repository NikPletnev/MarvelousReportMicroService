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
            using IDbConnection connection = ProvideConnection();

            string nameParam = GenerateParamString.Generate(lead.NameParam, lead.Name);
            string lastNameParam = GenerateParamString.Generate(lead.LastNameParam, lead.LastName);
            string emailParam = GenerateParamString.Generate(lead.EmailParam, lead.Email);
            string phoneParam = GenerateParamString.Generate(lead.PhoneParam, lead.Phone);

            return connection.
                Query<Lead>(
                Queries.GetLeadsByParameters
                , new
                {
                    lead.Id,
                    lead.StartBirthDate,
                    lead.EndBirthDate,
                    lead.Role,
                    lead.IsBanned,
                    NameParam = nameParam,
                    LastNameParam = lastNameParam,
                    EmailParam = emailParam,
                    PhoneParam = phoneParam
                }
                , commandType: CommandType.StoredProcedure)
                .ToList();

            //var phoneQuery = new Query().WhereLike("Phone", phoneParam);
            //var emailQuery = new Query().WhereLike("Email", emailParam).From(phoneQuery);
            //var lastNamelQuery = new Query().WhereLike("LastName", lastNameParam).From(emailQuery);
            //var namelQuery = new Query().WhereLike("Name", nameParam).From(lastNamelQuery);

            //var query = new Query().From(namelQuery);

            //IEnumerable<Lead> leads = _qeryFactory.Query("Lead").Where("Id", 100).Get<Lead>();

            //return (List<Lead>)leads;
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
    }
}