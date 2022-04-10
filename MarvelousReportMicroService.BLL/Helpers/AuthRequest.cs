using Marvelous.Contracts.Autentificator;
using Marvelous.Contracts.Endpoints;
using Marvelous.Contracts.Enums;
using MarvelousReportMicroService.BLL.Exceptions;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace MarvelousReportMicroService.BLL.Helpers
{
    public class AuthRequest : IAuthRequest
    {
        private readonly string _url = "https://piter-education.ru:6042";
        private readonly ILogger<AuthRequest> _logger;

        public AuthRequest(ILogger<AuthRequest> logger)
        {
            _logger = logger;
        }

        public async Task<bool> GetRestResponse(string token)
        {
            _logger.LogInformation($"Start sending a request to validate a token");
            bool response = false;

            try
            {
                response = await SendRequestWithToken(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while validating token");
            }

            return response;
        }

        private async Task<bool> SendRequestWithToken(string token)
        {
            var client = new RestClient(_url);

            client.Authenticator = new MarvelousAuthenticator(token);
            client.AddDefaultHeader(nameof(Microservice), value: Microservice.MarvelousReporting.ToString());

            var request = new RestRequest($"{AuthEndpoints.ApiAuth}{AuthEndpoints.ValidationMicroservice}", Method.Get);

            _logger.LogInformation($"Getting a response from {Microservice.MarvelousAuth}");

            var response = await client.ExecuteAsync(request);

            return CheckTransactionError(response);
        }


        private bool CheckTransactionError(RestResponse response)
        {
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new ForbiddenException(response.ErrorException.Message);
            }
            return true;
        }
    }
}
