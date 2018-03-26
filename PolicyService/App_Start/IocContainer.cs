using Autofac;
using Autofac.Integration.WebApi;
using Service.PolicyService.DAL;
using Service.PolicyService.Models;
using Service.PolicyService.Repositories;
using System.Reflection;

namespace Service.PolicyService.App_Start
{
    public class IocContainer
    {
        public static IContainer Create()
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.Register((a, b) => new PolicyRepository(new PolicyDatabaseEntities())).As<IRepository<InsurancePolicy>>();

            builder.Register((a, b) => new ConstructionTypeRepository(new PolicyDatabaseEntities())).As<IRepository<Models.ConstructionType>>();

            return builder.Build();
        }
    }
}