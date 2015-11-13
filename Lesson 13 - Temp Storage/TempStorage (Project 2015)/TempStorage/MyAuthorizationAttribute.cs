using System.Web;
using System.Web.Mvc;

namespace TempStorage
{
	public class MyAuthorizationAttribute : AuthorizeAttribute
	{
		protected override bool AuthorizeCore(HttpContextBase httpContext)
		{
			var user = httpContext.User.Identity.IsAuthenticated ? httpContext.User.Identity.Name : null;
			if (user != null)
			{
				// check permission in database
			}

			return false;
		}
	}
}