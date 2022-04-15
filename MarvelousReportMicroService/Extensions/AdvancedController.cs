using Marvelous.Contracts.Endpoints;
using Marvelous.Contracts.Enums;
using Microsoft.AspNetCore.Mvc;
using MarvelousReportMicroService.BLL.Exceptions;
using MarvelousReportMicroService.BLL.Helpers;

namespace MarvelousReportMicroService.API.Extensions
{
    public class AdvancedController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IRequestHelper _requestHelper;
        protected readonly ILogger _logger;
        public AdvancedController(IConfiguration configuration, IRequestHelper requestHelper, ILogger logger)
        {
            _configuration = configuration;
            _requestHelper = requestHelper;
            _logger = logger;
        }

        protected async Task CheckMicroservice(params Microservice[] service)
        {
            var token = HttpContext.Request.Headers.Authorization.FirstOrDefault();

            var identity = await _requestHelper
                .SendRequestCheckValidateToken(_configuration[Microservice.MarvelousAuth.ToString()],
                AuthEndpoints.ApiAuth + AuthEndpoints.ValidationMicroservice, token);

            if (!service.Select(r => r.ToString()).Contains(identity.IssuerMicroservice))
            {
                throw new ForbiddenException($"{identity.IssuerMicroservice} " +
                    $"doesn't have access to this endpiont");
            }
        }
    }
}
