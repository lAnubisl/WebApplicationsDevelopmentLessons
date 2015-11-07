using System.Web.Mvc;
using ValidationAttributes.Models;

namespace ValidationAttributes.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View(new FormModel());
        }

		[HttpPost]
	    public ActionResult Index([ModelBinder(typeof(MyModelBinder))] FormModel model)
	    {
			//ModelState.AddModelError("UserName", "User Name is not valid!");
			if (ModelState.IsValid)
			{
				return Redirect("Http://google.com");
			}

			return View(model);
		}
    }
}