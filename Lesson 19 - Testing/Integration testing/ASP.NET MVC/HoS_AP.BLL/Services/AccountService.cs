using System.Linq;
using HoS_AP.BLL.Models;
using HoS_AP.BLL.ServiceInterfaces;
using HoS_AP.BLL.Validation;
using HoS_AP.DAL.DaoInterfaces;

namespace HoS_AP.BLL.Services
{
    internal class AccountService : IAccountService
    {
        private readonly IValidationService validationService;
        private readonly IAccountDao accountDao;
        private readonly IEncryptionService encryptionService;
        private readonly IValidationMessageProvider validationMessageProvider;

        public AccountService(
            IValidationService validationService, 
            IAccountDao accountDao, 
            IEncryptionService encryptionService, 
            IValidationMessageProvider validationMessageProvider)
        {
            this.validationService = validationService;
            this.accountDao = accountDao;
            this.encryptionService = encryptionService;
            this.validationMessageProvider = validationMessageProvider;
        }

        ValidationResult IAccountService.Authenticate(AuthenticationModel model)
        {
            var validaitonErrors = validationService.Validate(model);
            if (validaitonErrors.Any())
            {
                return new ValidationResult(validaitonErrors);
            }

            var account = accountDao.Load(model.UserName);
            if (account == null)
            {
                return new ValidationResult("UserName", validationMessageProvider.Get(ValidationMessageKeys.Authentication_Invalid_Credentials));
            }

            if (!encryptionService.IsValidPassword(model.Password, account.Password))
            {
                return new ValidationResult("UserName", validationMessageProvider.Get(ValidationMessageKeys.Authentication_Invalid_Credentials));
            }

            return ValidationResult.Ok;
        }
    }
}

