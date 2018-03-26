using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Service.PolicyService.App_Start;
using Service.PolicyService.DAL;
using Service.PolicyService.Models;
using Service.PolicyService.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.PolicyService.Tests.Repositories
{
    [TestClass]
    public class PolicyRepositoryTests
    {
        [TestMethod]
        public async Task Fetch_All_Policies_Returns_All()
        {
            //Arrange
            var policies = new List<Policy>
            {
                new Policy
                {
                    PolicyNumber = 123456789
                }
            }.AsQueryable();
            var dbcontext = new Mock<PolicyDatabaseEntities>();
            var dbset = new Mock<DbSet<Policy>>();

            dbset.As<IDbAsyncEnumerable<Policy>>()
               .Setup(m => m.GetAsyncEnumerator())
               .Returns(new TestDbAsyncEnumerator<Policy>(policies.GetEnumerator()));

            dbset.As<IQueryable<Policy>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Policy>(policies.Provider));

            dbset.As<IQueryable<Policy>>().Setup(m => m.Expression).Returns(policies.Expression);
            dbset.As<IQueryable<Policy>>().Setup(m => m.ElementType).Returns(policies.ElementType);
            dbset.As<IQueryable<Policy>>().Setup(m => m.GetEnumerator()).Returns(policies.GetEnumerator());

            dbcontext.Setup(x => x.Policies).Returns(dbset.Object);

            var primaryInsureds = new List<DAL.PrimaryInsured>
            {
                new DAL.PrimaryInsured
                {
                    PolicyNumber = 123456789
                }
            }.AsQueryable();

            var dbset2 = new Mock<DbSet<DAL.PrimaryInsured>>();

            dbset2.As<IDbAsyncEnumerable<DAL.PrimaryInsured>>()
               .Setup(m => m.GetAsyncEnumerator())
               .Returns(new TestDbAsyncEnumerator<DAL.PrimaryInsured>(primaryInsureds.GetEnumerator()));

            dbset2.As<IQueryable<DAL.PrimaryInsured>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<DAL.PrimaryInsured>(primaryInsureds.Provider));

            dbset2.As<IQueryable<DAL.PrimaryInsured>>().Setup(m => m.Expression).Returns(primaryInsureds.Expression);
            dbset2.As<IQueryable<DAL.PrimaryInsured>>().Setup(m => m.ElementType).Returns(primaryInsureds.ElementType);
            dbset2.As<IQueryable<DAL.PrimaryInsured>>().Setup(m => m.GetEnumerator()).Returns(primaryInsureds.GetEnumerator());

            dbcontext.Setup(x => x.PrimaryInsureds).Returns(dbset2.Object);

            var sut = new PolicyRepository(dbcontext.Object);

            //Act
            var results = await sut.FetchAll();

            //Assert
            Assert.IsInstanceOfType(results, typeof(IEnumerable<InsurancePolicy>));
            Assert.AreEqual(policies.First().PolicyNumber.ToString(), results.First().PolicyNumber);
        }

        [TestMethod]
        public async Task Add_Policy_Call_SaveChanges_Asnyc()
        {
            //Arrange
            AutoMapperConfig.RegisterMappings();

            var insurancePolicy = new InsurancePolicy
            {
                PolicyNumber = "123456789",
                EffectiveDate = DateTime.Now,
                Expiration = DateTime.Now,
                InsuredRisk = new InsuredRisk
                {
                    ConstructionType = new Models.ConstructionType
                    {
                        Id = "1"
                    },
                    YearBuilt = "01-01-1900",
                    Location = new Models.Address
                    {
                        Street = "123 Main",
                        City = "Wales",
                        State = "WI",
                        ZipCode = "53193"
                    }
                },
                PrimaryInsured = new Models.PrimaryInsured
                {
                    PrimaryInsuredAddress = new Models.Address
                    {
                        Street = "123 Main",
                        City = "Wales",
                        State = "WI",
                        ZipCode = "53193"
                    },
                    PrimaryInsuredFirstName = "Stephan",
                    PrimaryInsuredLastName = "Frank"
                }
            };
            var dbset = new Mock<DbSet<Policy>>();
            var dbcontext = new Mock<PolicyDatabaseEntities>();
            var sut = new PolicyRepository(dbcontext.Object);
            dbcontext.Setup(m => m.Policies).Returns(dbset.Object);

            //Act
            await sut.Add(insurancePolicy);

            //Assert
            dbset.Verify(m => m.Add(It.IsAny<Policy>()), Times.Once());
            dbcontext.Verify(m => m.SaveChangesAsync(), Times.Once());
        }
    }
}
