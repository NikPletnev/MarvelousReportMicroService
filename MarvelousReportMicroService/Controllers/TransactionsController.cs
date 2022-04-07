using MarvelousReportMicroService.BLL.Services;
using MarvelousReportMicroService.API.Models;
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
            [FromQuery] DateTime endDate)
        {
            _logger.LogInformation($"Request to receive lead transactions for the period from {startDate} to {endDate}");

            var transactions =
                await _transactionService
                .GetTransactionsBetweenDatesByLeadId(
                leadId,
                startDate,
                endDate);

            _logger.LogInformation(
                $"Response to a request to receive lead transactions for the period from {startDate} to {endDate} " +
                $"in quantity = {transactions.Count}");
            return Ok(_mapper.Map<List<TransactionResponse>>(transactions.ToList()));
        }

        [HttpGet("lead-payment")]
        public async Task<ActionResult<List<TransactionResponse>>> GetServicePayTransactionsByLeadIdBetweenDate(
            [FromQuery] int leadId,
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            _logger.LogInformation($"Request to receive lead subscription payment transactions for the period from {startDate} to {endDate}");

            var transactions =
                await _transactionService
                .GetServicePayTransactionsByLeadIdBetweenDate(
                leadId,
                startDate,
                endDate);

            _logger.LogInformation(
                $"Response to a request to receive lead subscription payment transactions for the period from {startDate} to {endDate} " +
                $"in quantity = {transactions.Count}");
            return Ok(_mapper.Map<List<TransactionResponse>>(transactions.ToList()));
        }

        [HttpGet("count-transaction-without-withdrawal")]
        public async Task<ActionResult<int>> GetCountLeadTransactionWithoutWithdrawal(
            [FromQuery] int leadId)
        {
            _logger.LogInformation($"Request to receive count transaction without withdrawal by leadId = {leadId} for last two months");
            var count = await _transactionService.GetCountLeadTransactionWithoutWithdrawal(leadId);

            _logger.LogInformation(
                $"Request to receive count transaction without withdrawal by leadId = {leadId} for last two months in quantity = {count}");
            return Ok(count);
        }

        [HttpGet("by-leadId-last-month")]
        public async Task<ActionResult<List<TransactionResponse>>> GetLeadTransactionsForTheLastMonth([FromQuery] int leadId)
        {
            _logger.LogInformation($"Request to receive transactions for the last month by lead id = {leadId}");
            var transactions = await _transactionService.GetLeadTransactionsForTheLastMonth(leadId);

            _logger.LogInformation(
                $"Response to receive transactions for the last month by lead id = {leadId} in quantity = {transactions.Count}");
            return Ok(_mapper.Map<List<TransactionResponse>>(transactions.ToList()));
        }
    }
}