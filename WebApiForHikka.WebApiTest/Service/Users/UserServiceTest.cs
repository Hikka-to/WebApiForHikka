﻿
using FluentAssertions;
using WebApiForHikka.Application.Users;
using WebApiForHikka.Constants.Models.Users;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.SharedFunction.HashFunction;
using WebApiForHikka.Test.Shared;

namespace WebApiForHikka.Test.Service.Users;
public class UserServiceTest : SharedTest
{
    private readonly IHashFunctions _hashFunctions = new HashFunctions();


    [Fact]
    public async Task UserService_AuthenticateUserAsync_ReturnsUser()
    {
        // Arrange
        var dbContext = GetDatabaseContext();
        var userManager = GetUserManager(dbContext);
        var userRepository = new UserRepository(dbContext, userManager);
        var userService = new UserService(userRepository);
        var roleManager = GetRoleManager(dbContext);
        var role = await roleManager.FindByNameAsync(UserStringConstants.UserRole);
        var testUser = new User { UserName = "test", Email = "test@example.com", PasswordHash = "Password123!", Roles = [role!] };
        await userRepository.AddAsync(testUser, new CancellationToken());

        // Act
        var result = await userService.AuthenticateUserAsync(testUser.Email, "Password123!", new CancellationToken());

        // Assert
        result.Should().NotBeNull();
        result!.Email.Should().Be(testUser.Email);
    }

    [Fact]
    public async Task UserService_AuthenticateUserWithAdminRoleAsync_ReturnsUser()
    {
        // Arrange
        var dbContext = GetDatabaseContext();
        var userManager = GetUserManager(dbContext);
        var userRepository = new UserRepository(dbContext, userManager);
        var userService = new UserService(userRepository);
        var roleManager = GetRoleManager(dbContext);
        var role = await roleManager.FindByNameAsync(UserStringConstants.AdminRole);
        var testUser = new User { UserName = "test", Email = "test@example.com", PasswordHash = "Password123!", Roles = [role!] };
        await userRepository.AddAsync(testUser, new CancellationToken());

        // Act
        var result = await userService.AuthenticateUserWithAdminRoleAsync(testUser.Email, "Password123!", new CancellationToken());

        // Assert
        result.Should().NotBeNull();
        result!.Email.Should().Be(testUser.Email);
        result!.Roles.Should().Contain(role!);
    }

    [Fact]
    public async Task UserService_AuthenticateUserWithAdminRoleAsync_ReturnsNull()
    {
        // Arrange
        var dbContext = GetDatabaseContext();
        var userManager = GetUserManager(dbContext);
        var userRepository = new UserRepository(dbContext, userManager);
        var userService = new UserService(userRepository);
        var roleManager = GetRoleManager(dbContext);
        var role = await roleManager.FindByNameAsync(UserStringConstants.UserRole);
        var testUser = new User { UserName = "test", Email = "test@example.com", PasswordHash = "Password123!", Roles = [role!] };
        await userRepository.AddAsync(testUser, new CancellationToken());

        // Act
        var result = await userService.AuthenticateUserWithAdminRoleAsync(testUser.Email, testUser.PasswordHash, new CancellationToken());

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task UserService_CheckIfUserWithTheEmailIsAlreadyExistAsync_ReturnsTrue()
    {
        // Arrange
        var dbContext = GetDatabaseContext();
        var userManager = GetUserManager(dbContext);
        var userRepository = new UserRepository(dbContext, userManager);
        var userService = new UserService(userRepository);
        var roleManager = GetRoleManager(dbContext);
        var role = await roleManager.FindByNameAsync(UserStringConstants.UserRole);
        var testUser = new User { UserName = "test", Email = "test@example.com", PasswordHash = "Password123!", Roles = [role!] };
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
        var dbContext = GetDatabaseContext();
        var userManager = GetUserManager(dbContext);
        var userRepository = new UserRepository(dbContext, userManager);
        var userService = new UserService(userRepository);
        var roleManager = GetRoleManager(dbContext);
        var role = roleManager.FindByNameAsync(UserStringConstants.UserRole).Result;
        var testUser = new User { UserName = "test", Email = "test@example.com", PasswordHash = "Password123!", Roles = [role!] };
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
        var dbContext = GetDatabaseContext();
        var userManager = GetUserManager(dbContext);
        var userRepository = new UserRepository(dbContext, userManager);
        var userService = new UserService(userRepository);
        var roleManager = GetRoleManager(dbContext);
        var role = await roleManager.FindByNameAsync(UserStringConstants.UserRole);
        var testUser = new User { UserName = "test", Email = "test@example.com", PasswordHash = "Password123!", Roles = [role!] };
        await userService.RegisterUserAsync(testUser, new CancellationToken());

        // Act
        var result = await userService.AuthenticateUserAsync(testUser.Email, "Password123!", new CancellationToken());

        // Assert
        result.Should().NotBeNull();
        result!.Email.Should().Be(testUser.Email);
    }
}