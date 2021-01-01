using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using challenge.Services;
using challenge.Models;

namespace challenge.Controllers
{
    [Route("api/employee")]
    public class EmployeeController : Controller
    {
        private readonly ILogger _logger;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        [HttpPost(Name = "createEmployee")]
        public IActionResult CreateEmployee([FromBody] Employee employee)
        {
            _logger.LogDebug($"Received employee create request for '{employee.FirstName} {employee.LastName}'");

            _employeeService.Create(employee);
            return CreatedAtRoute("getEmployeeById", new { id = employee.EmployeeId }, employee);
        }

        [HttpGet("{id}", Name = "getEmployeeById")]
        public IActionResult GetEmployeeById(Guid id)
        {
            _logger.LogDebug($"Received employee get request for '{id}'");
            var employee = _employeeService.GetById(id);
            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPut("{id}", Name = "replaceEmployee")]
        public IActionResult ReplaceEmployee(Guid id, [FromBody]Employee newEmployee)
        {
            _logger.LogDebug($"Received employee update request for '{id}'");

            var existingEmployee = _employeeService.GetById(id);
            if (existingEmployee == null)
                return NotFound();
            
            _employeeService.Replace(existingEmployee, newEmployee);
            return Ok(newEmployee);
        }

        [HttpPut(Name = "updateEmployee")]
        public IActionResult UpdateEmployee([FromBody] Employee employee)
        {
            var existingEmployee = _employeeService.GetById(employee.EmployeeId);
            if (existingEmployee == null)
                return NotFound();
            
            _employeeService.Update(employee);
            return Ok(employee);
        }

    }
}
