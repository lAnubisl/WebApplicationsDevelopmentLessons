using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ValidationAttributes.Models
{
	public class FormModel
	{
		//[Display(ResourceType = typeof(Names), Name = "UserName")]
		//[Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "UserNameRequired")]
		//[StringLength(4, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "UserNameMaxLength")]
		[Required]
		[StringLength(4)]
		public string UserName { get; set; }

		[RegularExpression("[a-zA-Z0-9]+")]
		public string Password { get; set; }

		[Compare("Password")]
		public string ConfirmPassword { get; set; }

		[MyValidation]
		public string City { get; set; }

		public bool RememberMe { get; set; }

		public IEnumerable<string> Cities
		{
			get
			{
				return new[] { "Minsk", "Grodno", "Gomel" };
			}
		}
	}
}