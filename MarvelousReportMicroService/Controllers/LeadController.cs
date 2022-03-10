using AutoMapper;
using MarvelousReportMicroService.API.Models;
using MarvelousReportMicroService.BLL.Models;
using MarvelousReportMicroService.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace MarvelousReportMicroService.API.Controllers
{
    [ApiController]
    [Route("api/leads")]
    public class LeadController : Controller
    {
        private readonly ILeadService _leadService;
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;

        public LeadController(IMapper mapper, ILeadService leadService, ITransactionService transactionService)
        {
            _mapper = mapper;
            _leadService = leadService;
            _transactionService = transactionService; ;
        }

        [HttpGet]
        public ActionResult GetAllLeads()
        {
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
    }
}
