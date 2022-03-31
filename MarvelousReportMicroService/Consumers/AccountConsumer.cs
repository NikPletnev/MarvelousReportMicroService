using MarvelousReportMicroService.BLL.Services;
using MarvelousReportMicroService.BLL.Models;
using Marvelous.Contracts.ExchangeModels;
using Marvelous.Contracts.RequestModels;
using MassTransit;
using AutoMapper;


namespace MarvelousReportMicroService.API.Consumers
{
    public class AccountConsumer : IConsumer<AccountExchangeModel>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<AccountConsumer> _logger;
        private readonly IAccountService _accountService;

        public AccountConsumer(IMapper mapper, ILogger<AccountConsumer> logger, IAccountService accountService)
        {
            _mapper = mapper;
            _logger = logger;
            _accountService = accountService;
        }

        public async Task Consume(ConsumeContext<AccountExchangeModel> context)
        {
            _logger.LogInformation($"Getting acc {context.Message.Id}");
            var model = _mapper.Map<AccountModel>(context.Message);
            foreach (var item in model.GetType().GetProperties())
            {
                _logger.LogInformation($"{item.Name}: {item.GetValue(model)}");
            }
            _logger.LogInformation($"");
            await _accountService.AddAccount(_mapper.Map<AccountModel>(context.Message));
        }
    }
}