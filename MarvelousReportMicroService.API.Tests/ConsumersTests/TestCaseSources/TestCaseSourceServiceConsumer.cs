
using Marvelous.Contracts.ExchangeModels;
using System.Collections;

namespace MarvelousReportMicroService.API.Tests.ConsumersTests.TestCaseSources
{
    public class TestCaseSourceServiceConsumer : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            var accountExchangeModel = new ServiceExchangeModel()
            {
                Id = 1,
                Name = "TestAccOne",
                Description = "description",
                Price = 666,
                IsDeleted = false
            };
            var accountExchangeModelSecond = new ServiceExchangeModel()
            {
                Id = 2,
                Name = "TestAccTwo",
                Description = "description222",
                Price = 777,
                IsDeleted = false
            };


            yield return new object[] { accountExchangeModel };
            yield return new object[] { accountExchangeModelSecond };

        }
    }
}
