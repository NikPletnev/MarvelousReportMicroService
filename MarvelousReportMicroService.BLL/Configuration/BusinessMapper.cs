﻿using MarvelousReportMicroService.DAL.Entities;
using MarvelousReportMicroService.BLL.Models;
using MarvelousReportMicroService.DAL.Models;
using AutoMapper;
using MarvelousReportMicroService.BLL.Helpers;

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
                CreateMap<LeadSearchModel, LeadSearch>()
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