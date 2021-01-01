using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using challenge.Models;
using code_challenge.Tests.Integration.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace code_challenge.Tests.Integration
{
    [TestClass ]
    public class ReportingStructureControllerTests
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
            
            var expectedFirstName = "John";
            var expectedLastName = "Lennon";
            int expectedNumberOfReports = 4;

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/ReportingStructure/{employeeId}");
            HttpResponseMessage response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            ReportingStructure reportingStructure = response.DeserializeContent<ReportingStructure>();
            Assert.AreEqual(expectedFirstName, reportingStructure.employee.FirstName);
            Assert.AreEqual(expectedLastName, reportingStructure.employee.LastName);
            Assert.AreEqual(expectedNumberOfReports, reportingStructure.numberOfReports);
        }

        [TestMethod]
        public void GetEmployeeById_Returns_NotFound()
        {
            // Arrange
            Guid employeeId = Guid.NewGuid();

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/ReportingStructure/{employeeId}");
            HttpResponseMessage response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
        #endregion

    }
}
