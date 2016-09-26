using System;
using System.Web.Http.SelfHost;
using Web.Api.Configuration;
using Web.Api.IoC;

namespace Web.Api.SelfHostedTests
{
    internal sealed class SelfHostedServer : IDisposable
    {
        internal static readonly string BaseAddress = "http://localhost:8082";
        private readonly HttpSelfHostServer server;

        public SelfHostedServer()
        {
            var config = new HttpSelfHostConfiguration(BaseAddress);
            HttpServerConfiguration.Configure(config);
            // ReSharper disable once PossibleNullReferenceException
            Container = (config.DependencyResolver as MyDependencyResolver).GetContainer() as SimpleIocContainer;
            server = new HttpSelfHostServer(config);
            server.OpenAsync().Wait();
        }

        internal SimpleIocContainer Container { get; }

        void IDisposable.Dispose()
        {
            server.CloseAsync().Wait();
            server.Dispose();
        }
    }
}