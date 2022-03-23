using MarvelousReportMicroService.API.Models;
using MarvelousReportMicroService.BLL.Models;
using MarvelousReportMicroService.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace MarvelousReportMicroService.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(IMapper mapper, ILogger<AccountsController> logger, ITransactionService transactionService, IAccountService accountService)
        {
            _mapper = mapper;
            _accountService = accountService;
            _transactionService = transactionService;
            _logger = logger;
        }

        [HttpGet("{id}/balance/")]
        public async Task<ActionResult<decimal>> GetAccountBalance(int id)
        {
            _logger.LogInformation($"Request to get account balance with id = {id}");
            var balance = await _accountService.GetAccountBalance(id);

            _logger.LogInformation($"Answer to a request to get the balance of an account with id = {id}");
            return Ok(balance);
        }

        [HttpGet("{id}/transactions")]
        public async Task<ActionResult<List<TransactionResponse>>> GetTransactionsByAccountId(int id)
        {
            _logger.LogInformation($"Request to get all transactions of an account with id = {id}");
            List<TransactionModel> transactions = await _transactionService.GetTransactionsByAccountId(id);

            _logger.LogInformation($"Answer to a request to receive all transactions of an account with id = {id}");
            return Ok(_mapper.Map<List<TransactionResponse>>(transactions.ToList()));
        }
    }
}