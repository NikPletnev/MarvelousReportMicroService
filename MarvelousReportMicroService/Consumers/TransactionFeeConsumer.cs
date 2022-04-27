using AutoMapper;
using Marvelous.Contracts.ExchangeModels;
using MarvelousReportMicroService.BLL.Exceptions;
using MarvelousReportMicroService.BLL.Models;
using MarvelousReportMicroService.BLL.Services;
using MassTransit;

namespace MarvelousReportMicroService.API.Consumers
{
    public class TransactionFeeConsumer : IConsumer<ComissionTransactionExchangeModel>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<TransactionFeeConsumer> _logger;
        private readonly ITransactionFeeService _transactionFeeService;

        public TransactionFeeConsumer(IMapper mapper,
            ILogger<TransactionFeeConsumer> logger,
            ITransactionFeeService transactionFeeService)
        {
            _mapper = mapper;
            _logger = logger;
            _transactionFeeService = transactionFeeService;
        }

        public async Task Consume(ConsumeContext<ComissionTransactionExchangeModel> context)
        {
            if (context.Message == null)
            {
                _logger.LogError($"Transaction fee adding failed");
                throw new ExchangeModelRecivingError(nameof(context));
            }

            _logger.LogInformation($"Getting Transaction fee {context.Message.IdTransaction}");
            var model = _mapper.Map<TransactionFeeModel>(context.Message);
            await _transactionFeeService.AddTransactionFee(model);
            _logger.LogInformation($"Transaction fee {context.Message.IdTransaction} recived");

        }
    }
}
