using AutoMapper;
using MarvelousReportMicroService.API.Models;
using MarvelousReportMicroService.BLL.Models;
using MarvelousReportMicroService.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace MarvelousReportMicroService.API.Controllers
{

    [ApiController]
    [Route("api/accounts")]
    public class AccountController: Controller
    {
    
        private readonly IMapper _mapper;

    }
}
