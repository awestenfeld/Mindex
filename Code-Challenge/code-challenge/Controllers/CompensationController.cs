using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using challenge.Services;
using challenge.Models;

namespace challenge.Controllers
{
    [Route("api/compensation")]
    public class CompensationController : Controller
    {
        private readonly ILogger _logger;
        private readonly ICompensationService _compensationService;

        public CompensationController(ILogger<CompensationController> logger, 
            ICompensationService compensationService,
            IEmployeeService employeeService)
        {
            _logger = logger;
            _compensationService = compensationService;
        }

        [HttpGet("{id}", Name = "getCompensationById")]
        public IActionResult GetCompensationById(Guid id)
        {
            _logger.LogDebug($"Received Compensation request for '{id}'");

            Compensation compensation = _compensationService.GetCompensation(id);
            if (compensation == null)
                return NotFound();

            return Ok(compensation);
        }

        [HttpPost(Name= "createCompensation")]
        public IActionResult CreateCompensation([FromBody] Compensation compensation)
        {
            _logger.LogDebug($"Received Compensation create request for '{compensation.employee.EmployeeId}'");

            return Created("CreateCompensation", _compensationService.Create(compensation));
        }

    }
}
