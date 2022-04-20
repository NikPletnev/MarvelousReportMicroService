using AutoMapper;
using MarvelousReportMicroService.BLL.Models;
using MarvelousReportMicroService.DAL.Entities;
using MarvelousReportMicroService.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelousReportMicroService.BLL.Services
{
    public class InvoicePaymentService : IInvoicePaymentService
    {
        private readonly IInvoicePaymentRepository _invoicePaymentRepository;
        private readonly IMapper _mapper;

        public InvoicePaymentService(IInvoicePaymentRepository invoicePaymentRepositor, IMapper mapper)
        {
            _invoicePaymentRepository = invoicePaymentRepositor;
            _mapper = mapper;
        }

        public async Task AddIncnvoicePayment(InvoicePaymentModel model)
        {
            await _invoicePaymentRepository.AddInvoicePayment(_mapper.Map<InvoicePayment>(model));
        }
    }
}
