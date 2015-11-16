using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
	public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
			/// Get From Database
			var model = new UserModel();
	        model.UserName = "Alex";
	        model.Password = "123456";
	        model.City = "Minsk";

	        FillCities(model);

			return View(model);
        }

		[HttpPost]
	    public ActionResult Index(UserModel model)
	    {
			/// Save To Database
			return RedirectToAction("Index");
			FillCities(model);
		    return View(model);
	    }

		private void FillCities(UserModel model)
		{
			model.Cities = new Collection<string>();
			model.Cities.Add("Gomel");
			model.Cities.Add("Minsk");
			model.Cities.Add("Grodno");
		}
    }
}