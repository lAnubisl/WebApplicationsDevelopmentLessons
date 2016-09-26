using HoS_AP.BLL.Models;

namespace HoS_AP.BLL.ServiceInterfaces
{
    public interface IAccountService
    {
        ValidationResult Authenticate(AuthenticationModel model);
    }
}