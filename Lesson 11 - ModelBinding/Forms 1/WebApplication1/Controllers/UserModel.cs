using System.Collections.ObjectModel;

namespace WebApplication1.Models
{
	public class UserModel
	{
		public string UserName { get; set; }
		public string Password { get; set; }
		public string City { get; set; }
		public Collection<string> Cities { get; set; } 
	}
}