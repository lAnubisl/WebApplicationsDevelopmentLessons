using System.Web.Mvc;
using SimpleWebApp.Models;

namespace SimpleWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var provider = new CategoryProvider();
            return View(provider.Categorues());
        }
    }
}