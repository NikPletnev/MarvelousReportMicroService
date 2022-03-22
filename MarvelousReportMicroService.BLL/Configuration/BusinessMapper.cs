using MarvelousReportMicroService.DAL.Entities;
using MarvelousReportMicroService.BLL.Models;
using MarvelousReportMicroService.DAL.Models;
using AutoMapper;

namespace MarvelousReportMicroService.BLL.Configuration
{
    public class BusinessMapper : Profile
    {
            public BusinessMapper()
            {
                CreateMap<Lead, LeadModel>().ReverseMap();
                CreateMap<Account, AccountModel>().ReverseMap();
                CreateMap<LeadSearchModel, LeadSearch>();
                CreateMap<LeadSerchWithOffsetAndFetchModel, LeadSerchWithOffsetAndFetch>(); 

                CreateMap<Transaction, TransactionModel>().ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.ExternalId)).ReverseMap();

                CreateMap<Service, ServiceModel>().ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.ExternalId)).ReverseMap();
        }
    }
}
