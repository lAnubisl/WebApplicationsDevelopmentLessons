using FluentValidation;
using HoS_AP.BLL.Models;
using HoS_AP.BLL.ServiceInterfaces;

namespace HoS_AP.BLL.Validation.Validators
{
    internal class AuthenticationValidator : AbstractValidator<AuthenticationModel>
    {
        public AuthenticationValidator(IValidationMessageProvider validationMessageProvider)
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage(validationMessageProvider.Get(ValidationMessageKeys.Authentication_UserName_Required));
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(validationMessageProvider.Get(ValidationMessageKeys.Authentication_Password_Required));
        }
    }
}