using AutoMapper;
using Marvelous.Contracts.ExchangeModels;
using MarvelousReportMicroService.API.Configuration;
using MarvelousReportMicroService.API.Consumers;
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

        [Test]
        public void LeadConsumeTest_WhenLeadIsNotExist()
        {
            //given
            LeadFullExchangeModel model = new LeadFullExchangeModel()
            {
                Id = 1,
                Role = Marvelous.Contracts.Enums.Role.Regular,
                Name = "Name",
                LastName = "Last Name",
                BirthDate = new System.DateTime(2000, 01, 01),
                Email = "email@mail.ru",
                Phone = "7777777777",
                Password = "qwe",
                City = "gorod",
                IsBanned = false
            };

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

        [Test]
        public void LeadConsumeTest_WhenLeadIsExist()
        {
            //given
            LeadFullExchangeModel model = new LeadFullExchangeModel()
            {
                Id = 1,
                Role = Marvelous.Contracts.Enums.Role.Regular,
                Name = "Name",
                LastName = "Last Name",
                BirthDate = new System.DateTime(2000, 01, 01),
                Email = "email@mail.ru",
                Phone = "7777777777",
                Password = "qwe",
                City = "gorod",
                IsBanned = false
            };

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
