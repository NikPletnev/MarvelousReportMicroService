using Marvelous.Contracts.Enums;

namespace MarvelousReportMicroService.DAL.Models
{
    public class LeadStatusUpdate
    {
        public int? Id { get; set; }
        public Role? Role { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Email { get; set; }
    }
}