using AutoMapper;
using Marvelous.Contracts.ExchangeModels;
using MarvelousReportMicroService.BLL.Models;
using MarvelousReportMicroService.BLL.Services;
using MassTransit;

namespace MarvelousReportMicroService.API.Consumers
{
    public class ServiceConsumer : IConsumer<ServiceExchangeModel>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ServiceConsumer> _logger;
        private readonly IServiceService _serviceService;

        public ServiceConsumer(IMapper mapper, ILogger<ServiceConsumer> logger, IServiceService serviceService)
        {
            _mapper = mapper;
            _logger = logger;
            _serviceService = serviceService;
        }

        public async Task Consume(ConsumeContext<ServiceExchangeModel> context)
        {
            _logger.LogInformation($"Getting lead {context.Message.Id}");

            var serviceModel = _mapper.Map<ServiceModel>(context.Message);
            if (await _serviceService.GetServiceIdIfExsist(serviceModel.Id) == null)
            {
                await _serviceService.AddService(serviceModel);
                _logger.LogInformation($"Lead added");
            }
            else
            {
                await _serviceService.UpdateService(serviceModel);
                _logger.LogInformation($"Lead updated");
            }

        }
    }
}
