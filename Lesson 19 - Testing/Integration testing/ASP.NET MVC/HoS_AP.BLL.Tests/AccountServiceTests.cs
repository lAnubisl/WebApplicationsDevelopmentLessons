using System.Collections.Generic;
using System.Linq;
using HoS_AP.BLL.Models;
using HoS_AP.BLL.ServiceInterfaces;
using HoS_AP.BLL.Services;
using HoS_AP.BLL.Validation;
using HoS_AP.DAL.DaoInterfaces;
using HoS_AP.DAL.Dto;
using Moq;
using NUnit.Framework;

namespace HoS_AP.BLL.Tests
{
    [TestFixture]
    public class AccountServiceTests
    {
        [Test]
        public void Authenticate_should_return_fail_when_user_not_found()
        {
            var validationService = new Mock<IValidationService>();
            validationService
                .Setup(x => x.Validate(It.IsAny<AuthenticationModel>()))
                .Returns(new List<ValidationError>());

            var accountDao = new Mock<IAccountDao>();
            accountDao.Setup(x => x.Load(It.IsAny<string>())).Returns(null as Account);

            var validationMessageProvider = new Mock<IValidationMessageProvider>();
            validationMessageProvider.Setup(x => x.Get(It.IsAny<ValidationMessageKeys>())).Returns("Fail");

            IAccountService service = new AccountService(validationService.Object, accountDao.Object, null, validationMessageProvider.Object);
            var result = service.Authenticate(new AuthenticationModel());
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(1, result.Errors.Count);
            Assert.AreEqual("UserName", result.Errors.First().Property);
            Assert.AreEqual("Fail", result.Errors.First().Message);
        }

        [Test]
        public void Authenticate_should_return_fail_when_model_not_valid()
        {
            var errors = new List<ValidationError> { new ValidationError("Property", "Message")};
            var validationService = new Mock<IValidationService>();
            validationService
                .Setup(x => x.Validate(It.IsAny<AuthenticationModel>()))
                .Returns(errors);

            IAccountService service = new AccountService(validationService.Object, null, null, null);
            var result = service.Authenticate(new AuthenticationModel());
            Assert.IsFalse(result.IsValid);
            Assert.AreSame(errors, result.Errors);
        }

        [Test]
        public void Authenticate_should_return_fail_when_password_not_correct()
        {
            var validationService = new Mock<IValidationService>();
            validationService
                .Setup(x => x.Validate(It.IsAny<AuthenticationModel>()))
                .Returns(new List<ValidationError>());

            var accountDao = new Mock<IAccountDao>();
            accountDao.Setup(x => x.Load(It.IsAny<string>())).Returns(new Account());

            var validationMessageProvider = new Mock<IValidationMessageProvider>();
            validationMessageProvider.Setup(x => x.Get(It.IsAny<ValidationMessageKeys>())).Returns("Fail");

            var encryptionService = new Mock<IEncryptionService>();
            encryptionService.Setup(x => x.IsValidPassword(It.IsAny<string>(), It.IsAny<string>())).Returns(false);

            IAccountService service = new AccountService(validationService.Object, accountDao.Object, encryptionService.Object, validationMessageProvider.Object);
            var result = service.Authenticate(new AuthenticationModel());
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(1, result.Errors.Count);
            Assert.AreEqual("UserName", result.Errors.First().Property);
            Assert.AreEqual("Fail", result.Errors.First().Message);
        }

        [Test]
        public void Authenticate_should_return_success_when_password_is_correct()
        {
            var validationService = new Mock<IValidationService>();
            validationService
                .Setup(x => x.Validate(It.IsAny<AuthenticationModel>()))
                .Returns(new List<ValidationError>());

            var accountDao = new Mock<IAccountDao>();
            accountDao.Setup(x => x.Load(It.IsAny<string>())).Returns(new Account());

            var encryptionService = new Mock<IEncryptionService>();
            encryptionService.Setup(x => x.IsValidPassword(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            IAccountService service = new AccountService(validationService.Object, accountDao.Object, encryptionService.Object, null);
            var result = service.Authenticate(new AuthenticationModel());
            Assert.IsTrue(result.IsValid);
        }
    }
}