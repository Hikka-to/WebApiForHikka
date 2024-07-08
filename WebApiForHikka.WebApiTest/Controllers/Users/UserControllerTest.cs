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

public class UserControllerTest : BaseControllerTest
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
        var userRegistrationDto = new UserRegistrationDto
        {
            UserName = "Tesgacawefw21dasdacdqds", Email = "tes12dasddsadasdaat@example.com", Password = "dada!",
            Role = "User"
        };
        var userId = Guid.NewGuid();
        var dbContext = GetDatabaseContext();
        var userManager = GetUserManager(dbContext);
        var userService = GetUserService(dbContext, userManager);
        var jwtTokenFactory = GetJwtTokenFactory(userManager);
        var roleManager = GetRoleManager(dbContext);
        var controller = new UserController(userService, jwtTokenFactory, Configuration, roleManager, _mapper,
            GetHttpContextAccessForAnonymUser());

        // Act
        var result = await controller.Create(userRegistrationDto, CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<RegistratedResponseUserDto>(okResult.Value);
        Assert.Equal(UserStringConstants.MessageUserRegistrated, response.Message);
    }

    [Fact]
    public async Task Get_ValidId_ReturnsOkWithUser()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var dbContext = GetDatabaseContext();
        var roleManager = GetRoleManager(dbContext);
        var userManager = GetUserManager(dbContext);
        var userService = A.Fake<UserService>();
        var jwtTokenFactory = GetJwtTokenFactory(userManager);
        var role = await roleManager.FindByNameAsync(UserStringConstants.UserRole);
        var user = new User { Email = "test@example.com", Id = userId, Roles = [role!] };

        A.CallTo(() => userService.GetAsync(userId, A<CancellationToken>.Ignored)).Returns(user);

        var controller = new UserController(userService, jwtTokenFactory, Configuration, roleManager, _mapper,
            await GetHttpContextAccessForAdminUser(userManager, roleManager));

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
        var userService = A.Fake<UserService>();
        var jwtTokenFactory = GetJwtTokenFactory(userManager);
        var role = await roleManager.FindByNameAsync(UserStringConstants.AdminRole);
        var user = new User { Email = "test@example.com", Id = updateUserDto.Id, Roles = [role!] };
        A.CallTo(() => userService.GetAsync(updateUserDto.Id, A<CancellationToken>.Ignored)).Returns(user);

        var controller = new UserController(userService, jwtTokenFactory, Configuration, roleManager, _mapper,
            await GetHttpContextAccessForAdminUser(userManager, roleManager));

        // Act
        var result = await controller.Put(updateUserDto, CancellationToken.None);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Delete_ValidId_ReturnsNoContent()
    {
        // Arrange
        var dbContext = GetDatabaseContext();
        var userManager = GetUserManager(dbContext);
        var roleManager = GetRoleManager(dbContext);
        var userService = GetUserService(dbContext, userManager);
        var jwtTokenFactory = GetJwtTokenFactory(userManager);
        var controller = new UserController(userService, jwtTokenFactory, Configuration, roleManager, _mapper,
            await GetHttpContextAccessForAdminUser(userManager, roleManager));

        var role = await roleManager.FindByNameAsync(UserStringConstants.AdminRole);
        var user = new User { Email = "test@example.com", Roles = [role!], UserName = "tets", PasswordHash = "tesds" };

        var userId = await userService.CreateAsync(user, CancellationToken);

        // Act
        var result = await controller.Delete(userId, CancellationToken);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}