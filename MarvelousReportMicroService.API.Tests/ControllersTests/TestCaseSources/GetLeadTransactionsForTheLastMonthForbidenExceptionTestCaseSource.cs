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
    public class GetLeadTransactionsForTheLastMonthForbidenExceptionTestCaseSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            int leadId = 1;
            int trancactionsCount = 2;

            IdentityResponseModel model = new IdentityResponseModel()
            {
                Id = 1,
                Role = "role",
                IssuerMicroservice = null
            };
            yield return new object[] { leadId, trancactionsCount, model };

        }
    }
}
