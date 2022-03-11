using AutoMapper;
using MarvelousReportMicroService.API.Models;
using MarvelousReportMicroService.BLL.Models;
using MarvelousReportMicroService.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace MarvelousReportMicroService.API.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;

        public AccountController(IMapper mapper, ITransactionService transactionService)
        {
            _mapper = mapper;
            _transactionService = transactionService;
        }

        [HttpGet("{id}/transactions")]
        public ActionResult GetTransactionsByAccountId(int id)
        {
            List<TransactionModel> transactions = _transactionService.GetTransactionsByAccountId(id);

            return Ok(_mapper.Map<List<TransactionResponse>>(transactions));
        }
    }
}
