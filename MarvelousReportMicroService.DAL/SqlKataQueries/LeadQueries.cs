using MarvelousReportMicroService.DAL.Entities;
using MarvelousReportMicroService.DAL.Models;
using SqlKata;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelousReportMicroService.DAL.SqlKataQueries
{
    public static  class LeadQueries
    {
        const string sqlTableName = "Lead";
        public static IEnumerable<Lead> GetLeadsBySearchParams(LeadSearch searchParams, QueryFactory queryFactory)
        {
            string lastQueryName = sqlTableName;
            Query nestedQuery = null;
            string sign;
            foreach (var prop in searchParams.GetType().GetProperties())
            {
                if (prop.GetValue(searchParams) != null)
                {
                    if (prop.Name == nameof(LeadSearch.StartBirthDate) || prop.Name == nameof(LeadSearch.EndBirthDate))
                    {
                        sign = prop.Name == nameof(LeadSearch.StartBirthDate) ? ">" : "<";
                        nestedQuery = GetSqlKataBirthDateQuery((DateTime?)prop.GetValue(searchParams), sign, nestedQuery, lastQueryName).As(nameof(prop.Name));
                        lastQueryName = nameof(prop.Name);
                    }
                    else
                    {
                        if (nestedQuery != null)
                        {
                            nestedQuery = new Query(lastQueryName)
                                .WhereLike(prop.Name, prop.GetValue(searchParams)
                                .ToString()).From(nestedQuery)
                                .As(prop.Name);
                        }
                        else
                        {
                            nestedQuery = new Query(sqlTableName)
                                .WhereLike(prop.Name, prop.GetValue(searchParams)
                                .ToString())
                                .As(prop.Name);
                        }
                        lastQueryName = prop.Name;
                    }

                }
            }
            IEnumerable<Lead> leads;
            Query returnQuery;
            if (nestedQuery != null)
            {
                leads = queryFactory.Query(lastQueryName).From(nestedQuery).Get<Lead>();
            }
            else
            {
                leads = queryFactory.Query(lastQueryName).Get<Lead>();
            }
            return leads;
        }

        private static Query GetSqlKataBirthDateQuery(DateTime? dateParam, string sign, Query nestedQuery, string lastQueryName)
        {
            var birthDateNestedQuery = new Query(lastQueryName).Where(nameof(Lead.BirthDate), sign, dateParam);
            if (nestedQuery != null)
            {
                birthDateNestedQuery = birthDateNestedQuery.From(nestedQuery);
            }
            return birthDateNestedQuery;
        }
    }
}
