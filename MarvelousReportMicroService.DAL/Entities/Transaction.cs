using Marvelous.Contracts;

namespace MarvelousReportMicroService.DAL.Entities
{
    public class Transaction
    {
        public int ExternalId { get; set; }
        public DateTime Date { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public int AccountId { get; set; }
        public Currency Currency { get; set; }
    }
}
