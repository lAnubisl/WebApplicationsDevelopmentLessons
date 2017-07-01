using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private IProductPresentationService productPresentationService 
            = new ProductPresentationService(null, null);
        // GET: Home
        public ActionResult Index()
        {
            var model = productPresentationService.GetTop(1).FirstOrDefault();
            return View(model);
        }
    }
}