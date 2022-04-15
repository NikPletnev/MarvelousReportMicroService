using AutoMapper;
using Marvelous.Contracts.ExchangeModels;
using Marvelous.Contracts.ResponseModels;
using MarvelousReportMicroService.API.Configuration;
using MarvelousReportMicroService.API.Controllers;
using MarvelousReportMicroService.API.Tests.ControllersTests.TestCaseSources;
using MarvelousReportMicroService.BLL.Exceptions;
using MarvelousReportMicroService.BLL.Helpers;
using MarvelousReportMicroService.BLL.Models;
using MarvelousReportMicroService.BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarvelousReportMicroService.API.Tests.ControllersTests
{
    public class LeadsControllerTests : BaseTest<LeadsController>
    {
        private Mock<ILeadService> _leadServiceMock;
        private Mock<IConfiguration> _config;
        private LeadsController _leadsController;
        private Mock<IRequestHelper> _requestHelperMock;

        [SetUp]
        public void Setup()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<APIMapper>()));
            _logger = new Mock<ILogger<LeadsController>>();
            _leadServiceMock = new Mock<ILeadService>();
            _config = new Mock<IConfiguration>();
            _requestHelperMock = new Mock<IRequestHelper>();

            _leadsController = new LeadsController(_mapper,
                _leadServiceMock.Object,
                _logger.Object,
                _config.Object,
                _requestHelperMock.Object);
        }

        [TestCaseSource(typeof(GetAllLeadsTestCaseSource))]
        public async Task GetAllLeadsTests_Should200(
            List<LeadModel> leads,
            List<LeadAuthExchangeModel> expected,
            IdentityResponseModel model)
        {
            //given;
            string token = "token";
            var context = new DefaultHttpContext();
            context.Request.Headers.Authorization = token;
            _leadsController.ControllerContext.HttpContext = context;

            _requestHelperMock.Setup(x => x.SendRequestCheckValidateToken(
                It.IsAny<string>(), 
                It.IsAny<string>(), 
                It.IsAny<string>()))
                .ReturnsAsync(model);

            _leadServiceMock.Setup(l => l.GetAllLeads()).ReturnsAsync(leads);


            //when
            var result = await _leadsController.GetAllLeads();

            //then
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Id, result.Value[i].Id);
                Assert.AreEqual(expected[i].Role, result.Value[i].Role);
                Assert.AreEqual(expected[i].Email, result.Value[i].Email);
                Assert.AreEqual(expected[i].HashPassword, result.Value[i].HashPassword);
            }
        }

        [TestCaseSource(typeof(GetAllLeads_Should403TestCaseSource))]
        public async Task GetAllLeadsTests_WhenAccessIsDenied_ShouldThrowsForbiddenException(
            List<LeadModel> leads,
            IdentityResponseModel model)
        {
            //given;
            string token = "token";
            var context = new DefaultHttpContext();
            context.Request.Headers.Authorization = token;

            _leadsController.ControllerContext.HttpContext = context;

            _requestHelperMock.Setup(x => x.SendRequestCheckValidateToken(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .ReturnsAsync(model);

            _leadServiceMock.Setup(l => l.GetAllLeads()).ReturnsAsync(leads);


            //when

            //then
            Assert.ThrowsAsync<ForbiddenException>(async () => await _leadsController.GetAllLeads());
        }
    }
}
