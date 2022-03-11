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

        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;

        public AccountController(IMapper mapper, IAccountService accountService)
        {
            _mapper = mapper;
            _accountService = accountService;
        }

        [HttpGet("{id}/balance/")]
        public ActionResult GetAllLeads(int id)
        {
            var balance = _accountService.GetAccountBalance(id);
            return Ok(balance);
        }

    }
}
