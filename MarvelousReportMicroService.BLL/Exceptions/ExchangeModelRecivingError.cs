using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelousReportMicroService.BLL.Exceptions
{
    public class ExchangeModelRecivingError : Exception
    {
        public ExchangeModelRecivingError(string message) : base(message)
        {

        }
    }
}
