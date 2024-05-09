
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WebApiForHikka.Application.Users;
using WebApiForHikka.Constants.Users;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.SharedFunction.HashFunction;
using WebApiForHikka.Test.Shared;
using WebApiForHikka.WebApiTest.Repository.Users.FakeDataCreaters;

namespace WebApiForHikka.Test.Service.Users;
public class UserServiceTest : SharedTest
{
    private IHashFunctions _hashFunctions;

    public UserServiceTest()
    {
        _hashFunctions = new HashFunctions();
    }


    [Fact]
    public async Task UserService_AuthenticateUserAsync_ReturnsUser()
    {
        // Arrange
        var dbContext = await GetDatabaseContext();
        var userRepository = new UserRepository(dbContext, _hashFunctions);
        var userService = new UserService(userRepository);
        var testUser = new User { Email = "test@example.com", Password = "password", Role = UserStringConstants.UserRole };
        await userRepository.AddAsync(testUser, new CancellationToken());

        // Act
        var result = await userService.AuthenticateUserAsync(testUser.Email, testUser.Password, new CancellationToken());

        // Assert
        result.Should().NotBeNull();
        result.Email.Should().Be(testUser.Email);
    }

    [Fact]
    public async Task UserService_AuthenticateUserWithAdminRoleAsync_ReturnsUser()
    {
        // Arrange
        var dbContext = await GetDatabaseContext();
        var userRepository = new UserRepository(dbContext, _hashFunctions);
        var userService = new UserService(userRepository);
        var testUser = new User { Email = "test@example.com", Password = "password", Role = UserStringConstants.AdminRole };
        await userRepository.AddAsync(testUser, new CancellationToken());

        // Act
        var result = await userService.AuthenticateUserWithAdminRoleAsync(testUser.Email, testUser.Password, new CancellationToken());

        // Assert
        result.Should().NotBeNull();
        result.Email.Should().Be(testUser.Email);
        result.Role.Should().Be(UserStringConstants.AdminRole);
    }

    [Fact]
    public async Task UserService_AuthenticateUserWithAdminRoleAsync_ReturnsNull()
    {
        // Arrange
        var dbContext = await GetDatabaseContext();
        var userRepository = new UserRepository(dbContext, _hashFunctions);
        var userService = new UserService(userRepository);
        var testUser = new User { Email = "test@example.com", Password = "password", Role = UserStringConstants.UserRole };
        await userRepository.AddAsync(testUser, new CancellationToken());

        // Act
        var result = await userService.AuthenticateUserWithAdminRoleAsync(testUser.Email, testUser.Password, new CancellationToken());

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task UserService_CheckIfUserWithTheEmailIsAlreadyExistAsync_ReturnsTrue()
    {
        // Arrange
        var dbContext = await GetDatabaseContext();
        var userRepository = new UserRepository(dbContext, _hashFunctions);
        var userService = new UserService(userRepository);
        var testUser = new User { Email = "test@example.com", Password = "password", Role = UserStringConstants.UserRole };
        await userRepository.AddAsync(testUser, new CancellationToken());

        // Act
        var result = await userService.CheckIfUserWithTheEmailIsAlreadyExistAsync(testUser.Email, new CancellationToken());

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void UserService_CheckIfUserWithTheEmailIsAlreadyExist_ReturnsTrue()
    {
        // Arrange
        var dbContext = GetDatabaseContext().Result;
        var userRepository = new UserRepository(dbContext, _hashFunctions);
        var userService = new UserService(userRepository);
        var testUser = new User { Email = "test@example.com", Password = "password", Role = UserStringConstants.UserRole };
        userRepository.AddAsync(testUser, new CancellationToken()).Wait();

        // Act
        var result = userService.CheckIfUserWithTheEmailIsAlreadyExist(testUser.Email);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task UserService_RegistrateUserAsync_ReturnsUser()
    {
        // Arrange
        var dbContext = await GetDatabaseContext();
        var userRepository = new UserRepository(dbContext, _hashFunctions);
        var userService = new UserService(userRepository);
        var testUser = new User { Email = "test@example.com", Password = "password", Role = UserStringConstants.UserRole };
        await userService.RegisterUserAsync(testUser, new CancellationToken());

        // Act
        var result = await userService.AuthenticateUserAsync(testUser.Email, testUser.Password, new CancellationToken());

        // Assert
        result.Should().NotBeNull();
        result.Email.Should().Be(testUser.Email);
    }
}