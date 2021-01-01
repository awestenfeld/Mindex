using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Data;
using challenge.Models;
using challenge.Services;
using Microsoft.Extensions.Logging;

namespace challenge.Repositories
{
    public class CompensationRepository : ICompensationRepository
    {
        private readonly CompensationContext _compensationContext;
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<CompensationRepository> _logger;

        public CompensationRepository(
            CompensationContext compensationContext,
            IEmployeeService employeeService,
            ILogger<CompensationRepository> logger)
        {
            _compensationContext = compensationContext;
            _employeeService = employeeService;
            _logger = logger;
        }

        public Compensation GetCompensation(Guid id)
        {
            //valid employee check
            if (_employeeService.GetById(id) == null)
                return null;
            return _compensationContext.Compensation.ToList().SingleOrDefault(x => x.ID == id);
        }

        public Compensation Create(Compensation compensation)
        {
            _compensationContext.Update(compensation);
            return compensation;
        }
        public Task SaveAsync()
        {
            return _compensationContext.SaveChangesAsync();
        }
    }
}
