using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelousReportMicroService.DAL.Helpers
{
    public static class Queries
    {
        public const string GetAllLeads = "GetAllLeads";
        public const string GetTransactionsBetweenDatesByLeadId = "GetTransactionsBetweenDatesByLeadId";
        public const string GetTransactionsByAccountId = "GetTransactionsByAccountId";
    }
}
