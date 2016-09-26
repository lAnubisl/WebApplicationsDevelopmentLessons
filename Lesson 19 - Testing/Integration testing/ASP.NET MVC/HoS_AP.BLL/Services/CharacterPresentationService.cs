using System.Collections.Generic;
using System.Linq;
using HoS_AP.BLL.Models;
using HoS_AP.BLL.ServiceInterfaces;
using HoS_AP.DAL.DaoInterfaces;

namespace HoS_AP.BLL.Services
{
    internal class CharacterPresentationService : ICharacterPresentationService
    {
        private readonly ICharacterDao characterDao;

        public CharacterPresentationService(ICharacterDao characterDao)
        {
            this.characterDao = characterDao;
        }

        ICollection<CharacterListItemModel> ICharacterPresentationService.List()
        {
            return characterDao.Load().Select(x => new CharacterListItemModel(x)).ToList();
        }

        CharacterEditModel ICharacterPresentationService.Load(string name)
        {
            var character = characterDao.Load(name);
            if (character == null) return null;
            return new CharacterEditModel(character);
        }
    }
}