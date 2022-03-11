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

        public List<Lead> GetLeadByParameters(Lead lead)
        {
            using IDbConnection connection = ProvideConnection();

            return connection.
                Query<Lead>(
                Queries.GetLeadByParameters
                , new 
                {
                    lead.Id,
                    lead.Name,
                    lead.LastName,
                    lead.BirthDate,
                    lead.Email,
                    lead.Phone,
                    lead.Role,
                    lead.IsBanned
                }
                , commandType: CommandType.StoredProcedure)
                .ToList();
        }
    }
}
