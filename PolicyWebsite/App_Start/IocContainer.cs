using Autofac;
using Autofac.Integration.Mvc;
using Website.PolicyWebsite.Client;
using System.Reflection;
using System.Net.Http;

namespace PolciyWebsite.App_Start
{
    public  class IocContainer
    {
        public static IContainer Create()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.Register((a, b) => new PolicyServiceClient(new HttpClient())).As<IPolicyServiceClient>();

            return builder.Build();
        }
    }
}