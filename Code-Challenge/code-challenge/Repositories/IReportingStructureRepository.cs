using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;

namespace challenge.Repositories
{
    public interface IReportingStructureRepository
    {
        ReportingStructure GetById(Guid id);
    }
}
