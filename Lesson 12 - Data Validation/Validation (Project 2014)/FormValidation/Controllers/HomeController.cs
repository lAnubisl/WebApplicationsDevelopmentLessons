using System.Web.Mvc;
using FormValidation.Models;
using FormValidation.Repository;

namespace FormValidation.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View(new ArticleModel());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(ArticleModel model)
        {
            if (model.NewComment != null && ModelState.IsValid)
            {
                CommentsRepository.Comments.Add(model.NewComment.Comment);
                return View(new ArticleModel());
            }

            return View(model);
        }
    }
}