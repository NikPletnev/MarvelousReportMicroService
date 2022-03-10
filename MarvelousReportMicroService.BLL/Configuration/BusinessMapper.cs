using AutoMapper;
using MarvelousReportMicroService.BLL.Models;
using MarvelousReportMicroService.DAL.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelousReportMicroService.BLL.Configuration
{
    public class BusinessMapper : Profile
    {
            public BusinessMapper()
            {
                CreateMap<Lead, LeadModel>().ReverseMap();
                CreateMap<Account, AccountModel>().ReverseMap();
                CreateMap<Transaction, TransactionModel>().ReverseMap();
            }
    }
}
