using MarvelousReportMicroService.API.Models.Request;
using MarvelousReportMicroService.API.Models;
using MarvelousReportMicroService.BLL.Models;
using Marvelous.Contracts.ExchangeModels;
using AutoMapper;

namespace MarvelousReportMicroService.API.Configuration
{
    public class APIMapper: Profile
    {
        public APIMapper()
        {
            CreateMap<LeadModel, LeadResponse>().ReverseMap();
            CreateMap<AccountModel, AccountResponse>().ReverseMap();
            CreateMap<TransactionModel, TransactionResponse>().ReverseMap();
            CreateMap<LeadSearchModel, LeadSearchRequest>().ReverseMap();
            CreateMap<LeadSerchWithOffsetAndFetchModel, LeadSerchWithOffsetAndFetchRequest>().ReverseMap();
            CreateMap<ServiceModel, ServiceResponse>().ReverseMap();
            CreateMap<ITransactionExchangeModel, TransactionModel>().ReverseMap();
            CreateMap<ILeadFullExchangeModel, LeadModel>().ReverseMap();
            CreateMap<IAccountExchangeModel, AccountModel>().ReverseMap();
        }
    }
}
