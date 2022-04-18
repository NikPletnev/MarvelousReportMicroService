using Marvelous.Contracts.Enums;
using Marvelous.Contracts.ResponseModels;
using MarvelousReportMicroService.API.Models;
using MarvelousReportMicroService.BLL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelousReportMicroService.API.Tests.ControllersTests
{
    public class GetServicePayTransactionsByLeadIdBetweenDateTestCaseSource: IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            var leadId = 1;
            var startDate = new DateTime(1990, 1, 1);
            var endDate = new DateTime(1991, 1, 1);
            var transactionModels = new List<TransactionModel>
            {
                new TransactionModel()
                {
                    Id = 1,
                    AccountId = 1,
                    Amount = 100,
                    Currency = Marvelous.Contracts.Enums.Currency.USD,
                    Date = new DateTime(1990,2,2),
                    Rate = 1000,
                    Type = Marvelous.Contracts.Enums.TransactionType.Transfer
                },
                new TransactionModel()
                {
                    Id = 2,
                    AccountId = 1,
                    Amount = 100,
                    Currency = Marvelous.Contracts.Enums.Currency.USD,
                    Date = new DateTime(1900,3,3),
                    Rate = 1000,
                    Type = Marvelous.Contracts.Enums.TransactionType.Transfer
                },
                new TransactionModel()
                {
                    Id = 3,
                    AccountId = 1,
                    Amount = 100,
                    Currency = Marvelous.Contracts.Enums.Currency.USD,
                    Date = new DateTime(1990,2,2),
                    Rate = 1000,
                    Type = Marvelous.Contracts.Enums.TransactionType.Transfer
                }
            };

            var expectedTransactionModels = new List<TransactionResponse>
            {
                new TransactionResponse()
                {
                    Id = 1,
                    AccountId = 1,
                    Amount = 100,
                    Currency = Marvelous.Contracts.Enums.Currency.USD,
                    Date = new DateTime(1990,2,2),
                    Rate = 1,
                    Type = Marvelous.Contracts.Enums.TransactionType.Transfer
                },
                new TransactionResponse()
                {
                    Id = 2,
                    AccountId = 1,
                    Amount = 100,
                    Currency = Marvelous.Contracts.Enums.Currency.USD,
                    Date = new DateTime(1900,3,3),
                    Rate = 1,
                    Type = Marvelous.Contracts.Enums.TransactionType.Transfer
                },
                 new TransactionResponse()
                {
                    Id = 3,
                    AccountId = 1,
                    Amount = 100,
                    Currency = Marvelous.Contracts.Enums.Currency.USD,
                    Date = new DateTime(1990,2,2),
                    Rate = 1,
                    Type = Marvelous.Contracts.Enums.TransactionType.Transfer
                }
            };
            yield return new object[] { leadId, startDate, endDate, transactionModels , expectedTransactionModels };
        }
    }
}

