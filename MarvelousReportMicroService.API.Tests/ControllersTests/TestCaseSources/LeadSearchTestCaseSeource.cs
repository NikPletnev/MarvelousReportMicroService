

using Marvelous.Contracts.Enums;
using MarvelousReportMicroService.BLL.Models;
using MarvelousReportMicroService.DAL.Enums;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MarvelousReportMicroService.API.Tests.ControllersTests.TestCaseSources
{
    public class LeadSearchTestCaseSeource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            string name = "R";
            LeadSearchParams nameParam = LeadSearchParams.Start;
            string lastName = "ov";
            LeadSearchParams lastNameParam = LeadSearchParams.End;
            DateTime startBirthDate = new DateTime(2001, 01, 01);
            DateTime endBirthDate = new DateTime(2002, 01, 01);
            string email = "gmail";
            LeadSearchParams emailParam = LeadSearchParams.Contains;
            string phone = "7";
            LeadSearchParams phoneParam = LeadSearchParams.Start;
            Role role = Role.Regular;
            bool isBanned = false;

            LeadSearchModel leadModel = new LeadSearchModel()
            {
                Name = name,
                NameParam = nameParam,
                LastName = lastName,
                LastNameParam = lastNameParam,
                StartBirthDate = startBirthDate,
                EndBirthDate = endBirthDate,
                Email = email,
                Phone = phone,
                Role = role,
                IsBanned = isBanned,
                EmailParam = emailParam,
                PhoneParam = phoneParam
            };

            List<LeadModel> expectedList = new List<LeadModel>()
            {
                new LeadModel()
                {
                    Id = 18,
                    Name = "Roma",
                    LastName = "Azarov",
                    BirthDate = new DateTime(2001, 07, 29),
                    Email = "qwe@gmail.com",
                    Phone = "77777777777",
                    Role = Role.Regular,
                    IsBanned = false,

                }
            };

            yield return new object[] { name,
                nameParam,
                lastName,
                lastNameParam,
                startBirthDate,
                endBirthDate,
                email,
                emailParam,
                phone,
                phoneParam,
                role,
                isBanned,
                leadModel,
                expectedList};
        }
    }
}
