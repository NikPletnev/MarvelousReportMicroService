using CurrencyEnum;
using MarvelousContracts;

namespace MarvelousReportMicroService.API.Models
{
    public class AccountResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public Currency CurrencyType { get; set; }
        public bool IsBlocked { get; set; }
        public DateTime? LockDate { get; set; }
        public decimal? Balance { get; set; }
    }
}
