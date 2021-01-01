using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace challenge.Models
{
    public class Compensation
    {

        [Key]
        public Guid ID { get; set; } 

        [JsonProperty("Employee")]
        public Employee employee { get; set; }

        [JsonProperty("Salary")]
        public double salary { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [JsonProperty("EffectiveDate")]
        public DateTime effectiveDate { get; set; }
    }
}
