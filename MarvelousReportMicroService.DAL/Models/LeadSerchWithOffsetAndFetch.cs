using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelousReportMicroService.DAL.Models
{
    public class LeadSerchWithOffsetAndFetch
    {
        public int Offset { get; set; }
        public int Fetch { get; set; }
    }
}
