using challenge.Models;
using System;
using System.Threading.Tasks;

namespace challenge.Repositories
{
    public interface IEmployeeRepository
    {
        Employee GetById(Guid id);
        Employee Add(Employee employee);
        Employee Remove(Employee employee);
        Employee Update(Employee employee);
        Task SaveAsync();
    }
}