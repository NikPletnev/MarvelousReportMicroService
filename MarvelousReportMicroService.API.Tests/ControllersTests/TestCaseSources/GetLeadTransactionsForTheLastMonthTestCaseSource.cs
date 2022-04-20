﻿using Marvelous.Contracts.Enums;
using Marvelous.Contracts.ResponseModels;
using MarvelousReportMicroService.BLL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelousReportMicroService.API.Tests.ControllersTests
{
    public class GetLeadTransactionsForTheLastMonthTestCaseSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            int leadId = 1;
            int trancactionsCount = 2;
            List<ShortTransactionModel> shortTransactionModels = new List<ShortTransactionModel>
            {
                new ShortTransactionModel
                {
                    Amount = 100,
                    Rate = 1000
                },
                new ShortTransactionModel
                {
                    Amount = 200,
                    Rate = 1000
                },
            };
            IdentityResponseModel model = new IdentityResponseModel()
            {
                Id = 1,
                Role = "role",
                IssuerMicroservice = Microservice.MarvelousAccountChecking.ToString()
            };
            yield return new object[] { leadId, trancactionsCount, shortTransactionModels,  model };

        }
    }
}
