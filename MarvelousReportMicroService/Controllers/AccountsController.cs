using AutoMapper;
using MarvelousReportMicroService.API.Models;
using MarvelousReportMicroService.BLL.Models;
using MarvelousReportMicroService.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace MarvelousReportMicroService.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {

        private readonly ITransactionService _transactionService;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly Logger _logger;

        public AccountsController(IMapper mapper, ITransactionService transactionService, IAccountService accountService)
        {
            _mapper = mapper;
            _accountService = accountService;
            _transactionService = transactionService;
            _logger = NLog.Web.NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
        }

        [HttpGet("{id}/balance/")]
        public ActionResult GetAllLeads(int id)
        {
            _logger.Info($"Запрос на получение баланса аккаунта с id = {id}");
            var balance = _accountService.GetAccountBalance(id);
            return Ok(balance);
        }

        [HttpGet("{id}/transactions")]
        public ActionResult GetTransactionsByAccountId(int id)
        {
            List<TransactionModel> transactions = _transactionService.GetTransactionsByAccountId(id);

            return Ok(_mapper.Map<List<TransactionResponse>>(transactions));
        }
    }
}