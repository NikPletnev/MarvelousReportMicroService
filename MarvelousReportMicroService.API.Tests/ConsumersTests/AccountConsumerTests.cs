using AutoMapper;
using Marvelous.Contracts.ExchangeModels;
using MarvelousReportMicroService.API.Configuration;
using MarvelousReportMicroService.API.Consumers;
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
    public class AccountConsumerTests : BaseTest
    {
        private AccountConsumer _consumer;
        private Mock<IAccountService> _accountServiceMock;

        [SetUp]
        public void Setup()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<APIMapper>()));
            _logger = new Mock<ILogger<AccountConsumer>>();
            _accountServiceMock = new Mock<IAccountService>();
            _consumer = new AccountConsumer(_mapper, _logger.Object, _accountServiceMock.Object);
        }

        [TestCaseSource(typeof(TestCaseSourceAccountConsumer))]
        public async Task Test_AccountConsumer_MustWriteLogsAndCallService(AccountExchangeModel accountRecivedModel)
        {
            //given
            var context = Mock.Of<ConsumeContext<AccountExchangeModel>>(_ =>
             _.Message == accountRecivedModel);
            var messageGet = $"Getting Account {context.Message.Id}";
            var messageRecive = $"Account {context.Message.Id} recived";
            //when

            await _consumer.Consume(context);
            //then

            _accountServiceMock.Verify(x => x.AddAccount(It.IsAny<AccountModel>()), Times.Once);
            VerifyLogger(LogLevel.Information, messageGet);
            VerifyLogger(LogLevel.Information, messageRecive);

        }

        [Test]
        public async Task NegativeTest_AccountConsumer_MustThrowExchangeModelRecivingError()
        {
            //given
            AccountExchangeModel accountRecivedModel = null;
            var context = Mock.Of<ConsumeContext<AccountExchangeModel>>(_ =>
             _.Message == accountRecivedModel);
            //when

            //then
            Assert.ThrowsAsync<ExchangeModelRecivingError>(async () => await _consumer.Consume(context));
        }

    }
}