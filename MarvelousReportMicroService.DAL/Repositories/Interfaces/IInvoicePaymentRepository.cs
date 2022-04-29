using MarvelousReportMicroService.DAL.Entities;

namespace MarvelousReportMicroService.DAL.Repositories
{
    public interface IInvoicePaymentRepository
    {
        Task AddInvoicePayment(InvoicePayment invoice);
    }
}