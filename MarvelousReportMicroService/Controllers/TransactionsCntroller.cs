using AutoMapper;
using MarvelousReportMicroService.API.Models;
using MarvelousReportMicroService.BLL.Models;
using MarvelousReportMicroService.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace MarvelousReportMicroService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsCntroller : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;

        public TransactionsCntroller(IMapper mapper, ITransactionService transactionService)
        {
            _mapper = mapper;
            _transactionService = transactionService;
        }
    }
}