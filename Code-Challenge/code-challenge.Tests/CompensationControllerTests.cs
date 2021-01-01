using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using challenge.Models;
using code_challenge.Tests.Integration.Extensions;
using code_challenge.Tests.Integration.Helpers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace code_challenge.Tests.Integration
{
    
    [TestClass]
    public class CompensationControllerTests
    {
        private static HttpClient _httpClient;
        private static TestServer _testServer;

        [ClassInitialize]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<TestServerStartup>()
                .UseEnvironment("Development"));

            _httpClient = _testServer.CreateClient();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }

        #region GetEmployeeById
        [TestMethod]
        public void GetEmployeeById_Returns_Ok()
        {
            // Arrange
            Guid employeeId = new Guid("16a596ae-edd3-4847-99fe-c4518e82c86f");

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/Compensation/{employeeId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var compensation = response.DeserializeContent<Compensation>();
            Assert.AreNotEqual(0, compensation.salary);
        }

        [TestMethod]
        public void GetEmployeeById_Returns_NotFound()
        {
            // Arrange
            Guid employeeId = Guid.NewGuid();

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/Compensation/{employeeId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
        #endregion

        #region CreateCompensation
        [TestMethod]
        public void CreateCompensation_Returns_Created()
        {
            // Arrange
            Guid employeeGuid = new Guid("16a596ae-edd3-4847-99fe-c4518e82c86f");
            var compensation = new Compensation()
            {
                ID = employeeGuid,
                employee = new Employee
                {
                    EmployeeId = employeeGuid
                },
                effectiveDate = DateTime.Now,
                salary = 125000
            };

            var requestContent = new JsonSerialization().ToJson(compensation);

            // Execute
            var postRequestTask = _httpClient.PostAsync("api/compensation",
                new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var newCompensation = response.DeserializeContent<Compensation>();
            Assert.AreEqual(newCompensation.employee.EmployeeId,  employeeGuid);
            Assert.AreEqual(compensation.effectiveDate, newCompensation.effectiveDate);
            Assert.AreEqual(compensation.salary, newCompensation.salary);
        }
        #endregion
    }
}
