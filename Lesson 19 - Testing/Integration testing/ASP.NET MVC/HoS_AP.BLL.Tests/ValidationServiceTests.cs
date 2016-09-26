using System;
using System.Linq;
using HoS_AP.BLL.Models;
using HoS_AP.BLL.Services;
using HoS_AP.BLL.Validation;
using HoS_AP.DAL.DaoInterfaces;
using HoS_AP.DAL.Dto;
using Moq;
using NUnit.Framework;

namespace HoS_AP.BLL.Tests
{
    [TestFixture]
    public class ValidationServiceTests
    {
        [Test]
        public void Validate_should_validate_authentication_model_required_fields()
        {
            var validationMessageProvider = new ValidationMessageProvider();
            IValidationService service = new ValidationService(null, validationMessageProvider);
            var errors = service.Validate(new AuthenticationModel());
            Assert.AreEqual(2, errors.Count);
            Assert.AreSame(validationMessageProvider.Get(ValidationMessageKeys.Authentication_UserName_Required),
                errors.First().Message);
            Assert.AreSame(validationMessageProvider.Get(ValidationMessageKeys.Authentication_Password_Required),
                errors.Last().Message);
        }

        [Test]
        public void Validate_should_validate_character_edit_model_required_fields()
        {
            var characterDao = new Mock<ICharacterDao>();
            var validationMessageProvider = new ValidationMessageProvider();
            IValidationService service = new ValidationService(characterDao.Object, validationMessageProvider);
            var errors = service.Validate(new CharacterEditModel());
            Assert.That(errors.Any(e => e.Property == "Name" && ReferenceEquals(e.Message, validationMessageProvider.Get(ValidationMessageKeys.CharacterEdit_Name_Required))));
            Assert.That(errors.Any(e => e.Property == "Type" && ReferenceEquals(e.Message, validationMessageProvider.Get(ValidationMessageKeys.CharacterEdit_Type_Required))));
            Assert.That(errors.Any(e => e.Property == "Price" && ReferenceEquals(e.Message, validationMessageProvider.Get(ValidationMessageKeys.CharacterEdit_Price_Boundaries))));
        }

        [Test]
        public void Validate_should_validate_character_edit_model_name_characters()
        {
            var characterDao = new Mock<ICharacterDao>();
            var validationMessageProvider = new ValidationMessageProvider();
            IValidationService service = new ValidationService(characterDao.Object, validationMessageProvider);
            var errors = service.Validate(new CharacterEditModel() {Name = "123!@#"});
            Assert.That(errors.Any(e => e.Property == "Name" && ReferenceEquals(e.Message, validationMessageProvider.Get(ValidationMessageKeys.CharacterEdit_Name_Special_Characters))));
        }

        [Test]
        public void Validate_should_validate_character_edit_model_name_unique()
        {
            var characterDao = new Mock<ICharacterDao>();
            characterDao.Setup(x => x.Load(It.IsAny<string>())).Returns(new Character() {Id = Guid.NewGuid()});
            var validationMessageProvider = new ValidationMessageProvider();
            IValidationService service = new ValidationService(characterDao.Object, validationMessageProvider);
            var errors = service.Validate(new CharacterEditModel() {Name = "Zeratul"});
            Assert.That(errors.Any(e => e.Property == "Name" && ReferenceEquals(e.Message, validationMessageProvider.Get(ValidationMessageKeys.CharacterEdit_Name_Must_Be_Unique))));
        }
    }
}