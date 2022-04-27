using Marvelous.Contracts.ExchangeModels;
using System.Collections;

namespace MarvelousReportMicroService.API.Tests.ConsumersTests.TestCaseSources
{
    internal class AddTransactionFeeTestCaseSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            var TransactionFeeExchangeModel = new ComissionTransactionExchangeModel()
            {
                IdTransaction = 1,
                AmountComission = 33.12m
            };

            yield return TransactionFeeExchangeModel;
        }
    }
}
