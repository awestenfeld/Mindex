using challenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Services;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace challenge.Utils
{
    public class HelperTools
    {
        private readonly IEmployeeService _employeeService;
        public static List<Employee> FixUpReferences(List<Employee> employees)
        {
            var employeeIdRefMap =
                from employee in employees
                select new
                {
                    Id = employee.EmployeeId,
                    EmployeeRef = employee
                };
            employees.ForEach(employee =>
            {
                if (employee.DirectReports != null)
                {
                    var referencedEmployees = new List<Employee>(employee.DirectReports.Count);
                    employee.DirectReports.ForEach(report =>
                    {
                        var referencedEmployee = employeeIdRefMap.First(e => e.Id == report.EmployeeId).EmployeeRef;
                        referencedEmployees.Add(referencedEmployee);
                    });
                    employee.DirectReports = referencedEmployees;
                }
            });
            return employees;
        }

        public static int CountNumberOfReports(Employee employee)
        {
            int accumulator = 0;
            employee.DirectReports.ForEach(empl =>
            {
                accumulator += 1;
                if (empl.DirectReports != null)
                {
                    empl.DirectReports.ForEach(rept =>
                    {
                        accumulator += 1;
                    });
                }
            });
            return accumulator;
        }
    }
}
