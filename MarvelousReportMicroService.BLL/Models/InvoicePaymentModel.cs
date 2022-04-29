using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelousReportMicroService.BLL.Models
{
    public  class InvoicePaymentModel
    {
        public int AccountId { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal PaypalFee { get; set; }
        public decimal MarvelousFee { get; set; }
        public decimal TransactionAmount { get; set; }
        public long TransactionId { get; set; }
    }
}
