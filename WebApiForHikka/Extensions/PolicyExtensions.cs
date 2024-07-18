using Microsoft.AspNetCore.Authorization;
using WebApiForHikka.Constants.Controllers;
using WebApiForHikka.Constants.Models.Users;

namespace WebApiForHikka.WebApi.Extensions;

public static class PolicyExtensions
{
    public static void AddPolicies(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy(ControllerStringConstants.CanAccessEveryone, policy =>
                policy.RequireRole(UserStringConstants.AdminRole, UserStringConstants.UserRole,
                    UserStringConstants.BannedRole))
            .AddPolicy(ControllerStringConstants.CanAccessUserAndAdmin, policy =>
                policy.RequireRole(UserStringConstants.AdminRole, UserStringConstants.UserRole)
                    .NotInRole(UserStringConstants.BannedRole))
            .AddPolicy(ControllerStringConstants.CanAccessOnlyAdmin, policy =>
                policy.RequireRole(UserStringConstants.AdminRole)
                    .NotInRole(UserStringConstants.BannedRole, UserStringConstants.UserRole));
    }

    public static AuthorizationPolicyBuilder NotInRole(this AuthorizationPolicyBuilder policy, params string[] roles)
    {
        return policy.RequireAssertion(context =>
        {
            var user = context.User;
            return !roles.Any(role => user.IsInRole(role));
        });
    }
}