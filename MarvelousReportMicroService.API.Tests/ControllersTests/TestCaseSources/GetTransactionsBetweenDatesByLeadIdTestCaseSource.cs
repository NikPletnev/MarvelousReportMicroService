using Marvelous.Contracts.Enums;
using Marvelous.Contracts.ResponseModels;
using MarvelousReportMicroService.BLL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelousReportMicroService.API.Tests.ControllersTests.TestCaseSources
{
    public class GetTransactionsBetweenDatesByLeadIdTestCaseSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            var leadId = 1;
            var startDate  = new DateTime(1,1,1990);
            var endDate = new DateTime(1,1,1991);
            var transactionModels = new List<TransactionModel>
            {
                new TransactionModel()
                {
                    Id = 1,
                    AccountId = 1,
                    Amount = 100,
                    Currency = Marvelous.Contracts.Enums.Currency.USD,
                    Date = new DateTime(2,2,1990),
                    Rate = 1000,
                    Type = Marvelous.Contracts.Enums.TransactionType.Transfer
                },
                new TransactionModel()
                {
                    Id = 2,
                    AccountId = 1,
                    Amount = 100,
                    Currency = Marvelous.Contracts.Enums.Currency.USD,
                    Date = new DateTime(2,3,1990),
                    Rate = 1000,
                    Type = Marvelous.Contracts.Enums.TransactionType.Transfer
                },
                new TransactionModel()
                {
                    Id = 3,
                    AccountId = 1,
                    Amount = 100,
                    Currency = Marvelous.Contracts.Enums.Currency.USD,
                    Date = new DateTime(2,2,1994),
                    Rate = 1000,
                    Type = Marvelous.Contracts.Enums.TransactionType.Transfer
                }
            };

            var expectedTransactionModels = new List<TransactionModel>
            {
                new TransactionModel()
                {
                    Id = 1,
                    AccountId = 1,
                    Amount = 100,
                    Currency = Marvelous.Contracts.Enums.Currency.USD,
                    Date = new DateTime(2,2,1990),
                    Rate = 1000,
                    Type = Marvelous.Contracts.Enums.TransactionType.Transfer
                },
                new TransactionModel()
                {
                    Id = 2,
                    AccountId = 1,
                    Amount = 100,
                    Currency = Marvelous.Contracts.Enums.Currency.USD,
                    Date = new DateTime(2,3,1990),
                    Rate = 1000,
                    Type = Marvelous.Contracts.Enums.TransactionType.Transfer
                }
            };

            IdentityResponseModel model = new IdentityResponseModel()
            {
                Id = 1,
                Role = "role",
                IssuerMicroservice = Microservice.MarvelousAuth.ToString()
            };


            yield return new object[] { leadId, startDate, endDate, transactionModels, model, expectedTransactionModels };



        }
    }
}


//int leadId,
//            DateTime startDate,
//            DateTime endDate,
//            List<TransactionModel> transactionModels,
//            IdentityResponseModel model