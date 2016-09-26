using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HoS_AP.BLL.Validation;

namespace HoS_AP.BLL.Models
{
    public class ValidationResult
    {
        internal ValidationResult(string property, string message)
        {
            Errors = new Collection<ValidationError> { new ValidationError(property, message)};
        }

        internal ValidationResult(ICollection<ValidationError> errors)
        {
            Errors = errors;
        }

        public ICollection<ValidationError> Errors { get; private set; }

        public bool IsValid
        {
            get { return Errors == null || !Errors.Any(); }
        }

        public static ValidationResult Ok
        {
            get
            {
                return new ValidationResult(null);
            }
        }
    }
}