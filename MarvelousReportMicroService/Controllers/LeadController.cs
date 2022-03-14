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
    public class LeadController : Controller
    {
        private readonly ILeadService _leadService;
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;
        private readonly Logger _logger;

        public LeadController(IMapper mapper, ILeadService leadService, ITransactionService transactionService)
        {
            _mapper = mapper;
            _leadService = leadService;
            _transactionService = transactionService;
            _logger = NLog.Web.NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
        }

        [HttpGet]
        public ActionResult GetAllLeads()
        {
            _logger.Info($"Запрос на получение всех лидов");
            var leads = _leadService.GetAllLeads();
            return Ok(_mapper.Map<List<LeadResponse>>(leads));
        }

        [HttpGet("{id}/transactions-for-period/")]
        public ActionResult GetTransactionsBetweenDatesByLeadId(int id
            , [FromQuery] 
              DateTime startDate
            , DateTime finishDate)
        {
            List<TransactionModel> transactions =
                _transactionService
                .GetTransactionsBetweenDatesByLeadId(
                id
                , startDate
                , finishDate);

            return Ok(_mapper.Map<List<TransactionResponse>>(transactions));
        }

        [HttpGet("search")]
        public ActionResult<List<LeadResponse>> GetLeadByParameters(
            [FromQuery] int? id
            , string? name
            , string? lastName
            , DateTime? birthDate
            , string? email
            , string? phone
            , Role? role
            , bool? isBanned)
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
