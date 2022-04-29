using Marvelous.Contracts.ResponseModels;
using System.Collections;

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
