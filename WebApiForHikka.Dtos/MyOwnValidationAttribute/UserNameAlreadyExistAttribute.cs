using System.ComponentModel.DataAnnotations;
using System.Globalization;
using WebApiForHikka.Application.Users;
using WebApiForHikka.Constants.Models.Users;

namespace WebApiForHikka.Dtos.MyOwnValidationAttribute
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class UserNameAlreadyExistAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return new ValidationResult(ErrorMessage);

            string username = (string)value;
            IUserService? userService = validationContext.GetService(typeof(IUserService)) as IUserService
                ?? throw new Exception("The user service hasn't been registrated");

            if (userService.CheckIfUserWithTheUserNameIsAlreadyExist(username))
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success!;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture,
                             UserStringConstants.UserNameAlreadyExistErrorMessage, name);
        }
    }
}
