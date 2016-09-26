using System;
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
    public class CharacterOperationServiceTests
    {
        [Test]
        public void Delete_should_set_character_deleted_to_true()
        {
            var character = new Character { Name = "Zeratul", Deleted = false };
            var characterDao = new Mock<ICharacterDao>();
            characterDao.Setup(x => x.Load(It.IsAny<string>())).Returns(character);

            ICharacterOperationService operationService = new CharacterOperationService(null, characterDao.Object);
            operationService.Delete("Zeratul");

            characterDao.Verify(x => x.Save(It.Is<Character>(c => c.Deleted && ReferenceEquals(character.Name, c.Name))), Times.Once);
        }

        [Test]
        public void Delete_should_do_nothing_if_user_not_found()
        {
            var characterDao = new Mock<ICharacterDao>();
            characterDao.Setup(x => x.Load(It.IsAny<string>())).Returns(null as Character);

            ICharacterOperationService operationService = new CharacterOperationService(null, characterDao.Object);
            operationService.Delete("Zeratul");

            characterDao.Verify(x => x.Save(It.IsAny<Character>()), Times.Never());
        }

        [Test]
        public void Recover_should_set_character_deleted_to_false()
        {
            var character = new Character { Name = "Zeratul", Deleted = true };
            var characterDao = new Mock<ICharacterDao>();
            characterDao.Setup(x => x.Load(It.IsAny<string>())).Returns(character);

            ICharacterOperationService operationService = new CharacterOperationService(null, characterDao.Object);
            operationService.Recover("Zeratul");

            characterDao.Verify(x => x.Save(It.Is<Character>(c => !c.Deleted && ReferenceEquals(character.Name, c.Name))), Times.Once);
        }

        [Test]
        public void Recover_should_do_nothing_if_user_not_found()
        {
            var characterDao = new Mock<ICharacterDao>();
            characterDao.Setup(x => x.Load(It.IsAny<string>())).Returns(null as Character);

            ICharacterOperationService operationService = new CharacterOperationService(null, characterDao.Object);
            operationService.Recover("Zeratul");

            characterDao.Verify(x => x.Save(It.IsAny<Character>()), Times.Never());
        }

        [Test]
        public void Save_should_return_validation_errors()
        {
            var error = new ValidationError("Property", "Message");
            var validationService = new Mock<IValidationService>();
            validationService.Setup(x => x.Validate(It.IsAny<CharacterEditModel>())).Returns(new List<ValidationError>() { error });

            ICharacterOperationService operationService = new CharacterOperationService(validationService.Object, null);
            var result = operationService.Save(new CharacterEditModel());
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(1, result.Errors.Count);
            Assert.AreSame(error, result.Errors.First());
        }

        [Test]
        public void Save_should_create_new_character()
        {
            var validationService = new Mock<IValidationService>();
            validationService.Setup(x => x.Validate(It.IsAny<CharacterEditModel>())).Returns(new List<ValidationError>());

            var characterDao = new Mock<ICharacterDao>();

            ICharacterOperationService operationService = new CharacterOperationService(validationService.Object, characterDao.Object);
            var model = new CharacterEditModel {Name = "Zeratul"};
            var result = operationService.Save(model);
            Assert.IsTrue(result.IsValid);
            characterDao.Verify(x => x.Load(It.IsAny<Guid>()), Times.Never);
            characterDao.Verify(x => x.Save(It.Is<Character>(c => ReferenceEquals(c.Name, model.Name))), Times.Once);
        }

        [Test]
        public void Save_should_update_character()
        {
            var validationService = new Mock<IValidationService>();
            validationService.Setup(x => x.Validate(It.IsAny<CharacterEditModel>())).Returns(new List<ValidationError>());

            var character = new Character();
            var characterDao = new Mock<ICharacterDao>();
            characterDao.Setup(x => x.Load(It.IsAny<Guid>())).Returns(character);

            ICharacterOperationService operationService = new CharacterOperationService(validationService.Object, characterDao.Object);
            var model = new CharacterEditModel { Id = Guid.NewGuid() };
            var result = operationService.Save(model);
            Assert.IsTrue(result.IsValid);
            characterDao.Verify(x => x.Load(model.Id), Times.Once);
            characterDao.Verify(x => x.Save(It.Is<Character>(c => ReferenceEquals(c, character) && ReferenceEquals(c.Name, model.Name))), Times.Once);
        }

        [Test]
        public void Save_should_throw_exception()
        {
            var validationService = new Mock<IValidationService>();
            validationService.Setup(x => x.Validate(It.IsAny<CharacterEditModel>())).Returns(new List<ValidationError>());

            var characterDao = new Mock<ICharacterDao>();
            characterDao.Setup(x => x.Load(It.IsAny<Guid>())).Returns(null as Character);

            ICharacterOperationService operationService = new CharacterOperationService(validationService.Object, characterDao.Object);
            Assert.Throws<InvalidOperationException>(() => operationService.Save(new CharacterEditModel {Id = Guid.NewGuid()}));
        }
    }
}