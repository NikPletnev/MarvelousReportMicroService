using MarvelousReportMicroService.DAL.Entities;
using Marvelous.Contracts;

namespace MarvelousReportMicroService.DAL.Entities
{
    public class Service
    {
        public int ExternalId { get; set; }
        public string Name { get; set; }
        public int Type { get; set; } //tmp
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; }
    }
}
