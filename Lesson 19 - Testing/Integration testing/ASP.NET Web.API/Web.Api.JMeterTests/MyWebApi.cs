using System.Web.Http;
using Owin;
using Web.Api.Configuration;

namespace Web.Api.JMeterTests
{
    public class MyWebApi
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            HttpServerConfiguration.Configure(config);
            app.UseWebApi(config);
        }
    }
}