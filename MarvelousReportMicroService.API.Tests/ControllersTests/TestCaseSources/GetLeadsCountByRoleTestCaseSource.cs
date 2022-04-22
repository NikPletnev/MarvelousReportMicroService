using Marvelous.Contracts.Enums;
using System.Collections;

namespace MarvelousReportMicroService.API.Tests.ControllersTests.TestCaseSources
{
    public class GetLeadsCountByRoleTestCaseSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            Role role = Role.Regular;
            int count = 1;
            int expected = 3;

            yield return new object[] { role, count, count };
        }
    }
}
