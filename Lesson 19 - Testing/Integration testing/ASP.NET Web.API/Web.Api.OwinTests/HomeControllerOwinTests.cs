using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Microsoft.Owin.Hosting;
using Moq;
using NUnit.Framework;
using Web.Api.Services;

namespace Web.Api.OwinTests
{
    [TestFixture]
    public class HomeControllerOwinTests
    {
        private static readonly string BaseAddress = "http://localhost:8888/";
        private static readonly MyWebApi webApi = new MyWebApi();
        private IDisposable webApp;

        [OneTimeSetUp]
        public void Up()
        {
            webApp = WebApp.Start(BaseAddress, webApi.Configuration);
        }

        [OneTimeTearDown]
        public void Down()
        {
            webApp.Dispose();
        }

        [Test, TestCase(11), TestCase(22)]
        public void Count_should_return_value_from_service(int res)
        {
            var service = new Mock<IService>();
            service.Setup(x => x.CountProducts()).Returns(res);
            var registrations = new Dictionary<Type, Func<object>>
            {
                {typeof(IService), () => service.Object}
            };

            webApi.Container.Replace(registrations);
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(BaseAddress + "/Count"),
                Method = HttpMethod.Get,
            };
            using (var client = new HttpClient())
            {
                var response = client.SendAsync(request).Result;
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                Assert.AreEqual(res.ToString(), response.Content.ReadAsStringAsync().Result);
            }
        }
    }
}