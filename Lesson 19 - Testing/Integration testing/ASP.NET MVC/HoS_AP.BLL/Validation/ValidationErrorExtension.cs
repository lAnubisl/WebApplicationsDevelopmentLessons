using System.Collections.Generic;
using System.Collections.ObjectModel;
using FluentValidation.Results;

namespace HoS_AP.BLL.Validation
{
    internal static class ValidationErrorExtensions
    {
        public static ICollection<ValidationError> ToValidationResultItem(this IEnumerable<ValidationFailure> failures)
        {
            var result = new Collection<ValidationError>();
            if (failures == null)
            {
                return result;
            }
  
            foreach (var failure in failures)
            {
                result.Add(new ValidationError(failure.PropertyName, failure.ErrorMessage));
            }

            return result;
        }
    }
}