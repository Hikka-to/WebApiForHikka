
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using WebApiForHikka.Application.Users;
using WebApiForHikka.Constants.Models.Users;

namespace WebApiForHikka.Dtos.MyOwnValidationAttribute;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class EmailIsAlreadyExistAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null) return new ValidationResult(ErrorMessage);

        string email = (string)value;
        var userService = validationContext.GetRequiredService<IUserService>();

        if (userService.CheckIfUserWithTheEmailIsAlreadyExist(email))
        {
            return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success!;
    }

    public override string FormatErrorMessage(string name)
    {
        return string.Format(CultureInfo.CurrentCulture,
          UserStringConstants.UserEmailAlreadyExistErrorMessage, name);
    }
}