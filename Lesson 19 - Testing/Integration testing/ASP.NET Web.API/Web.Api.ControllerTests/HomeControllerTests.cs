using System.Net;
using System.Net.Http;
using System.Web.Http;
using Moq;
using NUnit.Framework;
using Web.Api.Controllers;
using Web.Api.Services;

namespace Web.Api.ControllerTests
{
    [TestFixture]
    public class HomeControllerTests
    {
        [Test, TestCase(11), TestCase(22)]
        public void Count_should_return_value_from_service(int res)
        {
            var service = new Mock<IService>();
            service.Setup(x => x.CountProducts()).Returns(res);

            var controller = new HomeController(service.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            var response = controller.Count();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(res.ToString(), response.Content.ReadAsStringAsync().Result);
        }
    }
}