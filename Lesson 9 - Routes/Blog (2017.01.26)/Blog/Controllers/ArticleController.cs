using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    [RoutePrefix("articles")]
    public class ArticleController : Controller
    {
        [Route("{pageNumber}/{pageSize}")]
        public ActionResult Listing(int pageNumber, int pageSize)
        {
            return new EmptyResult();
        }

        [Route("{articleName}")]
        public ActionResult View(string articleName)
        {
            return new EmptyResult();
        }
    }
}