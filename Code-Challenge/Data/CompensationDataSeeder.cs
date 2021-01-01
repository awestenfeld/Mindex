using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using challenge.Models;
using challenge.Services;
using challenge.Utils;
using Newtonsoft.Json;

namespace challenge.Data
{
    public class CompensationDataSeeder
    {
        private readonly CompensationContext _compensationContext;
        private const string COMPENSATION_SEED_DATA_FILE = "resources/EmployeeSeedData.json";

        public CompensationDataSeeder(CompensationContext compensationContext)
        {
            _compensationContext = compensationContext;
        }

        public async Task Seed()
        {
            if (!_compensationContext.Compensation.Any())
            {
                await _compensationContext.Compensation.AddRangeAsync(LoadCompensation());
                await _compensationContext.SaveChangesAsync();
            }
        }

        private static IEnumerable<Compensation> LoadCompensation()
        {
            using (StreamReader sr = File.OpenText(COMPENSATION_SEED_DATA_FILE))
            {
                List<Employee> list = JsonConvert.DeserializeObject<List<Employee>>(sr.ReadToEnd());
                List<Compensation> sampleListOfCompensations = new List<Compensation>();
                list.ForEach(employee =>
                {
                    sampleListOfCompensations.Add(
                        new Compensation()
                        {
                            ID = employee.EmployeeId,
                            employee = employee,
                            effectiveDate = RandomDay(),
                            salary = GetRandomNumber(10000, 1000000)
                        });
                });

                return sampleListOfCompensations;
            }
        }

        private static readonly Random gen = new Random();
        private static DateTime RandomDay()
        {
            lock (gen)
            {
                DateTime start = new DateTime(1995, 1, 1);
                int range = (DateTime.Today - start).Days;
                return start.AddDays(gen.Next(range));
            }

        }

        private static readonly Random getrandom = new Random();
        public static int GetRandomNumber(int min, int max)
        {
            lock (getrandom)
            {
                return getrandom.Next(min, max);
            }
        }
    }
}
