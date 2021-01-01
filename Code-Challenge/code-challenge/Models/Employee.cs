using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace challenge.Models
{
    public class Employee
    {
        [Key]
        [Required(ErrorMessage = "Valid EmployeeId required")]
        [JsonProperty("EmployeeId")]
        public Guid EmployeeId { get; set; }
        
        [MaxLength(255)]
        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [MaxLength(255)]
        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [MaxLength(255)]
        [JsonProperty("Position")]
        public string Position { get; set; }

        [MaxLength(255)]
        [JsonProperty("Department")]
        public string Department { get; set; }
        
        [JsonProperty("DirectReports")]
        public List<Employee> DirectReports { get; set; }
    }
}
