using Faker;
using Microsoft.AspNetCore.Identity;
using WebApiForHikka.Constants.Models.Users;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.Users;

namespace WebApiForHikka.SharedModels.Models.WithoutSeoAddition;

public class GetUserModels
{
    private static readonly PasswordHasher<User> PasswordHasher = new();
    private static readonly UpperInvariantLookupNormalizer KeyNormalizer = new();

    private static string? GenerateBase32()
    {
        var base32Type = typeof(UserManager<>).Assembly.GetType("Microsoft.AspNetCore.Identity.Base32");
        return base32Type?.GetMethod("GenerateBase32")?.Invoke(null, null)?.ToString();
    }

    public static User GetSample()
    {
        var user = new User
        {
            UserName = "Test12",
            Email = "test12@test.com",
            SecurityStamp = GenerateBase32(),
            NormalizedEmail = KeyNormalizer.NormalizeEmail("test12@test.com"),
            NormalizedUserName = KeyNormalizer.NormalizeName("Test12")
        };
        user.PasswordHash = PasswordHasher.HashPassword(user, "Test1234");
        return user;
    }

    public static User GetSampleForUpdate()
    {
        var user = new User
        {
            UserName = "Test24",
            Email = "test24@test.com",
            SecurityStamp = GenerateBase32(),
            NormalizedEmail = KeyNormalizer.NormalizeEmail("test24@test.com"),
            NormalizedUserName = KeyNormalizer.NormalizeName("Test24")
        };
        user.PasswordHash = PasswordHasher.HashPassword(user, "Test12345");
        return user;
    }

    public static GetUserDto GetGetDtoSample()
    {
        return new GetUserDto
        {
            Email = Lorem.GetFirstWord(),
            Roles = [UserStringConstants.AdminRole],
            Id = Guid.NewGuid()
        };
    }
}