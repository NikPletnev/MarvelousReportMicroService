using AutoMapper;
using Marvelous.Contracts.Enums;
using Marvelous.Contracts.ExchangeModels;
using Marvelous.Contracts.ResponseModels;
using MarvelousReportMicroService.API.Configuration;
using MarvelousReportMicroService.API.Controllers;
using MarvelousReportMicroService.API.Models;
using MarvelousReportMicroService.API.Tests.ControllersTests.TestCaseSources;
using MarvelousReportMicroService.BLL.Exceptions;
using MarvelousReportMicroService.BLL.Helpers;
using MarvelousReportMicroService.BLL.Models;
using MarvelousReportMicroService.BLL.Services;
using MarvelousReportMicroService.DAL.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
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
            var actualResult = result.Result as OkObjectResult;

            //then
            Assert.IsNotNull(actualResult);
            Assert.IsInstanceOf<OkObjectResult>(actualResult);
            var actuaLeads = (List<LeadAuthExchangeModel>)actualResult.Value;

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Id, actuaLeads[i].Id);
                Assert.AreEqual(expected[i].Role, actuaLeads[i].Role);
                Assert.AreEqual(expected[i].Email, actuaLeads[i].Email);
                Assert.AreEqual(expected[i].HashPassword, actuaLeads[i].HashPassword);
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

        [TestCaseSource(typeof(LeadSearchTestCaseSeource))]
        public async Task LeadSearchTest_Should200(
            string? name,
            LeadSearchParams? nameParam,
            string? lastName,
            LeadSearchParams? lastNameParam,
            DateTime? startBirthDate,
            DateTime? endBirthDate,
            string? email,
            LeadSearchParams? emailParam,
            string? phone,
            LeadSearchParams? phoneParam,
            Role? role,
            bool? isBanned,
            LeadSearchModel leadModel,
            List<LeadModel> expectedList)
        {
            //given 
            _leadServiceMock.Setup(l => l.GetLeadByParameters(_mapper.Map<LeadSearchModel>(leadModel)))
                .Returns(expectedList);
            string requestMessage = $"Request to get all leads for certain parameters";
            string responseMessage = $"Response to a request to get all leads for certain parameters";

            //when
            var result = _leadsController.GetLeadByParameters(
                            null,
                            name,
                            nameParam,
                            lastName,
                            lastNameParam,
                            startBirthDate,
                            endBirthDate,
                            email,
                            emailParam,
                            phone,
                            phoneParam,
                            role,
                            isBanned);

            var okResult = result.Result as OkObjectResult;

            //then
            Assert.IsNotNull(okResult);
            Assert.IsInstanceOf<OkObjectResult>(okResult);
            VerifyLogger(LogLevel.Information, requestMessage);
            VerifyLogger(LogLevel.Information, responseMessage);
        }

        [TestCaseSource(typeof(GetLeadWithOffsetAndFetchTestCaseSource))]
        public async Task GetLeadWithOffsetAndFetchTest_Should200(
            int fetch,
            int offset,
            List<LeadStatusUpdateModel> leadModels,
            List<LeadStatusUpdateResponse> expected,
            IdentityResponseModel model)
        {
            //given
            _leadServiceMock
                .Setup(l => l.GetLeadsByOffsetAndFetchParameters(It.IsAny<LeadSerchWithOffsetAndFetchModel>()))
                .ReturnsAsync(leadModels);

            string token = "token";
            var context = new DefaultHttpContext();
            context.Request.Headers.Authorization = token;

            _leadsController.ControllerContext.HttpContext = context;

            _requestHelperMock.Setup(x => x.SendRequestCheckValidateToken(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .ReturnsAsync(model);

            string requestString = $"Request to get for {fetch} leads starting with {offset}";
            string responseString = $"Response to a request to get for {fetch} leads starting with {offset}";

            //when
            var result = await _leadsController.GetLeadWithOffsetAndFetch(offset, fetch);
            var actualResult = result.Result as OkObjectResult;

            var actuaLeads = (List<LeadStatusUpdateResponse>)actualResult.Value;

            //then
            VerifyLogger(LogLevel.Information, requestString);
            VerifyLogger(LogLevel.Information, responseString);

            Assert.IsNotNull(actualResult);
            Assert.IsInstanceOf<OkObjectResult>(actualResult);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Id, actuaLeads[i].Id);
                Assert.AreEqual(expected[i].Role, actuaLeads[i].Role);
                Assert.AreEqual(expected[i].Email, actuaLeads[i].Email);
                Assert.AreEqual(expected[i].BirthDate, actuaLeads[i].BirthDate);
            }
        }
    }
}
