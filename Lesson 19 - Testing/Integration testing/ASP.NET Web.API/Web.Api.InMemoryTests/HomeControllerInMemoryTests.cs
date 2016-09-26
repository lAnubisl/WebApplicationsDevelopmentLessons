using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Moq;
using NUnit.Framework;
using Web.Api.InMemoryTests.IntegrationTests;
using Web.Api.Services;

namespace Web.Api.InMemoryTests
{
    [TestFixture]
    public class HomeControllerInMemoryTests
    {
        [Test, TestCase(11), TestCase(22)]
        public void Count_should_return_value_from_service(int res)
        {
            var service = new Mock<IService>();
            service.Setup(x => x.CountProducts()).Returns(res);
            var registrations = new Dictionary<Type, Func<object>>
            {
                { typeof(IService), () => service.Object }
            };

            var server = new InMemoryServer();
            server.Container.Replace(registrations);
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://foo/" + "Count"),
                Method = HttpMethod.Get,
            };

            using (var response = server.GetResponse(request))
            {
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                Assert.AreEqual(res.ToString(), response.Content.ReadAsStringAsync().Result);
            }
        }
    }
}
