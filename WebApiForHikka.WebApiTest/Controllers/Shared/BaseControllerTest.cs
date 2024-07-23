using AutoMapper;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using WebApiForHikka.Constants.AppSettings;
using WebApiForHikka.Constants.Models.Users;
using WebApiForHikka.Constants.Shared;
using WebApiForHikka.Domain;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.SharedDtos;
using WebApiForHikka.SharedFunction.JwtTokenFactories;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;
using WebApiForHikka.Test.Shared;
using WebApiForHikka.WebApi.Helper;
using WebApiForHikka.WebApi.Helpers;

namespace WebApiForHikka.Test.Controllers.Shared;

public abstract class BaseControllerTest : SharedTest
{
    private readonly IHttpContextAccessor _httpContextAccessor = A.Fake<HttpContextAccessor>();
    protected readonly IMapper _mapper;
    protected readonly IConfiguration Configuration = A.Fake<IConfiguration>();

    public BaseControllerTest()
    {
        A.CallTo(() => Configuration[AppSettingsStringConstants.JwtKey]).Returns("7DbP1lM5m0IiZWOWlaCSFApiHKfR0Zhb");
        var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfiles()));
        _mapper = mapperConfiguration.CreateMapper();
    }

    protected FilterPaginationDto FilterPaginationDto => new()
    {
        SearchTerm = "",
        PageNumber = SharedNumberConstatnts.DefaultPageToStartWith,
        PageSize = SharedNumberConstatnts.DefaultItemsInOnePage,
        Column = SharedStringConstants.IdName,
        SortOrder = SortOrder.Asc
    };

    // !!!!!!!!! Need to fix new roles
    protected User SampleUser = GetUserModels.GetSample();

    protected IJwtTokenFactory GetJwtTokenFactory(UserManager<User> userManager)
    {
        var options = new IdentityOptions(
        );
        var optionsAccessor = Options.Create(options);

        var userClaimsPrincipalFactory = new UserClaimsPrincipalFactory<User>(userManager, optionsAccessor);
        var jwtTokenFactory = new JwtTokenFactory(userClaimsPrincipalFactory);
        return jwtTokenFactory;
    }

    protected async Task<IHttpContextAccessor> GetHttpContextAccessForAdminUser(UserManager<User> userManager,
        RoleManager<IdentityRole<Guid>> roleManager)
    {
        // Generate JWT Token
        var jwtTokenFactory = GetJwtTokenFactory(userManager);
        var user = SampleUser;

        await userManager.CreateAsync(user, user.PasswordHash!);
        await userManager.AddToRoleAsync(user, UserStringConstants.AdminRole);


        var jwtToken = await jwtTokenFactory.GetJwtTokenAsync(user, Configuration);

        // CrudController_ mocks for HttpRequest and HttpContext
        var httpRequestMock = new Mock<HttpRequest>();
        var httpContextMock = new Mock<HttpContext>();

        httpRequestMock.Setup(req => req.Headers.Authorization).Returns(jwtToken);
        httpRequestMock.Setup(req => req.Scheme).Returns("https:7076://");
        httpRequestMock.Setup(req => req.Host).Returns(new HostString("api/v1"));
        httpRequestMock.Setup(req => req.Path).Returns(new PathString("/asdada/resdad/controller/GetGetAll"));

        // Setup the HttpContext mock to return the mocked HttpRequest
        httpContextMock.Setup(ctx => ctx.Request).Returns(httpRequestMock.Object);

        // Mock IHttpContextAccessor to return the mocked HttpContext
        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContextMock.Object);

        return httpContextAccessorMock.Object;
    }

    protected async Task<IHttpContextAccessor> GetHttpContextAccessForUserUser(UserManager<User> userManager,
        RoleManager<IdentityRole<Guid>> roleManager)
    {
        // Generate JWT Token
        var jwtTokenFactory = GetJwtTokenFactory(userManager);
        var user = SampleUser;

        await userManager.CreateAsync(user, user.PasswordHash!);
        await userManager.AddToRoleAsync(user, UserStringConstants.UserRole);


        var jwtToken = await jwtTokenFactory.GetJwtTokenAsync(user, Configuration);

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

    protected IHttpContextAccessor GetHttpContextAccessForAnonymUser()
    {
        // CrudController_ mocks for HttpRequest and HttpContext
        var httpRequestMock = new Mock<HttpRequest>();
        var httpContextMock = new Mock<HttpContext>();

        httpRequestMock.Setup(req => req.Headers.Authorization).Returns("");

        // Setup the HttpContext mock to return the mocked HttpRequest
        httpContextMock.Setup(ctx => ctx.Request).Returns(httpRequestMock.Object);

        // Mock IHttpContextAccessor to return the mocked HttpContext
        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContextMock.Object);

        return httpContextAccessorMock.Object;
    }
}