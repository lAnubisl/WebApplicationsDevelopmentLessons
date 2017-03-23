using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using TempStorage.Models;

namespace TempStorage.Controllers
{
	public class HomeController : Controller
	{
		// GET: Home
		public ActionResult Index()
		{
			var requestCookies = Request.Cookies["MyKey"];

			var responseCookie = new HttpCookie("MyKey");
			responseCookie.Value = "From Server";
			responseCookie.Expires = DateTime.Now.AddSeconds(5);
			Response.Cookies.Add(responseCookie);

			return View();
		}

		public ActionResult SessionTest()
		{
			if (HttpContext.Session != null)
			{
                HttpContext.Session["MySession"] = new Collection<string> {"Item1", "Item2", "Item3"};
                HttpContext.Session["blablabla"] = "Blablabla !!!";
                HttpContext.Session["User"] = new User() {Name = "Alex"};
			}

			return View();
		}



		[OutputCache(Duration = 60, Location = OutputCacheLocation.Server)]
		public ActionResult CachedPage()
		{
			Thread.Sleep(5000);
			return View();
		}

		[HttpGet]
		public ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Login(string username, string password)
		{
			//Alex's password from database ("123456")
            var passwordFromDatabase = "1000:x4EDMqYUMVwARzOGy/KyINiGXJzmAnsj:6tE2G9/X4ZozQP699EKLzhWuf8NiOsEM";
			if (PasswordHashHelper.ValidatePassword(password, passwordFromDatabase))
			{
				FormsAuthentication.SetAuthCookie(username, true);
			}

			//var ticket = new FormsAuthenticationTicket(
			//	2,
			//	username,
			//	DateTime.Now,
			//	DateTime.Now.AddMinutes(1),
			//	false, // Value of IsPersistent property
			//	String.Empty,
			//	FormsAuthentication.FormsCookiePath
			//);

			//string encryptedTicket = FormsAuthentication.Encrypt(ticket);
			//var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
			//authCookie.Expires = DateTime.Now.AddMinutes(1);
			//Response.Cookies.Add(authCookie);

			return Redirect(FormsAuthentication.GetRedirectUrl(username, false));
		}

		[Authorize(Users = "Alex")]
		//[MyAuthorization]
		public ActionResult ManageAccount()
		{
			return View();
		}
	}
}