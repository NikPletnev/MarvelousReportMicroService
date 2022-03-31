using MarvelousReportMicroService.BLL.Services;
using MarvelousReportMicroService.BLL.Models;
using Marvelous.Contracts.ExchangeModels;
using Marvelous.Contracts.RequestModels;
using MassTransit;
using AutoMapper;

namespace MarvelousReportMicroService.API.Consumers
{
    public class TransactionsConsumer : IConsumer<TransactionExchangeModel>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<TransactionsConsumer> _logger;
        private readonly ITransactionService _transactionService;

        public TransactionsConsumer(IMapper mapper, ILogger<TransactionsConsumer> logger, ITransactionService transactionService)
        {
            _mapper = mapper;
            _logger = logger;
            _transactionService = transactionService;
        }

        public async Task Consume(ConsumeContext<TransactionExchangeModel> context) //ITransactionExchangeModel
        {
            _logger.LogInformation($"Getting transaction {context.Message.Id}");
            await _transactionService.AddTransaction(_mapper.Map<TransactionModel>(context.Message));
        }
    }
}