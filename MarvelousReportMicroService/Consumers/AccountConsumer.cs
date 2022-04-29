using MarvelousReportMicroService.BLL.Services;
using MarvelousReportMicroService.BLL.Models;
using Marvelous.Contracts.ExchangeModels;
using MassTransit;
using AutoMapper;
using MarvelousReportMicroService.BLL.Exceptions;

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
            if(context.Message == null)
            {
                _logger.LogError($"Account adding failed");
                throw new ExchangeModelRecivingError(nameof(context));
            }

            _logger.LogInformation($"Getting Account {context.Message.Id}");
            var model = _mapper.Map<AccountModel>(context.Message);
            await _accountService.AddAccount(model);
            _logger.LogInformation($"Account {context.Message.Id} recived");
            
        }
    }
}