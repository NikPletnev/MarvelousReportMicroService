using AutoMapper;
using Marvelous.Contracts.ResponseModels;
using MarvelousReportMicroService.API.Configuration;
using MarvelousReportMicroService.API.Controllers;
using MarvelousReportMicroService.API.Models;
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
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarvelousReportMicroService.API.Tests.ControllersTests
{
    public class TransactionControllerTests : BaseTest<TransactionsController>
    {
        private const string jwtToken = "testToken";
        private Mock<ITransactionService> _transactionServiceMock;
        private TransactionsController _transactionController;
        private Mock<IRequestHelper> _requestHelperMock;
        private Mock<IConfiguration> _config;


        [SetUp]
        public void Setup()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<APIMapper>()));
            _transactionServiceMock = new Mock<ITransactionService>();
            _logger = new Mock<ILogger<TransactionsController>>();
            _requestHelperMock = new Mock<IRequestHelper>();
            _config = new Mock<IConfiguration>();
            _requestHelperMock = new Mock<IRequestHelper>();

            _transactionController = new TransactionsController(
                _mapper,
                _logger.Object,
                _config.Object,
                _requestHelperMock.Object,
                _transactionServiceMock.Object);
        }

        [TestCaseSource(typeof(GetTransactionsBetweenDatesByLeadIdTestCaseSource))]
        public async Task GetTransactionsBetweenDatesByLeadIdTest_ShouldRetrurnTransactions
            (int leadId,
            DateTime startDate,
            DateTime endDate,
            List<TransactionModel> transactionModels,
            List<TransactionResponse> expectedTransactionModels)
        {
            //given
            var firstMessage = $"Request to receive lead transactions for the period from {startDate} to {endDate}";
            var secondMessage = $"Response to a request to receive lead transactions for the period from {startDate} to {endDate} " +
                $"in quantity = {expectedTransactionModels.Count}";


            _transactionServiceMock.Setup(l => l.GetTransactionsBetweenDatesByLeadId(leadId, startDate, endDate)).ReturnsAsync(transactionModels);

            //when
            var result = await _transactionController.GetTransactionsBetweenDatesByLeadId(leadId, startDate, endDate);
            var actualResult = result.Result as OkObjectResult;

            //then
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedTransactionModels, actualResult.Value);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            VerifyLogger(LogLevel.Information, firstMessage);
            VerifyLogger(LogLevel.Information, secondMessage);

        }

        [TestCaseSource(typeof(GetServicePayTransactionsByLeadIdBetweenDateTestCaseSource))]
        public async Task GetServicePayTransactionsByLeadIdBetweenDateTest_ShouldReturnTransactions
            (int leadId,
            DateTime startDate,
            DateTime endDate,
            List<TransactionModel> transactionModels,
            List<TransactionResponse> expectedTransactionModels)
        {
            //given 
            var firstMessage = $"Request to receive lead subscription payment transactions for the period from " +
                $"{startDate} to {endDate}";
            var secondMessage = $"Response to a request to receive lead subscription payment transactions for the period from " +
                $"{startDate} to {endDate} " +
                $"in quantity = {transactionModels.Count}";

            _transactionServiceMock.Setup(l => l.GetServicePayTransactionsByLeadIdBetweenDate(leadId, startDate, endDate)).ReturnsAsync(transactionModels);
            //when
            var result = await _transactionController.GetServicePayTransactionsByLeadIdBetweenDate(leadId, startDate, endDate);
            var actualResult = result.Result as OkObjectResult;

            //then
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedTransactionModels, actualResult.Value);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            VerifyLogger(LogLevel.Information, firstMessage);
            VerifyLogger(LogLevel.Information, secondMessage);
        }

        [TestCaseSource(typeof(GetCountLeadTransactionWithoutWithdrawalTestCaseSource))]
        public async Task GetCountLeadTransactionWithoutWithdrawalTest_ShouldReturnCountLeadsWithoutWithdraws
            (int leadId,
            int leadCount,
            IdentityResponseModel model)
        {
            //given
            _transactionServiceMock.Setup(l => l.GetCountLeadTransactionWithoutWithdrawal(leadId)).ReturnsAsync(leadCount);
            var firstMessage = $"Request to receive count transaction without withdrawal by leadId = " +
                $"{leadId} for last two months";
            var secondMessage = $"Request to receive count transaction without withdrawal by leadId = " +
                $"{leadId} for last two months in quantity = {leadCount}";

            var context = new DefaultHttpContext();
            _transactionController.ControllerContext.HttpContext = context;

            _requestHelperMock.Setup(x => x.SendRequestCheckValidateToken(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .ReturnsAsync(model);

            //when
            var result = await _transactionController.GetCountLeadTransactionWithoutWithdrawal(leadId);
            var actualResult = result.Result as OkObjectResult;

            //then
            Assert.IsNotNull(result);
            Assert.AreEqual(leadCount, actualResult.Value);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            VerifyLogger(LogLevel.Information, firstMessage);
            VerifyLogger(LogLevel.Information, secondMessage);
        }

        [TestCaseSource(typeof(GetCountLeadTransactionWithoutWithdrawalForbidenExceptionTestCaseSource))]
        public async Task GetCountLeadTransactionWithoutWithdrawalTest_ShouldThrowForbiddenException
            (int leadId,
            int leadCount,
            IdentityResponseModel model)
        {
            //given
            var firstMessage = $"Request to receive count transaction without withdrawal by leadId = " +
                $"{leadId} for last two months";
            var context = new DefaultHttpContext();
            _transactionController.ControllerContext.HttpContext = context;

            _requestHelperMock.Setup(x => x.SendRequestCheckValidateToken(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .ReturnsAsync(model);
            //when

            //then
            Assert.ThrowsAsync<ForbiddenException>(async () => await _transactionController.GetCountLeadTransactionWithoutWithdrawal(leadId));
            VerifyLogger(LogLevel.Information, firstMessage);
        }

        [TestCaseSource(typeof(GetLeadTransactionsForTheLastMonthTestCaseSource))]
        public async Task GetLeadTransactionsForTheLastMonthTest_ShouldReturnTransactions
            (int leadId,
             int transactionsCount,
             List<ShortTransactionModel> shortTransactionModels,
             List<ShortTransactionResponse> expectedTransactions,
             IdentityResponseModel model)
        {
            //given
            var firstMessage = $"Request to receive transactions for the last month by lead id = {leadId}";
            var secondMessage = $"Response to receive transactions for the last month by lead id = {leadId} in quantity = {transactionsCount}";
            var context = new DefaultHttpContext();
            _transactionController.ControllerContext.HttpContext = context;

            _requestHelperMock.Setup(x => x.SendRequestCheckValidateToken(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .ReturnsAsync(model);

            _transactionServiceMock.Setup(l => l.GetLeadTransactionsForTheLastMonth(leadId)).ReturnsAsync(shortTransactionModels);
            //when

            var result = await _transactionController.GetLeadTransactionsForTheLastMonth(leadId);
            var actualResult = result.Result as OkObjectResult;

            //then

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedTransactions, actualResult.Value);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            VerifyLogger(LogLevel.Information, firstMessage);
            VerifyLogger(LogLevel.Information, secondMessage);
        }

        [TestCaseSource(typeof(GetLeadTransactionsForTheLastMonthForbidenExceptionTestCaseSource))]
        public async Task GetLeadTransactionsForTheLastMonthTest_ShouldThrowForbiddenException
            (int leadId,
             int transactionsCount,
             IdentityResponseModel model)
        {
            //given
            var firstMessage = $"Request to receive transactions for the last month by lead id = {leadId}";
            var context = new DefaultHttpContext();
            _transactionController.ControllerContext.HttpContext = context;

            _requestHelperMock.Setup(x => x.SendRequestCheckValidateToken(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .ReturnsAsync(model);

            //when

            //then
            Assert.ThrowsAsync<ForbiddenException>(async () => await _transactionController.GetLeadTransactionsForTheLastMonth(leadId));
            VerifyLogger(LogLevel.Information, firstMessage);

        }





    }
}
