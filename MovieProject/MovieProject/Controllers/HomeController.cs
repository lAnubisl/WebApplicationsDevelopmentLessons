using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            model.Top1Movie.Name = "Terminator 2";
            model.Top1Movie.Directors = new Collection<string>();
            model.Top1Movie.Directors.Add("Dave");
            model.Top1Movie.ImageUri = "/images/header-bg.jpg";
            model.Top1Movie.Rating = 5.5f;
            model.Top1Movie.ReleaDate = new DateTime(2011, 4, 5);

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