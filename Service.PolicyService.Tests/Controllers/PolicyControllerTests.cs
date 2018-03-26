using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.PolicyService.Controllers;
using Service.PolicyService.Models;
using Service.PolicyService.Repositories;
using Moq;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System.Linq;

namespace Service.PolicyService.Tests.Controllers
{
    /// <summary>
    /// Summary description for PolicyControllerTests
    /// </summary>
    [TestClass]
    public class PolicyControllerTests
    {
        [TestMethod]
        public async Task GetPolicies_Returns_All()
        {
            //Arrange
            var insurancePolicies = new List<InsurancePolicy>()
            {
                new InsurancePolicy
                {
                    PolicyNumber = Guid.NewGuid().ToString()
                }
            };

            var repositoryMock = new Mock<IRepository<InsurancePolicy>>();
            repositoryMock.Setup(x => x.FetchAll()).ReturnsAsync(insurancePolicies.AsEnumerable());
            var sut = new PolicyController(repositoryMock.Object);

            //Act
            var result = await sut.GetPolicies() as OkNegotiatedContentResult<IEnumerable<InsurancePolicy>>;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(insurancePolicies.Count, result.Content.Count());
            Assert.AreEqual(insurancePolicies.First().PolicyNumber, result.Content.First().PolicyNumber);
        }


        [TestMethod]
        public async Task Add_Calls_Repo()
        {
            //Arrange
            var insurancePolicy = new InsurancePolicy
            {
                PolicyNumber = Guid.NewGuid().ToString()
            };

            var repositoryMock = new Mock<IRepository<InsurancePolicy>>();
            var sut = new PolicyController(repositoryMock.Object);

            //Act
            await sut.Add(insurancePolicy);

            //Assert
            
            repositoryMock.Verify(x => x.Add(It.Is<InsurancePolicy>(y => y.Equals(insurancePolicy))));
        }

        [TestMethod]
        public async Task Add_Calls_With_Null_Retunrs_400()
        {
            //Arrange
            var repositoryMock = new Mock<IRepository<InsurancePolicy>>();
            var sut = new PolicyController(null);

            //Act
            var result = await sut.Add(null) as BadRequestResult;

            //Assert
            Assert.IsNotNull(result);
            repositoryMock.Verify(x => x.Add(It.IsAny<InsurancePolicy>()), Times.Never);
        }
    }
}
