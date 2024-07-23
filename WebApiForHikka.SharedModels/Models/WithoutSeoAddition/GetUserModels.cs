using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.Users;
using WebApiForHikka.SharedModels.MyDataFakers;
using System;
using System.Collections.Generic;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.SharedModels.Models.WithoutSeoAddition;

public class GetUserModels
{
    public static User GetSample()
    {
        return new User
        {
            UserSetting = GetUserSettingModels.GetSample(),
            Name = "JohnDoe",
            AvatarPath = "/avatars/johndoe.jpg",
            BackdropPath = "/backdrops/johndoe_backdrop.jpg",
            TelegramId = 123456789,
            Description = "Software developer and tech enthusiast",
            TelegramUrl = "https://t.me/johndoe",
            StatusIcon = "🚀",
            StatusText = "Coding away!",
            AllowAdult = true,
            LastSeenAt = DateTime.Now.AddHours(-2),
            UpdatedAt = DateTime.Now.AddDays(-1),
            CreatedAtTime = DateTime.Now.AddMonths(-3),
            UserName = "john_doe",
            Email = "john.doe@example.com",
            PasswordHash = "hashed_password_123"
        };
    }

    public static User GetSampleForUpdate()
    {
        return new User
        {
            UserSetting = GetUserSettingModels.GetSample(),
            Name = "JaneSmith",
            AvatarPath = "/avatars/janesmith.png",
            BackdropPath = "/backdrops/janesmith_backdrop.png",
            TelegramId = 987654321,
            Description = "UX designer and coffee lover",
            TelegramUrl = "https://t.me/janesmith",
            StatusIcon = "☕",
            StatusText = "Designing the future",
            AllowAdult = false,
            LastSeenAt = DateTime.Now.AddMinutes(-30),
            UpdatedAt = DateTime.Now.AddHours(-5),
            CreatedAtTime = DateTime.Now.AddYears(-1),
            UserName = "jane_smith",
            Email = "jane.smith@example.com",
            PasswordHash = "hashed_password_456"
        };
    }

    public static UserRegistrationDto GetCreateSampleDto()
    {
        return new UserRegistrationDto()
        {
            Email = "alice.johnson@example.com",
            UserName = "alice_j",
            Password = "securePassword123!",
            Role = "User",
        };
    }

    public static GetUserDto GetGetDtoSample()
    {
        return new GetUserDto
        {
            UserSetting = GetUserSettingModels.GetSample(),
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
            CreatedAtTime = DateTime.Now.AddMonths(-6),
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
            CreatedAtTime = DateTime.Now.AddMonths(-9),
        };
    }
}