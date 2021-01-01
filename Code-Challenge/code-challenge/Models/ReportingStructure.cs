using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace challenge.Models
{
    public class ReportingStructure
    {
        [JsonProperty("Employee")]
        public Employee employee { get; set; }
        
        [JsonProperty("NumberOfReports")]
        public int numberOfReports { get; set; }
    }
}
