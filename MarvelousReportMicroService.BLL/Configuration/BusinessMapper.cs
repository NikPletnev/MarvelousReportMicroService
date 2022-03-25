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
            CreateMap<Lead, LeadModel>()
                .ForMember(
                dest => dest.BirthDate,
                opt => opt.MapFrom(src =>
                    src.BirthYear.HasValue
                    ? new DateTime(src.BirthYear.Value, src.BirthMonth.Value, src.BirthDay.Value)
                    : default(DateTime?)
                ));
            CreateMap<LeadModel, Lead>()
                .ForMember(
                dest => dest.BirthDay,
                opt => opt.MapFrom(src =>
                src.BirthDate.Value.Day
                ))
                .ForMember(
                dest => dest.BirthMonth,
                opt => opt.MapFrom(src =>
                src.BirthDate.Value.Month
                ))
                .ForMember(
                dest => dest.BirthYear,
                opt => opt.MapFrom(src =>
                src.BirthDate.Value.Year
                ));

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
