using AutoMapper;
using MarvelousReportMicroService.API.Controllers;
using MarvelousReportMicroService.API.Tests.ConsumersTests;
using MarvelousReportMicroService.BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace MarvelousReportMicroService.API.Tests.ControllersTests
{
    public class AccountsControllerTests : BaseTest<AccountsController>
    {
        private Mock<IMapper> _mapperMock;
        private Mock<ITransactionService> _transactionServiceMock;
        private Mock<IAccountService> _accountServiceMock;
        private AccountsController _accountController;

        [SetUp]
        public void Setup()
        {
            _transactionServiceMock = new Mock<ITransactionService>();
            _logger = new Mock<ILogger<AccountsController>>();
            _mapperMock = new Mock<IMapper>();
            _accountServiceMock = new Mock<IAccountService>();

            _accountController = new AccountsController(
                _mapperMock.Object,
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
            string firstMessage = $"Request to get all transactions of an account with id = {id}";
            string secondMessage = $"Answer to a request to get the balance of an account with id = {id}, balance = {balance}";

            _accountServiceMock.Setup(b => b.GetAccountBalance(id)).ReturnsAsync(balance);

            //when
            var result = await _accountController.GetAccountBalance(id);

            //then
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            //VerifyLogger(LogLevel.Information, firstMessage);
            //VerifyLogger(LogLevel.Information, secondMessage);
        }
    }
}