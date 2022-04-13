using Marvelous.Contracts.ResponseModels;
using RestSharp;

namespace MarvelousReportMicroService.BLL.Helpers
{
    public interface IRequestHelper
    {
        Task<RestResponse<IdentityResponseModel>> SendRequestCheckValidateToken(string url, string path, string jwtToken);
        Task<RestResponse<T>> SendRequestForConfigs<T>(string url, string path, string jwtToken = "null");
    }
}