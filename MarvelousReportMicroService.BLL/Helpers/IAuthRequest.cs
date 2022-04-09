
namespace MarvelousReportMicroService.BLL.Helpers
{
    public interface IAuthRequest
    {
        Task<bool> GetRestResponse(string token);
    }
}