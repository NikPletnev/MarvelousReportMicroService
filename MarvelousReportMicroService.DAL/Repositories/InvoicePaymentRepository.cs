using MarvelousReportMicroService.DAL.Configuration;
using MarvelousReportMicroService.DAL.Helpers;
using Microsoft.Extensions.Options;
using System.Data;
using Dapper;
using MarvelousReportMicroService.DAL.Entities;

namespace MarvelousReportMicroService.DAL.Repositories
{
    public class InvoicePaymentRepository : BaseRepository, IInvoicePaymentRepository
    {
        public InvoicePaymentRepository(IOptions<DbConfiguration> options) : base(options)
        {

        }

        public async Task AddInvoicePayment(InvoicePayment invoice)
        {
            using IDbConnection connection = ProvideConnection();

            await connection
                   .QueryAsync<InvoicePayment>(
                   Queries.AddInvoicePayment
                   , new
                   {
                       invoice.AccountId,
                       invoice.GrossAmount,
                       invoice.PaypalFee,
                       invoice.MarvelousFee,
                       invoice.TransactionAmount,
                       invoice.TransactionId
                   }
                   , commandType: CommandType.StoredProcedure);
        }
    }
}
