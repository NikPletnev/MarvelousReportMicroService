using AutoMapper;
using MarvelousReportMicroService.BLL.Models;
using MarvelousReportMicroService.DAL.Models;
using MarvelousReportMicroService.DAL.Repositories;


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

        public List<LeadModel> GetLeadByParameters(LeadSearchModel model)
        {
            var leads = _leadRepository.GetLeadByParameters(_mapper.Map<LeadSearch>(model));
            return _mapper.Map<List<LeadModel>>(leads);
        }
    }
}
