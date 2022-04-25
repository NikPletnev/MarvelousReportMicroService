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
    public class ServiceConsumerTests : BaseTest<ServiceConsumer>
    {
        private ServiceConsumer _consumer;
        private Mock<IServiceService> _serviceServiceMock;

        [SetUp]
        public void Setup()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<APIMapper>()));
            _logger = new Mock<ILogger<ServiceConsumer>>();
            _serviceServiceMock = new Mock<IServiceService>();
            _consumer = new ServiceConsumer(_mapper, _logger.Object, _serviceServiceMock.Object);
        }

        [TestCaseSource(typeof(TestCaseSourceServiceConsumer))]
        public async Task Test_AccountConsumer_MustWriteLogsAndCallService(ServiceExchangeModel serviceRecivedModel)
        {
            //given
            var context = Mock.Of<ConsumeContext<ServiceExchangeModel>>(_ =>
             _.Message == serviceRecivedModel);
            var messageGet = $"Getting service {context.Message.Id}";
            var messageRecive = $"Service added";
            //when
            await _consumer.Consume(context);

            //then

            _serviceServiceMock.Verify(x => x.AddService(It.IsAny<ServiceModel>()), Times.Once);
            VerifyLogger(LogLevel.Information, messageGet);
            VerifyLogger(LogLevel.Information, messageRecive);

        }

        [Test]
        public async Task NegativeTest_ServiceConsumer_MustThrowExchangeModelRecivingError()
        {
            //given
            ServiceExchangeModel ServiceRecivedModel = null;
            var context = Mock.Of<ConsumeContext<ServiceExchangeModel>>(_ =>
             _.Message == ServiceRecivedModel);
            //when

            //then
            Assert.ThrowsAsync<ExchangeModelRecivingError>(async () => await _consumer.Consume(context));
        }
    }
}
