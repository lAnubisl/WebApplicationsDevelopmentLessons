using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Web.Api.IoC;

namespace Web.Api.Configuration
{
    public class MyDependencyScope : IDependencyScope
    {
        private bool disposed;
        private readonly IInversionOfControlContainer container;

        internal MyDependencyScope(IInversionOfControlContainer container)
        {
            this.container = container;
        }

        protected IInversionOfControlContainer Container => container;

        public object GetService(Type serviceType)
        {
            return Container.Resolve(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Container.ResolveAll(serviceType);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                Container?.Dispose();
            }

            disposed = true;
        }
    }
}