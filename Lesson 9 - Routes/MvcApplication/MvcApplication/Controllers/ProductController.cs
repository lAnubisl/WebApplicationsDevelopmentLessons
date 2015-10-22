using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication.Controllers
{
    [RoutePrefix("pro")]
    public class ProductController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

        [Route("duct/{productName}/details")]
        public ActionResult Details(string productName = "tv1")
        {
            return View();
        }
    }
}