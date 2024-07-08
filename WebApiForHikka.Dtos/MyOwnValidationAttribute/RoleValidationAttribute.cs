using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Models.Users;

namespace WebApiForHikka.Dtos.MyOwnValidationAttribute;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class RoleValidationAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value == null) return false;

        var role = (value as string)!.ToLower();


        if (!UserStringConstants.UsersRolesList.Contains(role)) return false;

        return true;
    }
}