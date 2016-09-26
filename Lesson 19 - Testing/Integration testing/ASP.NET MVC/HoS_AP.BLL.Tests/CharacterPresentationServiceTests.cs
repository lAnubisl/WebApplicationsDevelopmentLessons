using System.Collections.Generic;
using System.Linq;
using HoS_AP.BLL.ServiceInterfaces;
using HoS_AP.BLL.Services;
using HoS_AP.DAL.DaoInterfaces;
using HoS_AP.DAL.Dto;
using Moq;
using NUnit.Framework;

namespace HoS_AP.BLL.Tests
{
    [TestFixture]
    public class CharacterPresentationServiceTests
    {
        [Test]
        public void List_should_return_same_items_that_are_in_storage()
        {
            var character = new Character {Name = "Zeratul"};
            var characterDao = new Mock<ICharacterDao>();
            characterDao.Setup(x => x.Load()).Returns(new List<Character> { character });
            ICharacterPresentationService service = new CharacterPresentationService(characterDao.Object);
            var result = service.List();
            Assert.AreEqual(1, result.Count);
            Assert.AreSame(character.Name, result.First().Name);
        }

        [Test]
        public void Load_should_return_null_if_character_not_in_storage()
        {
            var characterDao = new Mock<ICharacterDao>();
            characterDao.Setup(x => x.Load(It.IsAny<string>())).Returns(null as Character);
            ICharacterPresentationService service = new CharacterPresentationService(characterDao.Object);
            var result = service.Load("foo");
            Assert.IsNull(result);
        }

        [Test]
        public void Load_should_return_same_character_that_is_in_storage()
        {
            var character = new Character { Name = "Zeratul" };
            var characterDao = new Mock<ICharacterDao>();
            characterDao.Setup(x => x.Load(It.IsAny<string>())).Returns(character);
            ICharacterPresentationService service = new CharacterPresentationService(characterDao.Object);
            var result = service.Load("Zeratul");
            Assert.IsNotNull(result);
            Assert.AreSame(character.Name, result.Name);
        }
    }
}