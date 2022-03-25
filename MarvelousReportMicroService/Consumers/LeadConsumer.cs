using MarvelousReportMicroService.BLL.Services;
using MarvelousReportMicroService.BLL.Models;
using Marvelous.Contracts.ExchangeModels;
using Marvelous.Contracts.RequestModels;
using MassTransit;
using AutoMapper;


namespace MarvelousReportMicroService.API.Consumers
{
    public class LeadConsumer : IConsumer<ILeadFullExchangeModel>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<LeadConsumer> _logger;
        private readonly ILeadService _leadService;

        public LeadConsumer(IMapper mapper, ILogger<LeadConsumer> logger, ILeadService leadService)
        {
            _mapper = mapper;
            _logger = logger;
            _leadService = leadService;
        }

        public async Task Consume(ConsumeContext<ILeadFullExchangeModel> context) 
        {
            _logger.LogInformation($"Getting lead {context.Message.Id}");
            var model = _mapper.Map<LeadModel>(context.Message);
            foreach (var item in model.GetType().GetProperties())
            {
                _logger.LogInformation($"{item.Name}: {item.GetValue(model)}");
            }
            _logger.LogInformation($"");
            await _leadService.AddLead(_mapper.Map<LeadModel>(context.Message));
        }
    }
}