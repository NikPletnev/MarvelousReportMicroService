using MarvelousReportMicroService.BLL.Services;
using MarvelousReportMicroService.BLL.Models;
using Marvelous.Contracts.ExchangeModels;
using Marvelous.Contracts.RequestModels;
using MassTransit;
using AutoMapper;
using MarvelousReportMicroService.BLL.Exceptions;

namespace MarvelousReportMicroService.API.Consumers
{
    public class InvoicePaymentConsumer : IConsumer<InvoicePaymentExchangeModel>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<InvoicePaymentConsumer> _logger;
        private readonly IInvoicePaymentService _invoicePaymentService;

        public InvoicePaymentConsumer(IMapper mapper, ILogger<InvoicePaymentConsumer> logger, IInvoicePaymentService invoicePayment)
        {
            _mapper = mapper;
            _logger = logger;
            _invoicePaymentService = invoicePayment;
        }

        public async Task Consume(ConsumeContext<InvoicePaymentExchangeModel> context)
        {

            _logger.LogInformation($"Getting Invoice Payment Exchange Model");
            if (context.Message == null)
            {
                _logger.LogError($"Invoice Payment adding failed");
                throw new ExchangeModelRecivingError(nameof(context));
            }
            var invoicePayment = _mapper.Map<InvoicePaymentModel>(context.Message);
            await _invoicePaymentService.AddIncnvoicePayment(invoicePayment);
            _logger.LogInformation($"Invoice Payment added");
        }
    }
}