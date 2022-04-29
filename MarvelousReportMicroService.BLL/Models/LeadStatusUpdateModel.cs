using Marvelous.Contracts.Enums;

namespace MarvelousReportMicroService.BLL.Models
{
    public class LeadStatusUpdateModel
    {
        public int? Id { get; set; }
        public Role? Role { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Email { get; set; }
    }
}
