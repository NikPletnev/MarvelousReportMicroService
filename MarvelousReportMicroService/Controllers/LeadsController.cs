using MarvelousReportMicroService.API.Models;
using MarvelousReportMicroService.BLL.Models;
using MarvelousReportMicroService.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Marvelous.Contracts;
using AutoMapper;

namespace MarvelousReportMicroService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeadsController : Controller
    {
        private readonly ILeadService _leadService;
        private readonly IMapper _mapper;
        private readonly ILogger<LeadsController> _logger;

        public LeadsController(IMapper mapper, ILeadService leadService, ILogger<LeadsController> logger)
        {
            _mapper = mapper;
            _leadService = leadService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult GetAllLeads()
        {
            _logger.LogInformation($"Запрос на получение всех лидов");
            var leads = _leadService.GetAllLeads();
            return Ok(_mapper.Map<List<LeadResponse>>(leads));
        }

        [HttpGet("search")]
        public ActionResult<List<LeadResponse>> GetLeadByParameters(
            [FromQuery] int? id,
            [FromQuery] string? name,
            [FromQuery] string? lastName,
            [FromQuery] DateTime? birthDate,
            [FromQuery] string? email,
            [FromQuery] string? phone,
            [FromQuery] Role? role,
            [FromQuery] bool? isBanned)
        {
            LeadModelSearchRequest leadModel = new LeadModelSearchRequest()
            {
                Id = id,
                Name = name,
                LastName = lastName,
                BirthDate = birthDate,
                Email = email,
                Phone = phone,
                Role = role,
                IsBanned = isBanned
            };

            List<LeadModel> leads = _leadService.GetLeadByParameters(leadModel);

            return Ok(_mapper.Map<List<LeadResponse>>(leads));
        }
    }
}
