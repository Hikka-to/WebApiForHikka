using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WebApiForHikka.Constants.Users;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.SharedFunction.HashFunction;
using WebApiForHikka.WebApiTest.Repository.Users.FakeDataCreaters;


namespace WebApiForHikka.WebApiTest.Repository.Users;

public class UserRepositoryTest
{
    private IHashFunctions _hashFunctions;

    public UserRepositoryTest()
    {
        _hashFunctions = new HashFunctions();
    }

    private async Task<HikkaDbContext> GetDatabaseContext()
    {
        var options = new DbContextOptionsBuilder<HikkaDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var databaseContext = new HikkaDbContext(options);
        databaseContext.Database.EnsureCreated();
        await CreateUserFakeData.CreateUsersWithRoleAsync(databaseContext, UserStringConstants.UserRole, 10);
        await CreateUserFakeData.CreateUsersWithRoleAsync(databaseContext, UserStringConstants.AdminRole, 10);

        return databaseContext;
    }
    [Fact]
    public async Task UserRepository_AuthenticateUserAsync_ReturnsUser()
    {
        // Arrange
        var dbContext = await GetDatabaseContext();
        var userRepository = new UserRepository(dbContext, _hashFunctions);
        var testUser = new User { Email = "test@example.com", Password = "password", Role="User" };
        await userRepository.AddAsync(new User {
            Email = testUser.Email,
            Password = testUser.Password,
            Role = testUser.Role,
        }, new CancellationToken());

        // Act
        var result = await userRepository.AuthenticateUserAsync(testUser.Email, testUser.Password, new CancellationToken());

        // Assert
        result.Should().NotBeNull();
        result.Email.Should().Be(testUser.Email);
    }

    [Fact]
    public async Task UserRepository_CheckIfUserWithTheEmailIsAlreadyExistAsync_ReturnsTrue()
    {
        // Arrange
        var dbContext = await GetDatabaseContext();
        var userRepository = new UserRepository(dbContext, _hashFunctions);
        var testUser = new User { Email = "test@example.com", Password = "password", Role="User" };
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
        var dbContext = GetDatabaseContext().Result;
        var userRepository = new UserRepository(dbContext, _hashFunctions);
        var testUser = new User { Email = "test@example.com", Password = "password", Role="User" };
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
        var dbContext = await GetDatabaseContext();
        var userRepository = new UserRepository(dbContext, _hashFunctions);
        var testUser = new User { Email = "test@example.com", Password = "password", Role="User" };

        // Act
        var result = await userRepository.AddAsync(testUser, new CancellationToken());

        // Assert
        result.Should().NotBeEmpty();
        var addedUser = await userRepository.GetAsync(result, new CancellationToken());
        addedUser.Should().NotBeNull();
        addedUser.Email.Should().Be(testUser.Email);
    }

    [Fact]
    public async Task UserRepository_UpdateAsync_UpdatesUserDetails()
    {
        // Arrange
        var dbContext = await GetDatabaseContext();
        var userRepository = new UserRepository(dbContext, _hashFunctions);
        var testUser = new User { Email = "test@example.com", Password = "password", Role="User" };
        var addedUserId = await userRepository.AddAsync(testUser, new CancellationToken());
        var updatedUser = new User { Id = addedUserId, Email = "updated@example.com", Password = "newpassword", Role="User" };

        // Act
        await userRepository.UpdateAsync(updatedUser, new CancellationToken());

        // Assert
        var updatedUserResult = await userRepository.GetAsync(addedUserId, new CancellationToken());
        updatedUserResult.Should().NotBeNull();
        updatedUserResult.Email.Should().Be(updatedUser.Email);
    }

    [Fact]
    public async Task UserRepository_DeleteAsync_DeletesUser()
    {
        // Arrange
        var dbContext = await GetDatabaseContext();
        var userRepository = new UserRepository(dbContext, _hashFunctions);
        var testUser = new User { Email = "test@example.com", Password = "password", Role="User" };
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
        var dbContext = await GetDatabaseContext();
        var userRepository = new UserRepository(dbContext, _hashFunctions);
        var testUser = new User { Email = "test@example.com", Password = "password", Role="User" };
        var addedUserId = await userRepository.AddAsync(testUser, new CancellationToken());

        // Act
        var result = await userRepository.GetAsync(addedUserId, new CancellationToken());

        // Assert
        result.Should().NotBeNull();
        result.Email.Should().Be(testUser.Email);
    }


}
