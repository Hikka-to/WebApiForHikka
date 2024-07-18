using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Models.Users;

namespace WebApiForHikka.Dtos.MyOwnValidationAttribute;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class RoleValidationAttribute : ValidationAttribute
{
    public RoleValidationAttribute()
    {
        ErrorMessage = UserStringConstants.UserRoleIsNotValidErrorMessage;
    }

    public override bool IsValid(object? value)
    {
        if (value == null) return false;

        var role = (value as string)!.ToLower();

        return UserStringConstants.UsersRolesList.Contains(role);
    }
}