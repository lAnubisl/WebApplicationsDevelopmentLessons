using System.Collections.Generic;
using HoS_AP.BLL.Models;

namespace HoS_AP.BLL.ServiceInterfaces
{
    public interface ICharacterPresentationService
    {
        ICollection<CharacterListItemModel> List();
        CharacterEditModel Load(string name);
    }
}