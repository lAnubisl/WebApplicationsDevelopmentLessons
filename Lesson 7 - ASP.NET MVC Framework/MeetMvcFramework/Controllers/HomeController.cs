using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MeetMvcFramework.Models;

namespace MeetMvcFramework.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var cat = new Cat();
            cat.Name = "Tom";
            cat.Age = 2;

            //var dog = new Dog();
            //dog.Name = "Chappie";
            //dog.Owner = "Alex";

            return View(cat);
        }

        public ActionResult Dog()
        {
            var dog = new Dog();
            dog.Name = "Chappie";
            dog.Owner = "Alex";

            return View(dog);
        }
    }
}