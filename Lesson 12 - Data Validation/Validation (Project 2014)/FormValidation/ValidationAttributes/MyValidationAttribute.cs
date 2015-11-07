using System.ComponentModel.DataAnnotations;
using FormValidation.Models;
using FormValidation.Resources;

namespace FormValidation.ValidationAttributes
{
    public class MyValidationAttribute : ValidationAttribute
    {
        //public override bool IsValid(object value)
        //{
        //    var age = (int)value;
        //    return age > 0;
        //}

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = validationContext.ObjectInstance as AddCommentModel;
            if (model == null)
            {
                return new ValidationResult("Validation Failed");
            }

            if (model.Age < 1)
            {
                return new ValidationResult(ErrorMessages.InvalidAge);
            }

            return ValidationResult.Success;
        }
    }
}