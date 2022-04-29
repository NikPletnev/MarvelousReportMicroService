using AutoMapper;
using Marvelous.Contracts.ExchangeModels;
using MarvelousReportMicroService.API.Configuration;
using MarvelousReportMicroService.API.Consumers;
using MarvelousReportMicroService.API.Tests.ConsumersTests.TestCaseSources;
using MarvelousReportMicroService.BLL.Exceptions;
using MarvelousReportMicroService.BLL.Models;
using MarvelousReportMicroService.BLL.Services;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace MarvelousReportMicroService.API.Tests.ConsumersTests
{
    internal class TransactionFeeConsumerTest : BaseTest<TransactionFeeConsumer>
    {
        private TransactionFeeConsumer _consumer;
        private Mock<ITransactionFeeService> _transactionFeeServiceMock;

        [SetUp]
        public void Setup()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<APIMapper>()));
            _logger = new Mock<ILogger<TransactionFeeConsumer>>();
            _transactionFeeServiceMock = new Mock<ITransactionFeeService>();
            _consumer = new TransactionFeeConsumer(_mapper, _logger.Object, _transactionFeeServiceMock.Object);
        }

        [TestCaseSource(typeof(AddTransactionFeeTestCaseSource))]
        public async Task Test_TransactionFeeConsumer_MustWriteLogsAndCallService(ComissionTransactionExchangeModel transactionFeeRecivedModel)
        {
            //given
            var context = Mock.Of<ConsumeContext<ComissionTransactionExchangeModel>>(_ =>
             _.Message == transactionFeeRecivedModel);
            var messageGet = $"Getting Transaction fee {context.Message.IdTransaction}";
            var messageRecive = $"Transaction fee {context.Message.IdTransaction} recived";
            //when
            await _consumer.Consume(context);

            //then

            _transactionFeeServiceMock.Verify(x => x.AddTransactionFee(It.IsAny<TransactionFeeModel>()), Times.Once);
            VerifyLogger(LogLevel.Information, messageGet);
            VerifyLogger(LogLevel.Information, messageRecive);
        }

        [Test]
        public async Task NegativeTest_AccountConsumer_MustThrowExchangeModelRecivingError()
        {
            //given
            ComissionTransactionExchangeModel transactionFeeRecivedModel = null;
            var context = Mock.Of<ConsumeContext<ComissionTransactionExchangeModel>>(_ =>
             _.Message == transactionFeeRecivedModel);
            //when

            //then
            Assert.ThrowsAsync<ExchangeModelRecivingError>(async () => await _consumer.Consume(context));
        }
    }
}
