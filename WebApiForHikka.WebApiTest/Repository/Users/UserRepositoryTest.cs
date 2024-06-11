using FluentAssertions;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.SharedFunction.HashFunction;
using WebApiForHikka.Test.Shared;


namespace WebApiForHikka.WebApiTest.Repository.Users;

public class UserRepositoryTest : SharedTest
{
    private readonly IHashFunctions _hashFunctions = new HashFunctions();

    [Fact]
    public async Task UserRepository_AuthenticateUserAsync_ReturnsUser()
    {
        // Arrange
        var dbContext = GetDatabaseContext();
        var userManager = GetUserManager(dbContext);
        var userRepository = new UserRepository(dbContext, _hashFunctions, userManager);
        // !!!!!!!!!! Need role fix
        var testUser = new User
        {
            UserName = "test",
            Email = "test@example.com",
            PasswordHash = "Password123!",
        };
        await userRepository.AddAsync(new User
        {
            UserName = testUser.UserName,
            Email = testUser.Email,
            PasswordHash = testUser.PasswordHash,
        }, new CancellationToken());

        // Act
        var result = await userRepository.AuthenticateUserAsync(testUser.Email, testUser.PasswordHash, new CancellationToken());

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
        var userRepository = new UserRepository(dbContext, _hashFunctions, userManager);
        // !!!!!!!!!! Need role fix
        var testUser = new User
        {
            UserName = "test",
            Email = "test@example.com",
            PasswordHash = "Password123!",
        };
        await userRepository.AddAsync(new User
        {
            UserName = testUser.UserName,
            Email = testUser.Email,
            PasswordHash = testUser.PasswordHash,
        }, new CancellationToken());

        // Act
        var result = await userRepository.AuthenticateUserWithAdminRoleAsync(testUser.Email, testUser.PasswordHash, new CancellationToken());

        // Assert
        result.Should().NotBeNull();
        result!.Email.Should().Be(testUser.Email);
        // !!!!!!!!!! Add role check;
    }
    [Fact]
    public async Task UserRepository_AuthenticateUserWithAdminRoleAsync_ReturnsNull()
    {
        // Arrange
        var dbContext = GetDatabaseContext();
        var userManager = GetUserManager(dbContext);
        var userRepository = new UserRepository(dbContext, _hashFunctions, userManager);
        // !!!!!!!!!! Need role fix
        var testUser = new User
        {
            UserName = "test",
            Email = "test@example.com",
            PasswordHash = "Password123!"
        };
        await userRepository.AddAsync(new User
        {
            UserName = testUser.UserName,
            Email = testUser.Email,
            PasswordHash = testUser.PasswordHash,
        }, new CancellationToken());

        // Act
        var result = await userRepository.AuthenticateUserWithAdminRoleAsync(testUser.Email, testUser.PasswordHash, new CancellationToken());

        // Assert
        result.Should().BeNull();
    }


    [Fact]
    public async Task UserRepository_CheckIfUserWithTheEmailIsAlreadyExistAsync_ReturnsTrue()
    {
        // Arrange
        var dbContext = GetDatabaseContext();
        var userManager = GetUserManager(dbContext);
        var userRepository = new UserRepository(dbContext, _hashFunctions, userManager);
        // !!!!!!!!!! Need role fix
        var testUser = new User
        {
            UserName = "test",
            Email = "test@example.com",
            PasswordHash = "Password123!",
        };
        await userRepository.AddAsync(testUser, new CancellationToken());

        // Act
        var result = await userRepository.CheckIfUserWithTheEmailIsAlreadyExistAsync(testUser.Email, new CancellationToken());

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void UserRepository_CheckIfUserWithTheEmailIsAlreadyExist_ReturnsTrue()
    {
        // Arrange
        var dbContext = GetDatabaseContext();
        var userManager = GetUserManager(dbContext);
        var userRepository = new UserRepository(dbContext, _hashFunctions, userManager);
        // !!!!!!!!!! Need role fix
        var testUser = new User
        {
            UserName = "test",
            Email = "test@example.com",
            PasswordHash = "Password123!"
        };
        userRepository.AddAsync(testUser, new CancellationToken()).Wait();

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
        var userRepository = new UserRepository(dbContext, _hashFunctions, userManager);
        // !!!!!!!!!! Need role fix
        var testUser = new User
        {
            UserName = "test",
            Email = "test@example.com",
            PasswordHash = "Password123!",
        };

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
        var userRepository = new UserRepository(dbContext, _hashFunctions, userManager);
        // !!!!!!!!!! Need role fix
        var testUser = new User
        {
            UserName = "test",
            Email = "test@example.com",
            PasswordHash = "Password123!"
        };
        var addedUserId = await userRepository.AddAsync(testUser, new CancellationToken());
        var updatedUser = new User
        {
            Id = addedUserId,
            Email = "updated@example.com",
            PasswordHash = "newpassword",
        };

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
        var userRepository = new UserRepository(dbContext, _hashFunctions, userManager);
        // !!!!!!!!!! Need role fix
        var testUser = new User
        {
            UserName = "test",
            Email = "test@example.com",
            PasswordHash = "Password123!",
        };
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
        var userRepository = new UserRepository(dbContext, _hashFunctions, userManager);
        // !!!!!!!!!! Need role fix
        var testUser = new User
        {
            UserName = "test",
            Email = "test@example.com",
            PasswordHash = "Password123!",
        };
        var addedUserId = await userRepository.AddAsync(testUser, new CancellationToken());

        // Act
        var result = await userRepository.GetAsync(addedUserId, new CancellationToken());

        // Assert
        result.Should().NotBeNull();
        result!.Email.Should().Be(testUser.Email);
    }
}