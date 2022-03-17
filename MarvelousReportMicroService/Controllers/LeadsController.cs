using AutoMapper;
using Marvelous.Contracts;
using MarvelousReportMicroService.API.Models;
using MarvelousReportMicroService.API.Models.Request;
using MarvelousReportMicroService.BLL.Models;
using MarvelousReportMicroService.BLL.Services;
using MarvelousReportMicroService.DAL.Enums;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace MarvelousReportMicroService.API.Controllers
{
    [ApiController]
    [Route("api/leads")]
    public class LeadsController : Controller
    {
        private readonly ILeadService _leadService;
        private readonly IMapper _mapper;
        private readonly Logger _logger;

        public LeadsController(IMapper mapper, ILeadService leadService)
        {
            _mapper = mapper;
            _leadService = leadService;
            _logger = NLog.Web.NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
        }

        [HttpGet]
        public ActionResult GetAllLeads()
        {
            _logger.Info($"Запрос на получение всех лидов");
            var leads = _leadService.GetAllLeads();
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

            return Ok(_mapper.Map<List<LeadResponse>>(leads));
        }
    }
}
