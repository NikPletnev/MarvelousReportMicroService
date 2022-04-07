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
            CreateMap<LeadStatusUpdateResponse, LeadStatusUpdateModel>().ReverseMap();
            CreateMap<ShortTransactionResponse, ShortTransactionModel>().ReverseMap();
            CreateMap<AccountModel, AccountResponse>().ReverseMap();
            CreateMap<LeadSearchModel, LeadSearchRequest>().ReverseMap();
            CreateMap<LeadSerchWithOffsetAndFetchModel, LeadSerchWithOffsetAndFetchRequest>().ReverseMap();
            CreateMap<ServiceModel, ServiceResponse>().ReverseMap();
            CreateMap<LeadModel, LeadAuthExchangeModel>()
                .ForMember(
                dest => dest.HashPassword,
                opt => opt.MapFrom(src => src.Password));
            CreateMap<LeadFullExchangeModel, LeadModel>().ReverseMap();
            CreateMap<AccountExchangeModel, AccountModel>().ReverseMap();
            CreateMap<TransactionExchangeModel, TransactionModel>().ForMember(
                dest => dest.Rate,
                opt => opt.MapFrom(src => src.RubRate * 1000));

            CreateMap<TransactionModel, TransactionResponse>().ForMember(
                dest => dest.Rate,
                opt => opt.MapFrom(src => (decimal)src.Rate / 1000));
        }
    }
}
