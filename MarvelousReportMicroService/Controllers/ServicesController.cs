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
            _logger.LogInformation($"Запрос на получение списка услуг, отсортированных по популярности");
            var services = await _serviceService.GetServicesSortedByCountLeads();

            _logger.LogInformation($"Ответ на запрос о получении списка услуг, отсортированных по популярности");
            return Ok(_mapper.Map<List<ServiceResponse>>(services));
        }
    }
}
