using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Models;

namespace Blog.Controllers
{
    [Route]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            PageModel model = new PageModel("Alex", 32);
            PageModel model2 = new PageModel
            {
                Name2 = "Lisa",
                Age2 = 28
            };

            return View(model);
        }

        public ActionResult Index2()
        {
            PageModel model = new PageModel("Alex", 32);
            return View(model);
        }

        public ActionResult Contact()
        {
            PageModel model = new PageModel("Alisa", 4);
            return View("Contact", model);
        }
    }
}