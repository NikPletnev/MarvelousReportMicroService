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
            _logger.LogInformation($"Запрос на получение баланса аккаунта с id = {id}");
            var balance = await _accountService.GetAccountBalance(id);

            _logger.LogInformation($"Выдан ответ на запрос о получении баланса аккаунта с id = {id}");
            return Ok(balance);
        }

        [HttpGet("{id}/transactions")]
        public async Task<ActionResult<List<TransactionResponse>>> GetTransactionsByAccountId(int id)
        {
            _logger.LogInformation($"Запрос на получение всех транзакций аккаунта с id = {id}");
            List<TransactionModel> transactions = await _transactionService.GetTransactionsByAccountId(id);

            _logger.LogInformation($"Выдан ответ на запрос о получении всех транзакций аккаунта с id = {id}");
            return Ok(_mapper.Map<List<TransactionResponse>>(transactions.ToList()));
        }
    }
}