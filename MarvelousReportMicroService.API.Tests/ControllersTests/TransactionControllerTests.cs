using AutoMapper;
using Marvelous.Contracts.ResponseModels;
using MarvelousReportMicroService.API.Configuration;
using MarvelousReportMicroService.API.Controllers;
using MarvelousReportMicroService.API.Models;
using MarvelousReportMicroService.API.Tests.ConsumersTests;
using MarvelousReportMicroService.API.Tests.ControllersTests.TestCaseSources;
using MarvelousReportMicroService.BLL.Helpers;
using MarvelousReportMicroService.BLL.Models;
using MarvelousReportMicroService.BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarvelousReportMicroService.API.Tests.ControllersTests
{
    public class TransactionControllerTests : BaseTest<TransactionsController>
    {
        private Mock<ITransactionService> _transactionServiceMock;
        private TransactionsController _transactionController;
        private const string jwtToken = "testToken";
        private Mock<IRequestHelper> _requestHelperMock;

        [SetUp]
        public void Setup()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<APIMapper>()));
            _transactionServiceMock = new Mock<ITransactionService>();
            _logger = new Mock<ILogger<TransactionsController>>();
            _requestHelperMock = new Mock<IRequestHelper>();
            _transactionController = new TransactionsController(
                _mapper,
                _logger.Object,
                _transactionServiceMock.Object);
        }

        [TestCaseSource(typeof(GetTransactionsBetweenDatesByLeadIdTestCaseSource))]
        public async Task GetTransactionsBetweenDatesByLeadIdTest_ShouldRetrurnTransactions
            (int leadId, 
            DateTime startDate, 
            DateTime endDate, 
            List<TransactionModel> transactionModels,
            IdentityResponseModel model,
            List<TransactionModel> expectedTransactionModels)
        {
            //given
            var context = new DefaultHttpContext();
            context.Request.Headers.Authorization = jwtToken;
            _transactionController.ControllerContext.HttpContext = context;

            _requestHelperMock.Setup(x => x.SendRequestCheckValidateToken(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .ReturnsAsync(model);

            _transactionServiceMock.Setup(l => l.GetTransactionsBetweenDatesByLeadId(leadId, startDate, endDate)).ReturnsAsync(transactionModels);

            //when
            var result = await _transactionController.GetTransactionsBetweenDatesByLeadId(leadId, startDate, endDate);


            //then
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);

        }



    }
}
