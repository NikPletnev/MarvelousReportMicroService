using AutoMapper;
using Marvelous.Contracts;
using MarvelousReportMicroService.API.Models;
using MarvelousReportMicroService.BLL.Models;
using MarvelousReportMicroService.BLL.Services;
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
