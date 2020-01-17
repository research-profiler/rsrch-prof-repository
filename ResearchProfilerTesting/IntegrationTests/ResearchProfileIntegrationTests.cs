using System;
using System.Collections.Generic;
using System.Text;
using GrpcIntegrationTestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResearchProfilerRepo;
namespace ResearchProfiler.Tests.Integration 
{
    [TestClass]
    public class ResearchProfileIntegrationTests : FunctionalTestBase<ResearchProfilerRepo.Startup>
    {
        [TestMethod]
        public void TestSetup()
        {
            // Arrange
            var client = new Greeter.GreeterClient(Channel);

            // Act
            var response = client.SayHello(new HelloRequest { Name = "Joe" });

            // Assert
            Assert.AreEqual("Hello Joe", response.Message);
        }
    }
}
