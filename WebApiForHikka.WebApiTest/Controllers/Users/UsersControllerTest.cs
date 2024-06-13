using FakeItEasy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApiForHikka.Application.Users;
using WebApiForHikka.Constants.Models.Users;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.Users;
using WebApiForHikka.Dtos.ResponseDto;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Test.Controller.Shared;
using WebApiForHikka.WebApi.Controllers;

namespace WebApiForHikka.Test.Controller.Users;

public class UsersControllerTest : BaseControllerTest
{
    protected IUserService GetUserService(HikkaDbContext dbContext, UserManager<User> userManager)
    {
        var userRepository = new UserRepository(dbContext, userManager);

        return new UserService(userRepository);
    }

    [Fact]
    public async Task Register_ValidModel_ReturnsOk()
    {
        // Arrange
        var userRegistrationDto = new UserRegistrationDto { UserName = "test", Email = "test@example.com", Password = "Password123!", Role = "User" };
        var userId = Guid.NewGuid();
        var dbContext = GetDatabaseContext();
        var userManager = GetUserManager(dbContext);
        var userService = GetUserService(dbContext, userManager);
        var jwtTokenFactory = GetJwtTokenFactory(userManager);
        A.CallTo(() => userService.RegisterUserAsync(A<User>.Ignored, A<CancellationToken>.Ignored)).Returns(userId);
        var roleManager = GetRoleManager();
        var controller = new UsersController(userService, jwtTokenFactory, Configuration, roleManager, _mapper, GetHttpContextAccessForAdminUser(userManager));

        // Act
        var result = await controller.Create(userRegistrationDto, CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<RegistratedResponseUserDto>(okResult.Value);
        Assert.Equal("User created successfully", response.Message);
        Assert.Equal(userId, response.Id);
    }

    [Fact]
    public async Task Get_ValidId_ReturnsOkWithUser()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var dbContext = GetDatabaseContext();
        var roleManager = GetRoleManager(dbContext);
        var userManager = GetUserManager(dbContext);
        var userService = GetUserService(dbContext, userManager);
        var jwtTokenFactory = GetJwtTokenFactory(userManager);
        var role = await roleManager.FindByNameAsync(UserStringConstants.UserRole);
        var user = new User { Email = "test@example.com", Id = userId, Roles = [role!] };
        A.CallTo(() => userService.GetAsync(userId, A<CancellationToken>.Ignored)).Returns(user);

        var controller = new UsersController(userService, jwtTokenFactory, Configuration, roleManager, _mapper, GetHttpContextAccessForAdminUser(userManager));

        // Act
        var result = await controller.Get(userId, CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Put_ValidModel_ReturnsNoContent()
    {
        // Arrange
        var updateUserDto = new UpdateUserDto { Id = Guid.NewGuid(), Email = "test@example.com", Role = "User" };
        var dbContext = GetDatabaseContext();
        var userManager = GetUserManager(dbContext);
        var roleManager = GetRoleManager(dbContext);
        var userService = GetUserService(dbContext, userManager);
        var jwtTokenFactory = GetJwtTokenFactory(userManager);
        var role = await roleManager.FindByNameAsync(UserStringConstants.AdminRole);
        var user = new User { Email = "test@example.com", Id = updateUserDto.Id, Roles = [role!] };
        A.CallTo(() => userService.GetAsync(updateUserDto.Id, A<CancellationToken>.Ignored)).Returns(user);

        var controller = new UsersController(userService, jwtTokenFactory, Configuration, roleManager, _mapper, GetHttpContextAccessForAdminUser(userManager));

        // Act
        var result = await controller.Put(updateUserDto, CancellationToken.None);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Delete_ValidId_ReturnsNoContent()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var dbContext = GetDatabaseContext();
        var userManager = GetUserManager(dbContext);
        var roleManager = GetRoleManager(dbContext);
        var userService = GetUserService(dbContext, userManager);
        var jwtTokenFactory = GetJwtTokenFactory(userManager);
        var controller = new UsersController(userService, jwtTokenFactory, Configuration, roleManager, _mapper, GetHttpContextAccessForAdminUser(userManager));

        // Act
        var result = await controller.Delete(userId, CancellationToken.None);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}