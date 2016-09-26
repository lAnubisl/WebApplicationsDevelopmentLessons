using HoS_AP.BLL.Models;
using HoS_AP.BLL.ServiceInterfaces;
using System.Web.Mvc;
using System.Web.Security;

namespace HoS_AP.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [Route]
        public ActionResult Login()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Character");
            }

            return View(new AuthenticationModel());
        }

        [Route, HttpPost]
        public ActionResult Login(AuthenticationModel model)
        {
            var operationResult = accountService.Authenticate(model);
            if (operationResult.IsValid)
            {
                FormsAuthentication.SetAuthCookie(model.UserName, true);
                return RedirectToAction("Index", "Character");
            }

            operationResult.ToModelErrors(ModelState);
            return View(model);
        }

        [Route("logout")]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}