using Marvelous.Contracts.Enums;
using MarvelousReportMicroService.API.Models;
using MarvelousReportMicroService.BLL.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MarvelousReportMicroService.API.Tests.ControllersTests.TestCaseSources
{
    public class GetTransactionsByAccountIdTestCasrSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            List<TransactionModel> transactions = new List<TransactionModel>()
            {
                new TransactionModel()
                {
                    Id = 1,
                    Date = new DateTime(2001, 07, 29),
                    Type = TransactionType.Deposit,
                    Amount = 100,
                    AccountId = 1,
                    Currency = Currency.USD,
                    Rate = 5000
                },
                new TransactionModel()
                {
                    Id = 2,
                    Date = new DateTime(2022, 10, 15),
                    Type = TransactionType.Withdraw,
                    Amount = 100,
                    AccountId = 1,
                    Currency = Currency.USD,
                    Rate = 4000
                }
            };

            List<TransactionResponse> transactionResponses = new List<TransactionResponse>()
            {
                new TransactionResponse()
                {
                    Id = 1,
                    Date = new DateTime(2001, 07, 29),
                    Type = TransactionType.Deposit,
                    Amount = 100,
                    AccountId = 1,
                    Currency = Currency.USD,
                    Rate = 5
                },
                new TransactionResponse()
                {
                    Id = 2,
                    Date = new DateTime(2022, 10, 15),
                    Type = TransactionType.Withdraw,
                    Amount = 100,
                    AccountId = 1,
                    Currency = Currency.USD,
                    Rate = 4
                }
            };

            yield return new object[] { transactions, transactionResponses };
        }
    }
}
