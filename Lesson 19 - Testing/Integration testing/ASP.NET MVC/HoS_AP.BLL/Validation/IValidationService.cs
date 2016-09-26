using System.Collections.Generic;
using HoS_AP.BLL.Models;

namespace HoS_AP.BLL.Validation
{
    internal interface IValidationService
    {
        ICollection<ValidationError> Validate(AuthenticationModel model);

        ICollection<ValidationError> Validate(CharacterEditModel model);
    }
}