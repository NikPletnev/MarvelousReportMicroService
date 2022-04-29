using Marvelous.Contracts.ExchangeModels;
using System.Collections;

namespace MarvelousReportMicroService.API.Tests.ConsumersTests
{
    internal class TestCaseSourceAccountConsumer : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            var accountExchangeModel = new AccountExchangeModel()
            {
                Id = 1,
                Name = "TestAccOne",
                CurrencyType = Marvelous.Contracts.Enums.Currency.RUB,
                LeadId = 1,
                IsBlocked = false
            };
            var accountExchangeModelSecond = new AccountExchangeModel()
            {
                Id = 2,
                Name = "TestAccTwo",
                CurrencyType = Marvelous.Contracts.Enums.Currency.GYD,
                LeadId = 1,
                IsBlocked = true
            };


            yield return new object[] { accountExchangeModel };
            yield return new object[] { accountExchangeModelSecond };

        }
    }
}
