using AutoMapper;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using WebApiForHikka.Domain;

namespace WebApiForHikka.Test.Controller.Shared;

public class BaseControllerTest
{
    protected readonly IMapper _mapper = A.Fake<IMapper>();
    protected readonly IHttpContextAccessor _httpContextAccessor = A.Fake<HttpContextAccessor>();

    protected CancellationToken _cancellationToken => new();
    protected FilterPaginationDto _filterPaginationDto => new();
}
