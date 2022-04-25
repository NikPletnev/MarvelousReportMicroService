using Marvelous.Contracts.ResponseModels;
using System.Collections;

namespace MarvelousReportMicroService.API.Tests.ControllersTests
{
    public class GetCountLeadTransactionWithoutWithdrawalForbidenExceptionTestCaseSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            int leadId = 1;
            int leadCount = 4;

            IdentityResponseModel model = new IdentityResponseModel()
            {
                Id = 1,
                Role = "role",
                IssuerMicroservice = null
            };

            yield return new object[] { leadId, leadCount, model };
        }
    }
}

