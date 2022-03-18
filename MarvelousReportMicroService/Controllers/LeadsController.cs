using MarvelousReportMicroService.API.Models;
using MarvelousReportMicroService.API.Models.Request;
using MarvelousReportMicroService.BLL.Models;
using MarvelousReportMicroService.BLL.Services;
using MarvelousReportMicroService.DAL.Enums;
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
            _leadService = leadService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult GetAllLeads()
        {
            _logger.LogInformation($"Запрос на получение всех лидов");
            var leads = _leadService.GetAllLeads();

            _logger.LogInformation($"Ответ на запрос о получении всех лидов");
            return Ok(_mapper.Map<List<LeadResponse>>(leads));
        }

        [HttpGet("search")]
        public ActionResult<List<LeadResponse>> GetLeadByParameters(
            [FromQuery] int? id,
            [FromQuery] string? name,
            [FromQuery] LeadSearchParams? nameParam,
            [FromQuery] string? lastName,
            [FromQuery] LeadSearchParams? lastNameParam,
            [FromQuery] DateTime? startBirthDate,
            [FromQuery] DateTime? endBirthDate,
            [FromQuery] string? email,
            [FromQuery] LeadSearchParams? emailParam,
            [FromQuery] string? phone,
            [FromQuery] LeadSearchParams? phoneParam,
            [FromQuery] Role? role,
            [FromQuery] bool? isBanned)
        {
            _logger.LogInformation($"Запрос на получение всех лидов по определенным параметрам");

            LeadSearchRequest leadModel = new LeadSearchRequest()
            {
                Id = id,
                Name = name,
                NameParam = nameParam,
                LastName = lastName,
                LastNameParam = lastNameParam,
                StartBirthDate = startBirthDate,
                EndBirthDate = endBirthDate,
                Email = email,
                EmailParam = emailParam,
                PhoneParam = phoneParam,
                Phone = phone,
                Role = role,
                IsBanned = isBanned
            };

            List<LeadModel> leads = _leadService.GetLeadByParameters(_mapper.Map<LeadSearchModel>(leadModel));

            _logger.LogInformation($"Ответ на запрос о получении всех лидов по определенным параметрам");
            return Ok(_mapper.Map<List<LeadResponse>>(leads));
        }

        [HttpGet("take-from-{offset}-to-{fetch}")]
        public ActionResult<List<LeadResponse>> GetLeadWithOffsetAndFetch(int offset, int fetch)
        {
            LeadSerchWithOffsetAndFetchRequest leadModel = new LeadSerchWithOffsetAndFetchRequest()
            {
                Offset = offset,
                Fetch = fetch
            };

            var leads = _leadService.GetLeadsByOffsetAndFetchParameters(_mapper.Map<LeadSerchWithOffsetAndFetchModel>(leadModel));
            return Ok(_mapper.Map<List<LeadResponse>>(leads));
        }
    }
}