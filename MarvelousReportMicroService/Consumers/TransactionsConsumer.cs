using MarvelousReportMicroService.BLL.Services;
using MarvelousReportMicroService.BLL.Models;
using Marvelous.Contracts.RequestModels;
using MassTransit;
using AutoMapper;

namespace TransactionStore.API.Consumers
{
    public class TransactionsConsumer : IConsumer<TransactionRequestModel>
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

        public Task Consume(ConsumeContext<TransactionRequestModel> context)
        {
            _logger.LogInformation("Getting transaction");
            _transactionService.AddTransaction(_mapper.Map<TransactionModel>(context.Message));
            return Task.CompletedTask;
        }
    }
}