using System.Web;
using System.Web.Http;
using Web.Api.Configuration;

namespace Web.Api
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            HttpServerConfiguration.Configure(GlobalConfiguration.Configuration);
        }
    }
}