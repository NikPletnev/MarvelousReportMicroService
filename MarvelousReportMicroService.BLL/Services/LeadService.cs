using MarvelousReportMicroService.DAL.Repositories;
using MarvelousReportMicroService.DAL.Entities;
using MarvelousReportMicroService.BLL.Models;
using MarvelousReportMicroService.DAL.Models;
using Marvelous.Contracts.Enums;
using AutoMapper;
using MarvelousReportMicroService.BLL.Helpers;
using MarvelousReportMicroService.BLL.Exceptions;

namespace MarvelousReportMicroService.BLL.Services
{
    public class LeadService : ILeadService
    {
        private readonly ILeadRepository _leadRepository;
        private readonly IAuthRequest _authRequest;
        private readonly IMapper _mapper;
        public LeadService(ILeadRepository leadRepository, IAuthRequest auth, IMapper mapper)
        {
            _leadRepository = leadRepository;
            _authRequest = auth;
            _mapper = mapper;
        }

        public async Task<List<LeadModel>> GetAllLeads(string token)
        {
            if (!await _authRequest.GetRestResponse(token))
                throw new ForbiddenException("invalid token");

            var leads = await _leadRepository.GetAllLeads();
            return _mapper.Map<List<LeadModel>>(leads);
        }

        public List<LeadModel> GetLeadByParameters(LeadSearchModel model)
        {
            var leads = _leadRepository.GetLeadByParameters(_mapper.Map<LeadSearch>(model));
            return _mapper.Map<List<LeadModel>>(leads);
        }

        public async Task<List<LeadStatusUpdateModel>> GetLeadsByOffsetAndFetchParameters(
            LeadSerchWithOffsetAndFetchModel model,
            string token)
        {
            if (!await _authRequest.GetRestResponse(token))
                throw new ForbiddenException("invalid token");

            var leads = await _leadRepository.GetLeadsByOffsetANdFetchParameters(_mapper.Map<LeadSerchWithOffsetAndFetch>(model));
            return _mapper.Map<List<LeadStatusUpdateModel>>(leads);
        }

        public async Task<List<LeadModel>> GetLeadsByServiceId(int serviceId)
        {
            var leads = await _leadRepository.GetLeadsByServiceId(serviceId);
            return _mapper.Map<List<LeadModel>>(leads);
        }
        public async Task AddLead(LeadModel model)
        {
            await _leadRepository.AddLead(_mapper.Map<Lead>(model));
        }

        public async Task<List<LeadModel>> GetBirthdayLead(int day, int month)
        {
            if (day == 0)
            {
                day = DateTime.Today.Day;
            }

            if (month == 0)
            {
                month = DateTime.Today.Month;
            }

            var leads = await _leadRepository.GetBirthdayLead(day, month);
            return _mapper.Map<List<LeadModel>>(leads);

        }

        public async Task<int> GetLeadsCountByRole(Role role)
        {
            var leads = await _leadRepository.GetLeadsCountByRole((int)role);
            return leads;
        }

        public async Task UpdateLead(LeadModel model)
        {
            await _leadRepository.UpdateLead(_mapper.Map<Lead>(model));
        }

        public async Task<int?> GetLeadIdIfExist(int id)
        {
            return await _leadRepository.GetLeadIdIfExsist(id);
        }

        public async Task<List<LeadModel>> GetLeadsWithNegativeBalance()
        {
            List<Lead> leads =  await _leadRepository.GetLeadsWithNegativeBalance();
            return _mapper.Map<List<LeadModel>>(leads);
        }
    }
}