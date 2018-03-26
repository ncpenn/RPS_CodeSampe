using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;
using Website.PolicyWebsite.App_Start;
using Website.PolicyWebsite.Client;
using Website.PolicyWebsite.Models;

namespace Website.PolicyWebsite.Tests.Client
{
    [TestClass]
    public class PolicyServiceClientTests
    {
        [TestMethod]
        public async Task PolicyServiceCLient_FetchAll_Returns_All()
        {
            //Arrange
            var mockHttp = new MockHttpMessageHandler();
            var insurancePolicy = new InsurancePolicy
            {
                EffectiveDate = DateTime.Now,
                Expiration = DateTime.Now,
                PolicyNumber = "123456",
                PrimaryInsured = new PrimaryInsured
                {
                    PrimaryInsuredFirstName = "Mark",
                    PrimaryInsuredLastName = "Wilson"
                }
            };

            mockHttp.When("http://localhost:50697/api/Policy/fetchall*")
                    .Respond("application/json", JsonConvert.SerializeObject(
                        new List<InsurancePolicy> { insurancePolicy })); 

            var client = mockHttp.ToHttpClient();
            var sut = new PolicyServiceClient(client);

            //Act
            var displayPolicies = await sut.FetchAll();

            //Assert
            Assert.IsTrue(displayPolicies.Count() == 1);
            Assert.AreEqual(123456, displayPolicies.First().PolicyNumber);
        }

        [TestMethod]
        public async Task PolicyServiceCLient_Save_Posts_To_Add()
        {
            //Arrange
            var expectedResult = JsonConvert.SerializeObject(new { test = "Hello World" });
            AutoMapperConfig.RegisterMappings();
            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When("http://localhost:50697/api/Policy/add*")
                    .Respond("application/json", expectedResult);

            var client = mockHttp.ToHttpClient();
            var sut = new PolicyServiceClient(client);

            //Act
            var response = await sut.Save(new AddPolicy { PolicyNumber = "123456"});

            //Assert
            Assert.AreEqual(expectedResult, response);
        }

        [TestMethod]
        public async Task PolicyServiceCLient_GetConstructionTypes_Returns_Types()
        {
            //Arrange
            var mockHttp = new MockHttpMessageHandler();
            var buildingTypes = new BuildingType
            {
                Id = "1",
                Name = "one"
            };

            mockHttp.When("http://localhost:50697/api/constructiontype/fetchall*")
                    .Respond("application/json", JsonConvert.SerializeObject(
                        new List<BuildingType> { buildingTypes }));

            var client = mockHttp.ToHttpClient();
            var sut = new PolicyServiceClient(client);

            //Act
            var constructionTypes = await sut.GetConstructionTypes();

            //Assert
            Assert.IsTrue(constructionTypes.Count() == 1);
            Assert.AreEqual("1", constructionTypes.First().Id);
        }
    }
}
