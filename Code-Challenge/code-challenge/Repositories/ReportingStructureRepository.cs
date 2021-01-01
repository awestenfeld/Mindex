using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Data;
using challenge.Models;
using challenge.Services;
using challenge.Utils;
using Microsoft.Extensions.Logging;

namespace challenge.Repositories
{
    public class ReportingStructureRepository : IReportingStructureRepository
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<IReportingStructureRepository> _logger;

        public ReportingStructureRepository(
            IEmployeeService employeeService,
            ILogger<ReportingStructureRepository> logger
            )
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        public ReportingStructure GetById(Guid id)
        {
            Employee employee = _employeeService.GetById(id);
            if (employee == null)
                return null;

            return new ReportingStructure
            {
                employee = employee,
                numberOfReports = HelperTools.CountNumberOfReports(employee)
            };
        }
    }
}
