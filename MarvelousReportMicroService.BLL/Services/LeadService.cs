using AutoMapper;
using MarvelousReportMicroService.BLL.Models;
using MarvelousReportMicroService.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelousReportMicroService.BLL.Services
{
    public class LeadService : ILeadService
    {
        private readonly ILeadRepository _leadRepository;
        private readonly IMapper _mapper;
        public LeadService(ILeadRepository leadRepository, IMapper mapper)
        {
            _leadRepository = leadRepository;
            _mapper = mapper;
        }


        public List<LeadModel> GetAllLeads()
        {
            var leads = _leadRepository.GetAllLeads();
            return _mapper.Map<List<LeadModel>>(leads);
        }
    }
}
