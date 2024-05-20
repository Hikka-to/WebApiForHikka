using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using WebApiForHikka.Application.Users;
using WebApiForHikka.Constants.AppSettings;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.Users;
using WebApiForHikka.Dtos.ResponseDto;
using WebApiForHikka.SharedFunction.JwtTokenFactories;
using WebApiForHikka.Test.Controller.Shared;
using WebApiForHikka.WebApi.Controllers;

namespace WebApiForHikka.Test.Controller.Users;

public class UsersControllerTest : BaseControllerTest
{
    private readonly IUserService _userService = A.Fake<IUserService>();
    private readonly IConfiguration _configuration = A.Fake<IConfiguration>();
    private readonly IJwtTokenFactory _jwtTokenFactory = new JwtTokenFactory();


    public UsersControllerTest() 
    {
        A.CallTo(() => _configuration[AppSettingsStringConstants.JwtKey]).Returns("7DbP1lM5m0IiZWOWlaCSFApiHKfR0Zhb");
    }

    [Fact]
    public async Task UsersController_GetAll_ReturnsOK()
    {
        //Arrange
        var users = A.Fake<ICollection<GetUserDto>>();
        var usersList = A.Fake<List<GetUserDto>>();
        A.CallTo(() => _mapper.Map<List<GetUserDto>>(users)).Returns(usersList);
        // Generate JWT Token
        var jwtToken = _jwtTokenFactory.GetJwtToken(_userWithAdminRole, _configuration);

        // Create mocks for HttpRequest and HttpContext
        var httpRequestMock = new Mock<HttpRequest>();
        var httpContextMock = new Mock<HttpContext>();

        httpRequestMock.Setup(req => req.Headers.Authorization).Returns(jwtToken);

        // Setup the HttpContext mock to return the mocked HttpRequest
        httpContextMock.Setup(ctx => ctx.Request).Returns(httpRequestMock.Object);

        // Mock IHttpContextAccessor to return the mocked HttpContext
        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContextMock.Object);


        var controller = new UsersController(
            _userService,
            _jwtTokenFactory,
            _configuration,
            _mapper,
            httpContextAccessorMock.Object
            );

        //Act
        var result = await controller.GetAll(
            _filterPaginationDto,
            _cancellationToken
            );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));
    }

    [Fact]
    public async Task Register_ValidModel_ReturnsOk()
    {
        // Arrange
        var userRegistrationDto = new UserRegistrationDto { Email = "test@example.com", Password = "password", Role = "User" };
        var userId = Guid.NewGuid();
        A.CallTo(() => _userService.RegisterUserAsync(A<User>.Ignored, A<CancellationToken>.Ignored)).Returns(userId);
        var controller = new UsersController(_userService, _jwtTokenFactory, _configuration, _mapper, _httpContextAccessor);

        // Act
        var result = await controller.Create(userRegistrationDto, CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<RegistratedResponseUserDto>(okResult.Value);
        Assert.Equal("User created successfully", response.Message);
        Assert.Equal(userId, response.Id);
    }

    //[Fact]
    //public async Task Login_ValidCredentials_ReturnsOkWithToken()
    //{
    //    // Arrange
    //    var userLoginDto = new UserLoginDto { Email = "test@example.com", Password = "password" };
    //    var user = new User { Email = "test@example.com", Role = "User", Id = Guid.NewGuid() };
    //    A.CallTo(() => _userService.AuthenticateUserAsync(userLoginDto.Email, userLoginDto.Password, A<CancellationToken>.Ignored)).Returns(user);
    //    A.CallTo(() => _configuration[AppSettingsStringConstants.JwtKey]).Returns("your_jwt_key_here");
    //    var controller = new UsersController(_userService, _configuration, _mapper, _httpContextAccessor);

    //    // Act
    //    var result = await controller.Login(userLoginDto, CancellationToken.None);

    //    // Assert
    //    var okResult = Assert.IsType<OkObjectResult>(result);
    //}

    [Fact]
    public async Task Get_ValidId_ReturnsOkWithUser()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new User { Email = "test@example.com", Role = "User", Id = userId };
        A.CallTo(() => _userService.GetAsync(userId, A<CancellationToken>.Ignored)).Returns(user);
        var controller = new UsersController(_userService, _jwtTokenFactory, _configuration, _mapper, _httpContextAccessor);

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
        var user = new User { Email = "test@example.com", Role = "User", Id = updateUserDto.Id };
        A.CallTo(() => _userService.GetAsync(updateUserDto.Id, A<CancellationToken>.Ignored)).Returns(user);

        // Generate JWT Token
        var jwtToken = _jwtTokenFactory.GetJwtToken(_userWithAdminRole, _configuration);

        // Create mocks for HttpRequest and HttpContext
        var httpRequestMock = new Mock<HttpRequest>();
        var httpContextMock = new Mock<HttpContext>();

        httpRequestMock.Setup(req => req.Headers.Authorization).Returns(jwtToken);

        // Setup the HttpContext mock to return the mocked HttpRequest
        httpContextMock.Setup(ctx => ctx.Request).Returns(httpRequestMock.Object);

        // Mock IHttpContextAccessor to return the mocked HttpContext
        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContextMock.Object);

        var controller = new UsersController(_userService, _jwtTokenFactory, _configuration, _mapper, httpContextAccessorMock.Object);

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
        var controller = new UsersController(_userService, _jwtTokenFactory, _configuration, _mapper, _httpContextAccessor);

        // Act
        var result = await controller.Delete(userId, CancellationToken.None);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}