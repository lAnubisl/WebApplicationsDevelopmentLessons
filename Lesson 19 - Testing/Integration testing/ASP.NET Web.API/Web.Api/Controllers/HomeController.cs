using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web.Api.Services;

namespace Web.Api.Controllers
{
    //[Authorize]
    public class HomeController : ApiController
    {
        private readonly IService service;

        public HomeController(IService service)
        {
            this.service = service;
        }

        [Route("Count"), HttpGet]
        public HttpResponseMessage Count()
        {
            return Request.CreateResponse(HttpStatusCode.OK, service.CountProducts());
        }
    }
}