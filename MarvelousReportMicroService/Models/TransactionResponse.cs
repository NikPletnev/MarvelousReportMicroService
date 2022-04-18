using Marvelous.Contracts.Enums;

namespace MarvelousReportMicroService.API.Models
{
    public class TransactionResponse
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public int AccountId { get; set; }
        public Currency Currency { get; set; }
        public decimal Rate { get; set; }

        public override bool Equals(object obj)
        {
            return obj is TransactionResponse response &&
                   Id == response.Id &&
                   Date == response.Date &&
                   Type == response.Type &&
                   Amount == response.Amount &&
                   AccountId == response.AccountId &&
                   Currency == response.Currency &&
                   Rate == response.Rate;
        }
    }
}