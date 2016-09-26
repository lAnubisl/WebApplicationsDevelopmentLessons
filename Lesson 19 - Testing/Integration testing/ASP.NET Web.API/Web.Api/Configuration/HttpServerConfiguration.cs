using System.Web.Http;
using Web.Api.IoC;

namespace Web.Api.Configuration
{
    public static class HttpServerConfiguration
    {
        public static void Configure(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.DependencyResolver = new MyDependencyResolver(new SimpleIocContainer());
            config.EnsureInitialized();
        }
    }
}