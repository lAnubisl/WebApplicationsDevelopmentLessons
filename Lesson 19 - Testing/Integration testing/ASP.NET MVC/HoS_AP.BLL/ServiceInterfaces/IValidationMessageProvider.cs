using HoS_AP.BLL.Validation;

namespace HoS_AP.BLL.ServiceInterfaces
{
    internal interface IValidationMessageProvider
    {
        string Get(ValidationMessageKeys key);
    }
}