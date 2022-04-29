using MarvelousReportMicroService.DAL.Repositories;
using MarvelousReportMicroService.DAL.Entities;
using MarvelousReportMicroService.BLL.Models;
using AutoMapper;

namespace MarvelousReportMicroService.BLL.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;
        public ServiceService(IServiceRepository serviceRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }

        public async Task AddService(ServiceModel model)
        {
            _serviceRepository.AddService(_mapper.Map<Service>(model));
        }

        public async Task UpdateService(ServiceModel model)
        {
            _serviceRepository.UpdateService(_mapper.Map<Service>(model));
        }

        public async Task<int?> GetServiceIdIfExsist(int id)
        {
            return await _serviceRepository.GetServiceIdIfExsist(id);

        }

        public async Task<List<ServiceModel>> GetServicesSortedByCountLeads()
        {
            List<Service> services = await _serviceRepository
                .GetServicesSortedByCountLeads();

            return _mapper.Map<List<ServiceModel>>(services);
        }
    }
}
