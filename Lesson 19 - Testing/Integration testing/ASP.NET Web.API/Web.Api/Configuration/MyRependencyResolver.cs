using System.Web.Http.Dependencies;
using Web.Api.IoC;

namespace Web.Api.Configuration
{
    public sealed class MyDependencyResolver : MyDependencyScope, IDependencyResolver
    {
        public MyDependencyResolver(IInversionOfControlContainer container) : base(container)
        {
        }

        public IDependencyScope BeginScope()
        {
            return new MyDependencyScope(Container.CreateScope());
        }

        internal IInversionOfControlContainer GetContainer()
        {
            return Container;
        }
    }
}