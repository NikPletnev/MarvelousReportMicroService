using Marvelous.Contracts.Enums;
using Marvelous.Contracts.ResponseModels;
using MarvelousReportMicroService.API.Models;
using MarvelousReportMicroService.BLL.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MarvelousReportMicroService.API.Tests.ControllersTests.TestCaseSources
{
    public class GetLeadWithOffsetAndFetchTestCaseSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            int fetch = 1;
            int offset = 3;

            LeadSerchWithOffsetAndFetchModel leadSearchModel = new LeadSerchWithOffsetAndFetchModel()
            {
                Offset = offset,
                Fetch = fetch
            };

            List<LeadStatusUpdateResponse> expected = new List<LeadStatusUpdateResponse>()
            {
                new LeadStatusUpdateResponse()
                {
                    Id = 1,
                    Role = Role.Regular,
                    BirthDate = new DateTime(2000, 01, 01),
                    Email = "email"
                },
                new LeadStatusUpdateResponse()
                {
                    Id = 2,
                    Role = Role.Regular,
                    BirthDate = new DateTime(2001, 01, 01),
                    Email = "email1"
                },new LeadStatusUpdateResponse()
                {
                    Id = 3,
                    Role = Role.Regular,
                    BirthDate = new DateTime(2002, 01, 01),
                    Email = "email2"
                }
            };

            IdentityResponseModel model = new IdentityResponseModel()
            {
                Id = 1,
                Role = "role",
                IssuerMicroservice = "MarvelousAccountChecking"
            };

            yield return new object[] { fetch, offset, leadSearchModel, expected, model };
        }
    }
}
