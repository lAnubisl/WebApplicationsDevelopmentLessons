using System;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using HoS_AP.DI;
using log4net;

namespace HoS_AP.Web
{
    public class Global : HttpApplication
    {
        private static readonly ILog Logger = LogManager.GetLogger("Global");
        private static InversionOfControlContainer container;

        public static InversionOfControlContainer Container
        {
            get { return container; }
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();
            Logger.Debug("Application Start");
            RouteTable.Routes.MapMvcAttributeRoutes();
            ModelValidatorProviders.Providers.Clear();
            container = new InversionOfControlContainer();
            container.RegisterControllers(
                Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.BaseType == typeof(Controller))
            );

            ControllerBuilder.Current.SetControllerFactory(new MyControllerFactory(container));
        }

        void Application_Error(object sender, EventArgs e)
        {
            Exception exc = Server.GetLastError();
            Logger.ErrorFormat("Error in application: {0}", exc.Message + exc.StackTrace);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            Logger.DebugFormat("Incoming request: {0}", HttpContext.Current.Request.Url);   
        }
    }
}