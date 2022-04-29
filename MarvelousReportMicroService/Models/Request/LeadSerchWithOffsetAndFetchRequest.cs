using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarvelousReportMicroService.API.Models.Request
{
    public class LeadSerchWithOffsetAndFetchRequest
    {
        public int Offset { get; set; }
        public int Fetch { get; set; }
    }
}
