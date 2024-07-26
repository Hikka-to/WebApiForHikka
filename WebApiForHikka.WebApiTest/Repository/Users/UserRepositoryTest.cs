using FluentAssertions;
using WebApiForHikka.Constants.Models.Users;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Shared;

namespace WebApiForHikka.Test.Repository.Users;

public class UserRepositoryTest : SharedTest
{
    [Fact]
    public async Task UserRepository_AuthenticateUserAsync_ReturnsUser()
    {
        // Arrange
        var dbContext = GetDatabaseContext();
        var userManager = GetUserManager(dbContext);
        var userRepository = new UserRepository(dbContext, userManager);
        var roleManager = GetRoleManager(dbContext);
        var role = await roleManager.FindByNameAsync(UserStringConstants.UserRole);
        var testUser = GetUserModels.GetSample();


        await userRepository.AddAsync(new User
        {
            UserSetting = testUser.UserSetting,
            UserName = testUser.UserName,
            Email = testUser.Email,
            PasswordHash = testUser.PasswordHash,
            Roles = [role!],
            StatusIcon = testUser.StatusIcon,
            AllowAdult = testUser.AllowAdult,
            LastSeenAt = testUser.LastSeenAt,
            UpdatedAt = testUser.UpdatedAt,
            CreatedAt = testUser.CreatedAt
        }, new CancellationToken());

        // Act
        var result =
            await userRepository.AuthenticateUserAsync(testUser.Email, testUser.PasswordHash!, new CancellationToken());

        // Assert
        result.Should().NotBeNull();
        result!.Email.Should().Be(testUser.Email);
    }

    [Fact]
    public async Task UserRepository_AuthenticateUserWithAdminRoleAsync_ReturnsUser()
    {
        // Arrange
        var dbContext = GetDatabaseContext();
        var userManager = GetUserManager(dbContext);
        var userRepository = new UserRepository(dbContext, userManager);
        var roleManager = GetRoleManager(dbContext);
        var role = await roleManager.FindByNameAsync(UserStringConstants.AdminRole);
        var testUser = GetUserModels.GetSample();


        await userRepository.AddAsync(new User
        {
            UserSetting = testUser.UserSetting,
            UserName = testUser.UserName,
            Email = testUser.Email,
            PasswordHash = testUser.PasswordHash,
            Roles = [role!],
            StatusIcon = testUser.StatusIcon,
            AllowAdult = testUser.AllowAdult,
            LastSeenAt = testUser.LastSeenAt,
            UpdatedAt = testUser.UpdatedAt,
            CreatedAt = testUser.CreatedAt
        }, new CancellationToken());

        // Act
        var result =
            await userRepository.AuthenticateUserWithAdminRoleAsync(testUser.Email, testUser.PasswordHash!,
                new CancellationToken());

        // Assert
        result.Should().NotBeNull();
        result!.Email.Should().Be(testUser.Email);
        result.Roles.Should().Contain(role!);
    }

    [Fact]
    public async Task UserRepository_AuthenticateUserWithAdminRoleAsync_ReturnsNull()
    {
        // Arrange
        var dbContext = GetDatabaseContext();
        var userManager = GetUserManager(dbContext);
        var userRepository = new UserRepository(dbContext, userManager);
        var roleManager = GetRoleManager(dbContext);
        var role = await roleManager.FindByNameAsync(UserStringConstants.UserRole);
        var testUser = GetUserModels.GetSample();


        await userRepository.AddAsync(new User
        {
            UserSetting = testUser.UserSetting,
            UserName = testUser.UserName,
            Email = testUser.Email,
            PasswordHash = testUser.PasswordHash,
            Roles = [role!],
            StatusIcon = testUser.StatusIcon,
            AllowAdult = testUser.AllowAdult,
            LastSeenAt = testUser.LastSeenAt,
            UpdatedAt = testUser.UpdatedAt,
            CreatedAt = testUser.CreatedAt
        }, new CancellationToken());

        // Act
        var result =
            await userRepository.AuthenticateUserWithAdminRoleAsync(testUser.Email, testUser.PasswordHash!,
                new CancellationToken());

        // Assert
        result.Should().BeNull();
    }


