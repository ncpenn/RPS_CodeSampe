using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Website.PolicyWebsite.Client;
using Website.PolicyWebsite.Controllers;
using Website.PolicyWebsite.Models;

namespace Website.PolicyWebsite.Tests.Controllers
{
    [TestClass]
    public class InsurancePolicyControllerTests
    {
        [TestMethod]
        public async Task InsurancePolicyController_Index_Returns_View()
        {
            //Arrange
            var displayPolicy = new DisplayPolicy();
            var httpClientMock = new Mock<IPolicyServiceClient>();
            var sut = new InsurancePolicyController(httpClientMock.Object);
            httpClientMock.Setup(x => x.FetchAll()).ReturnsAsync(new List<DisplayPolicy> { displayPolicy });

            //Act
            await sut.Index();

            //Assert
            httpClientMock.Verify(x => x.FetchAll(), Times.Once);

        }

        [TestMethod]
        public async Task InsurancePolicyController_Get_Add_Returns_PartialView()
        {
            //Arrange
            var httpClientMock = new Mock<IPolicyServiceClient>();
            var sut = new InsurancePolicyController(httpClientMock.Object);

            //Act
            await sut.Add();

            //Assert
            httpClientMock.Verify(x => x.GetConstructionTypes(), Times.Once);
        }

        [TestMethod]
        public async Task InsurancePolicyController_Post_Add_Calls_Client_Save()
        {
            //Arrange
            var httpClientMock = new Mock<IPolicyServiceClient>();
            var sut = new InsurancePolicyController(httpClientMock.Object);
            var addPolicy = new AddPolicy();

            //Act
            await sut.Add(addPolicy);

            //Assert
            httpClientMock.Verify(x => x.Save(It.Is<AddPolicy>(y => y.Equals(addPolicy))));
        }
    }
}
