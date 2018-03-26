using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Website.PolicyWebsite.Models;

namespace Website.PolicyWebsite.Tests.Models
{
    [TestClass]
    public class AddPolicyTests
    {
        [TestMethod]
        public void AddPolicy_Maps_ConstuctionTypes_To_SelectedList()
        {
            //Arrange

            var buildingTypes = new List<BuildingType>()
            {
                new BuildingType
                {
                    Id = "1",
                    Name = "One"
                },
                new BuildingType
                {
                    Id = "2",
                    Name = "Two"
                }
            }.AsEnumerable();

            //Act
            var sut = new AddPolicy(buildingTypes);

            //Assert
            var expectedList = buildingTypes.ToList();
            var actualList = sut.RiskConstruction.ToList();
            for (int i = 0; i < expectedList.Count; i++)
            {
                Assert.AreEqual(expectedList[i].Name, actualList[i].Text);
                Assert.AreEqual(expectedList[i].Id, actualList[i].Value);
            }
        }
    }
}
