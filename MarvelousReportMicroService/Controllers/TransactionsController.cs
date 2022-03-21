using MarvelousReportMicroService.BLL.Services;
using MarvelousReportMicroService.API.Models;
using MarvelousReportMicroService.BLL.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace MarvelousReportMicroService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly ILogger<TransactionsController> _logger;
        private readonly IMapper _mapper;

        public TransactionsController(IMapper mapper, ILogger<TransactionsController> logger, ITransactionService transactionService)
        {
            _mapper = mapper;
            _logger = logger;
            _transactionService = transactionService;
        }

        [HttpGet("by-lead-id/in-range")]
        public async Task<ActionResult<List<TransactionResponse>>> GetTransactionsBetweenDatesByLeadId(
            [FromQuery] int leadId,
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime finishDate)
        {
            _logger.LogInformation($"Запрос на получение транзакций лида за период с {startDate} по {finishDate}");

            var transactions =
                await _transactionService
                .GetTransactionsBetweenDatesByLeadId(
                leadId,
                startDate,
                finishDate);

            _logger.LogInformation($"Запрос на получение транзакций лида за период с {startDate} по {finishDate} успешно выполнен");
            return Ok(_mapper.Map<List<TransactionResponse>>(transactions.ToList()));
        }
    }
}