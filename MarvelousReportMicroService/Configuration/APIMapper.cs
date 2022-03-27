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
            CreateMap<LeadSearchModel, LeadSearchRequest>().ReverseMap();
            CreateMap<LeadSerchWithOffsetAndFetchModel, LeadSerchWithOffsetAndFetchRequest>().ReverseMap();
            CreateMap<ServiceModel, ServiceResponse>().ReverseMap();
            CreateMap<ILeadFullExchangeModel, LeadModel>().ReverseMap();
            CreateMap<IAccountExchangeModel, AccountModel>().ReverseMap();
            CreateMap<ITransactionExchangeModel, TransactionModel>().ForMember(
                dest => dest.Rate,
                opt => opt.MapFrom(src => src.RubRate * 1000));

            CreateMap<TransactionModel, TransactionResponse>().ForMember(
                dest => dest.Rate,
                opt => opt.MapFrom(src => (decimal)src.Rate / 1000));
        }
    }
}
