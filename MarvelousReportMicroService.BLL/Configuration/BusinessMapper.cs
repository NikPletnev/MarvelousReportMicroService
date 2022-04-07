using MarvelousReportMicroService.DAL.Entities;
using MarvelousReportMicroService.BLL.Models;
using MarvelousReportMicroService.DAL.Models;
using MarvelousReportMicroService.BLL.Helpers;
using AutoMapper;

namespace MarvelousReportMicroService.BLL.Configuration
{
    public class BusinessMapper : Profile
    {
            public BusinessMapper()
            {
            CreateMap<Lead, LeadModel>();

            CreateMap<LeadStatusUpdateModel, LeadStatusUpdate>().ReverseMap();

            CreateMap<ShortTransaction, ShortTransactionModel>()
                .ForMember(
                dest => dest.Rate,
                opt => opt.MapFrom(src => ((decimal)src.Rate) / 1000)
                );

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
                dest => dest.BirthDate,
                opt => opt.MapFrom(src =>
                src.BirthDate
                ));

                CreateMap<Account, AccountModel>().ReverseMap();
                CreateMap<LeadSearchModel, LeadSearch>()
                .ForMember(
                    dest => dest.ExternalId,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => GenerateParamString.Generate(src.NameParam, src.Name)))
                .ForMember(
                    dest => dest.LastName,
                    opt => opt.MapFrom(src => GenerateParamString.Generate(src.LastNameParam, src.LastName)))
                .ForMember(
                    dest => dest.Email,
                    opt => opt.MapFrom(src => GenerateParamString.Generate(src.EmailParam, src.Email)))
                .ForMember(
                    dest => dest.Phone,
                    opt => opt.MapFrom(src => GenerateParamString.Generate(src.PhoneParam, src.Phone)));
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


//lead.Name = GenerateParamString.Generate(lead.NameParam, lead.Name);
//lead.NameParam = null;
//lead.LastName = GenerateParamString.Generate(lead.LastNameParam, lead.LastName);
//lead.LastNameParam = null;
//lead.Email = GenerateParamString.Generate(lead.EmailParam, lead.Email);
//lead.EmailParam = null;
//lead.Phone = GenerateParamString.Generate(lead.PhoneParam, lead.Phone);
//lead.PhoneParam = null;