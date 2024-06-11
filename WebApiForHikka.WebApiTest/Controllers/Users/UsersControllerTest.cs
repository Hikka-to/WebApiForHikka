using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using WebApiForHikka.Application.Users;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.Users;
using WebApiForHikka.Dtos.ResponseDto;
using WebApiForHikka.Test.Controller.Shared;
using WebApiForHikka.WebApi.Controllers;

namespace WebApiForHikka.Test.Controller.Users;

public class UsersControllerTest : BaseControllerTest
{
    private readonly IUserService _userService = A.Fake<IUserService>();


    [Fact]
    public async Task Register_ValidModel_ReturnsOk()
    {
        // Arrange
        var userRegistrationDto = new UserRegistrationDto { UserName = "test", Email = "test@example.com", Password = "Password123!", Role = "User" };
        var userId = Guid.NewGuid();
        A.CallTo(() => _userService.RegisterUserAsync(A<User>.Ignored, A<CancellationToken>.Ignored)).Returns(userId);
        var userManager = GetUserManager();
        var controller = new UsersController(_userService, JwtTokenFactory, Configuration, userManager, _mapper, GetHttpContextAccessForAdminUser());

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
        // !!!!!!!!!! Need role fix
        var user = new User { Email = "test@example.com", Id = userId };
        A.CallTo(() => _userService.GetAsync(userId, A<CancellationToken>.Ignored)).Returns(user);
        var userManager = GetUserManager();
        var controller = new UsersController(_userService, JwtTokenFactory, Configuration, userManager, _mapper, GetHttpContextAccessForAdminUser());

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
        // !!!!!!!!!! Need role fix
        var user = new User { Email = "test@example.com", Id = updateUserDto.Id };
        A.CallTo(() => _userService.GetAsync(updateUserDto.Id, A<CancellationToken>.Ignored)).Returns(user);

        var userManager = GetUserManager();
        var controller = new UsersController(_userService, JwtTokenFactory, Configuration, userManager, _mapper, GetHttpContextAccessForAdminUser());

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
        var userManager = GetUserManager();
        var controller = new UsersController(_userService, JwtTokenFactory, Configuration, userManager, _mapper, GetHttpContextAccessForAdminUser());

        // Act
        var result = await controller.Delete(userId, CancellationToken.None);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}