using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ValidationAttributes.Models;

namespace ValidationAttributes
{
	public class MyValidationAttribute : ValidationAttribute
	{
		public override bool IsValid(object value)
		{
			var str = value as string;
			if (str != null && !str.StartsWith("M"))
			{
				return false;
			}

			return true;
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var model = validationContext.ObjectInstance as FormModel;
			if (model != null && model.UserName != null)
			{
				return ValidationResult.Success;
			}

			return base.IsValid(value, validationContext);
		}
	}
}