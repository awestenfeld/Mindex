using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;

namespace challenge.Services
{
    public interface ICompensationService
    {
        Compensation GetCompensation(Guid id);
        Compensation Create(Compensation compensation);
    }
}
