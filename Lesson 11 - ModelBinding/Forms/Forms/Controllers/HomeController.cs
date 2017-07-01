using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forms.Controllers
{
    public class UserModel
    {
        public string name { get; set; }
    }
    public class FormModel
    {
        public string myName { get; set; }

        public string selectedOption { get; set; }

        public string[] AvailableOptions
        {
            get { return new[] {"Option1", "Option2", "Option3"}; }
        }

        public UserModel user { get; set; }

        public HttpPostedFileBase avatar { get; set; }
    }

    public class HomeController : Controller
    {
        [Route()]
        [HttpGet]
        public ActionResult Index()
        {
            return View(new FormModel());
        }

        [Route()]
        [HttpPost]
        public ActionResult Index([ModelBinder(typeof(MyModelBinder))] FormModel model)
        //public ActionResult Index(FormModel model)
        {
            //model.avatar.SaveAs("C:/temp/" + model.avatar.FileName);
            return View(new FormModel());
        }
    }

    public class MovieController : Controller
    {
        [Route("Movie")]
        public ActionResult Index()
        {
            return View();
        }
    }
}