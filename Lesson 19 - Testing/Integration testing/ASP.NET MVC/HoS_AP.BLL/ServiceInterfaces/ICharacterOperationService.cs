using HoS_AP.BLL.Models;

namespace HoS_AP.BLL.ServiceInterfaces
{
    public interface ICharacterOperationService
    {
        ValidationResult Save(CharacterEditModel model);
        void Delete(string name);
        void Recover(string name);
    }
}