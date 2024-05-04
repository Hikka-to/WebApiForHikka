
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using WebApiForHikka.Application.Users;
using WebApiForHikka.Constants.Users;

namespace WebApiForHikka.Dtos.MyOwnValidationAttribute;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class EmailIsAlreadyExist : ValidationAttribute
{

   
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        string email = (string)value;
        IUserService? userService = (IUserService)validationContext.GetService(typeof(IUserService));

        if (userService == null) {
            throw new Exception("The user service hasn't been registrated");
        }

        if (userService.CheckIfUserWithTheEmailIsAlreadyExist(email)) 
        {
            return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success;
    }

    public override string FormatErrorMessage(string name)
    {
        return String.Format(CultureInfo.CurrentCulture,
          UserStringConstants.UserIsAlreadyExistErrorMessage, name);
    }
}
