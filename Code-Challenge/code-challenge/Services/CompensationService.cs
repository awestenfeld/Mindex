using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using challenge.Repositories;
using Microsoft.Extensions.Logging;

namespace challenge.Services
{
    public class CompensationService : ICompensationService
    {
        private readonly ICompensationRepository _compensationRepository;
        private readonly ILogger<CompensationService> _logger;

        public CompensationService(ICompensationRepository compensationRepository,
            ILogger<CompensationService> logger)
        {
            _compensationRepository = compensationRepository;
            _logger = logger;
        }
        public Compensation GetCompensation(Guid id)
        {
            return _compensationRepository.GetCompensation(id);
        }
        public Compensation Create(Compensation compensation)
        {
            _compensationRepository.Create(compensation);
            _compensationRepository.SaveAsync().Wait();
            return compensation;
        }
    }
}
