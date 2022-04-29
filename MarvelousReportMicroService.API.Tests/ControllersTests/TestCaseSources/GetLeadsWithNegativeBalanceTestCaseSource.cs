using MarvelousReportMicroService.API.Models;
using MarvelousReportMicroService.BLL.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MarvelousReportMicroService.API.Tests.ControllersTests.TestCaseSources
{
    public class GetLeadsWithNegativeBalanceTestCaseSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            List<LeadModel> leads = new List<LeadModel>()
            {
                new LeadModel()
                {
                    Id = 1,
                    Name = "Rom",
                    LastName = "name",
                    BirthDate = new DateTime(2001, 07, 29),
                    Email = "email@mail.com",
                    Phone = "77777777777",
                    Password = "password",
                    Accounts = new List<AccountModel>(),
                    Role = Marvelous.Contracts.Enums.Role.Regular,
                    IsBanned = false
                }
            };

            List<LeadResponse> expected = new List<LeadResponse>()
            {
                new LeadResponse()
                {
                    Id = 1,
                    Name = "Rom",
                    LastName = "name",
                    BirthDate = new DateTime(2001, 07, 29),
                    Email = "email@mail.com",
                    Phone = "77777777777",
                    Role = Marvelous.Contracts.Enums.Role.Regular,
                    IsBanned = false
                }
            };

            yield return new object[] { leads, expected };
        }
    }
}
