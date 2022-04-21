using Marvelous.Contracts.Enums;
using MarvelousReportMicroService.API.Models;
using MarvelousReportMicroService.BLL.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MarvelousReportMicroService.API.Tests.ControllersTests.TestCaseSources
{
    public class GetBirthdayLeadTestCaseSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            int day = 1;
            int month = 1;

            List<LeadModel> leadModels = new List<LeadModel>()
            {
                new LeadModel()
                {
                    Id = 1,
                    Name = "Test",
                    LastName = "TestLast",
                    BirthDate = new DateTime(2000, 01, 01),
                    Email = "Email",
                    Phone = "77777777777",
                    Password = "qwe",
                    Accounts = new List<AccountModel>(),
                    Role = Role.Regular,
                    IsBanned = false
                },
                new LeadModel()
                {
                    Id = 2,
                    Name = "Test",
                    LastName = "TestLast",
                    BirthDate = new DateTime(2000, 02, 01),
                    Email = "Email",
                    Phone = "77777777777",
                    Password = "qwe",
                    Accounts = new List<AccountModel>(),
                    Role = Role.Regular,
                    IsBanned = false
                }
            };

            List<LeadResponse> expected = new List<LeadResponse>()
            {
                new LeadResponse()
                {
                    Id = 1,
                    Name = "Test",
                    LastName = "TestLast",
                    BirthDate = new DateTime(2000, 01, 01),
                    Email = "Email",
                    Phone = "77777777777",
                    Role = Role.Regular,
                    IsBanned = false
                },
                new LeadResponse()
                {
                    Id = 2,
                    Name = "Test",
                    LastName = "TestLast",
                    BirthDate = new DateTime(2000, 02, 01),
                    Email = "Email",
                    Phone = "77777777777",
                    Role = Role.Regular,
                    IsBanned = false
                }
            };

            yield return new object[] { day, month, leadModels, expected };
        }
    }
}
