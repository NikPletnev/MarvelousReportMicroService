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
            string nameParam = GenerateParamString.Generate(lead.NameParam, lead.Name);
            string lastNameParam = GenerateParamString.Generate(lead.LastNameParam, lead.LastName);
            string emailParam = GenerateParamString.Generate(lead.EmailParam, lead.Email);
            string phoneParam = GenerateParamString.Generate(lead.PhoneParam, lead.Phone);

            //return connection.
            //    Query<Lead>(
            //    Queries.GetLeadsByParameters
            //    , new
            //    {
            //        lead.Id,
            //        lead.StartBirthDate,
            //        lead.EndBirthDate,
            //        lead.Role,
            //        lead.IsBanned,
            //        NameParam = nameParam,
            //        LastNameParam = lastNameParam,
            //        EmailParam = emailParam,
            //        PhoneParam = phoneParam
            //    }
            //    , commandType: CommandType.StoredProcedure)
            //    .ToList();
            var query = new Query("Lead");
            List<Query> listQuery = new List<Query>();
            

            foreach (var prop in lead.GetType().GetProperties())
            {
                if (prop.GetValue(lead) != null)
                {
                    if (listQuery.Count != 0)
                    {
                        var nestedQuery = new Query(nestedQuery).WhereLike(prop.Name.Replace("name", ""), prop.GetValue(lead)).As(prop.Name);
                        listQuery.Add(nestedQuery);
                    }
                    else
                    {
                        var nestedQuery = new Query("Lead").WhereLike(prop.Name.Replace("name", ""), prop.GetValue(lead)).As(prop.Name);
                        listQuery.Add(nestedQuery);

                    }
                }

            }



          

            query = new Query("Name").From(namelQuery);


            if (lead.Id != null)
            {
                query = query.Where("Id", lead.Id);
            }

            if (lead.StartBirthDate != null && lead.EndBirthDate != null)
            {
                query = query
                    .Where("BirthDay", ">", lead.StartBirthDate.Value.Day)
                    .Where("BirthMonth", ">", lead.StartBirthDate.Value.Month)
                    .Where("BirthYear", ">", lead.StartBirthDate.Value.Year)
                    .Where("BirthDay", "<", lead.EndBirthDate.Value.Day)
                    .Where("BirthMonth", "<", lead.EndBirthDate.Value.Month)
                    .Where("BirthYear", "<", lead.EndBirthDate.Value.Year);
            }

            if (lead.Role != null)
            {
                query = query.Where("Role", lead.Role);
            }

            if (lead.IsBanned != null)
            {
                query = query.Where("IsBanned", lead.IsBanned);
            }

            if (lead.NameParam != null)
            {
                query = query.WhereLike("Name", nameParam);
            }

            if (lead.LastNameParam != null)
            {
                query = query.WhereLike("LastName", lastNameParam);
            }

            if (lead.EmailParam != null)
            {
                query = query.WhereLike("Email", emailParam);
            }

            if (lead.PhoneParam != null)
            {
                query = query.WhereLike("Phone", phoneParam);
            }

            query = query.As("Query");



            IEnumerable<Lead> leads = _qeryFactory.Query("Query").From(query).Get<Lead>();

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