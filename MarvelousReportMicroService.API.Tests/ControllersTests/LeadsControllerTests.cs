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

            var actualLeads = (List<LeadStatusUpdateResponse>)actualResult.Value;

            //then
            Assert.IsNotNull(actualResult);
            Assert.IsInstanceOf<OkObjectResult>(actualResult);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Id, actualLeads[i].Id);
                Assert.AreEqual(expected[i].Role, actualLeads[i].Role);
                Assert.AreEqual(expected[i].Email, actualLeads[i].Email);
                Assert.AreEqual(expected[i].BirthDate, actualLeads[i].BirthDate);
            }

            VerifyLogger(LogLevel.Information, requestString);
            VerifyLogger(LogLevel.Information, responseString);
        }

        [TestCaseSource(typeof(GetLeadsByServiceIdTestCaseSource))]
        public async Task GetLeadsByServiceIdTest(int id, List<LeadModel> leadModels, List<LeadResponse> expected)
        {
            //given
            string requestString = $"Request to get all service(id = {id}) subscribers";
            string responseString = $"Response to a request to get all service(id = {id}) subscribers";

            _leadServiceMock.Setup(l => l.GetLeadsByServiceId(id)).ReturnsAsync(leadModels);

            //when
            var result = await _leadsController.GetLeadsByServiceId(id);
            var actualResult = result.Result as OkObjectResult;

            var actualLeads = (List<LeadResponse>)actualResult.Value;

            //then
            Assert.IsNotNull(actualResult);
            Assert.IsInstanceOf<OkObjectResult>(actualResult);
            Assert.AreEqual(expected.Count, actualLeads.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Id, actualLeads[i].Id);
                Assert.AreEqual(expected[i].Name, actualLeads[i].Name);
                Assert.AreEqual(expected[i].LastName, actualLeads[i].LastName);
                Assert.AreEqual(expected[i].Phone, actualLeads[i].Phone);
                Assert.AreEqual(expected[i].IsBanned, actualLeads[i].IsBanned);
                Assert.AreEqual(expected[i].Role, actualLeads[i].Role);
                Assert.AreEqual(expected[i].Email, actualLeads[i].Email);
                Assert.AreEqual(expected[i].BirthDate, actualLeads[i].BirthDate);
            }

            VerifyLogger(LogLevel.Information, requestString);
            VerifyLogger(LogLevel.Information, responseString);
        }

        [TestCaseSource(typeof(GetBirthdayLeadTestCaseSource))]
        public async Task GetBirthdayLeadTest(int day, int month, List<LeadModel> leads, List<LeadResponse> expected)
        {
            //given
            _leadServiceMock.Setup(l => l.GetBirthdayLead(day, month)).ReturnsAsync(leads);

            string requestString = $"Request to get all birthady {month}\\{day} leads";
            string responseString = $"Response to a request to get all get all birthday {month}\\{day} " +
                $"leads in quantity = {leads.Count}";

            //when
            var actual = await _leadsController.GetBirthdayLead(day, month);
            var actualResult = actual.Result as OkObjectResult;

            var actualLeads = (List<LeadResponse>)actualResult.Value;

            //then
            Assert.IsNotNull(actualResult);
            Assert.IsInstanceOf<OkObjectResult>(actualResult);
            Assert.AreEqual(expected.Count, actualLeads.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Id, actualLeads[i].Id);
                Assert.AreEqual(expected[i].Name, actualLeads[i].Name);
                Assert.AreEqual(expected[i].LastName, actualLeads[i].LastName);
                Assert.AreEqual(expected[i].Phone, actualLeads[i].Phone);
                Assert.AreEqual(expected[i].IsBanned, actualLeads[i].IsBanned);
                Assert.AreEqual(expected[i].Role, actualLeads[i].Role);
                Assert.AreEqual(expected[i].Email, actualLeads[i].Email);
                Assert.AreEqual(expected[i].BirthDate, actualLeads[i].BirthDate);
            }

            VerifyLogger(LogLevel.Information, requestString);
            VerifyLogger(LogLevel.Information, responseString);
        }
    }
}
