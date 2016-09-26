using System.Web.Http;
using Owin;
using Web.Api.Configuration;
using Web.Api.IoC;

namespace Web.Api.OwinTests
{
    public class MyWebApi
    {
        internal void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            HttpServerConfiguration.Configure(config);
            Container = (config.DependencyResolver as MyDependencyResolver).GetContainer() as SimpleIocContainer;
            app.UseWebApi(config);
        }

        internal SimpleIocContainer Container { get; set; }
    }
}