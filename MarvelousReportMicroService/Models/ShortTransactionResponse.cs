namespace MarvelousReportMicroService.API.Models
{
    public class ShortTransactionResponse
    {
        public decimal Amount { get; set; }
        public decimal Rate { get; set; }

        public override bool Equals(object obj)
        {
            return obj is ShortTransactionResponse response &&
                   Amount == response.Amount &&
                   Rate == response.Rate;
        }
    }
}
