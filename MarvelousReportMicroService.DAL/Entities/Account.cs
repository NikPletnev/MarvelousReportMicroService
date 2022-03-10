using CurrencyEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelousReportMicroService.DAL.Entityes
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Currency CurrencyType { get; set; }
        public Lead Lead { get; set; }
        public bool IsBlocked { get; set; }
        public DateTime? LockDate { get; set; }
    }
}
