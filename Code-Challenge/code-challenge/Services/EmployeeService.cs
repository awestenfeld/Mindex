using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using Microsoft.Extensions.Logging;
using challenge.Repositories;

namespace challenge.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(ILogger<EmployeeService> logger, IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public Employee Create(Employee employee)
        {
            if (employee == null)
                return null;
            
            _employeeRepository.Add(employee);
            _employeeRepository.SaveAsync().Wait();
            return employee;
        }

        public Employee GetById(Guid id)
        {
            return _employeeRepository.GetById(id);
        }

        public Employee Replace(Employee originalEmployee, Employee newEmployee)
        {
            if(originalEmployee != null)
            {
                _employeeRepository.Remove(originalEmployee);
                if (newEmployee != null)
                {
                    // ensure the original has been removed, otherwise EF will complain another entity w/ same id already exists
                    _employeeRepository.SaveAsync().Wait();

                    _employeeRepository.Add(newEmployee);
                    // overwrite the new id with previous employee id
                    newEmployee.EmployeeId = originalEmployee.EmployeeId;
                }
                _employeeRepository.SaveAsync().Wait();
            }
            return newEmployee;
        }
        public Employee Update(Employee employee)
        {
            _employeeRepository.Update(employee);
            _employeeRepository.SaveAsync().Wait();
            return employee;
        }

        public bool isValidEmployee(Guid id)
        {
            var employee = _employeeRepository.GetById(id);
            return employee != null;
        }
    }
}
