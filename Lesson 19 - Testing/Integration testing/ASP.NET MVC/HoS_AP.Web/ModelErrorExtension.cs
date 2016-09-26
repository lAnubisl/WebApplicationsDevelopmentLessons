using System.Web.Mvc;
using HoS_AP.BLL.Models;

namespace HoS_AP.Web
{
    public static class ModelErrorExtension
    {
        public static void ToModelErrors(this ValidationResult validationResult, ModelStateDictionary modelState)
        {
            modelState.Clear();
            foreach (var validationError in validationResult.Errors)
            {
                modelState.AddModelError(validationError.Property, validationError.Message);
            }
        }
    }
}