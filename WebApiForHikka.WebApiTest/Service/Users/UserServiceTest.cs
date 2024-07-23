using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using WebApiForHikka.Application.Users;
using WebApiForHikka.Constants.Models.Users;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.SharedFunction.HashFunction;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Shared;
using WebApiForHikka.WebApi.Helper.FileHelper;

namespace WebApiForHikka.Test.Service.Users;

public class UserServiceTest : SharedTest
{
    private readonly IHashFunctions _hashFunctions = new HashFunctions();


    [Fact]
    public async Task UserService_AuthenticateUserAsync_ReturnsUser()
    {
        Mock<IFileHelper> fileHelperMock = new Mock<IFileHelper>();
        fileHelperMock.Setup(m => m.DeleteFile(It.IsAny<string[]>(), It.IsAny<string>()));

        // Arrange
        var dbContext = GetDatabaseContext();
        var userManager = GetUserManager(dbContext);
        var userRepository = new UserRepository(dbContext, userManager);
        var userService = new UserService(userRepository, fileHelperMock.Object);
        var roleManager = GetRoleManager(dbContext);
        var role = await roleManager.FindByNameAsync(UserStringConstants.UserRole);
        
        var testUser = GetUserModels.GetSample();
        string password = testUser.PasswordHash;
        
        await userRepository.AddAsync(testUser, new CancellationToken());

        // Act
        var result = await userService.AuthenticateUserAsync(testUser.Email, password, new CancellationToken());

        // Assert
        result.Should().NotBeNull();
        result!.Email.Should().Be(testUser.Email);
    }

    [Fact]
    public async Task UserService_AuthenticateUserWithAdminRoleAsync_ReturnsUser()
    {
        Mock<IFileHelper> fileHelperMock = new Mock<IFileHelper>();
        fileHelperMock.Setup(m => m.DeleteFile(It.IsAny<string[]>(), It.IsAny<string>()));

        // Arrange
        var dbContext = GetDatabaseContext();
        var userManager = GetUserManager(dbContext);
        var userRepository = new UserRepository(dbContext, userManager);
        var userService = new UserService(userRepository, fileHelperMock.Object);
        var roleManager = GetRoleManager(dbContext);
        var role = await roleManager.FindByNameAsync(UserStringConstants.AdminRole);
        
        var testUser = GetUserModels.GetSample();
        string password = testUser.PasswordHash;

        testUser.Roles = [roleManager.Roles.First()];
        
        await userRepository.AddAsync(testUser, new CancellationToken());

        // Act
        var result =
            await userService.AuthenticateUserWithAdminRoleAsync(testUser.Email, password,
                new CancellationToken());

        // Assert
        result.Should().NotBeNull();
        result!.Email.Should().Be(testUser.Email);
        result!.Roles.Should().Contain(role!);
    }

    [Fact]
    public async Task UserService_AuthenticateUserWithAdminRoleAsync_ReturnsNull()
    {
        Mock<IFileHelper> fileHelperMock = new Mock<IFileHelper>();
        fileHelperMock.Setup(m => m.DeleteFile(It.IsAny<string[]>(), It.IsAny<string>()));

        // Arrange
        var dbContext = GetDatabaseContext();
        var userManager = GetUserManager(dbContext);
        var userRepository = new UserRepository(dbContext, userManager);
        var userService = new UserService(userRepository, fileHelperMock.Object);
        var roleManager = GetRoleManager(dbContext);
        var role = await roleManager.FindByNameAsync(UserStringConstants.UserRole);
        var testUser = GetUserModels.GetSample();
        await userRepository.AddAsync(testUser, new CancellationToken());

        // Act
        var result =
            await userService.AuthenticateUserWithAdminRoleAsync(testUser.Email, testUser.PasswordHash,
                new CancellationToken());

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task UserService_CheckIfUserWithTheEmailIsAlreadyExistAsync_ReturnsTrue()
    {
        Mock<IFileHelper> fileHelperMock = new Mock<IFileHelper>();
        fileHelperMock.Setup(m => m.DeleteFile(It.IsAny<string[]>(), It.IsAny<string>()));

        // Arrange
        var dbContext = GetDatabaseContext();
        var userManager = GetUserManager(dbContext);
        var userRepository = new UserRepository(dbContext, userManager);
        var userService = new UserService(userRepository, fileHelperMock.Object);
        var roleManager = GetRoleManager(dbContext);
        var role = await roleManager.FindByNameAsync(UserStringConstants.UserRole);

        var testUser = GetUserModels.GetSample();

        await userRepository.AddAsync(testUser, new CancellationToken());

        // Act
        var result =
            await userService.CheckIfUserWithTheEmailIsAlreadyExistAsync(testUser.Email, new CancellationToken());

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void UserService_CheckIfUserWithTheEmailIsAlreadyExist_ReturnsTrue()
    {
        Mock<IFileHelper> fileHelperMock = new Mock<IFileHelper>();
        fileHelperMock.Setup(m => m.DeleteFile(It.IsAny<string[]>(), It.IsAny<string>()));


        // Arrange
        var dbContext = GetDatabaseContext();
        var userManager = GetUserManager(dbContext);
        var userRepository = new UserRepository(dbContext, userManager);
        var userService = new UserService(userRepository, fileHelperMock.Object);
        var roleManager = GetRoleManager(dbContext);
        var role = roleManager.FindByNameAsync(UserStringConstants.UserRole).Result;
        var testUser = GetUserModels.GetSample();
        
        userRepository.AddAsync(testUser, new CancellationToken()).Wait();

        // Act
        var result = userService.CheckIfUserWithTheEmailIsAlreadyExist(testUser.Email);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task UserService_RegistrateUserAsync_ReturnsUser()
    {
        Mock<IFileHelper> fileHelperMock = new Mock<IFileHelper>();
        fileHelperMock.Setup(m => m.DeleteFile(It.IsAny<string[]>(), It.IsAny<string>()));

        // Arrange
        var dbContext = GetDatabaseContext();
        var userManager = GetUserManager(dbContext);
        var userRepository = new UserRepository(dbContext, userManager);
        var userService = new UserService(userRepository, fileHelperMock.Object);
        var roleManager = GetRoleManager(dbContext);
        var role = await roleManager.FindByNameAsync(UserStringConstants.UserRole);

        var testUser = GetUserModels.GetSample();
        string password = testUser.PasswordHash;
        await userService.RegisterUserAsync(testUser, new CancellationToken());
        // Act
        var result =
            await userService.AuthenticateUserAsync(testUser.Email, password, new CancellationToken());


        // Assert
        result.Should().NotBeNull();
        result!.Email.Should().Be(testUser.Email);
    }
}