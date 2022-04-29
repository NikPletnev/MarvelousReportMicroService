using Marvelous.Contracts.ExchangeModels;
using System.Collections;

namespace MarvelousReportMicroService.API.Tests.ConsumersTests.TestCaseSources
{
    public class LeadConsumeTestTestCaseSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            LeadFullExchangeModel model = new LeadFullExchangeModel()
            {
                Id = 1,
                Role = Marvelous.Contracts.Enums.Role.Regular,
                Name = "Name",
                LastName = "Last Name",
                BirthDate = new System.DateTime(2000, 01, 01),
                Email = "email@mail.ru",
                Phone = "7777777777",
                Password = "qwe",
                City = "gorod",
                IsBanned = false
            };

            yield return model;
        }
    }
}