    [Fact]
    public async Task UserRepository_CheckIfUserWithTheEmailIsAlreadyExistAsync_ReturnsTrue()
    {
        // Arrange
        var dbContext = GetDatabaseContext();
        var userManager = GetUserManager(dbContext);
        var userRepository = new UserRepository(dbContext, userManager);
        var testUser = GetUserModels.GetSample();

        await userRepository.AddAsync(testUser, new CancellationToken());

        // Act
        var result =
            await userRepository.CheckIfUserWithTheEmailIsAlreadyExistAsync(testUser.Email, new CancellationToken());

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void UserRepository_CheckIfUserWithTheEmailIsAlreadyExist_ReturnsTrue()
    {
        // Arrange
        var dbContext = GetDatabaseContext();
        var userManager = GetUserManager(dbContext);
        var userRepository = new UserRepository(dbContext, userManager);
        var testUser = GetUserModels.GetSample();

#pragma warning disable xUnit1031
        userRepository.AddAsync(testUser, new CancellationToken()).Wait();
#pragma warning restore xUnit1031

        // Act
        var result = userRepository.CheckIfUserWithTheEmailIsAlreadyExist(testUser.Email);

        // Assert
        result.Should().BeTrue();
    }


    [Fact]
    public async Task UserRepository_AddAsync_AddsUserAndReturnsId()
    {
        // Arrange
        var dbContext = GetDatabaseContext();
        var userManager = GetUserManager(dbContext);
        var userRepository = new UserRepository(dbContext, userManager);
        var testUser = GetUserModels.GetSample();


        // Act
        var result = await userRepository.AddAsync(testUser, new CancellationToken());

        // Assert
        result.Should().NotBeEmpty();
        var addedUser = await userRepository.GetAsync(result, new CancellationToken());
        addedUser.Should().NotBeNull();
        addedUser!.Email.Should().Be(testUser.Email);
    }

    [Fact]
    public async Task UserRepository_UpdateAsync_UpdatesUserDetails()
    {
        // Arrange
        var dbContext = GetDatabaseContext();
        var userManager = GetUserManager(dbContext);
        var userRepository = new UserRepository(dbContext, userManager);
        var testUser = GetUserModels.GetSample();

        var addedUserId = await userRepository.AddAsync(testUser, new CancellationToken());
        var updatedUser = GetUserModels.GetSample();


        // Act
        await userRepository.UpdateAsync(updatedUser, new CancellationToken());

        // Assert
        var updatedUserResult = await userRepository.GetAsync(addedUserId, new CancellationToken());
        updatedUserResult.Should().NotBeNull();
        updatedUserResult!.Email.Should().Be(updatedUser.Email);
    }

    [Fact]
    public async Task UserRepository_DeleteAsync_DeletesUser()
    {
        // Arrange
        var dbContext = GetDatabaseContext();
        var userManager = GetUserManager(dbContext);
        var userRepository = new UserRepository(dbContext, userManager);
        var testUser = GetUserModels.GetSample();

        var addedUserId = await userRepository.AddAsync(testUser, new CancellationToken());

        // Act
        await userRepository.DeleteAsync(addedUserId, new CancellationToken());

        // Assert
        var deletedUser = await userRepository.GetAsync(addedUserId, new CancellationToken());
        deletedUser.Should().BeNull();
    }

    [Fact]
    public async Task UserRepository_GetAsync_ReturnsUserById()
    {
        // Arrange
        var dbContext = GetDatabaseContext();
        var userManager = GetUserManager(dbContext);
        var userRepository = new UserRepository(dbContext, userManager);
        var testUser = GetUserModels.GetSample();

        var addedUserId = await userRepository.AddAsync(testUser, new CancellationToken());

        // Act
        var result = await userRepository.GetAsync(addedUserId, new CancellationToken());

        // Assert
        result.Should().NotBeNull();
        result!.Email.Should().Be(testUser.Email);
    }
}