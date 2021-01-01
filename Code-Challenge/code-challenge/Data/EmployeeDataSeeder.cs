using challenge.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using challenge.Utils;

namespace challenge.Data
{
    public class EmployeeDataSeeder
    {
        private readonly EmployeeContext _employeeContext;
        private const string EMPLOYEE_SEED_DATA_FILE = "resources/EmployeeSeedData.json";

        public EmployeeDataSeeder(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }

        public async Task Seed()
        {
            if(!_employeeContext.Employees.Any())
            {
                await _employeeContext.Employees.AddRangeAsync(LoadEmployees());
                await _employeeContext.SaveChangesAsync();
            }
        }

        private static IEnumerable<Employee> LoadEmployees()
        {
            using (StreamReader sr = File.OpenText(EMPLOYEE_SEED_DATA_FILE))
            {
                return HelperTools.FixUpReferences(JsonConvert.DeserializeObject<List<Employee>>(sr.ReadToEnd()));
            }
        }
    }
}
