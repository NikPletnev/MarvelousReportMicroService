using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarvelousReportMicroService.API.Models.Request
{
    public class LeadSerchWithOffsetAndFetchRequest
    {
        int Offset { get; set; }
        int Fetch { get; set; }
    }
}
