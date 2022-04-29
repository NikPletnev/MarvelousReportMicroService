using Marvelous.Contracts.Enums;
using Marvelous.Contracts.ExchangeModels;
using Marvelous.Contracts.ResponseModels;
using MarvelousReportMicroService.BLL.Models;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelousReportMicroService.API.Tests.ControllersTests.TestCaseSources
{
    public class GetAllLeadsTestCaseSource : IEnumerable
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

            List<LeadAuthExchangeModel> expected = new List<LeadAuthExchangeModel>()
            {
                new LeadAuthExchangeModel()
                {
                    Id = 1,
                    Role = 3,
                    Email = "email@mail.com",
                    HashPassword = "password"
                }
            };

            IdentityResponseModel model = new IdentityResponseModel()
            {
                Id = 1,
                Role = "role",
                IssuerMicroservice = Microservice.MarvelousAuth.ToString()
            };

            yield return new object[] { leads, expected, model };
        }
    }
}
