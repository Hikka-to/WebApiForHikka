using Faker;
using Microsoft.AspNetCore.Identity;
using WebApiForHikka.Constants.Models.Users;
using WebApiForHikka.Constants.Models.WithoutSeoAddition.RelatedType;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.Users;
using WebApiForHikka.SharedModels.MyDataFakers;

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
            UserSetting = GetUserSettingModels.GetSample(),
            AllowAdult = false,
            Name = "JohnDoe",
            AvatarPath = "/avatars/johndoe.jpg",
            BackdropPath = "/backdrops/johndoe_backdrop.jpg",
            TelegramId = 123456789,
            Description = "Software developer and tech enthusiast",
            TelegramUrl = "https://t.me/johndoe",
            StatusIcon = "🚀",
            StatusText = "Coding away!",
            LastSeenAt = DateTime.Now.AddHours(-2),
            UpdatedAt = DateTime.Now.AddDays(-1),
            CreatedAtTime = DateTime.Now.AddMonths(-3),
            UserName = "john_doe",
            Email = "john.doe@example.com",
            SecurityStamp = GenerateBase32(),
            NormalizedEmail = KeyNormalizer.NormalizeEmail("john.doe@example.com"),
            NormalizedUserName = KeyNormalizer.NormalizeName("john_doe")
        };
        user.PasswordHash = PasswordHasher.HashPassword(user, "Test1234");
        return user;
    }

    public static User GetSampleForUpdate()
    {
        var user = new User
        {
            UserSetting = GetUserSettingModels.GetSample(),
            Name = "JaneSmith",
            AvatarPath = "/avatars/janesmith.png",
            BackdropPath = "/backdrops/janesmith_backdrop.png",
            AllowAdult = true,
            TelegramId = 987654321,
            Description = "UX designer and coffee lover",
            TelegramUrl = "https://t.me/janesmith",
            StatusIcon = "☕",
            StatusText = "Designing the future",
            LastSeenAt = DateTime.Now.AddMinutes(-30),
            UpdatedAt = DateTime.Now.AddHours(-5),
            CreatedAtTime = DateTime.Now.AddYears(-1),
            UserName = "jane_smith",
            Email = "jane.smith@example.com",
            SecurityStamp = GenerateBase32(),
            NormalizedEmail = KeyNormalizer.NormalizeEmail("jane.smith@example.com"),
            NormalizedUserName = KeyNormalizer.NormalizeName("jane_smith")
        };
        user.PasswordHash = PasswordHasher.HashPassword(user, "Test12345");
        return user;
    }

    public static UserRegistrationDto GetCreateSampleDto()
    {
        return new UserRegistrationDto
        {
            Email = "alice.johnson@example.com",
            UserName = "alice_j",
            Password = "securePassword123!",
            Role = "User"
        };
    }

    public static GetUserDto GetGetDtoSample()
    {
        return new GetUserDto
        {
            UserSetting = GetUserSettingModels.GetGetDtoSample(),
            Email = "bob.wilson@example.com",
            Roles = [],
            Name = "Bob Wilson",
            AvatarUrl = "https://example.com/avatars/bobwilson.jpg",
            BackdropUrl = "https://example.com/backdrops/bobwilson_backdrop.jpg",
            Description = "Passionate about photography and travel",
            StatusText = "Exploring the world 🌍",
            AllowAdult = true,
            LastSeenAt = DateTime.Now.AddDays(-1),
            UpdatedAt = DateTime.Now,
            CreatedAtTime = DateTime.Now.AddMonths(-6)
        };
    }

    public static UpdateUserDto GetUpdateDtoSample()
    {
        return new UpdateUserDto
        {
            UserSetting = GetUserSettingModels.GetUpdateDtoSample(),
            Id = Guid.NewGuid(),
            Email = "emma.davis@example.com",
            Role = "Admin",
            Name = "Emma Davis",
            AvatarImage = MyDataFaker.GetFakeImage(),
            BackdropImage = MyDataFaker.GetFakeImage(),
            StatusText = "Leading the team to success 💪",
            Description = "Tech lead with a passion for AI and machine learning",
            AllowAdult = true,
            LastSeenAt = DateTime.Now.AddHours(-1),
            UpdatedAt = DateTime.Now.AddDays(-2),
            CreatedAtTime = DateTime.Now.AddMonths(-9)
        };
    }

    public static UserRegistrationDto GetUserRegistrationDtoForAdminSample()
    {
        return new UserRegistrationDto()
        {
            Email = "user@gmail.com",
            Password = "test123",
            Role = UserStringConstants.AdminRole,
            UserName = Lorem.GetFirstWord(),
        };
    }
    public static UserRegistrationDto GetUserRegistrationDtoForUserSample()
    {
        return new UserRegistrationDto()
        {
            Email = "user@gmail.com",
            Password = "test123",
            Role = UserStringConstants.UserRole,
            UserName = Lorem.GetFirstWord(),
        };
    }


}