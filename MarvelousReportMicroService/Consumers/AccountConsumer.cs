using MarvelousReportMicroService.BLL.Services;
using MarvelousReportMicroService.BLL.Models;
using Marvelous.Contracts.ExchangeModels;
using Marvelous.Contracts.RequestModels;
using MassTransit;
using AutoMapper;


namespace MarvelousReportMicroService.API.Consumers
{
    public class AccountConsumer : IConsumer<IAccountExchangeModel>
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

        public async Task Consume(ConsumeContext<IAccountExchangeModel> context)
        {
            _logger.LogInformation($"Getting lead {context.Message.Id}");
            await _accountService.AddAccount(_mapper.Map<AccountModel>(context.Message));
        }
    }
}