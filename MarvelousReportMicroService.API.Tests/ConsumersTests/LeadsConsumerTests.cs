using AutoMapper;
using Marvelous.Contracts.ExchangeModels;
using MarvelousReportMicroService.API.Configuration;
using MarvelousReportMicroService.API.Consumers;
using MarvelousReportMicroService.API.Tests.ConsumersTests.TestCaseSources;
using MarvelousReportMicroService.BLL.Services;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace MarvelousReportMicroService.API.Tests.ConsumersTests
{
    public class LeadsConsumerTests : BaseTest<LeadConsumer>
    {
        private LeadConsumer _consumer;
        private Mock<ILeadService> _leadServiceMock;

        [SetUp]
        public void Setup()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<APIMapper>()));
            _logger = new Mock<ILogger<LeadConsumer>>();
            _leadServiceMock = new Mock<ILeadService>();

            _consumer = new LeadConsumer(_mapper, _logger.Object, _leadServiceMock.Object);
        }

        [TestCaseSource(typeof(LeadConsumeTestTestCaseSource))]
        public void LeadConsumeTest_WhenLeadIsNotExist(LeadFullExchangeModel model)
        {
            //given
            var context = Mock.Of<ConsumeContext<LeadFullExchangeModel>>(m =>
            m.Message == model);

            string getMessage = $"Getting lead {context.Message.Id}";
            string addLeadMessage = $"Lead added";

            int? existLeadId = null;
            _leadServiceMock.Setup(l => l.GetLeadIdIfExist(context.Message.Id)).ReturnsAsync(existLeadId);

            //when
            _consumer.Consume(context);

            //then
            _leadServiceMock.Verify(x => x.GetLeadIdIfExist(context.Message.Id), Times.Once);
            VerifyLogger(LogLevel.Information, getMessage);
            VerifyLogger(LogLevel.Information, addLeadMessage);
        }

        [TestCaseSource(typeof(LeadConsumeTestTestCaseSource))]
        public void LeadConsumeTest_WhenLeadIsExist(LeadFullExchangeModel model)
        {
            //given
            var context = Mock.Of<ConsumeContext<LeadFullExchangeModel>>(m =>
            m.Message == model);

            string getMessage = $"Getting lead {context.Message.Id}";
            string addLeadMessage = $"Lead updated";
            
            _leadServiceMock.Setup(l => l.GetLeadIdIfExist(context.Message.Id)).ReturnsAsync(It.IsAny<int>());

            //when
            _consumer.Consume(context);

            //then
            _leadServiceMock.Verify(x => x.GetLeadIdIfExist(context.Message.Id), Times.Once);
            VerifyLogger(LogLevel.Information, getMessage);
            VerifyLogger(LogLevel.Information, addLeadMessage);
        }
    }
}
