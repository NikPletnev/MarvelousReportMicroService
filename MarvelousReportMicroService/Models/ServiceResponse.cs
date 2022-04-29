namespace MarvelousReportMicroService.API.Models
{
    public class ServiceResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; } //tmp
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; }
    }
}
