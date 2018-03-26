using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Service.PolicyService.Controllers;
using Service.PolicyService.Models;
using Service.PolicyService.Repositories;

namespace Service.PolicyService.Tests.Controllers
{
    [TestClass]
    public class ConstructionTypeControllerTests
    {
        [TestMethod]
        public async Task FetchAll_Returns_All()
        {
            //Arrange
            var constructionTypes = new List<ConstructionType>
            {
                new ConstructionType
                {
                    Name = Guid.NewGuid().ToString()
                }
            };
            var repositoryMock = new Mock<IRepository<ConstructionType>>();
            repositoryMock.Setup(x => x.FetchAll()).ReturnsAsync(constructionTypes.AsEnumerable());
            var sut = new ConstructionTypeController(repositoryMock.Object);

            //Act
            var result = await sut.FetchAll() as OkNegotiatedContentResult<IEnumerable<ConstructionType>>;
            
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(constructionTypes.Count, result.Content.Count());
            Assert.AreEqual(constructionTypes.First().Name, result.Content.First().Name);
        }
    }
}
