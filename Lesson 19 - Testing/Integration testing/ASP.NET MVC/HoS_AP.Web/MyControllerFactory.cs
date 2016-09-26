using System;
using System.Web.Mvc;
using HoS_AP.Misc;

namespace HoS_AP.Web
{
    public class MyControllerFactory : DefaultControllerFactory
    {
        private readonly IInversionOfControlContainer container;
        public MyControllerFactory(IInversionOfControlContainer container)
        {
            this.container = container;
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            if (controllerType != null && container.IsRegistered(controllerType))
            {
                return (IController)container.Resolve(controllerType);
            }

            return base.GetControllerInstance(requestContext, controllerType);
        }
    }
}