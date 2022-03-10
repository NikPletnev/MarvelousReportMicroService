using AutoMapper;
using MarvelousReportMicroService.API.Models;
using MarvelousReportMicroService.BLL.Models;

namespace MarvelousReportMicroService.API.Configuration
{
    public class APIMapper: Profile
    {
        public APIMapper()
        {
            CreateMap<LeadModel, LeadResponse>().ReverseMap();
            CreateMap<AccountModel, AccountResponse>().ReverseMap();
            CreateMap<TransactionModel, TransactionResponse>().ReverseMap();
        }
    }
}
