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
    public class TransactionControllerTests : BaseTest<TransactionsController>
    {
        private Mock<ITransactionService> _transactionServiceMock;
        private TransactionsController _transactionController;

        [SetUp]
        public void Setup()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<APIMapper>()));
            _transactionServiceMock = new Mock<ITransactionService>();
            _logger = new Mock<ILogger<TransactionsController>>();
            _transactionController = new TransactionsController(
                _mapper,
                _logger.Object,
                _transactionServiceMock.Object);
        }

        [Test]
        public async Task GetTransactionsBetweenDatesByLeadIdTest_ShouldRetrurnTransactionsBetweenDates()
        {
            //given

            //when


            //then
        }





        //[Test]
        //public async Task GetAccountBalanceByAccountId_Returns200()
        //{
        //    //given
        //    int id = 42;
        //    decimal balance = 15;

        //    string firstMessage = $"Request to get account balance with id = {id}";
        //    string secondMessage = $"Answer to a request to get the balance of an account" +
        //        $" with id = {id}, balance = {balance}";

        //    _accountServiceMock.Setup(a => a.GetAccountBalance(id)).ReturnsAsync(balance);

        //    //when
        //    var result = await _transactionController.GetAccountBalance(id);

        //    //then
        //    Assert.AreEqual(balance, result);
        //    //Assert.IsInstanceOf<OkObjectResult>(result.Result);
        //    VerifyLogger(LogLevel.Information, firstMessage);
        //    VerifyLogger(LogLevel.Information, secondMessage);
        //}
    }
}