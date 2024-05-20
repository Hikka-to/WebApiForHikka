using AutoMapper;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using WebApiForHikka.Constants.Models.Users;
using WebApiForHikka.Domain;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.Authorization;

namespace WebApiForHikka.Test.Controller.Shared;

public class BaseControllerTest
{
    protected readonly IMapper _mapper = A.Fake<IMapper>();
    protected readonly IHttpContextAccessor _httpContextAccessor = A.Fake<HttpContextAccessor>();

    protected CancellationToken _cancellationToken => new();
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

}
