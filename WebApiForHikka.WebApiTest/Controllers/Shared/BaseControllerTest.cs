using AutoMapper;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;
using WebApiForHikka.Constants.AppSettings;
using WebApiForHikka.Constants.Models.Users;
using WebApiForHikka.Domain;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.SharedFunction.JwtTokenFactories;
using WebApiForHikka.Test.Shared;
using WebApiForHikka.WebApi.Helper;

namespace WebApiForHikka.Test.Controller.Shared;

public abstract class BaseControllerTest : SharedTest
{
    protected readonly IMapper _mapper;

    protected readonly IHttpContextAccessor _httpContextAccessor = A.Fake<HttpContextAccessor>();
    protected readonly IConfiguration _configuration = A.Fake<IConfiguration>();

    protected readonly IJwtTokenFactory _jwtTokenFactory = new JwtTokenFactory();

    protected FilterPaginationDto _filterPaginationDto => new();

    protected User _userWithAdminRole => new User()
    {
        Email = "test@gmail.com",
        Id = new Guid(),
        Role = UserStringConstants.AdminRole,
    };
    protected User _userWithUserRole => new User()
    {
        Email = "test@gmail.com",
        Id = new Guid(),
        Role = UserStringConstants.UserRole,
    };

    public BaseControllerTest() 
    {
        A.CallTo(() => _configuration[AppSettingsStringConstants.JwtKey]).Returns("7DbP1lM5m0IiZWOWlaCSFApiHKfR0Zhb");
        var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfiles()));
        _mapper = mapperConfiguration.CreateMapper();

    }


    protected IHttpContextAccessor GetHttpContextAccessForAdminUser() 
    {
        // Generate JWT Token
        var jwtToken = _jwtTokenFactory.GetJwtToken(_userWithAdminRole, _configuration);

        // CrudController_ mocks for HttpRequest and HttpContext
        var httpRequestMock = new Mock<HttpRequest>();
        var httpContextMock = new Mock<HttpContext>();

        httpRequestMock.Setup(req => req.Headers.Authorization).Returns(jwtToken);

        // Setup the HttpContext mock to return the mocked HttpRequest
        httpContextMock.Setup(ctx => ctx.Request).Returns(httpRequestMock.Object);

        // Mock IHttpContextAccessor to return the mocked HttpContext
        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContextMock.Object);

        return httpContextAccessorMock.Object;
    }

    protected IHttpContextAccessor GetHttpContextAccessForUserUser()
    {
        // Generate JWT Token
        var jwtToken = _jwtTokenFactory.GetJwtToken(_userWithUserRole, _configuration);

        // CrudController_ mocks for HttpRequest and HttpContext
        var httpRequestMock = new Mock<HttpRequest>();
        var httpContextMock = new Mock<HttpContext>();

        httpRequestMock.Setup(req => req.Headers.Authorization).Returns(jwtToken);

        // Setup the HttpContext mock to return the mocked HttpRequest
        httpContextMock.Setup(ctx => ctx.Request).Returns(httpRequestMock.Object);

        // Mock IHttpContextAccessor to return the mocked HttpContext
        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContextMock.Object);

        return httpContextAccessorMock.Object;
    }


}
