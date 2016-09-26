using System;
using System.Collections.Generic;
using System.Linq;
using Web.Api.Services;

namespace Web.Api.IoC
{
    public class SimpleIocContainer : IInversionOfControlContainer
    {
        private IDictionary<Type, Func<object>> registrations = new Dictionary<Type, Func<object>>();

        public SimpleIocContainer()
        {
            RegisterServices();
        }

        private SimpleIocContainer(IDictionary<Type, Func<object>> registrations)
        {
            this.registrations = registrations;
        }

        private void RegisterServices()
        {
            Register<IService, Service>();
        }

        private void Register<TService, TImplementation>()
        {
            if (registrations.ContainsKey(typeof(TService))) return;
            registrations.Add(typeof(IService), () => Resolve(typeof(TImplementation)));
        }

        public void Dispose() {}

        public object Resolve(Type serviceType)
        {
            if (registrations.ContainsKey(serviceType))
            {
                return registrations[serviceType]();
            }
            if (!serviceType.IsAbstract)
            {
                return CreateInstance(serviceType);
            }

            return null;
        }

        private object CreateInstance(Type implementationType)
        {
            var ctor = implementationType.GetConstructors().Single();
            var parameterTypes = ctor.GetParameters().Select(p => p.ParameterType);
            var dependencies = parameterTypes.Select(Resolve).ToArray();
            return Activator.CreateInstance(implementationType, dependencies);
        }

        internal void Replace(IDictionary<Type, Func<object>> registrations)
        {
            this.registrations = registrations;
            RegisterServices();
        }

        public IEnumerable<object> ResolveAll(Type serviceType)
        {
            yield return Resolve(serviceType);
        }

        public IInversionOfControlContainer CreateScope()
        {
            return new SimpleIocContainer(registrations);
        }
    }
}