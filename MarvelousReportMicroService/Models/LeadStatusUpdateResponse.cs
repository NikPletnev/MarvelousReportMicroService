using Marvelous.Contracts.Enums;

namespace MarvelousReportMicroService.API.Models
{
    public class LeadStatusUpdateResponse
    {
        public int? Id { get; set; }
        public Role? Role { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Email { get; set; }
    }
}
