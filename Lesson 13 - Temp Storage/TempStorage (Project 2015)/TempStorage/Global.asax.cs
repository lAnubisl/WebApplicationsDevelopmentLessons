using System.Web.Mvc;
using System.Web.Routing;

namespace TempStorage
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

	    protected void Session_OnStart()
	    {
		    
	    }

	    protected void Session_OnEnd()
	    {
		    
	    }
    }
}