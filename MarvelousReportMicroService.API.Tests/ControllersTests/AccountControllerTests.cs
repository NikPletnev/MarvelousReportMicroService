using AutoMapper;
using MarvelousReportMicroService.API.Configuration;
using MarvelousReportMicroService.API.Controllers;
using MarvelousReportMicroService.API.Models;
using MarvelousReportMicroService.API.Tests.ConsumersTests;
using MarvelousReportMicroService.API.Tests.ControllersTests.TestCaseSources;
using MarvelousReportMicroService.BLL.Models;
using MarvelousReportMicroService.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarvelousReportMicroService.API.Tests.ControllersTests
{
    public class AccountsControllerTests : BaseTest<AccountsController>
    {
        private IMapper _mapper;
        private Mock<ITransactionService> _transactionServiceMock;
        private Mock<IAccountService> _accountServiceMock;
        private AccountsController _accountController;

        [SetUp]
        public void Setup()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<APIMapper>()));
            _transactionServiceMock = new Mock<ITransactionService>();
            _logger = new Mock<ILogger<AccountsController>>();
            _accountServiceMock = new Mock<IAccountService>();

            _accountController = new AccountsController(
                _mapper,
                _logger.Object,
                _transactionServiceMock.Object,
                _accountServiceMock.Object);
        }

        [Test]
        public async Task GetAccountBalanceByAccountId_Returns200()
        {
            //given
            int id = 42;
            decimal balance = 15;

            string firstMessage = $"Request to get account balance with id = {id}";
            string secondMessage = $"Answer to a request to get the balance of an account" +
                $" with id = {id}, balance = {balance}";

            _accountServiceMock.Setup(a => a.GetAccountBalance(id)).ReturnsAsync(balance);

            //when
            var result = await _accountController.GetAccountBalance(id);
            var actualResult = result.Result as OkObjectResult;
          
            //then
            Assert.AreEqual(balance, actualResult.Value);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            VerifyLogger(LogLevel.Information, firstMessage);
            VerifyLogger(LogLevel.Information, secondMessage);
        }

        [TestCaseSource(typeof(GetTransactionsByAccountIdTestCasrSource))]
        public async Task GetTransactionsByAccountId_Returns200(
            List<TransactionModel> transactions,
            List<TransactionResponse> expected)
        {
            //given
            int id = 42;
            string firstMessage = $"Request to get all transactions of an account with id = {id}";
            string secondMessage = $"Answer to a request to receive all transactions of an account" +
                $" with id = {id} in quantity = {transactions.Count}";

            _transactionServiceMock.Setup(a => a.GetTransactionsByAccountId(id)).ReturnsAsync(transactions);

            //when
            var result = await _accountController.GetTransactionsByAccountId(id);

            //then
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            Assert.AreEqual(expected.Count, transactions.Count);

            VerifyLogger(LogLevel.Information, firstMessage);
            VerifyLogger(LogLevel.Information, secondMessage);

            for (int i = 0; i < transactions.Count; i++)
            {
                Assert.AreEqual(transactions[i].Id, expected[i].Id);
                Assert.AreEqual(transactions[i].AccountId, expected[i].AccountId);
                Assert.AreEqual(transactions[i].Date, expected[i].Date);
                Assert.AreEqual(transactions[i].Amount, expected[i].Amount);
                Assert.AreEqual(transactions[i].Currency, expected[i].Currency);
                Assert.AreEqual(transactions[i].Rate/1000, expected[i].Rate);
            }
        }
    }
}