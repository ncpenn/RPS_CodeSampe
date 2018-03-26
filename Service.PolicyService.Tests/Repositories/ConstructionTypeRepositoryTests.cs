using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Service.PolicyService.DAL;
using Service.PolicyService.Repositories;

namespace Service.PolicyService.Tests.Repositories
{
    [TestClass]
    public class ConstructionTypeRepositoryTests
    {
        [TestMethod]
        public async Task FetchAll_ConstructionType_Returns_All ()
        {
            //Arrange
            var constructionTypes = new List<ConstructionType>
            {
                new ConstructionType
                {
                    ConstructionTypeId = 1,
                    Name = "Hello world"
                }
            }.AsQueryable();
            var dbcontext = new Mock<PolicyDatabaseEntities>();
            var dbset = new Mock<DbSet<ConstructionType>>();

            dbset.As<IDbAsyncEnumerable<ConstructionType>>()
               .Setup(m => m.GetAsyncEnumerator())
               .Returns(new TestDbAsyncEnumerator<ConstructionType>(constructionTypes.GetEnumerator()));

            dbset.As<IQueryable<ConstructionType>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<ConstructionType>(constructionTypes.Provider));

            dbset.As<IQueryable<ConstructionType>>().Setup(m => m.Expression).Returns(constructionTypes.Expression);
            dbset.As<IQueryable<ConstructionType>>().Setup(m => m.ElementType).Returns(constructionTypes.ElementType);
            dbset.As<IQueryable<ConstructionType>>().Setup(m => m.GetEnumerator()).Returns(constructionTypes.GetEnumerator());

            dbcontext.Setup(x => x.ConstructionTypes).Returns(dbset.Object);
            var sut = new ConstructionTypeRepository(dbcontext.Object);

            //Act
            var results = await sut.FetchAll();

            //Assert
            Assert.IsInstanceOfType(results, typeof(IEnumerable<Models.ConstructionType>));
            Assert.AreEqual(constructionTypes.First().Name, results.First().Name);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void Add_ConstructionType_Throws_NotImpementedException()
        {
            //Arrange
            var contextStub = new Mock<PolicyDatabaseEntities>();
            var sut = new ConstructionTypeRepository(contextStub.Object);

            //Act
            sut.Add(new Models.ConstructionType());
        }
    }
}
