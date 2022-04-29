using MarvelousReportMicroService.BLL.Models;

namespace MarvelousReportMicroService.BLL.Services
{
    public interface IInvoicePaymentService
    {
        Task AddIncnvoicePayment(InvoicePaymentModel model);
    }
}