using System.Net.Http;
using System.Threading;
using System.Web.Http;
using Web.Api.Configuration;
using Web.Api.IoC;

namespace Web.Api.InMemoryTests.IntegrationTests
{
    internal sealed class InMemoryServer
    {
        private readonly HttpMessageInvoker messageInvoker;

        public InMemoryServer()
        {
            var config = new HttpConfiguration();
            HttpServerConfiguration.Configure(config);
            // ReSharper disable once PossibleNullReferenceException
            Container = (config.DependencyResolver as MyDependencyResolver).GetContainer() as SimpleIocContainer;
            var server = new HttpServer(config);
            messageInvoker = new HttpMessageInvoker(new InMemoryHttpContentSerializationHandler(server));
        }

        internal HttpResponseMessage GetResponse(HttpRequestMessage request)
        {
            return messageInvoker.SendAsync(request, CancellationToken.None).Result;
        }

        internal SimpleIocContainer Container { get; }
    }
}