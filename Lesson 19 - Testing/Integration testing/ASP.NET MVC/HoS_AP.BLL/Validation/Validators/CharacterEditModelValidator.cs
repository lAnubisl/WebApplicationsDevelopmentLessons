using FluentValidation;
using HoS_AP.BLL.Models;
using HoS_AP.BLL.ServiceInterfaces;
using HoS_AP.DAL.DaoInterfaces;

namespace HoS_AP.BLL.Validation.Validators
{
    internal class CharacterEditModelValidator : AbstractValidator<CharacterEditModel>
    {
        private readonly ICharacterDao characterDao;

        public CharacterEditModelValidator(ICharacterDao characterDao, IValidationMessageProvider validationMessageProvider)
        {
            this.characterDao = characterDao;
            RuleFor(x => x.Name)
                .NotEmpty()
                    .WithMessage(validationMessageProvider.Get(ValidationMessageKeys.CharacterEdit_Name_Required))
                .Matches("^[a-zA-Z ]+$")
                    .WithMessage(validationMessageProvider.Get(ValidationMessageKeys.CharacterEdit_Name_Special_Characters))
                .Must((model, name) => BeUnique(model))
                    .WithMessage(validationMessageProvider.Get(ValidationMessageKeys.CharacterEdit_Name_Must_Be_Unique));
            RuleFor(x => x.Price)   
                .InclusiveBetween(5, 25)
                    .WithMessage(validationMessageProvider.Get(ValidationMessageKeys.CharacterEdit_Price_Boundaries));
            RuleFor(x => x.Type)
                .NotEmpty()
                    .WithMessage(validationMessageProvider.Get(ValidationMessageKeys.CharacterEdit_Type_Required));
        }

        private bool BeUnique(CharacterEditModel model)
        {
            var character = characterDao.Load(model.Name);
            return character == null || character.Id == model.Id;
        }
    }
}