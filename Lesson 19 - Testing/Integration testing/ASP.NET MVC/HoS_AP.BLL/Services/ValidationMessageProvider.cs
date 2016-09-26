using System.Collections.Generic;
using HoS_AP.BLL.ServiceInterfaces;
using HoS_AP.BLL.Validation;

namespace HoS_AP.BLL.Services
{
    internal class ValidationMessageProvider : IValidationMessageProvider
    {
        private static readonly IDictionary<ValidationMessageKeys, string> Map = new Dictionary
            <ValidationMessageKeys, string>
        {
            {ValidationMessageKeys.Authentication_Invalid_Credentials, "Sorry, your credentials are not valid"},
            {ValidationMessageKeys.Authentication_UserName_Required, "Please enter username" },
            {ValidationMessageKeys.Authentication_Password_Required, "Please enter password" },
            {ValidationMessageKeys.CharacterEdit_Name_Required, "Please enter name" },
            {ValidationMessageKeys.CharacterEdit_Name_Must_Be_Unique, "Sorry, character with such name is already registered in the system" },
            {ValidationMessageKeys.CharacterEdit_Name_Special_Characters, "Please use only english letters and space" },
            {ValidationMessageKeys.CharacterEdit_Price_Boundaries, "Please set value between 5 and 25" },
            {ValidationMessageKeys.CharacterEdit_Type_Required, "Please select Type" }
        };

        public string Get(ValidationMessageKeys key)
        {
            return Map[key];
        }
    }
}