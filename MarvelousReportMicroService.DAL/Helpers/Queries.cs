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
        public const string GetAccountBalance = "GetAccountBalance";
        public const string GetTransactionsBetweenDatesByLeadId = "GetTransactionsBetweenDatesByLeadId";
        public const string GetLeadsByParameters = "GetLeadsByParameters";
        public const string GetTransactionsByAccountId = "GetTransactionsByAccountId";
        public const string GetLeadsByOffsetAndFetchParameters = "GetLeadsByOffsetAndFetchParameters";
        public const string GetServicePayTransactionsByLeadIdBetweenDate = "GetServicePayTransactionsByLeadIdBetweenDate";
        public const string GetLeadsByServiceId = "GetLeadsByServiceId";
        public const string GetServicesSortedByCountLeads = "GetServicesSortedByCountLeads";
        public const string AddTransaction = "AddTransaction";
        public const string AddLead = "AddLead";
        public const string AddAccount = "AddAccount";
    }
}
