using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.Users;
using WebApiForHikka.Constants.Models.Users;

namespace WebApiForHikka.Dtos.MyOwnValidationAttribute;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class EmailIsAlreadyExistAttribute : ValidationAttribute
{
    public EmailIsAlreadyExistAttribute()
    {
        ErrorMessage = UserStringConstants.UserEmailAlreadyExistErrorMessage;
    }

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null) return new ValidationResult(ErrorMessage);

        var email = (string)value;
        var userService = validationContext.GetRequiredService<IUserService>();

        return userService.CheckIfUserWithTheEmailIsAlreadyExist(email)
            ? new ValidationResult(ErrorMessage)
            : ValidationResult.Success!;
    }
}