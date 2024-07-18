using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.Users;
using WebApiForHikka.Constants.Models.Users;

namespace WebApiForHikka.Dtos.MyOwnValidationAttribute;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class UserNameAlreadyExistAttribute : ValidationAttribute
{
    public UserNameAlreadyExistAttribute()
    {
        ErrorMessage = UserStringConstants.UserNameAlreadyExistErrorMessage;
    }

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null) return new ValidationResult(ErrorMessage);

        var username = (string)value;
        var userService = validationContext.GetRequiredService<IUserService>();

        return userService.CheckIfUserWithTheUserNameIsAlreadyExist(username)
            ? new ValidationResult(ErrorMessage)
            : ValidationResult.Success!;
    }
}