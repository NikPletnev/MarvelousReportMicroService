using MarvelousReportMicroService.BLL.Services;
using MarvelousReportMicroService.API.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace MarvelousReportMicroService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicesController : Controller
    {
        private readonly IServiceService _serviceService;
        private readonly IMapper _mapper;
        private readonly ILogger<LeadsController> _logger;

        public ServicesController(IMapper mapper, IServiceService leadService, ILogger<LeadsController> logger)
        {
            _serviceService = leadService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("desc-by-popularity")]
        public async Task<ActionResult<List<ServiceResponse>>> GetServicesSortedByCountLeads()
        {
            _logger.LogInformation($"Request to get a list of services sorted by popularity");
            var services = await _serviceService.GetServicesSortedByCountLeads();

            _logger.LogInformation($"Response to a request for a list of services sorted by popularity");
            return Ok(_mapper.Map<List<ServiceResponse>>(services));
        }
    }
}
