using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieProject.Models;

namespace MovieProject.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var model = new IndexModel();
            model.Top1Movie = new Top1MovieModel();
            return View(model);
        }

        public ActionResult Error404()
        {
            return View();
        }

        public ActionResult Reviews()
        {
            return View();
        }
    }
}