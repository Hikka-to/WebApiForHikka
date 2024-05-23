using FluentAssertions;
using WebApiForHikka.Constants.Models.Users;
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
        var dbContext =  GetDatabaseContext();
        var userRepository = new UserRepository(dbContext, _hashFunctions);
        var testUser = new User
        {
            Email = "test@example.com",
            Password = "password",
            Role = UserStringConstants.UserRole
        };
        await userRepository.AddAsync(new User
        {
            Email = testUser.Email,
            Password = testUser.Password,
            Role = testUser.Role,
        }, new CancellationToken());

        // Act
        var result = await userRepository.AuthenticateUserAsync(testUser.Email, testUser.Password, new CancellationToken());

        // Assert
        result.Should().NotBeNull();
        result!.Email.Should().Be(testUser.Email);
    }

    [Fact]
    public async Task UserRepository_AuthenticateUserWithAdminRoleAsync_ReturnsUser()
    {
        // Arrange
        var dbContext =  GetDatabaseContext();
        var userRepository = new UserRepository(dbContext, _hashFunctions);
        var testUser = new User
        {
            Email = "test@example.com",
            Password = "password",
            Role = UserStringConstants.AdminRole
        };
        await userRepository.AddAsync(new User
        {
            Email = testUser.Email,
            Password = testUser.Password,
            Role = testUser.Role,
        }, new CancellationToken());

        // Act
        var result = await userRepository.AuthenticateUserWithAdminRoleAsync(testUser.Email, testUser.Password, new CancellationToken());

        // Assert
        result.Should().NotBeNull();
        result!.Email.Should().Be(testUser.Email);
        result.Role.Should().Be(UserStringConstants.AdminRole);
    }
    [Fact]
    public async Task UserRepository_AuthenticateUserWithAdminRoleAsync_ReturnsNull()
    {
        // Arrange
        var dbContext =  GetDatabaseContext();
        var userRepository = new UserRepository(dbContext, _hashFunctions);
        var testUser = new User
        {
            Email = "test@example.com",
            Password = "password",
            Role = UserStringConstants.UserRole
        };
        await userRepository.AddAsync(new User
        {
            Email = testUser.Email,
            Password = testUser.Password,
            Role = testUser.Role,
        }, new CancellationToken());

        // Act
        var result = await userRepository.AuthenticateUserWithAdminRoleAsync(testUser.Email, testUser.Password, new CancellationToken());

        // Assert
        result.Should().BeNull();
    }


    [Fact]
    public async Task UserRepository_CheckIfUserWithTheEmailIsAlreadyExistAsync_ReturnsTrue()
    {
        // Arrange
        var dbContext =  GetDatabaseContext();
        var userRepository = new UserRepository(dbContext, _hashFunctions);
        var testUser = new User
        {
            Email = "test@example.com",
            Password = "password",
            Role = UserStringConstants.UserRole
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
        var userRepository = new UserRepository(dbContext, _hashFunctions);
        var testUser = new User
        {
            Email = "test@example.com",
            Password = "password",
            Role = UserStringConstants.UserRole
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
        var dbContext =  GetDatabaseContext();
        var userRepository = new UserRepository(dbContext, _hashFunctions);
        var testUser = new User
        {
            Email = "test@example.com",
            Password = "password",
            Role = UserStringConstants.UserRole
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
        var userRepository = new UserRepository(dbContext, _hashFunctions);
        var testUser = new User
        {
            Email = "test@example.com",
            Password = "password",
            Role = "User"
        };
        var addedUserId = await userRepository.AddAsync(testUser, new CancellationToken());
        var updatedUser = new User
        {
            Id = addedUserId,
            Email = "updated@example.com",
            Password = "newpassword",
            Role = UserStringConstants.UserRole
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
        var dbContext =  GetDatabaseContext();
        var userRepository = new UserRepository(dbContext, _hashFunctions);
        var testUser = new User
        {
            Email = "test@example.com",
            Password = "password",
            Role = UserStringConstants.UserRole
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
        var userRepository = new UserRepository(dbContext, _hashFunctions);
        var testUser = new User
        {
            Email = "test@example.com",
            Password = "password",
            Role = UserStringConstants.UserRole
        };
        var addedUserId = await userRepository.AddAsync(testUser, new CancellationToken());

        // Act
        var result = await userRepository.GetAsync(addedUserId, new CancellationToken());

        // Assert
        result.Should().NotBeNull();
        result!.Email.Should().Be(testUser.Email);
    }
}