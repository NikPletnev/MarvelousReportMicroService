﻿using AutoMapper;
using MarvelousReportMicroService.BLL.Models;
using MarvelousReportMicroService.DAL.Entityes;
using MarvelousReportMicroService.DAL.Models;

namespace MarvelousReportMicroService.BLL.Configuration
{
    public class BusinessMapper : Profile
    {
            public BusinessMapper()
            {
                CreateMap<Lead, LeadModel>().ReverseMap();
                CreateMap<Account, AccountModel>().ReverseMap();
                CreateMap<Transaction, TransactionModel>().ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.ExternalId)).ReverseMap();
                CreateMap<LeadSearchModel, LeadSearch>();
                CreateMap<LeadSerchWithOffsetAndFetchModel, LeadSerchWithOffsetAndFetch>();
            }
    }
}
