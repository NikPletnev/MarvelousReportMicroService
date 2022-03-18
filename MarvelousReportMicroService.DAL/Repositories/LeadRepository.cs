using MarvelousReportMicroService.DAL.Configuration;
using MarvelousReportMicroService.DAL.Entityes;
using MarvelousReportMicroService.DAL.Helpers;
using Microsoft.Extensions.Options;
using System.Data;
using Dapper;
using MarvelousReportMicroService.DAL.Models;

namespace MarvelousReportMicroService.DAL.Repositories
{
    public class LeadRepository : BaseRepository, ILeadRepository
    {

        public LeadRepository(IOptions<DbConfiguration> options) : base(options)
        {

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
                    lead.Name,
                    lead.LastName,
                    lead.StartBirthDate,
                    lead.EndBirthDate,
                    lead.Email,
                    lead.Phone,
                    lead.Role,
                    lead.IsBanned,
                    NameParam = nameParam,
                    LastNameParam = lastNameParam,
                    EmailParam = emailParam,
                    PhoneParam = phoneParam
                }
                , commandType: CommandType.StoredProcedure)
                .ToList();
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