﻿using AutoMapper;
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
        private readonly IAccountService _accountService;

        public AccountController(IMapper mapper, ITransactionService transactionService, IAccountService accountService)
        {
            _mapper = mapper;
            _accountService = accountService;
            _transactionService = transactionService;
        }

        [HttpGet("{id}/balance/")]
        public ActionResult GetAllLeads(int id)
        {
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