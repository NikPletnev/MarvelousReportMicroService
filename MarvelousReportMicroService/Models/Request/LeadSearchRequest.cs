using Marvelous.Contracts;
using MarvelousReportMicroService.DAL.Enums;

namespace MarvelousReportMicroService.API.Models.Request
{
    public class LeadSearchRequest
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public LeadSearchParams? NameParam { get; set; }
        public string? LastName { get; set; }
        public LeadSearchParams? LastNameParam { get; set; }
        public DateTime? StartBirthDate { get; set; }
        public DateTime? EndBirthDate { get; set; }
        public string? Email { get; set; }
        public LeadSearchParams? EmailParam { get; set; }
        public string? Phone { get; set; }
        public LeadSearchParams? PhoneParam { get; set; }
        public Role? Role { get; set; }
        public bool? IsBanned { get; set; }
    }
}
