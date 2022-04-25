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
        public const string GetBirthdayLead = "GetBirthdayLead";
        public const string GetCountLeadTransactionWithoutWithdrawal = "GetCountLeadTransactionWithoutWithdrawal";
        public const string GetLeadsCountByRole = "GetLeadsCountByRole";
        public const string GetLeadTransactionsForTheLastMonth = "GetLeadTransactionsForTheLastMonth";
        public const string UpdateLead = "UpdateLead";
        public const string GetLeadById = "GetLeadById";
        public const string GetLeadsWithNegativeBalance = "GetLeadsWithNegativeBalance";
        public const string AddInvoicePayment = "AddInvoicePayment";
        public const string AddService = "AddService";
    }
}
